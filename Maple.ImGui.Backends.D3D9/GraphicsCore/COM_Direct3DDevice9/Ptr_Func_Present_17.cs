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
    /// 封装 IDirect3DDevice9::Present 函数指针 (VTable 索引 17)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ptr_Func_Present_17(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>,
            UnsafeRef<RECT>,
            UnsafeRef<RECT>, 
            HWND,
            UnsafeRef<global::Windows.Win32.Graphics.Gdi.RGNDATA>, 
            COM_HRESULT> _proc 
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>,
            UnsafeRef<RECT>,
            UnsafeRef<RECT>,
            HWND,
            UnsafeRef<global::Windows.Win32.Graphics.Gdi.RGNDATA>, 
            COM_HRESULT>)ptr;

        public const string Name = "Present";

        public COM_HRESULT Invoke(
            Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pThis, 
            UnsafePtr pSourceRect,
            UnsafePtr pDestRect, 
            nint hDestWindowOverride, 
            UnsafePtr pDirtyRegion) => 
            _proc(pThis,   
                pSourceRect.GetUnsafeRef<global::Windows.Win32.Foundation.RECT>(), 
                pDestRect.GetUnsafeRef<global::Windows.Win32.Foundation.RECT>(),
                new HWND(hDestWindowOverride), 
                pDirtyRegion.GetUnsafeRef<global::Windows.Win32.Graphics.Gdi.RGNDATA>());

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}