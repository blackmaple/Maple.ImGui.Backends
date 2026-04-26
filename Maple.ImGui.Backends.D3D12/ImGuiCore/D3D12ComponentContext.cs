using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D12;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{
    internal sealed class D3D12ComponentContext : IDisposable
    {
        public required COM_PTR_IUNKNOWN<ID3D12DeviceImp> ID3D12DevicePtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> ID3D12CommandListPtr { get; init; }
        public COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> ID3D12CommandAllocatorPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> SRVHeapPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> RTVHeapPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12FenceImp> ID3D12FencePtr { get; init; }

        public COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> ID3D12CommandQueuePtr { get; set; } = default;

        public void Dispose()
        {
            var pCommandQueue = this.ID3D12CommandQueuePtr;
            if(pCommandQueue) pCommandQueue.Release();
            this.ID3D12FencePtr.Release();
            this.ID3D12CommandListPtr.Release();
            this.ID3D12CommandAllocatorPtr.Release();
            this.SRVHeapPtr.Release();
            this.RTVHeapPtr.Release();
            this.ID3D12DevicePtr.Release();
           
        }

        public void SetCommandQueue(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> commandQueue)
        {
            if (this.ID3D12CommandQueuePtr == nint.Zero)
            {
                if (commandQueue.GetDesc().Type != D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT)
                {
                    this.ID3D12CommandQueuePtr = commandQueue;
                }
            }
        }

        #region Fence&Signal
        ulong SignaledValue = 0;
        public ulong GetNextSignaledValue()
        {
            return Interlocked.Increment(ref SignaledValue);
        }
        public ulong GetCurrSignaledValue()
        {
            return Interlocked.Read(ref SignaledValue);
        }
        public ulong SignalFence(ulong frameFence)
        {
            this.ID3D12CommandQueuePtr.Signal(this.ID3D12FencePtr, frameFence);
            return frameFence;
        }
        public void WaitForFence(ulong fenceValue)
        {
            //当前无需等待
            if (fenceValue == 0)
            {
                return;
            }
            //已经处理
            var completed = this.ID3D12FencePtr.GetCompletedValue();
            if (completed >= fenceValue)
            {
                return;
            }
            //等待
            var fenceEvent = ID3D12FenceImpExtension.CreateEvent();
            this.ID3D12FencePtr.SetEventOnCompletion(fenceValue, fenceEvent);
            PInvoke.WaitForSingleObject(fenceEvent, PInvoke.INFINITE);
            PInvoke.CloseHandle(fenceEvent);
        }
        public ulong SignalNextFrame()
        {
            var fenceValue = GetNextSignaledValue();
            this.SignalFence(fenceValue);
            return fenceValue;
        }
        public void WaitForGPU()
        {
            this.WaitForFence(this.SignalNextFrame());
        }

        #endregion
    }
}