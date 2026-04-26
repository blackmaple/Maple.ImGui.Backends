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
    /// ID3D12DescriptorHeap::GetDevice
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDevice_7(nint ptr) : IHookMethod
    {
        public const string Name = "GetDevice";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>, Guid*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> pThis, Guid* riid, void** ppvDevice) => _proc(pThis, riid, ppvDevice);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
