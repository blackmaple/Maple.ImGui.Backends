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
    /// ID3D12GraphicsCommandList::ResolveQueryData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ResolveQueryData_54(nint ptr) : IHookMethod
    {
        public const string Name = "ResolveQueryData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_QUERY_TYPE, uint, uint, void*, ulong, void> ResolveQueryData_54;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, D3D12_QUERY_TYPE, uint, uint, void*, ulong, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, D3D12_QUERY_TYPE, uint, uint, void*, ulong, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, void* pQueryHeap, D3D12_QUERY_TYPE type, uint startIndex, uint numQueries, void* pDestinationBuffer, ulong alignedDestinationBufferOffset) => _proc(pThis, pQueryHeap, type, startIndex, numQueries, pDestinationBuffer, alignedDestinationBufferOffset);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
