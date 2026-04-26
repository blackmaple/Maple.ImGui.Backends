using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ptr_Func_Release(nint ptr)
    {
        public readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, COM_HRESULT> _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, COM_HRESULT>)ptr;

        public readonly COM_HRESULT Invoke(COM_PTR_IUNKNOWN @this)
        {
            return _proc(@this);
        }

        public readonly nint Pointer => new(_proc);

        public readonly override string ToString()
        {
            return Pointer.ToString("X8");
        }
    }

}