using Hexa.NET.ImGui.Backends.D3D11;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11RenderTargetView;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Resource;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11ShaderResourceView;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Texture2D;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.D3D11.ImGuiCore
{
    internal class D3D11ComponentContext : IDisposable
    {
        public required COM_PTR_IUNKNOWN<ID3D11DeviceImp> ID3D11DevicePtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> ID3D11DeviceContextPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> ID3D11RenderTargetViewPtr { get; set; }

        public static bool TryCreateRenderTarget(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, COM_PTR_IUNKNOWN<ID3D11DeviceImp> pDevice, out COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> pRTV)
        {
            Unsafe.SkipInit(out pRTV);
            if (pSwapChain.GetBuffer<ID3D11Texture2DImp>(in ID3D11Texture2DImp.GUID, out var pBackBuffer))
            {
                using (pBackBuffer)
                {
                    return pDevice.CreateRenderTargetView(pBackBuffer, out pRTV);
                }
            }
            return false;
        }
        public void CreateRenderTarget(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            if (TryCreateRenderTarget(pSwapChain, this.ID3D11DevicePtr, out var pRTV))
            {
                this.ID3D11RenderTargetViewPtr = pRTV;
            }
        }
        public void SetRenderTarget(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            // 3. 绑定渲染目标
            this.ID3D11DeviceContextPtr.OMSetRenderTargets(this.ID3D11RenderTargetViewPtr);

            //4. 设置视口（覆盖整个窗口）
            if (pSwapChain.GetDesc<DXGI_SWAP_CHAIN_DESC>(out var pDesc))
            {
                ref var bufferDesc = ref pDesc.BufferDesc;
                D3D11_VIEWPORT view = new()
                {
                    Height = bufferDesc.Height,
                    Width = bufferDesc.Width,
                    MaxDepth = 1F,
                };
                this.ID3D11DeviceContextPtr.RSSetViewports(view);
            }
        }
        public void ClearRenderTarget()
        {
            //5. 清空渲染目标为指定颜色（例如深灰色）
            this.ID3D11DeviceContextPtr.ClearRenderTargetView(this.ID3D11RenderTargetViewPtr, 0.45f, 0.55f, 0.60f, 1.00f);
        }
        public void DestroyRenderTarget()
        {
            var pRenderTargetView = this.ID3D11RenderTargetViewPtr;
            this.ID3D11RenderTargetViewPtr = default;
            if (pRenderTargetView) pRenderTargetView.Release();
        }
        public void WaitForGPU() => this.ID3D11DeviceContextPtr.Flush();


        public bool TryCreateShaderResourceView(COM_PTR_IUNKNOWN<ID3D11ResourceImp> pResource, out COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp> pSRView)
        {
            Unsafe.SkipInit(out pSRView);
            if (!pResource.QueryInterface<ID3D11Texture2DImp>(in ID3D11Texture2DImp.GUID, out var pTexture2D))
            {
                return false;
            }
            using (pTexture2D)
            {
                pTexture2D.GetDesc(out var pDesc);
                var srvDesc = new D3D11_SHADER_RESOURCE_VIEW_DESC()
                {
                    Format = pDesc.Format,
                    ViewDimension = D3D_SRV_DIMENSION.D3D11_SRV_DIMENSION_TEXTURE2D,
                    Anonymous = new D3D11_SHADER_RESOURCE_VIEW_DESC._Anonymous_e__Union()
                    {
                        Texture2D = new D3D11_TEX2D_SRV()
                        {
                            MostDetailedMip = 0,
                            MipLevels = pDesc.MipLevels,
                        }
                    }
                };
                return this.ID3D11DevicePtr.CreateShaderResourceView(pResource, srvDesc, out pSRView);
            }
        }



        public void Dispose()
        {
            this.DestroyRenderTarget();
            this.ID3D11DevicePtr.Release();


        }
    }
}
