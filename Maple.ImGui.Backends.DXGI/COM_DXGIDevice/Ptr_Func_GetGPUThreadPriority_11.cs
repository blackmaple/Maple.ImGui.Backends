using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{
    /// <summary>
    /// 封装 IDXGIDevice::GetGPUThreadPriority 函数指针 (VTable 索引 11)
    /// 获取 GPU 线程优先级
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetGPUThreadPriority_11(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, int*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, int*, int>)ptr;

        public const string Name = "GetGPUThreadPriority";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pThis, int* pPriority) => _proc(pThis, pPriority);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
