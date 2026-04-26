using Maple.UnmanagedExtensions;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe readonly struct COM_IUNKNOWN(nint ptr)
    {
        public readonly UnsafePtr<COM_IUNKNOWN_VTABLE> VTablePointer = ptr;

        public readonly ref COM_IUNKNOWN_VTABLE VTable => ref VTablePointer.Raw;
    }


    [StructLayout(LayoutKind.Sequential)]
    public unsafe readonly struct COM_IUNKNOWN<T>(nint ptr) where T : unmanaged
    {
        public readonly UnsafePtr<COM_IUNKNOWN_VTABLE<T>> Ptr_VTable = ptr;

        public ref readonly COM_IUNKNOWN_VTABLE<T> VTable => ref Ptr_VTable.Raw;
        public ref readonly COM_IUNKNOWN_VTABLE IUnknown_VTable => ref VTable.IUnknown_VTable;
        public ref readonly T Interface_VTable => ref VTable.Interface_VTable;

        public static implicit operator COM_IUNKNOWN(COM_IUNKNOWN<T> value)
        {
            return new(value.Ptr_VTable.Pointer);
        }
    }
}