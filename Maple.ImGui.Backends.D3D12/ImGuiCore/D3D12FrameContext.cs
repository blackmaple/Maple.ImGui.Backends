using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using VorticeD3D12 = Vortice.Direct3D12;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{
    internal struct D3D12FrameContext : IDisposable
    {
        public COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> CommandAllocator;
        public VorticeD3D12.ID3D12CommandAllocator? VorticeCommandAllocator;
        public ulong FenceValue;

        public void Dispose()
        {
            this.CommandAllocator.Release();
        }
    }
}