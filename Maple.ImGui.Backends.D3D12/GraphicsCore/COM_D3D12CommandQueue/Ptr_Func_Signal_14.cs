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
    /// ID3D12CommandQueue::Signal
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Signal_14(nint ptr) : IHookMethod
    {
        public const string Name = "Signal";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, ulong, int> Signal_14;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, COM_PTR_IUNKNOWN, ulong, COM_HRESULT> _proc = 
            (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, COM_PTR_IUNKNOWN, ulong, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis, COM_PTR_IUNKNOWN pFence, ulong value) => _proc(pThis, pFence, value);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
