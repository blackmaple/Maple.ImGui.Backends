using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Windows.Win32.Graphics.Direct3D12;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{
    internal struct D3D12BackBuffer : IDisposable
    {
        public COM_PTR_IUNKNOWN<ID3D12ResourceImp> Resource { set; get; }
        public D3D12_CPU_DESCRIPTOR_HANDLE RTV { set; get; }
        public readonly void Dispose()
        {
            this.Resource.Release();
        }
    }
}