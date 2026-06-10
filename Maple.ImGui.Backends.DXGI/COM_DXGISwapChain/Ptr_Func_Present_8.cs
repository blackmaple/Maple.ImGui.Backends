using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::Present 函数指针 (VTable 索引 8)
    /// 呈现交换链的缓冲区内容
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT, int> Present_8;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Ptr_Func_Present_8(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, uint, COM_HRESULT> _proc
            = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, uint, COM_HRESULT>)ptr;

        public const string Name = "Present";

        /// <summary>
        /// 呈现交换链的缓冲区内容
        /// </summary>
        /// <param name="pThis">IDXGISwapChain 接口指针</param>
        /// <param name="SyncInterval">同步间隔</param>
        /// <param name="Flags">呈现标志</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, uint SyncInterval, uint Flags) => _proc(pThis, SyncInterval, Flags);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
