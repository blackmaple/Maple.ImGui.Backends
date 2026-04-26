using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIAdapter
{
    /// <summary>
    /// 封装 IDXGIAdapter::CheckInterfaceSupport 函数指针 (VTable 索引 9)
    /// 检查系统是否支持特定的设备接口
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, long*, int> CheckInterfaceSupport_9;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CheckInterfaceSupport_9(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, global::System.Guid*, long*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, global::System.Guid*, long*, int>)ptr;

        public const string Name = "CheckInterfaceSupport";

        /// <summary>
        /// 检查系统是否支持特定的设备接口
        /// </summary>
        /// <param name="pThis">IDXGIAdapter 接口指针</param>
        /// <param name="InterfaceName">要检查的接口的 GUID</param>
        /// <param name="pUMDVersion">用户模式驱动程序版本号</param>
        /// <returns>HRESULT</returns>
        public int Invoke(COM_PTR_IUNKNOWN<IDXGIAdapterImp> pThis, global::System.Guid* InterfaceName, long* pUMDVersion) => _proc(pThis, InterfaceName, pUMDVersion);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
