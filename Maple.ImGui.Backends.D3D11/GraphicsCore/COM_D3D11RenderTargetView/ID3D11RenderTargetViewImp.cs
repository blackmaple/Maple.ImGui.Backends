using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11RenderTargetView
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D11RenderTargetViewImp
    {
        public static readonly Guid GUID = new("DFDBA067-0B8D-4865-875B-D7B4516CC164");
    }
}
