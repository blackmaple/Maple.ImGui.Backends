using Hexa.NET.ImGui.Backends.Vulkan;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Collections;
using System.Runtime.CompilerServices;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D12;
using VorticeD3D12 = Vortice.Direct3D12;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{
    internal sealed class D3D12ComponentContext : IDisposable
    {
        public const uint RTV_HEAP_CAPACITY = 8;
        public const uint SRV_HEAP_CAPACITY = IMGUI_SRV_MAX_SLOT + TEXTURE_MAX_SLOT;

        public const int IMGUI_SRV_START_SLOT = 0;
        public const int IMGUI_SRV_MAX_SLOT = 128;

        public const int TEXTURE_START_SLOT = IMGUI_SRV_MAX_SLOT;
        public const int TEXTURE_MAX_SLOT = 10240;


        public required COM_PTR_IUNKNOWN<ID3D12DeviceImp> ID3D12DevicePtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> ID3D12CommandListPtr { get; init; }
        public COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> ID3D12CommandAllocatorPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> SRVHeapPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> RTVHeapPtr { get; init; }
        public required COM_PTR_IUNKNOWN<ID3D12FenceImp> ID3D12FencePtr { get; init; }

        public required VorticeD3D12.ID3D12Device VorticeDevice { get; init; }
        public required VorticeD3D12.ID3D12GraphicsCommandList VorticeCommandList { get; init; }
        public required VorticeD3D12.ID3D12DescriptorHeap VorticeSRVHeap { get; init; }
        public required VorticeD3D12.ID3D12DescriptorHeap VorticeRTVHeap { get; init; }
        public required VorticeD3D12.ID3D12Fence VorticeFence { get; init; }

        public COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> ID3D12CommandQueuePtr { get; set; } = default;
        public VorticeD3D12.ID3D12CommandQueue? VorticeCommandQueue { get; set; }

        private static VorticeD3D12.CpuDescriptorHandle ToVortice(D3D12_CPU_DESCRIPTOR_HANDLE handle)
            => new() { Ptr = (nuint)handle.ptr };

        public void Dispose()
        {
            var pCommandQueue = this.ID3D12CommandQueuePtr;
            if (pCommandQueue) pCommandQueue.Release();
            this.ID3D12FencePtr.Release();
            this.ID3D12CommandListPtr.Release();
            this.ID3D12CommandAllocatorPtr.Release();
            this.SRVHeapPtr.Release();
            this.RTVHeapPtr.Release();
            this.ID3D12DevicePtr.Release();

        }

        public bool SetCommandQueue(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> commandQueue)
        {
            if (this.ID3D12CommandQueuePtr != nint.Zero)
            {
                return true;
            }
            var vorticeQueue = new VorticeD3D12.ID3D12CommandQueue((nint)commandQueue);
            if (vorticeQueue.GetDescription().Type == VorticeD3D12.CommandListType.Direct)
            {
                this.ID3D12CommandQueuePtr = commandQueue;
                this.VorticeCommandQueue = vorticeQueue;
                return true;
            }
            vorticeQueue.NativePointer = IntPtr.Zero;
            return false;
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
            this.VorticeCommandQueue!.Signal(this.VorticeFence, frameFence);
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
            var completed = this.VorticeFence.CompletedValue;
            if (completed >= fenceValue)
            {
                return;
            }
            //等待
            var fenceEvent = ID3D12FenceImpExtension.CreateEvent();
            this.VorticeFence.SetEventOnCompletion(fenceValue, fenceEvent);
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

        #region TextureSlot
        public required Queue<D3D12TextureSlot> TextureSlots { get; set; }
        public static Queue<D3D12TextureSlot> CreateTextureSlot(
            COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> pSrvHeap
            , D3D12_CPU_DESCRIPTOR_HANDLE cpu, D3D12_GPU_DESCRIPTOR_HANDLE gpu)
        {
            Queue<D3D12TextureSlot> textureSlots = new(TEXTURE_MAX_SLOT);
            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            var srvSize = vorticeDevice.GetDescriptorHandleIncrementSize(VorticeD3D12.DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView);
            vorticeDevice.NativePointer = IntPtr.Zero;
            //var cpu = pSrvHeap.GetCPUDescriptorHandleForHeapStart();
            //var gpu = pSrvHeap.GetGPUDescriptorHandleForHeapStart();
            for (uint slot = TEXTURE_START_SLOT; slot < SRV_HEAP_CAPACITY; ++slot)
            {
                var offset = slot * srvSize;
                cpu.ptr += offset;
                gpu.ptr += offset;
                textureSlots.Enqueue(new D3D12TextureSlot()
                {
                    CPU = cpu,
                    GPU = gpu,
                });
            }
            return textureSlots;
        }
        public bool TryCreateShaderResourceView(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pResource, out D3D12_GPU_DESCRIPTOR_HANDLE pSRV)
        {
            Unsafe.SkipInit(out pSRV);
            if (!TextureSlots.TryDequeue(out var textureSlots))
            {
                return false;   
            }
            var vorticeResource = new VorticeD3D12.ID3D12Resource((nint)pResource);
            this.VorticeDevice.CreateShaderResourceView(vorticeResource, null, ToVortice(textureSlots.CPU));
            vorticeResource.NativePointer = IntPtr.Zero;
            pSRV = textureSlots.GPU;
            return true;
        }
        #endregion
    }
}