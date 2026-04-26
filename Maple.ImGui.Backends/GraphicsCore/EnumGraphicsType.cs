using System.Collections.Concurrent;

namespace Maple.ImGui.Backends.GraphicsCore
{
    public enum EnumGraphicsType
    {
        D3D9,
        D3D10,
        D3D11,
        D3D12,
        OPENGL,
        VULKAN,
    }


    public interface IHookGraphics
    {
        EnumGraphicsType GraphicsType { get; }
    }
}
