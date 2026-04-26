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
    /// 设置对话框模式
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_SetDialogBoxMode_20(nint ptr): Hook.Abstractions.IHookMethod
    {
        // 原函数指针: private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, int, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, int, int>)ptr;
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, int, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, int, COM_HRESULT>)ptr;

        public const string Name = "SetDialogBoxMode";

        public COM_HRESULT Invoke(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pThis, int bEnableDialogs) => _proc(pThis, bEnableDialogs);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}