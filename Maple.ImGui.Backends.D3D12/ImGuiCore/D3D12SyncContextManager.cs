using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain3;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi;
using VorticeD3D12 = Vortice.Direct3D12;

namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{

    internal sealed class D3D12SyncContextManager : IDisposable
    {

        public const int NUM_FRAMES_IN_FLIGHT = 2;



        public required D3D12FrameContext[] FrameContexts { get; set; }
        public required D3D12BackBuffer[] BackBuffers { get; set; }
        
        public void Dispose()
        {
            this.DestroyBackBuffer();
            this.DestroyFrameContexts();
        }

        #region D3D12FrameContext

        public static IEnumerable<D3D12FrameContext> CreateFrameContexts(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            for (uint i = 0; i < NUM_FRAMES_IN_FLIGHT; ++i)
            {
                if (pDevice.CreateDirectCommandAllocator(out var pCommandAllocator))
                {
                    yield return new D3D12FrameContext()
                    {
                        CommandAllocator = pCommandAllocator,
                        VorticeCommandAllocator = new VorticeD3D12.ID3D12CommandAllocator((nint)pCommandAllocator),
                        FenceValue = 0UL
                    };
                }
            }
        }
        public static void DestroyFrameContexts(ReadOnlySpan<D3D12FrameContext> frameContexts)
        {
            foreach (var frameContext in frameContexts)
            {
                frameContext.Dispose();
            }
        }
        public void DestroyFrameContexts() => DestroyFrameContexts(this.FrameContexts);
        public static void ResetFrameContexts(Span<D3D12FrameContext> frameContexts)
        {
            foreach (ref var frameContext in frameContexts)
            {
                frameContext.FenceValue = 0UL;
            }
        }
        public void ResetFrameContexts() => ResetFrameContexts(this.FrameContexts);
        //public ref D3D12FrameContext WaitForNextFrameContext()
        //{
        //    var backBufferIdx = this.SyncContextManager.GetCurrIndex();
        //    ref var frameContext = ref this.SyncContextManager.FrameContexts[backBufferIdx];
        //    this.WaitForFenceValue(frameContext.FenceValue);
        //    var pCommandAllocator = frameContext.CommandAllocator;
        //    pCommandAllocator.Reset();
        //    return ref frameContext;
        //}
        public ref D3D12FrameContext GetCurrentFrameContext(ulong index)
        {
            return ref this.FrameContexts[index % NUM_FRAMES_IN_FLIGHT];
        }

        #region D3D12BackBuffer

        public static IEnumerable<D3D12BackBuffer> CreateBackBuffers(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> rtvHeap, uint bufferCount)
        {
            if (bufferCount == 0U)
            {
                yield break;
            }

            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            var vorticeRtvHeap = new VorticeD3D12.ID3D12DescriptorHeap((nint)rtvHeap);
            var rtvSize = vorticeDevice.GetDescriptorHandleIncrementSize(VorticeD3D12.DescriptorHeapType.RenderTargetView);
            var vorticeRtvHandle = vorticeRtvHeap.GetCPUDescriptorHandleForHeapStart();
            var rtvHandle = new D3D12_CPU_DESCRIPTOR_HANDLE() { ptr = (nuint)vorticeRtvHandle.Ptr };

            try
            {
                for (uint i = 0u; i < bufferCount; i++)
                {
                    if (pSwapChain.GetBuffer<ID3D12ResourceImp>(i, ID3D12ResourceImp.GUID, out var pBackBuffer))
                    {
                        var vorticeBackBuffer = new VorticeD3D12.ID3D12Resource((nint)pBackBuffer);
                        vorticeDevice.CreateRenderTargetView(vorticeBackBuffer, null, new VorticeD3D12.CpuDescriptorHandle() { Ptr = (nuint)rtvHandle.ptr });
                        yield return new D3D12BackBuffer()
                        {
                            RTV = rtvHandle,
                            Resource = pBackBuffer,
                            VorticeResource = vorticeBackBuffer,
                        };
                        rtvHandle.ptr += rtvSize;
                    }
                }
            }
            finally
            {
                vorticeRtvHeap.NativePointer = IntPtr.Zero;
                vorticeDevice.NativePointer = IntPtr.Zero;
            }
        }
        public static D3D12BackBuffer[] CreateBackBuffers(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> rtvHeap)
        {
            pSwapChain.GetDesc<DXGI_SWAP_CHAIN_DESC>(out var pDesc);
            return [.. CreateBackBuffers(
                pSwapChain,
                pDevice,
                rtvHeap,
                pDesc.BufferCount
                )];
        }
        public void ResetBackBuffers(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> rtvHeap)
        {
            this.BackBuffers = CreateBackBuffers(pSwapChain, pDevice, rtvHeap);
        }
        public ref D3D12BackBuffer GetCurrentBackBuffer(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pSwapChain3)
        {
            var backBufferIdx = pSwapChain3.GetCurrentBackBufferIndex();
            return ref this.BackBuffers[(int)backBufferIdx];
        }
        public static void DestroyBackBuffer(ReadOnlySpan<D3D12BackBuffer> backBuffers)
        {
            foreach (var backbuffer in backBuffers)
            {
                backbuffer.Dispose();
            }
        }
        public void DestroyBackBuffer() => DestroyBackBuffer(this.BackBuffers);
        #endregion


        #endregion


    }
}