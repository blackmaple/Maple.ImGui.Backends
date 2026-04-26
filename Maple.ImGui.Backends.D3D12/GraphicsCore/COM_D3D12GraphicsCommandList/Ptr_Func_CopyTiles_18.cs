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
    /// ID3D12GraphicsCommandList::CopyTiles
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CopyTiles_18(nint ptr) : IHookMethod
    {
        public const string Name = "CopyTiles";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILED_RESOURCE_COORDINATE*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_REGION_SIZE*, void*, ulong, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_COPY_FLAGS, void> CopyTiles_18;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, D3D12_TILED_RESOURCE_COORDINATE*, D3D12_TILE_REGION_SIZE*, void*, ulong, D3D12_TILE_COPY_FLAGS, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, D3D12_TILED_RESOURCE_COORDINATE*, D3D12_TILE_REGION_SIZE*, void*, ulong, D3D12_TILE_COPY_FLAGS, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, void* pTiledResource, D3D12_TILED_RESOURCE_COORDINATE* pTileRegionStartCoordinate, D3D12_TILE_REGION_SIZE* pTileRegionSize, void* pBuffer, ulong bufferStartOffsetInBytes, D3D12_TILE_COPY_FLAGS flags) => _proc(pThis, pTiledResource, pTileRegionStartCoordinate, pTileRegionSize, pBuffer, bufferStartOffsetInBytes, flags);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
