using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList
{

    /// <summary>
    /// ID3D12GraphicsCommandList::SetDescriptorHeaps
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetDescriptorHeaps_28(nint ptr) : IHookMethod
    {
        public const string Name = "SetDescriptorHeaps";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void> SetDescriptorHeaps_28;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, void> _proc = 
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        //public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint numDescriptorHeaps, void** ppDescriptorHeaps) => _proc(pThis, numDescriptorHeaps, ppDescriptorHeaps);

        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, params ReadOnlySpan<COM_PTR_IUNKNOWN> ppDescriptorHeaps)
        {
            _proc(pThis, (uint)ppDescriptorHeaps.Length, UnsafeRef<COM_PTR_IUNKNOWN>.FromRef(ref MemoryMarshal.GetReference(ppDescriptorHeaps)));
        }

        public override string ToString() => PtrMethod.ToString("X8");
    }
}
