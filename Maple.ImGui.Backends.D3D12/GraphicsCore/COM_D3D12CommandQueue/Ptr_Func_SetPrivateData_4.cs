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
    /// ID3D12CommandQueue::SetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateData_4(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, Guid*, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, Guid*, uint, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> pThis, Guid* guid, uint dataSize, void* pData) => _proc(pThis, guid, dataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
