using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11Device;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11DeviceContext;
using Maple.RenderSpy.Graphics.D3D11.COM_ID3D11RenderTargetView;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
using Maple.UnmanagedExtensions;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D11
{
    internal sealed class D3D11BackendImp(
        ImGuiContextPtr guiContextPtr,
        COM_PTR_IUNKNOWN<ID3D11DeviceImp> d3D11DevicePtr,
        COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> d3D11DeviceContextPtr,
        WinMsgHookItem winMsgHookItem,
        ImGuiController controller) : ImGuiRaiseRenderBase(controller)
    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        COM_PTR_IUNKNOWN<ID3D11DeviceImp> ID3D11DevicePtr { get; set; } = d3D11DevicePtr;
        COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> ID3D11DeviceContextPtr { get; set; } = d3D11DeviceContextPtr;
        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;
        COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> ID3D11RenderTargetViewPtr = default;

        public unsafe static D3D11BackendImp CreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, ImGuiController controller)
        {
            var hWnd = pSwapChain.GetOutputWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>("GetOutputWindow IS NULL");
            }

            // ImGuiWin32InputBridge.SetWindowHandle(hWnd);
            var hResult = pSwapChain.GetDevice<ID3D11DeviceImp>(ID3D11DeviceImp.GUID, out var pDevice);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"GetDevice ERROR:{hResult}");
            }
            pDevice.GetImmediateContext(out var pContext);

            //   pDevice.TryCreateBackbufferRTV(pSwapChain, out var pRTView);
            //if (pDevice.TryCreateBackbufferRTV(pSwapChain, out var pRTView))
            //{

            //}
            // var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            //var inputWindowHandle = mainWindowHandle != nint.Zero ? mainWindowHandle : hWnd;
            //customRender.Logger.LogInformation("renderHWnd:{hWnd}/inputWindowHandle:{inputWindowHandle}/mainWindowHandle:{mainWindowHandle}"
            //  , hWnd, inputWindowHandle, mainWindowHandle);

            //   var previousDpiContext = ImGuiWin32InputBridge.MatchThreadDpiAwarenessContext(hWnd);
            var imguiContext = ImGuiApi.CreateContext();
            ImGuiApi.SetCurrentContext(imguiContext);
            var io = ImGuiApi.GetIO();
            io.MouseDrawCursor = true;
            io.WantTextInput = true;
            io.UserData = controller.Handle.ToPointer();



            delegate* unmanaged[Cdecl]<UnsafePtr<ImGuiContext>, UnsafePtr<ImGuiViewport>, UnsafePtr<ImGuiPlatformImeData>, void> setImeDataProc = &SetImeData;
            var platformIO = ImGuiApi.GetPlatformIO();
            platformIO.PlatformSetImeDataFn = setImeDataProc;

            //  platformIO..




            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.AdditionalContent.Set(nameof(ImGuiController), controller);
            winMsgHookItem.SyncCallback += WinProcCallback;

            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();
            //   customRender.Logger.LogInformation("hook windows msg:{enable}", );

            ImGuiImplWin32.SetCurrentContext(imguiContext);
            //    var dpiScale = MathF.Max(1.0f, ImGuiWin32InputBridge.GetWindowDpiScale(hWnd));
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                //      ImGuiWin32InputBridge.RestoreThreadDpiAwarenessContext(previousDpiContext);
                return ImGuiBackendException.Throw<D3D11BackendImp>($"ImGuiImplWin32 INIT ERROR");
            }
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f);

            //   ImGuiWin32InputBridge.RestoreThreadDpiAwarenessContext(previousDpiContext);

            var pID3D11DevicePtr = new ID3D11DevicePtr(pDevice.AsPointer<ID3D11DeviceImp, ID3D11Device>());
            var pID3D11DeviceContextPtr = new ID3D11DeviceContextPtr(pContext.AsPointer<ID3D11DeviceContextImp, ID3D11DeviceContext>());
            ImGuiImplD3D11.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D11.Init(pID3D11DevicePtr, pID3D11DeviceContextPtr))
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"ImGuiImplD3D11 INIT ERROR");
            }
            return new D3D11BackendImp(imguiContext, pDevice, pContext, winMsgHookItem, controller);
        }

        protected override void Starting(nint context)
        {
            if (this.ID3D11RenderTargetViewPtr == nint.Zero)
            {
                var pSwapChain = new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context);
                if (this.ID3D11DevicePtr.TryCreateBackbufferRTV(pSwapChain, out var pRTView))
                {
                    this.ID3D11RenderTargetViewPtr = pRTView;
                }
            }

            ImGuiImplWin32.NewFrame();
            ImGuiImplD3D11.NewFrame();
            ImGuiApi.NewFrame();
        }
        protected override void Start(nint context)
        {
            this.Controller.CustomRender?.RaiseRender();
        }
        protected override void Started(nint context)
        {
            ImGuiApi.EndFrame();
            ImGuiApi.Render();

            if (this.ID3D11RenderTargetViewPtr != nint.Zero)
            {
                var pSwapChain = new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context);
                this.ID3D11DeviceContextPtr.OMSetRenderTarget(this.ID3D11RenderTargetViewPtr);
                this.ID3D11DeviceContextPtr.EnsureViewportMatchesBackbuffer(pSwapChain);
            }
        }
        protected override void Build(nint context)
        {
            ImGuiImplD3D11.RenderDrawData(ImGuiApi.GetDrawData());
        }



        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                ImGuiImplWin32.Shutdown();
                ImGuiImplD3D11.Shutdown();
                ImGuiApi.DestroyContext(imguiContext);
            }

            this.WinMsgHookItem.Dispose();
        }

        public override void Resetting(nint context)
        {
            var pRTView = this.ID3D11RenderTargetViewPtr;
            this.ID3D11RenderTargetViewPtr = default;
            if (pRTView != nint.Zero)
            {
                this.ID3D11DeviceContextPtr.Clear_OMSetRenderTargets();
                pRTView.Release();
            }


        }
        public override void Reset(nint context)
        {
            ImGuiImplD3D11.InvalidateDeviceObjects();

        }
        public override void Resetted(nint context)
        {
            ImGuiImplD3D11.CreateDeviceObjects();

        }


        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe void SetImeData(UnsafePtr<ImGuiContext> ctx, UnsafePtr<ImGuiViewport> viewport, UnsafePtr<ImGuiPlatformImeData> data)
        {
            //0x000002650ad21388
            // ctx.Raw.p
            var ptr_userData = new nint(ctx.Raw.IO.UserData);
            if (ptr_userData == nint.Zero || false == ImGuiController.TryGet<ImGuiController>(ptr_userData, out var controller))
            {
                return;
            }
            ref var ref_data = ref data.Raw;
            bool wantIME = ref_data.WantVisible == 1;
            controller.UnityInputBridge?.PlatformSetImeDataFn(wantIME);
        }

        private static bool WinProcCallback(nint hWnd,uint uMsg, nuint w,nint l, WinMsgHookItem hooItem)
        {
            //ImGuiApi.SetCurrentContext(imguiContext);
            //ImGuiImplWin32.SetCurrentContext(imguiContext);

            //if (ImGuiWin32InputBridge.TryHandleImeComposition(hWnd, uMsg, l))
            //{
            //    return true;
            //}
            //var text = ImGuiWin32InputBridge.Debug(uMsg);
            //if (!string.IsNullOrEmpty(text))
            //{
            //    controller.Logger.LogInformation("WinMsgHookItem: {text}", text);
            //}
            //   
            //if (ImGuiWin32InputBridge.TryHandleImeComposition(hWnd, uMsg, w, l))
            //{
            //    controller.Logger.LogInformation("TryHandleImeComposition: true");
            //    return true;
            //}

            if (hooItem.AdditionalContent.TryGet<ImGuiController>(nameof(ImGuiController),out var controller))
            {
                if (controller.Win32InputBridge?.TryHandleImeComposition(hWnd, uMsg, w, l) ==true)
                {
                    return true;
                }
            }
            
            return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
        }


    }






}
