using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{
    /// <summary>
    /// 封装 IDXGIDevice::QueryResourceResidency 函数指针 (VTable 索引 9)
    /// 查询资源的驻留状态
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_QueryResourceResidency_9(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, global::System.IntPtr*, int*, uint, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, global::System.IntPtr*, int*, uint, int>)ptr;

        public const string Name = "QueryResourceResidency";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pThis, global::System.IntPtr* ppResources, int* pResidencyStatus, uint NumResources) => _proc(pThis, ppResources, pResidencyStatus, NumResources);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
