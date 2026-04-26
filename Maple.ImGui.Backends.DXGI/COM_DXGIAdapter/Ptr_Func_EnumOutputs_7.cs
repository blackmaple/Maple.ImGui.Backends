using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIAdapter
{
    /// <summary>
    /// 封装 IDXGIAdapter::EnumOutputs 函数指针 (VTable 索引 7)
    /// 枚举适配器的输出（显示器）
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, void**, int> EnumOutputs_7;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_EnumOutputs_7(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, uint, void**, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIAdapterImp>, uint, void**, int>)ptr;

        public const string Name = "EnumOutputs";

        /// <summary>
        /// 枚举适配器的输出（显示器）
        /// </summary>
        /// <param name="pThis">IDXGIAdapter 接口指针</param>
        /// <param name="Output">输出索引</param>
        /// <param name="ppOutput">接收 IDXGIOutput 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public int Invoke(COM_PTR_IUNKNOWN<IDXGIAdapterImp> pThis, uint Output, void** ppOutput) => _proc(pThis, Output, ppOutput);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
