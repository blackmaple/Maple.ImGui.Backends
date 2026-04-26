using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{
    /// <summary>
    /// 封装 IDXGIDevice::CreateSurface 函数指针 (VTable 索引 8)
    /// 创建一个表面
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SURFACE_DESC*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_USAGE, global::Windows.Win32.Graphics.Dxgi.DXGI_SHARED_RESOURCE*, global::System.IntPtr*, int> CreateSurface_8;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateSurface_8(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, DXGI_SURFACE_DESC*, uint, DXGI_USAGE, DXGI_SHARED_RESOURCE*, global::System.IntPtr*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, DXGI_SURFACE_DESC*, uint, DXGI_USAGE, DXGI_SHARED_RESOURCE*, global::System.IntPtr*, int>)ptr;

        public const string Name = "CreateSurface";

        /// <summary>
        /// 创建一个表面
        /// </summary>
        /// <param name="pThis">IDXGIDevice 接口指针</param>
        /// <param name="pDesc">表面描述数组</param>
        /// <param name="NumSurfaces">要创建的表面数量</param>
        /// <param name="Usage">表面使用标志</param>
        /// <param name="pSharedResource">共享资源参数</param>
        /// <param name="ppSurface">接收 IDXGISurface 接口指针数组的指针</param>
        /// <returns>HRESULT</returns>
        public int Invoke(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pThis, DXGI_SURFACE_DESC* pDesc, uint NumSurfaces, DXGI_USAGE Usage, DXGI_SHARED_RESOURCE* pSharedResource, global::System.IntPtr* ppSurface) => _proc(pThis, pDesc, NumSurfaces, Usage, pSharedResource, ppSurface);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
