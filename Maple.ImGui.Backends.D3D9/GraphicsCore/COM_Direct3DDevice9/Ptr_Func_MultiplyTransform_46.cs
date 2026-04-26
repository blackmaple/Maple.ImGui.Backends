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
    /// 封装 IDirect3DDevice9::MultiplyTransform 函数指针 (VTable 索引 46)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_MultiplyTransform_46(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, global::Windows.Win32.Graphics.Direct3D9.D3DTRANSFORMSTATETYPE, UnsafeRef<global::Windows.Win32.Graphics.Direct3D.D3DMATRIX>, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, global::Windows.Win32.Graphics.Direct3D9.D3DTRANSFORMSTATETYPE, UnsafeRef<global::Windows.Win32.Graphics.Direct3D.D3DMATRIX>, COM_HRESULT>)ptr;

        public const string Name = "MultiplyTransform";

        public COM_HRESULT Invoke(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pThis, global::Windows.Win32.Graphics.Direct3D9.D3DTRANSFORMSTATETYPE State, UnsafeRef<global::Windows.Win32.Graphics.Direct3D.D3DMATRIX> pMatrix) => _proc(pThis, State, pMatrix);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}