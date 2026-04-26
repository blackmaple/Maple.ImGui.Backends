using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Maple.UnmanagedExtensions;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::GetPrivateData 函数指针 (VTable 索引 34)
    /// 获取私有数据
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_34;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetPrivateData_34(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, Guid*, uint*, void*, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, Guid*, uint*, void*, HRESULT>)ptr;
        public const string Name = "GetPrivateData";
        /// <summary>
        /// 获取私有数据
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="guid">数据 GUID</param>
        /// <param name="pDataSize">数据大小</param>
        /// <param name="pData">返回数据</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            Guid* guid,
            uint* pDataSize,
            void* pData) => _proc(pThis, guid, pDataSize, pData);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
