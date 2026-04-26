using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap
{
    /// <summary>
    /// ID3D12DescriptorHeap::GetGPUDescriptorHandleForHeapStart
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetGPUDescriptorHandleForHeapStart_10(nint ptr) : IHookMethod
    {
        public const string Name = "GetGPUDescriptorHandleForHeapStart";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_GPU_DESCRIPTOR_HANDLE> GetGPUDescriptorHandleForHeapStart_10;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, D3D12_GPU_DESCRIPTOR_HANDLE> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, D3D12_GPU_DESCRIPTOR_HANDLE>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe D3D12_GPU_DESCRIPTOR_HANDLE Invoke(COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
