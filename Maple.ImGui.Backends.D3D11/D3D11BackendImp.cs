using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11Device;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11DeviceContext;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11RenderTargetView;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11Resource;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11ShaderResourceView;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
using Microsoft.Extensions.DependencyInjection;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D11
{
    internal sealed class D3D11BackendImp(
        ImGuiContextPtr guiContextPtr,
        COM_PTR_IUNKNOWN<ID3D11DeviceImp> d3D11DevicePtr,
        COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> d3D11DeviceContextPtr,

        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view) : ImGuiBackendImpBase(bridgeCollection, view)
    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        COM_PTR_IUNKNOWN<ID3D11DeviceImp> ID3D11DevicePtr { get; set; } = d3D11DevicePtr;
        COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> ID3D11DeviceContextPtr { get; set; } = d3D11DeviceContextPtr;

        COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> ID3D11RenderTargetViewPtr = default;

        public unsafe static D3D11BackendImp CreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, D3D11BackendHostedService hostedService)
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

            var imguiContext = ImGuiApi.CreateContext();
            ImGuiApi.SetCurrentContext(imguiContext);
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (false == hostedService.InitPlatform(imguiContext, hWnd))
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"InitPlatform INIT ERROR");
            }

            var pID3D11DevicePtr = new ID3D11DevicePtr(pDevice.AsPointer<ID3D11DeviceImp, ID3D11Device>());
            var pID3D11DeviceContextPtr = new ID3D11DeviceContextPtr(pContext.AsPointer<ID3D11DeviceContextImp, ID3D11DeviceContext>());
            ImGuiImplD3D11.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D11.Init(pID3D11DevicePtr, pID3D11DeviceContextPtr))
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"ImGuiImplD3D11 INIT ERROR");
            }
            return new D3D11BackendImp(imguiContext, pDevice, pContext, hostedService.BridgeCollection, hostedService.View);
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
            this.View.RaiseRender();
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


      

        protected override ImTextureID CreateImTextureID(nint textureNativePtr)
        {
            _ = TryCreateShaderResourceView(textureNativePtr, out var pSRView);
            nint ptr = pSRView;
            return new ImTextureID(ptr);
        }

        private bool TryCreateShaderResourceView(nint textureNativePtr, out COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp> pSRView)
        {
            var pResource = ID3D11ResourceImpExtension.Create(textureNativePtr);
            return this.ID3D11DevicePtr.TryCreateShaderResourceView(pResource, out pSRView);
        }
    }



   


}
