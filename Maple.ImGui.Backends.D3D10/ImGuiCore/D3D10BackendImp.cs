using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D10;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10Device;
using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10RenderTargetView;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Windows.Win32.Graphics.Dxgi;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D10.ImGuiCore
{
    internal sealed class D3D10BackendImp(
        ImGuiContextPtr guiContextPtr,
        D3D10ComponentContext componentContext,
        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view
        ) : ImGuiBackendImpBase(bridgeCollection, view)

    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        D3D10ComponentContext ComponentContext { get; } = componentContext;

        private static DXGI_SWAP_CHAIN_DESC GetDesc(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            var hResult = pSwapChain.GetDesc<DXGI_SWAP_CHAIN_DESC>(out var pDesc);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<DXGI_SWAP_CHAIN_DESC>($"{nameof(IDXGISwapChainImpExtension.GetDesc)}:{hResult}");
            }
            return pDesc;
        }
        private static COM_PTR_IUNKNOWN<ID3D10DeviceImp> CreateID3D10Device(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            var hResult = pSwapChain.GetDevice<ID3D10DeviceImp>(ID3D10DeviceImp.GUID, out var pDevice);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D10DeviceImp>>($"{nameof(IDXGISwapChainImpExtension.GetDevice)}:{hResult}");
            }
            return pDevice;
        }

        public unsafe static bool TryCreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, D3D10BackendService backendService, out D3D10BackendImp backendImp)
        {
            Unsafe.SkipInit(out backendImp);
            COM_PTR_IUNKNOWN<ID3D10DeviceImp> pDevice = default;
            COM_PTR_IUNKNOWN<ID3D10RenderTargetViewImp> pRTV = default;
            ImGuiContextPtr imguiContext = default;
            var d3d10Init = false;
            var win32Init = false;

            try
            {
                var pDesc = GetDesc(pSwapChain);
                pDevice = CreateID3D10Device(pSwapChain);

                if (!D3D10ComponentContext.TryCreateRenderTarget(pSwapChain, pDevice, out pRTV))
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(D3D10ComponentContext.TryCreateRenderTarget)}:ERROR");
                }
                var componentContext = new D3D10ComponentContext() { ID3D10DevicePtr = pDevice, ID3D10RenderTargetViewPtr = pRTV };

                imguiContext = ImGuiApi.CreateContext();
                ImGuiApi.SetCurrentContext(imguiContext);
                win32Init = backendService.InitPlatform(imguiContext, pDesc.OutputWindow);
                if (false == win32Init)
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(D3D10BackendService.InitPlatform)}:ERROR");
                }

                var pID3D10DevicePtr = new ID3D10DevicePtr(pDevice.AsPointer<ID3D10DeviceImp, Hexa.NET.ImGui.Backends.D3D10.ID3D10Device>());
                ImGuiImplD3D10.SetCurrentContext(imguiContext);
                d3d10Init = ImGuiImplD3D10.Init(pID3D10DevicePtr);
                if (!d3d10Init)
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(ImGuiImplD3D11.Init)}:ERROR");
                }

                backendImp = new D3D10BackendImp(imguiContext, componentContext,
                    backendService.BridgeCollection, backendService.View);
                return true;
            }
            catch (Exception ex)
            {
                backendService.Logger.LogError("{NAME} EXCEPTION: {EX}", nameof(TryCreateImp), ex);

                if (pRTV) pRTV.Release();
                if (pDevice) pDevice.Release();
                if (d3d10Init) ImGuiImplD3D11.Shutdown();
                if (win32Init) ImGuiImplWin32.Shutdown();
                if (!imguiContext.IsNull) ImGuiApi.DestroyContext(imguiContext);

            }
            return false;
        }

        protected override void Starting(nint context)
        {
            if (this.ComponentContext.ID3D10RenderTargetViewPtr != nint.Zero)
            {
                this.ComponentContext.SetRenderTarget(new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context));
               // this.ComponentContext.ClearRenderTarget();
            }
            ImGuiImplWin32.NewFrame();
            ImGuiImplD3D10.NewFrame();
            ImGuiApi.NewFrame();
        }

        protected override void Start(nint context)
        {
            this.View.RaiseRender();
        }

        protected override void Started(nint context)
        {
            //   ImGuiApi.EndFrame();
            ImGuiApi.Render();

        }

        protected override void Build(nint context)
        {
            ImGuiImplD3D10.RenderDrawData(ImGuiApi.GetDrawData());
        }






        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                this.ComponentContext.WaitForGPU();
                this.ComponentContext.Dispose();

                ImGuiImplD3D10.Shutdown();
                ImGuiImplWin32.Shutdown();

                ImGuiApi.DestroyContext(imguiContext);
            }
        }

        public override void Resetting(nint context)
        {
            this.ComponentContext.WaitForGPU();
        }

        public override void Reset(nint context)
        {
            this.ComponentContext.DestroyRenderTarget();
            ImGuiImplD3D10.InvalidateDeviceObjects();
        }

        public override void Resetted(nint context)
        {
            this.ComponentContext.CreateRenderTarget(new  COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context));
            ImGuiImplD3D10.CreateDeviceObjects();
        }





    }

}
