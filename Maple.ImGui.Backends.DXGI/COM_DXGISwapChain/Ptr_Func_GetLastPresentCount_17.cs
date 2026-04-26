using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::GetLastPresentCount 函数指针 (VTable 索引 17)
    /// 获取最后的呈现计数
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, int> GetLastPresentCount_17;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetLastPresentCount_17(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<uint>, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<uint>, COM_HRESULT>)ptr;

        public const string Name = "GetLastPresentCount";

        /// <summary>
        /// 获取最后的呈现计数
        /// </summary>
        /// <param name="pThis">IDXGISwapChain 接口指针</param>
        /// <param name="pLastPresentCount">接收最后呈现计数的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, UnsafeOut<uint> pLastPresentCount) => _proc(pThis, pLastPresentCount);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
