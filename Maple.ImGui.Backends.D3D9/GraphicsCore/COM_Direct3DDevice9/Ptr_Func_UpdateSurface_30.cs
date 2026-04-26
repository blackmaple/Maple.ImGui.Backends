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
    /// 更新表面
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_UpdateSurface_30(nint ptr): Hook.Abstractions.IHookMethod
    {
 
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnsafeRef<RECT>, nint, UnsafeRef<global::System.Drawing.Point>, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnsafeRef<RECT>, nint, UnsafeRef<global::System.Drawing.Point>, COM_HRESULT>)ptr;
         
        public const string Name = "UpdateSurface";

        public COM_HRESULT Invoke(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pThis, nint pSourceSurface, UnsafeRef<RECT> pSourceRect, nint pDestinationSurface, UnsafeRef<global::System.Drawing.Point> pDestPoint) => _proc(pThis, pSourceSurface, pSourceRect, pDestinationSurface, pDestPoint);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}