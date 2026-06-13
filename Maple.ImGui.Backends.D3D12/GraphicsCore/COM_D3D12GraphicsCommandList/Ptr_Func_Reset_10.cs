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
    /// ID3D12GraphicsCommandList::Reset
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
        internal unsafe readonly struct Ptr_Func_Reset_10(nint ptr) : IHookMethod
        {
            public const string Name = "Reset";
            /// <summary>
            /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void*, int> Reset_10;
            /// </summary>
            private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, COM_PTR_IUNKNOWN , COM_PTR_IUNKNOWN , COM_HRESULT> _proc = 
            (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, COM_PTR_IUNKNOWN, COM_PTR_IUNKNOWN, COM_HRESULT>)ptr;
            public nint PtrMethod => (nint)_proc;
            public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, COM_PTR_IUNKNOWN pAllocator, COM_PTR_IUNKNOWN pInitialState) => _proc(pThis, pAllocator, pInitialState);
            public override string ToString() => PtrMethod.ToString("X8");
        }
}
