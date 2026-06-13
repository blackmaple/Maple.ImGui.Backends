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
    /// ID3D12GraphicsCommandList::ClearRenderTargetView
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ClearRenderTargetView_48(nint ptr) : IHookMethod
    {
        public const string Name = "ClearRenderTargetView";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, float*, uint, global::Windows.Win32.Foundation.RECT*, void> ClearRenderTargetView_48;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, D3D12_CPU_DESCRIPTOR_HANDLE, float*, uint, RECT*, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, D3D12_CPU_DESCRIPTOR_HANDLE, float*, uint, RECT*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, D3D12_CPU_DESCRIPTOR_HANDLE renderTargetView, float* colorRGBA, uint numRects, RECT* pRects) => _proc(pThis, renderTargetView, colorRGBA, numRects, pRects);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
