using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList
{
    /// <summary>
    /// ID3D12GraphicsCommandList::Close
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
        internal unsafe readonly struct Ptr_Func_Close_9(nint ptr) : IHookMethod
        {
            public const string Name = "Close";
            /// <summary>
            /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> Close_9;
            /// </summary>
            private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, COM_HRESULT>)ptr;
            public nint PtrMethod => (nint)_proc;
            public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis) => _proc(pThis);
            public override string ToString() => PtrMethod.ToString("X8");
        }
}
