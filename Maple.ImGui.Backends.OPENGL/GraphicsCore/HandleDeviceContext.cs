using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;

namespace Maple.ImGui.Backends.OPENGL.GraphicsCore
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct HandleDeviceContext(nint hdc)
    {
        readonly HDC _hdc = new(hdc);

        public nint HandleContext => _hdc;
        public nint WindowHandle => PInvoke.WindowFromDC(_hdc);
    }
}
