using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CheckCounter 函数指针 (VTable 索引 32)
    /// 检查计数器
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, int> CheckCounter_32;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CheckCounter_32(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_COUNTER_DESC*, D3D11_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_COUNTER_DESC*, D3D11_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, HRESULT>)ptr;
        public const string Name = "CheckCounter";
        /// <summary>
        /// 检查计数器
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pDesc">计数器描述</param>
        /// <param name="pType">返回计数器类型</param>
        /// <param name="pActiveCounters">返回活动计数器</param>
        /// <param name="szName">返回名称</param>
        /// <param name="pNameLength">名称长度</param>
        /// <param name="szUnits">返回单位</param>
        /// <param name="pUnitsLength">单位长度</param>
        /// <param name="szDescription">返回描述</param>
        /// <param name="pDescriptionLength">描述长度</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            D3D11_COUNTER_DESC* pDesc,
            D3D11_COUNTER_TYPE* pType,
            uint* pActiveCounters,
            byte* szName,
            uint* pNameLength,
            byte* szUnits,
            uint* pUnitsLength,
            byte* szDescription,
            uint* pDescriptionLength) => _proc(
                pThis,
                pDesc,
                pType,
                pActiveCounters,
                szName,
                pNameLength,
                szUnits,
                pUnitsLength,
                szDescription,
                pDescriptionLength);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
