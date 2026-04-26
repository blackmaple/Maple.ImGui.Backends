using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::OpenSharedResource 函数指针 (VTable 索引 28)
    /// 打开共享资源
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.Guid*, void**, int> OpenSharedResource_28;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_OpenSharedResource_28(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, void*, Guid*, void**, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, void*, Guid*, void**, HRESULT>)ptr;
        public const string Name = "OpenSharedResource";
        /// <summary>
        /// 打开共享资源
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="hResource">资源句柄</param>
        /// <param name="ReturnedInterface">返回的接口 GUID</param>
        /// <param name="ppResource">接收资源接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            void* hResource,
            Guid* ReturnedInterface,
            void** ppResource) => _proc(pThis, hResource, ReturnedInterface, ppResource);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
