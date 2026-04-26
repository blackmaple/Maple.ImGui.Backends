using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::ResizeBuffers 函数指针 (VTable 索引 13)
    /// 调整交换链缓冲区大小
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, int> ResizeBuffers_13;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ptr_Func_ResizeBuffers_13(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, uint, uint, DXGI_FORMAT, uint, COM_HRESULT> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, uint, uint, DXGI_FORMAT, uint, COM_HRESULT>)ptr;

        public const string Name = "ResizeBuffers";

        /// <summary>
        /// 调整交换链缓冲区大小
        /// </summary>
        /// <param name="pThis">IDXGISwapChain 接口指针</param>
        /// <param name="BufferCount">缓冲区数量</param>
        /// <param name="Width">新宽度</param>
        /// <param name="Height">新高度</param>
        /// <param name="NewFormat">新格式</param>
        /// <param name="SwapChainFlags">交换链标志</param>
        /// <returns>HRESULT</returns>
        internal COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags) =>
            _proc(pThis, BufferCount, Width, Height, NewFormat, SwapChainFlags);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
