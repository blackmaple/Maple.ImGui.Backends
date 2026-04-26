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
    /// ID3D12GraphicsCommandList::ClearUnorderedAccessViewUint
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ClearUnorderedAccessViewUint_49(nint ptr) : IHookMethod
    {
        public const string Name = "ClearUnorderedAccessViewUint";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_GPU_DESCRIPTOR_HANDLE, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void*, uint*, uint, global::Windows.Win32.Foundation.RECT*, void> ClearUnorderedAccessViewUint_49;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, D3D12_GPU_DESCRIPTOR_HANDLE, D3D12_CPU_DESCRIPTOR_HANDLE, void*, uint*, uint, RECT*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, D3D12_GPU_DESCRIPTOR_HANDLE, D3D12_CPU_DESCRIPTOR_HANDLE, void*, uint*, uint, RECT*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, D3D12_GPU_DESCRIPTOR_HANDLE viewGPUHandle, D3D12_CPU_DESCRIPTOR_HANDLE viewCPUHandle, void* pValues, uint* pValuesSize, uint numRects, RECT* pRects) => _proc(pThis, viewGPUHandle, viewCPUHandle, pValues, pValuesSize, numRects, pRects);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
