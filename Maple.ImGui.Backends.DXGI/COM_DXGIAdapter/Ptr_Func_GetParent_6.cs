using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIAdapter
{
    /// <summary>
    /// 封装 IDXGIAdapter::GetParent 函数指针 (VTable 索引 6)
    /// 获取对象的父对象
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetParent_6;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetParent_6(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, UnsafeIn<System.Guid>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, UnsafeIn<System.Guid>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT>)ptr;

        public const string Name = "GetParent";

        /// <summary>
        /// 获取对象的父对象
        /// </summary>
        /// <param name="pThis">IDXGIAdapter 接口指针</param>
        /// <param name="riid">父接口的 IID</param>
        /// <param name="ppParent">接收父对象接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIAdapterImp> pThis, in global::System.Guid riid, out COM_PTR_IUNKNOWN ppParent) =>
            _proc(pThis, UnsafeIn<System.Guid>.FromIn(in riid), UnsafeOut<COM_PTR_IUNKNOWN>.FromOut(out ppParent));

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
