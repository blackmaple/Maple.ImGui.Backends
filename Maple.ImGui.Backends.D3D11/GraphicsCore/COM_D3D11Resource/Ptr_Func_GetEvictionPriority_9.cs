using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Resource
{
    /// <summary>
    /// Wraps the ID3D11ResourceImp::GetEvictionPriority function pointer (VTable slot 9).
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetEvictionPriority_9;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetEvictionPriority_9(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11ResourceImp>, uint> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11ResourceImp>, uint>)ptr;
        public const string Name = "GetEvictionPriority";
        /// <summary>
        /// Invokes ID3D11ResourceImp::GetEvictionPriority.
        /// </summary>
        /// <param name="pThis">ID3D11ResourceImp interface pointer.</param>
        /// <returns>Returns the underlying call result.</returns>
        public uint Invoke(COM_PTR_IUNKNOWN<ID3D11ResourceImp> pThis) => _proc(pThis);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
