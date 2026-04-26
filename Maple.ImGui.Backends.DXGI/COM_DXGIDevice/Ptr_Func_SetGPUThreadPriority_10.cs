using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{
    /// <summary>
    /// 封装 IDXGIDevice::SetGPUThreadPriority 函数指针 (VTable 索引 10)
    /// 设置 GPU 线程优先级
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_SetGPUThreadPriority_10(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, int, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, int, int>)ptr;

        public const string Name = "SetGPUThreadPriority";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pThis, int Priority) => _proc(pThis, Priority);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
