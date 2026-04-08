using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D10;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends;
using Maple.RenderSpy.Graphics.D3D10.COM_D3D10Device;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;

namespace Maple.ImGui.Backends.D3D10
{
    internal sealed class D3D10BackendImp(
        ImGuiContextPtr guiContextPtr,
        COM_PTR_IUNKNOWN<ID3D10DeviceImp> D3D10DevicePtr,
        WinMsgHookItem winMsgHookItem,
        ImGuiController controller) : ImGuiRaiseRenderBase(controller)

    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        COM_PTR_IUNKNOWN<ID3D10DeviceImp> ID3D10DevicePtr { get; set; } = D3D10DevicePtr;

        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;

        public unsafe static D3D10BackendImp CreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, ImGuiController controller)
        {
            var hWnd = pSwapChain.GetOutputWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D10BackendImp>("GetOutputWindow IS NULL");
            }

            var hResult = pSwapChain.GetDevice<ID3D10DeviceImp>(ID3D10DeviceImp.GUID, out var pDevice);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<D3D10BackendImp>($"GetDevice ERROR:{hResult}");
            }

            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<D3D10BackendImp>($"ImGuiImplWin32 INIT ERROR");
            }
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f);

            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += (hWnd, uMsg, w, l, hookItem) =>
            {
                return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            };
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();

            var pID3D10DevicePtr = new ID3D10DevicePtr(pDevice.AsPointer<ID3D10DeviceImp, ID3D10Device>());
            ImGuiImplD3D10.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D10.Init(pID3D10DevicePtr))
            {
                return ImGuiBackendException.Throw<D3D10BackendImp>($"ImGuiImplD3D10 INIT ERROR");
            }
            return new D3D10BackendImp(imguiContext, pDevice, winMsgHookItem, controller);
        }

        protected override void Starting(nint context)
        {
            ImGuiImplWin32.NewFrame();
            ImGuiImplD3D10.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }

        protected override void Start(nint context)
        {
            this.Controller.CustomRender?.RaiseRender();
        }

        protected override void Started(nint context)
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
        }

        protected override void Build(nint context)
        {
            ImGuiImplD3D10.RenderDrawData(Hexa.NET.ImGui.ImGui.GetDrawData());
        }






        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                ImGuiImplWin32.Shutdown();
                ImGuiImplD3D10.Shutdown();
                Hexa.NET.ImGui.ImGui.DestroyContext(imguiContext);
            }
            this.WinMsgHookItem.Dispose();
        }

        public override void Resetting(nint context)
        {


        }
        public override void Reset(nint context)
        {
           ImGuiImplD3D10.InvalidateDeviceObjects();
        }
        public override void Resetted(nint context)
        {
            ImGuiImplD3D10.CreateDeviceObjects();
        }

    }
}
