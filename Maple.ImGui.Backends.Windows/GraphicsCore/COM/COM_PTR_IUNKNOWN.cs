using Maple.UnmanagedExtensions;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct COM_PTR_IUNKNOWN(nint ptr) : IDisposable
    {
        public readonly UnsafePtr<COM_IUNKNOWN> PTR_IUNKNOWN = new(ptr);

        public COM_PTR_IUNKNOWN(void* ptr) : this(new nint(ptr))
        {
        }

        public readonly override string? ToString()
        {
            return PTR_IUNKNOWN.ToString();
        }

        public readonly void Dispose()
        {
            this.Release();
        }

        public COM_PTR_IUNKNOWN<T> Get<T>() where T : unmanaged
        => new(PTR_IUNKNOWN.Pointer);

        public static implicit operator nint(COM_PTR_IUNKNOWN value) => value.PTR_IUNKNOWN.Pointer;
        public static implicit operator void*(COM_PTR_IUNKNOWN value) => value.PTR_IUNKNOWN.Pointer.ToPointer();

    }



    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct COM_PTR_IUNKNOWN<T>(nint ptr) : IDisposable where T : unmanaged
    {
        public UnsafePtr<COM_IUNKNOWN<T>> PTR_IUNKNOWN = new(ptr);

        public COM_PTR_IUNKNOWN(void* ptr) : this(new nint(ptr))
        {
        }

        public static implicit operator COM_PTR_IUNKNOWN(COM_PTR_IUNKNOWN<T> value) => new(value.PTR_IUNKNOWN.Pointer);
        public static implicit operator nint(COM_PTR_IUNKNOWN<T> value) => value.PTR_IUNKNOWN.Pointer;
        public static implicit operator void*(COM_PTR_IUNKNOWN<T> value) => value.PTR_IUNKNOWN.Pointer.ToPointer();
        public static implicit operator bool(COM_PTR_IUNKNOWN<T> value) => value.PTR_IUNKNOWN.Pointer != nint.Zero;

        public readonly override string? ToString()
        {
            return PTR_IUNKNOWN.ToString();
        }

        public readonly void Dispose()
        {
            this.Release();
        }


    }
}