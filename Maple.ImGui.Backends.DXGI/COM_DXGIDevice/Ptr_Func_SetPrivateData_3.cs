using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{
    /// <summary>
    /// 封装 IDXGIDevice::SetPrivateData 函数指针 (VTable 索引 3)
    /// 设置与接口关联的私有数据
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_SetPrivateData_3(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, global::System.Guid*, uint, void*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIDeviceImp>, global::System.Guid*, uint, void*, int>)ptr;

        public const string Name = "SetPrivateData";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pThis, global::System.Guid* Name, uint DataSize, void* pData) => _proc(pThis, Name, DataSize, pData);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
