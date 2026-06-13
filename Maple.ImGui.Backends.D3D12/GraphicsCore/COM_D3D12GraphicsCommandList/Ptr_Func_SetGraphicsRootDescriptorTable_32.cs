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
    /// ID3D12GraphicsCommandList::SetGraphicsRootDescriptorTable
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetGraphicsRootDescriptorTable_32(nint ptr) : IHookMethod
    {
        public const string Name = "SetGraphicsRootDescriptorTable";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_GPU_DESCRIPTOR_HANDLE, void> SetGraphicsRootDescriptorTable_32;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, D3D12_GPU_DESCRIPTOR_HANDLE, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, D3D12_GPU_DESCRIPTOR_HANDLE, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint rootParameterIndex, D3D12_GPU_DESCRIPTOR_HANDLE baseDescriptor) => _proc(pThis, rootParameterIndex, baseDescriptor);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
