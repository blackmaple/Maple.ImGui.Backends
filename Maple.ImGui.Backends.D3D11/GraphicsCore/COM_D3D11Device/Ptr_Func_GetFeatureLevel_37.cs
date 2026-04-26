using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::GetFeatureLevel 函数指针 (VTable 索引 37)
    /// 获取特性级别
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_FEATURE_LEVEL> GetFeatureLevel_37;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetFeatureLevel_37(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D_FEATURE_LEVEL>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D_FEATURE_LEVEL>)ptr;
        public const string Name = "GetFeatureLevel";
        /// <summary>
        /// 获取特性级别
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <returns>D3D_FEATURE_LEVEL</returns>
        public D3D_FEATURE_LEVEL Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis) => _proc(pThis);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
