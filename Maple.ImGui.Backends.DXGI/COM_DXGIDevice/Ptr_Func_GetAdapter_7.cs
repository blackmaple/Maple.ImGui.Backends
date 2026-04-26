using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Maple.ImGui.Backends.DXGI.COM_DXGIAdapter;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{
    /// <summary>
    /// 封装 IDXGIDevice::GetAdapter 函数指针 (VTable 索引 7)
    /// 获取与设备关联的适配器
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetAdapter_7;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetAdapter_7(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>, COM_HRESULT> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>, COM_HRESULT>)ptr;

        public const string Name = "GetAdapter";

        /// <summary>
        /// 获取与设备关联的适配器
        /// </summary>
        /// <param name="pThis">IDXGIDevice 接口指针</param>
        /// <param name="ppAdapter">接收 IDXGIAdapter 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pThis, out COM_PTR_IUNKNOWN<IDXGIAdapterImp> ppAdapter) =>
            _proc(pThis, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>.FromOut(out ppAdapter));

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
