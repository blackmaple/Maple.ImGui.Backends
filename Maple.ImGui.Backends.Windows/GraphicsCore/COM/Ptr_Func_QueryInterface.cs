using Maple.UnmanagedExtensions;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ptr_Func_QueryInterface(nint ptr)
    {
        public readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, UnsafeIn<Guid>, UnsafePtr, COM_HRESULT> _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, UnsafeIn<Guid>, UnsafePtr, COM_HRESULT>)ptr;

        public readonly COM_HRESULT Invoke<T>(COM_PTR_IUNKNOWN @this, in Guid riid, out COM_PTR_IUNKNOWN<T> ppvObject)
            where T : unmanaged
        {
            return _proc(@this, UnsafeIn<Guid>.FromIn(in riid), UnsafeOut<COM_PTR_IUNKNOWN<T>>.FromOut(out ppvObject));
        }

        public readonly nint Pointer => new(_proc);

        public readonly override string ToString()
        {
            return Pointer.ToString("X8");
        }
    }

}