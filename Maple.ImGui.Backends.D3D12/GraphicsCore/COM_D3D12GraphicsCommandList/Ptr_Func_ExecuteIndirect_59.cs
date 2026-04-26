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
    /// ID3D12GraphicsCommandList::ExecuteIndirect
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ExecuteIndirect_59(nint ptr) : IHookMethod
    {
        public const string Name = "ExecuteIndirect";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void*, ulong, void*, ulong, void> ExecuteIndirect_59;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, uint, void*, ulong, void*, ulong, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, uint, void*, ulong, void*, ulong, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, void* pCommandSignature, uint maxCommandCount, void* pArgumentBuffer, ulong argumentBufferSize, void* pCountBuffer, ulong countBufferSize) => _proc(pThis, pCommandSignature, maxCommandCount, pArgumentBuffer, argumentBufferSize, pCountBuffer, countBufferSize);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
