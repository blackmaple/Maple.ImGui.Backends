using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::ResizeTarget 函数指针 (VTable 索引 14)
    /// 调整目标窗口大小
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_MODE_DESC*, int> ResizeTarget_14;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_ResizeTarget_14(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<DXGI_MODE_DESC>, COM_HRESULT>
            _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<DXGI_MODE_DESC>, COM_HRESULT>)ptr;

        public const string Name = "ResizeTarget";

        /// <summary>
        /// 调整目标窗口大小
        /// </summary>
        /// <param name="pThis">IDXGISwapChain 接口指针</param>
        /// <param name="pNewTargetParameters">新的目标参数</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, UnsafeIn<DXGI_MODE_DESC> pNewTargetParameters) => _proc(pThis, pNewTargetParameters);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
