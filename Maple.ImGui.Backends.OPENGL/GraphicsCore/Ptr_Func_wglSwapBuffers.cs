using Maple.Hook.Abstractions;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Gdi;

namespace Maple.ImGui.Backends.OPENGL.GraphicsCore
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe readonly struct Ptr_Func_wglSwapBuffers(nint ptr) : IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<HandleDeviceContext, bool> _proc =
           (delegate* unmanaged[Stdcall, SuppressGCTransition]<HandleDeviceContext, bool>)ptr;

        public const string Name = "wglSwapBuffers";

        internal bool Invoke(HandleDeviceContext hdc) => _proc(hdc);

        public bool Invoke(nint hdc) => _proc(new HandleDeviceContext(hdc));

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
