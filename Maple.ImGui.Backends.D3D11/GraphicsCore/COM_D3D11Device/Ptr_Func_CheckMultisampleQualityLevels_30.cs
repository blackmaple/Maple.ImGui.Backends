using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CheckMultisampleQualityLevels 函数指针 (VTable 索引 30)
    /// 检查多重采样质量级别
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, uint*, int> CheckMultisampleQualityLevels_30;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CheckMultisampleQualityLevels_30(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, DXGI_FORMAT, uint, uint*, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, DXGI_FORMAT, uint, uint*, HRESULT>)ptr;
        public const string Name = "CheckMultisampleQualityLevels";
        /// <summary>
        /// 检查多重采样质量级别
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="Format">要检查的格式</param>
        /// <param name="SampleCount">采样数</param>
        /// <param name="pNumQualityLevels">返回质量级别数</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            DXGI_FORMAT Format,
            uint SampleCount,
            uint* pNumQualityLevels) => _proc(pThis, Format, SampleCount, pNumQualityLevels);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
