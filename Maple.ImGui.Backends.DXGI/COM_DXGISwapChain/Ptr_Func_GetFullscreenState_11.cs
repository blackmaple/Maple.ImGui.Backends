using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::GetFullscreenState 函数指针 (VTable 索引 11)
    /// 获取全屏模式状态
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.BOOL*, global::Windows.Win32.Graphics.Dxgi.IDXGIOutput_unmanaged**, int> GetFullscreenState_11;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetFullscreenState_11(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<BOOL>, UnsafeOut<UnsafePtr>, COM_HRESULT>
            _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<BOOL>, UnsafeOut<UnsafePtr>, COM_HRESULT>)ptr;

        public const string Name = "GetFullscreenState";

        /// <summary>
        /// 获取全屏模式状态
        /// </summary>
        /// <param name="pThis">IDXGISwapChain 接口指针</param>
        /// <param name="pFullscreen">接收是否全屏的指针</param>
        /// <param name="ppTarget">接收目标输出设备的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, UnsafeOut<BOOL> pFullscreen, UnsafeOut<UnsafePtr> ppTarget) => _proc(pThis, pFullscreen, ppTarget);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
