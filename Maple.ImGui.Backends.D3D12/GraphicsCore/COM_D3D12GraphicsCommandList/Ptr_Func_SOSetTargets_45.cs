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
    /// ID3D12GraphicsCommandList::SOSetTargets
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SOSetTargets_45(nint ptr) : IHookMethod
    {
        public const string Name = "SOSetTargets";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_STREAM_OUTPUT_BUFFER_VIEW*, void> SOSetTargets_45;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, D3D12_STREAM_OUTPUT_BUFFER_VIEW*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, D3D12_STREAM_OUTPUT_BUFFER_VIEW*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint startSlot, uint numViews, D3D12_STREAM_OUTPUT_BUFFER_VIEW* pViews) => _proc(pThis, startSlot, numViews, pViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
