using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CheckFeatureSupport 函数指针 (VTable 索引 33)
    /// 检查特性支持
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_FEATURE, void*, uint, int> CheckFeatureSupport_33;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CheckFeatureSupport_33(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_FEATURE, void*, uint, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_FEATURE, void*, uint, HRESULT>)ptr;
        public const string Name = "CheckFeatureSupport";
        /// <summary>
        /// 检查特性支持
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="Feature">要查询的特性</param>
        /// <param name="pFeatureSupportData">返回特性支持数据</param>
        /// <param name="FeatureSupportDataSize">数据大小</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            D3D11_FEATURE Feature,
            void* pFeatureSupportData,
            uint FeatureSupportDataSize) => _proc(pThis, Feature, pFeatureSupportData, FeatureSupportDataSize);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
