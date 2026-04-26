using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::SetPrivateDataInterface 函数指针 (VTable 索引 36)
    /// 设置私有数据接口
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_36;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_SetPrivateDataInterface_36(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, Guid*, void*, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, Guid*, void*, HRESULT>)ptr;
        public const string Name = "SetPrivateDataInterface";
        /// <summary>
        /// 设置私有数据接口
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="guid">数据 GUID</param>
        /// <param name="pData">接口指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            Guid* guid,
            void* pData) => _proc(pThis, guid, pData);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
