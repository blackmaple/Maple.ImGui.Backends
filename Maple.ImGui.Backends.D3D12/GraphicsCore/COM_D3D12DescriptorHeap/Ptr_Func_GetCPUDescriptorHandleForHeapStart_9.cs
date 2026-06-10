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
    /// ID3D12DescriptorHeap::GetCPUDescriptorHandleForHeapStart
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetCPUDescriptorHandleForHeapStart_9(nint ptr) : IHookMethod
    {
        public const string Name = "GetCPUDescriptorHandleForHeapStart";
        /// <summary>
        /// Win32 SDK (_WIN32): STDMETHODCALLTYPE(This, RetVal).
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, UnsafeOut<D3D12_CPU_DESCRIPTOR_HANDLE>, void> _proc
            = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, UnsafeOut<D3D12_CPU_DESCRIPTOR_HANDLE>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> pThis, out D3D12_CPU_DESCRIPTOR_HANDLE descHandle) => _proc(pThis, UnsafeOut<D3D12_CPU_DESCRIPTOR_HANDLE>.FromOut(out descHandle));
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
