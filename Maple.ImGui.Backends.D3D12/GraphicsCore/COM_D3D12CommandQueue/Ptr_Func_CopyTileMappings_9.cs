using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue
{
    /// <summary>
    /// ID3D12CommandQueue::CopyTileMappings
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CopyTileMappings_9(nint ptr) : IHookMethod
    {
        public const string Name = "CopyTileMappings";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILED_RESOURCE_COORDINATE*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILED_RESOURCE_COORDINATE*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_REGION_SIZE*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_MAPPING_FLAGS, void> CopyTileMappings_9;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, void*, D3D12_TILED_RESOURCE_COORDINATE*, void*, D3D12_TILED_RESOURCE_COORDINATE*, D3D12_TILE_REGION_SIZE*, D3D12_TILE_MAPPING_FLAGS, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, void*, D3D12_TILED_RESOURCE_COORDINATE*, void*, D3D12_TILED_RESOURCE_COORDINATE*, D3D12_TILE_REGION_SIZE*, D3D12_TILE_MAPPING_FLAGS, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis, void* pDstResource, D3D12_TILED_RESOURCE_COORDINATE* pDstRegionStartCoordinate, void* pSrcResource, D3D12_TILED_RESOURCE_COORDINATE* pSrcRegionStartCoordinate, D3D12_TILE_REGION_SIZE* pRegionSize, D3D12_TILE_MAPPING_FLAGS flags) => _proc(pThis, pDstResource, pDstRegionStartCoordinate, pSrcResource, pSrcRegionStartCoordinate, pRegionSize, flags);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
