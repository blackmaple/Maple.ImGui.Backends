using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CheckCounterInfo 函数指针 (VTable 索引 31)
    /// 检查计数器信息
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_INFO*, void> CheckCounterInfo_31;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CheckCounterInfo_31(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_COUNTER_INFO*, void>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_COUNTER_INFO*, void>)ptr;
        public const string Name = "CheckCounterInfo";
        /// <summary>
        /// 检查计数器信息
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pCounterInfo">返回计数器信息</param>
        public void Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            D3D11_COUNTER_INFO* pCounterInfo) => _proc(pThis, pCounterInfo);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
