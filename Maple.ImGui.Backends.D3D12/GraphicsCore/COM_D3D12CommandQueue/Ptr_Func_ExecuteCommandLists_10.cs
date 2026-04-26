using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue
{
    /// <summary>
    /// ID3D12CommandQueue::ExecuteCommandLists
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe readonly struct Ptr_Func_ExecuteCommandLists_10(nint ptr) : IHookMethod
    {
        public const string Name = "ExecuteCommandLists";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void> ExecuteCommandLists_10;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, uint, UnsafePtr, void> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, uint, UnsafePtr, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis, ReadOnlySpan<COM_PTR_IUNKNOWN> ppCommandLists)
        {
            _proc(pThis, (uint)ppCommandLists.Length, UnsafeRef<COM_PTR_IUNKNOWN>.FromRef(ref MemoryMarshal.GetReference(ppCommandLists)));
        }
        public void Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis, uint NumCommandLists, UnsafeRef<COM_PTR_IUNKNOWN> ppCommandLists)
            => _proc(pThis, NumCommandLists, ppCommandLists);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
