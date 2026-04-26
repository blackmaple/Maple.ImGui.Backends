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
    /// ID3D12GraphicsCommandList::OMSetRenderTargets
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMSetRenderTargets_46(nint ptr) : IHookMethod
    {
        public const string Name = "OMSetRenderTargets";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE*, int, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE*, void> OMSetRenderTargets_46;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, UnsafeRef<D3D12_CPU_DESCRIPTOR_HANDLE>, bool, UnsafeIn<D3D12_CPU_DESCRIPTOR_HANDLE>, void> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, UnsafeRef<D3D12_CPU_DESCRIPTOR_HANDLE>, bool, UnsafeIn<D3D12_CPU_DESCRIPTOR_HANDLE>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint numRenderTargetDescriptors, UnsafeRef<D3D12_CPU_DESCRIPTOR_HANDLE> pRenderTargetDescriptors, bool RTsSingleHandleToDescriptorRange, UnsafeIn<D3D12_CPU_DESCRIPTOR_HANDLE> pDepthStencilDescriptor)
            => _proc(pThis, numRenderTargetDescriptors, pRenderTargetDescriptors, RTsSingleHandleToDescriptorRange, pDepthStencilDescriptor);

        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, ReadOnlySpan<D3D12_CPU_DESCRIPTOR_HANDLE> pRenderTargetDescriptors, bool RTsSingleHandleToDescriptorRange, UnsafeIn<D3D12_CPU_DESCRIPTOR_HANDLE> pDepthStencilDescriptor)
           => _proc(pThis, (uint)pRenderTargetDescriptors.Length, UnsafeRef<D3D12_CPU_DESCRIPTOR_HANDLE>.FromRef(ref MemoryMarshal.GetReference(pRenderTargetDescriptors)), RTsSingleHandleToDescriptorRange, pDepthStencilDescriptor);


        public override string ToString() => PtrMethod.ToString("X8");
    }
}
