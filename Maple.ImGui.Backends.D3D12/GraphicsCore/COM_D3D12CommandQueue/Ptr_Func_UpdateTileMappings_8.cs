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
    /// ID3D12CommandQueue::UpdateTileMappings
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_UpdateTileMappings_8(nint ptr) : IHookMethod
    {
        public const string Name = "UpdateTileMappings";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILED_RESOURCE_COORDINATE*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_REGION_SIZE*, void*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_RANGE_FLAGS*, uint*, uint*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_MAPPING_FLAGS, void> UpdateTileMappings_8;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, void*, uint, D3D12_TILED_RESOURCE_COORDINATE*, D3D12_TILE_REGION_SIZE*, void*, uint, D3D12_TILE_RANGE_FLAGS*, uint*, uint*, D3D12_TILE_MAPPING_FLAGS, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, void*, uint, D3D12_TILED_RESOURCE_COORDINATE*, D3D12_TILE_REGION_SIZE*, void*, uint, D3D12_TILE_RANGE_FLAGS*, uint*, uint*, D3D12_TILE_MAPPING_FLAGS, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis, void* pResource, uint numResourceRegions, D3D12_TILED_RESOURCE_COORDINATE* pResourceRegionStartCoordinates, D3D12_TILE_REGION_SIZE* pResourceRegionSizes, void* pHeap, uint numRanges, D3D12_TILE_RANGE_FLAGS* pRangeFlags, uint* pHeapRangeStartOffsets, uint* pRangeTileCounts, D3D12_TILE_MAPPING_FLAGS flags) => _proc(pThis, pResource, numResourceRegions, pResourceRegionStartCoordinates, pResourceRegionSizes, pHeap, numRanges, pRangeFlags, pHeapRangeStartOffsets, pRangeTileCounts, flags);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
