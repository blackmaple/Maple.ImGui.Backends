using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIFactory
{
    /// <summary>
    /// 封装 IDXGIFactory::CreateSoftwareAdapter 函数指针 (VTable 索引 11)
    /// 创建软件适配器
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateSoftwareAdapter_11(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, void*, void**, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, void*, void**, int>)ptr;

        public const string Name = "CreateSoftwareAdapter";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pThis, void* Module, void** ppAdapter) => _proc(pThis, Module, ppAdapter);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
