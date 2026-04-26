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
    /// ID3D12CommandQueue::GetDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDesc_18(nint ptr) : IHookMethod
    {
        public const string Name = "GetDesc";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_COMMAND_QUEUE_DESC> GetDesc_18;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, D3D12_COMMAND_QUEUE_DESC> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, D3D12_COMMAND_QUEUE_DESC>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe D3D12_COMMAND_QUEUE_DESC Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
