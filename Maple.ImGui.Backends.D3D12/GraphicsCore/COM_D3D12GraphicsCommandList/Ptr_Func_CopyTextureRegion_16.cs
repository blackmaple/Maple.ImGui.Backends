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
    /// ID3D12GraphicsCommandList::CopyTextureRegion
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CopyTextureRegion_16(nint ptr) : IHookMethod
    {
        public const string Name = "CopyTextureRegion";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TEXTURE_COPY_LOCATION*, uint, uint, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_TEXTURE_COPY_LOCATION*, global::Windows.Win32.Graphics.Direct3D12.D3D12_BOX*, void> CopyTextureRegion_16;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, D3D12_TEXTURE_COPY_LOCATION*, uint, uint, uint, D3D12_TEXTURE_COPY_LOCATION*, D3D12_BOX*, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, D3D12_TEXTURE_COPY_LOCATION*, uint, uint, uint, D3D12_TEXTURE_COPY_LOCATION*, D3D12_BOX*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, D3D12_TEXTURE_COPY_LOCATION* pDst, uint dstX, uint dstY, uint dstZ, D3D12_TEXTURE_COPY_LOCATION* pSrc, D3D12_BOX* pSrcBox) => _proc(pThis, pDst, dstX, dstY, dstZ, pSrc, pSrcBox);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
