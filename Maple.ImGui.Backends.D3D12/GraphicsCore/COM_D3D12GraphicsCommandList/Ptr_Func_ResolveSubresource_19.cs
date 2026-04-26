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
    /// ID3D12GraphicsCommandList::ResolveSubresource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ResolveSubresource_19(nint ptr) : IHookMethod
    {
        public const string Name = "ResolveSubresource";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void*, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, void> ResolveSubresource_19;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, uint, void*, uint, DXGI_FORMAT, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, uint, void*, uint, DXGI_FORMAT, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, void* pDstResource, uint dstSubresource, void* pSrcResource, uint srcSubresource, DXGI_FORMAT format) => _proc(pThis, pDstResource, dstSubresource, pSrcResource, srcSubresource, format);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
