using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    /// <summary>
    /// Wraps the ID3D11DeviceContext::FinishCommandList function pointer (VTable slot 114).
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int, global::Windows.Win32.Graphics.Direct3D11.ID3D11CommandList_unmanaged**, int> FinishCommandList_114;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_FinishCommandList_114(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, int, global::Windows.Win32.Graphics.Direct3D11.ID3D11CommandList_unmanaged**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, int, global::Windows.Win32.Graphics.Direct3D11.ID3D11CommandList_unmanaged**, COM_HRESULT>)ptr;
        public const string Name = "FinishCommandList";
        /// <summary>
        /// Invokes ID3D11DeviceContext::FinishCommandList.
        /// </summary>
        /// <param name="pThis">ID3D11DeviceContext interface pointer.</param>
        /// <param name="arg1">Argument 1.</param>
        /// <param name="arg2">Argument 2.</param>
        /// <returns>Returns the underlying call result.</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, int arg1, global::Windows.Win32.Graphics.Direct3D11.ID3D11CommandList_unmanaged** arg2) => _proc(pThis, arg1, arg2);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}