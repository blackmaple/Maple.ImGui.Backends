using Windows.Win32.Graphics.Direct3D12;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{
    internal struct D3D12TextureSlot
    {
        public D3D12_CPU_DESCRIPTOR_HANDLE CPU { set; get; }
        public D3D12_GPU_DESCRIPTOR_HANDLE GPU { set; get; }
    }
}