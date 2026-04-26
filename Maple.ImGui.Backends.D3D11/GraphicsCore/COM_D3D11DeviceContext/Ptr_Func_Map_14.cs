using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    /// <summary>
    /// Wraps the ID3D11DeviceContext::Map function pointer (VTable slot 14).
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAP, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAPPED_SUBRESOURCE*, int> Map_14;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_Map_14(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAP, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAPPED_SUBRESOURCE*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAP, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAPPED_SUBRESOURCE*, COM_HRESULT>)ptr;
        public const string Name = "Map";
        /// <summary>
        /// Invokes ID3D11DeviceContext::Map.
        /// </summary>
        /// <param name="pThis">ID3D11DeviceContext interface pointer.</param>
        /// <param name="arg1">Argument 1.</param>
        /// <param name="arg2">Argument 2.</param>
        /// <param name="arg3">Argument 3.</param>
        /// <param name="arg4">Argument 4.</param>
        /// <param name="arg5">Argument 5.</param>
        /// <returns>Returns the underlying call result.</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, void* arg1, uint arg2, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAP arg3, uint arg4, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAPPED_SUBRESOURCE* arg5) => _proc(pThis, arg1, arg2, arg3, arg4, arg5);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}