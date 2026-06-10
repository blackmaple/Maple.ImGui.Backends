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
    /// ID3D12CommandQueue::EndEvent
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_EndEvent_13(nint ptr) : IHookMethod
    {
        public const string Name = "EndEvent";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> EndEvent_13;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
