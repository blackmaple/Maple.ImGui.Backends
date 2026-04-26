using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIAdapter
{
    /// <summary>
    /// 封装 IDXGIAdapter::SetPrivateDataInterface 函数指针 (VTable 索引 4)
    /// 设置与接口关联的私有接口
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_4;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_SetPrivateDataInterface_4(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, global::System.Guid*, void*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, global::System.Guid*, void*, int>)ptr;

        public const string Name = "SetPrivateDataInterface";

        /// <summary>
        /// 设置与接口关联的私有接口
        /// </summary>
        /// <param name="pThis">IDXGIAdapter 接口指针</param>
        /// <param name="Name">接口标识符的 GUID</param>
        /// <param name="pUnknown">指向 IUnknown 接口的指针</param>
        /// <returns>HRESULT</returns>
        public int Invoke(COM_PTR_IUNKNOWN<IDXGIAdapterImp> pThis, global::System.Guid* Name, void* pUnknown) => _proc(pThis, Name, pUnknown);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
