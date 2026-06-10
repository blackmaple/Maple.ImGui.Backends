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
    /// ID3D12GraphicsCommandList::ResourceBarrier
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ResourceBarrier_26(nint ptr) : IHookMethod
    {
        public const string Name = "ResourceBarrier";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_BARRIER*, void> ResourceBarrier_26;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, UnsafeRef<D3D12_RESOURCE_BARRIER>, void> _proc =
            (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, UnsafeRef<D3D12_RESOURCE_BARRIER>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint numBarriers, UnsafeRef<D3D12_RESOURCE_BARRIER> pBarriers) => _proc(pThis, numBarriers, pBarriers);
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, ReadOnlySpan<D3D12_RESOURCE_BARRIER> pBarriers) => _proc(pThis, (uint)pBarriers.Length, UnsafeRef<D3D12_RESOURCE_BARRIER>.FromRef(ref MemoryMarshal.GetReference(pBarriers)));

        public override string ToString() => PtrMethod.ToString("X8");
    }
}
