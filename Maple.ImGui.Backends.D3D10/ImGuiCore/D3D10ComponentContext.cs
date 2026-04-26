using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10Device;
using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10RenderTargetView;
using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10Texture2D;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using Windows.Win32.Graphics.Direct3D10;
using Windows.Win32.Graphics.Dxgi;
namespace Maple.ImGui.Backends.D3D10.ImGuiCore
{
    internal class D3D10ComponentContext : IDisposable
    {
        public required COM_PTR_IUNKNOWN<ID3D10DeviceImp> ID3D10DevicePtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D10RenderTargetViewImp> ID3D10RenderTargetViewPtr { get; set; } = default;

        public static bool TryCreateRenderTarget(
            COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain,
            COM_PTR_IUNKNOWN<ID3D10DeviceImp> pDevice,
            out COM_PTR_IUNKNOWN<ID3D10RenderTargetViewImp> pRTV)
        {
            Unsafe.SkipInit(out pRTV);
            // 1. 获取当前后台缓冲区索引（用于多缓冲）
            if (pSwapChain.GetBuffer<ID3D10Texture2DImp>(in ID3D10Texture2DImp.GUID, out var pBackBuffer))
            {
                using (pBackBuffer)
                {
                    // 2. 获取或创建该缓冲区的渲染目标视图（RTV）
                    return pDevice.CreateRenderTargetView(pBackBuffer, out pRTV);
                }
            }
            return false;
        }

        public void CreateRenderTarget(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)  
        {
            if (TryCreateRenderTarget(pSwapChain, this.ID3D10DevicePtr, out var pRTV))
            {
                this.ID3D10RenderTargetViewPtr = pRTV;
            }
        }

        public void SetRenderTarget(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            // 3. 绑定渲染目标
            this.ID3D10DevicePtr.OMSetRenderTargets(this.ID3D10RenderTargetViewPtr);

            //4. 设置视口（覆盖整个窗口）
            if (pSwapChain.GetDesc<DXGI_SWAP_CHAIN_DESC>(out var pDesc))
            {
                ref var bufferDesc = ref pDesc.BufferDesc;
                D3D10_VIEWPORT view = new()
                {
                    Height = bufferDesc.Height,
                    Width = bufferDesc.Width,
                    MaxDepth = 1F,
                };
                this.ID3D10DevicePtr.RSSetViewports(view);
            }
        }
        public void ClearRenderTarget()
        {
            //5. 清空渲染目标为指定颜色（例如深灰色）
            this.ID3D10DevicePtr.ClearRenderTargetView(this.ID3D10RenderTargetViewPtr, 0.45f, 0.55f, 0.60f, 1.00f);
        }
        public void DestroyRenderTarget()
        {
            var pRenderTargetView = this.ID3D10RenderTargetViewPtr;
            this.ID3D10RenderTargetViewPtr = default;
            if (pRenderTargetView) pRenderTargetView.Release();
        }

        public void WaitForGPU() => this.ID3D10DevicePtr.Flush();

        public void Dispose()
        {
            this.DestroyRenderTarget();
            this.ID3D10DevicePtr.Release();
        }
    }

}
