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
    /// ID3D12DescriptorHeap::GetDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDesc_8(nint ptr) : IHookMethod
    {
        public const string Name = "GetDesc";
        /// <summary>
        /// Win32 SDK (_WIN32): STDMETHODCALLTYPE(This, RetVal).
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, UnsafeOut<D3D12_DESCRIPTOR_HEAP_DESC>, void> _proc
            = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, UnsafeOut<D3D12_DESCRIPTOR_HEAP_DESC>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> pThis, out D3D12_DESCRIPTOR_HEAP_DESC desc) => _proc(pThis, UnsafeOut<D3D12_DESCRIPTOR_HEAP_DESC>.FromOut(out desc));
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
