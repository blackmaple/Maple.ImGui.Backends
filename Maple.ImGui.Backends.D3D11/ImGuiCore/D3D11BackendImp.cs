using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D10;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.D3D12;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11RenderTargetView;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Resource;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11ShaderResourceView;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Texture2D;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D11.ImGuiCore
{
    internal sealed class D3D11BackendImp(
        ImGuiContextPtr guiContextPtr,
        D3D11ComponentContext componentContext,
        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view) : ImGuiBackendImpBase(bridgeCollection, view)
    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        D3D11ComponentContext ComponentContext { get; } = componentContext;

        private static DXGI_SWAP_CHAIN_DESC GetDesc(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            var hResult = pSwapChain.GetDesc<DXGI_SWAP_CHAIN_DESC>(out var pDesc);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<DXGI_SWAP_CHAIN_DESC>($"{nameof(IDXGISwapChainImpExtension.GetDesc)}:{hResult}");
            }
            return pDesc;
        }
        private static COM_PTR_IUNKNOWN<ID3D11DeviceImp> CreateID3D11Device(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            var hResult = pSwapChain.GetDevice<ID3D11DeviceImp>(ID3D11DeviceImp.GUID, out var pDevice);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D11DeviceImp>>($"{nameof(IDXGISwapChainImpExtension.GetDevice)}:{hResult}");
            }
            return pDevice;
        }

        public unsafe static bool TryCreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, D3D11BackendService backendService, out D3D11BackendImp backendImp)
        {

            Unsafe.SkipInit(out backendImp);
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pDevice = default;
            COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pContext = default;
            COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> pRTV = default;
            var win32Init = false;
            var d3d11Init = false;
            ImGuiContextPtr imguiContext = default;
            try
            {
                var pDesc = GetDesc(pSwapChain);
                pDevice = CreateID3D11Device(pSwapChain);
                pDevice.GetImmediateContext(out pContext);
                if (!D3D11ComponentContext.TryCreateRenderTarget(pSwapChain, pDevice, out pRTV))
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(D3D11ComponentContext.TryCreateRenderTarget)}:ERROR");
                }
                var componentContext = new D3D11ComponentContext() { ID3D11DevicePtr = pDevice, ID3D11DeviceContextPtr = pContext, ID3D11RenderTargetViewPtr = pRTV };



                imguiContext = ImGuiApi.CreateContext();
                ImGuiApi.SetCurrentContext(imguiContext);
                win32Init = backendService.InitPlatform(imguiContext, pDesc.OutputWindow);
                if (false == win32Init)
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(D3D11BackendService.InitPlatform)}:ERROR");
                }

                var pID3D11DevicePtr = new ID3D11DevicePtr(pDevice.AsPointer<ID3D11DeviceImp, Hexa.NET.ImGui.Backends.D3D11.ID3D11Device>());
                var pID3D11DeviceContextPtr = new ID3D11DeviceContextPtr(pContext.AsPointer<ID3D11DeviceContextImp, Hexa.NET.ImGui.Backends.D3D11.ID3D11DeviceContext>());
                ImGuiImplD3D11.SetCurrentContext(imguiContext);
                d3d11Init = ImGuiImplD3D11.Init(pID3D11DevicePtr, pID3D11DeviceContextPtr);
                if (!d3d11Init)
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(ImGuiImplD3D11.Init)}:ERROR");
                }

                backendImp = new D3D11BackendImp(imguiContext, componentContext,
                    backendService.BridgeCollection, backendService.View);
                return true;
            }
            catch (Exception ex)
            {
                backendService.Logger.LogError("{NAME} EXCEPTION: {EX}", nameof(TryCreateImp), ex);

                if (pRTV) pRTV.Release();
                if (pContext) pContext.Release();
                if (pDevice) pDevice.Release();

                if (d3d11Init) ImGuiImplD3D11.Shutdown();
                if (win32Init) ImGuiImplWin32.Shutdown();
                if (!imguiContext.IsNull) ImGuiApi.DestroyContext(imguiContext);

            }

            return false;










        }


        protected override void Starting(nint context)
        {
            if (this.ComponentContext.ID3D11RenderTargetViewPtr != nint.Zero)
            {
                this.ComponentContext.SetRenderTarget(new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context));
               // this.ComponentContext.ClearRenderTarget();
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
            //  ImGuiApi.EndFrame();
            ImGuiApi.Render();


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
                this.ComponentContext.WaitForGPU();
                this.ComponentContext.Dispose();

                ImGuiImplD3D11.Shutdown();
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
            ImGuiImplD3D11.InvalidateDeviceObjects();

        }
        public override void Resetted(nint context)
        {
            this.ComponentContext.CreateRenderTarget(new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context));
            ImGuiImplD3D11.CreateDeviceObjects();

        }




        protected override ImTextureID CreateImTextureID(nint textureNativePtr)
        {
            _ = TryCreateShaderResourceView(textureNativePtr, out var pSRV);
            nint ptr = pSRV;
            return new ImTextureID(ptr);
        }

        private bool TryCreateShaderResourceView(nint textureNativePtr, out COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp> pSRV)
        {
            var pResource = new COM_PTR_IUNKNOWN<ID3D11ResourceImp>(textureNativePtr);
            return this.ComponentContext.ID3D11DevicePtr.TryCreateShaderResourceView(pResource, out pSRV);
        }
    }






}
