using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIFactory
{
    /// <summary>
    /// 封装 IDXGIFactory::SetPrivateDataInterface 函数指针 (VTable 索引 4)
    /// 设置与接口关联的私有接口
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_SetPrivateDataInterface_4(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, global::System.Guid*, void*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, global::System.Guid*, void*, int>)ptr;

        public const string Name = "SetPrivateDataInterface";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pThis, global::System.Guid* Name, void* pUnknown) => _proc(pThis, Name, pUnknown);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
