using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::GetDesc 函数指针 (VTable 索引 12)
    /// 获取交换链的描述信息
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC*, int> GetDesc_12;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetDesc_12(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafePtr, COM_HRESULT> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafePtr, COM_HRESULT>)ptr;

        public const string Name = "GetDesc";

        ///// <summary>
        ///// 获取交换链的描述信息
        ///// </summary>
        ///// <param name="pThis">IDXGISwapChain 接口指针</param>
        ///// <param name="pDesc">接收交换链描述的结构体指针</param>
        ///// <returns>HRESULT</returns>
        //public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, out DXGI_SWAP_CHAIN_DESC pDesc)
        //    => _proc(pThis, UnsafeOut<DXGI_SWAP_CHAIN_DESC>.FromOut(out pDesc));

        public COM_HRESULT Invoke<T>(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, UnsafeOut<T> pDesc) where T : unmanaged
            => _proc(pThis, pDesc);
        //public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, UnsafePtr pDesc)
        //    => _proc(pThis, pDesc);


        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
