using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    /// <summary>
    /// Wraps the ID3D11DeviceContext::OMSetRenderTargetsAndUnorderedAccessViews function pointer (VTable slot 34).
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void*, uint, uint, global::System.IntPtr*, uint*, void> OMSetRenderTargetsAndUnorderedAccessViews_34;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_OMSetRenderTargetsAndUnorderedAccessViews_34(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, uint, global::System.IntPtr*, void*, uint, uint, global::System.IntPtr*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, uint, global::System.IntPtr*, void*, uint, uint, global::System.IntPtr*, uint*, void>)ptr;
        public const string Name = "OMSetRenderTargetsAndUnorderedAccessViews";
        /// <summary>
        /// Invokes ID3D11DeviceContext::OMSetRenderTargetsAndUnorderedAccessViews.
        /// </summary>
        /// <param name="pThis">ID3D11DeviceContext interface pointer.</param>
        /// <param name="arg1">Argument 1.</param>
        /// <param name="arg2">Argument 2.</param>
        /// <param name="arg3">Argument 3.</param>
        /// <param name="arg4">Argument 4.</param>
        /// <param name="arg5">Argument 5.</param>
        /// <param name="arg6">Argument 6.</param>
        /// <param name="arg7">Argument 7.</param>
        public void Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, uint arg1, global::System.IntPtr* arg2, void* arg3, uint arg4, uint arg5, global::System.IntPtr* arg6, uint* arg7) => _proc(pThis, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}