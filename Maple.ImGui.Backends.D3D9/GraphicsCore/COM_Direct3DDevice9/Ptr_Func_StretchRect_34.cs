using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9
{
    /// <summary>
    /// 拉伸矩形
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_StretchRect_34(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef<RECT>, nint, UnmanagedExtensions.UnsafeRef<RECT>, global::Windows.Win32.Graphics.Direct3D9.D3DTEXTUREFILTERTYPE, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef<RECT>, nint, UnmanagedExtensions.UnsafeRef<RECT>, global::Windows.Win32.Graphics.Direct3D9.D3DTEXTUREFILTERTYPE, COM_HRESULT>)ptr;

        public const string Name = "StretchRect";

        public COM_HRESULT Invoke(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pThis, nint pSourceSurface, UnmanagedExtensions.UnsafeRef<RECT> pSourceRect, nint pDestSurface, UnmanagedExtensions.UnsafeRef<RECT> pDestRect, global::Windows.Win32.Graphics.Direct3D9.D3DTEXTUREFILTERTYPE Filter) => _proc(pThis, pSourceSurface, pSourceRect, pDestSurface, pDestRect, Filter);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}