using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct COM_IUNKNOWN_VTABLE
    {
        public readonly Ptr_Func_QueryInterface QueryInterface_0;
        public readonly Ptr_Func_AddRef AddRef_1;
        public readonly Ptr_Func_Release Release_2;
    }


    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct COM_IUNKNOWN_VTABLE<T> where T : unmanaged
    {
        public readonly COM_IUNKNOWN_VTABLE IUnknown_VTable;
        public readonly T Interface_VTable;
    }
}