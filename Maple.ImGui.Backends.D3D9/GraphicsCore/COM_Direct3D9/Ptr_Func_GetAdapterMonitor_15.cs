using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3D9
{
    /// <summary>
    /// 封装 IDirect3D9::GetAdapterMonitor 函数指针 (VTable 索引 15)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetAdapterMonitor_15(nint ptr)
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, uint, nint> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, uint, nint>)ptr;

        public nint Invoke(COM_PTR_IUNKNOWN pThis, uint Adapter) => _proc(pThis, Adapter);

        public override string ToString()
        {
            return (new nint(_proc)).ToString("X8");
        }
    }
}