using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Data;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain
{
    /// <summary>
    /// 封装 IDXGISwapChain::GetBuffer 函数指针 (VTable 索引 9)
    /// 获取交换链的后备缓冲区
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.Guid*, void**, int> GetBuffer_9;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetBuffer_9(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, UnsafeIn<System.Guid>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, UnsafeIn<System.Guid>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT>)ptr;

        public const string Name = "GetBuffer";

        /// <summary>
        /// 获取交换链的后备缓冲区
        /// </summary>
        /// <param name="pThis">IDXGISwapChain 接口指针</param>
        /// <param name="Buffer">缓冲区索引</param>
        /// <param name="riid">请求的接口 IID</param>
        /// <param name="ppSurface">接收表面接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, uint Buffer, UnsafeIn<System.Guid> riid, UnsafeOut<COM_PTR_IUNKNOWN> ppSurface) 
            => _proc(pThis, Buffer, riid, ppSurface);
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, uint Buffer, in Guid riid, UnsafeOut<COM_PTR_IUNKNOWN> ppSurface)
        {
           return _proc(pThis, Buffer, UnsafeIn<System.Guid>.FromIn( in riid), ppSurface);
        }
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pThis, uint Buffer, in Guid riid, out COM_PTR_IUNKNOWN pSurface)
        {
            return _proc(pThis, Buffer, UnsafeIn<System.Guid>.FromIn(in riid), UnsafeOut<COM_PTR_IUNKNOWN>.FromOut(out pSurface));
        }

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
