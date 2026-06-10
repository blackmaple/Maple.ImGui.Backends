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
    /// ID3D12GraphicsCommandList::ExecuteBundle
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ExecuteBundle_27(nint ptr) : IHookMethod
    {
        public const string Name = "ExecuteBundle";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> ExecuteBundle_27;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, void* pBundle) => _proc(pThis, pBundle);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
