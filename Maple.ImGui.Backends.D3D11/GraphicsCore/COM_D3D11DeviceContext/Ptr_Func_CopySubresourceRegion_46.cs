using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    /// <summary>
    /// Wraps the ID3D11DeviceContext::CopySubresourceRegion function pointer (VTable slot 46).
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, uint, uint, uint, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_BOX*, void> CopySubresourceRegion_46;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CopySubresourceRegion_46(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, void*, uint, uint, uint, uint, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_BOX*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, void*, uint, uint, uint, uint, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_BOX*, void>)ptr;
        public const string Name = "CopySubresourceRegion";
        /// <summary>
        /// Invokes ID3D11DeviceContext::CopySubresourceRegion.
        /// </summary>
        /// <param name="pThis">ID3D11DeviceContext interface pointer.</param>
        /// <param name="arg1">Argument 1.</param>
        /// <param name="arg2">Argument 2.</param>
        /// <param name="arg3">Argument 3.</param>
        /// <param name="arg4">Argument 4.</param>
        /// <param name="arg5">Argument 5.</param>
        /// <param name="arg6">Argument 6.</param>
        /// <param name="arg7">Argument 7.</param>
        /// <param name="arg8">Argument 8.</param>
        public void Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, void* arg1, uint arg2, uint arg3, uint arg4, uint arg5, void* arg6, uint arg7, global::Windows.Win32.Graphics.Direct3D11.D3D11_BOX* arg8) => _proc(pThis, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}