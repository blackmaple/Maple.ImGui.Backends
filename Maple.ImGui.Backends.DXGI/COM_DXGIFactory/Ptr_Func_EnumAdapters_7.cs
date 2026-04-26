using Maple.ImGui.Backends.DXGI.COM_DXGIAdapter;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIFactory
{
    /// <summary>
    /// 封装 IDXGIFactory::EnumAdapters 函数指针 (VTable 索引 7)
    /// 枚举适配器
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_EnumAdapters_7(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, uint, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, uint, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>, COM_HRESULT>)ptr;

        public const string Name = "EnumAdapters";

        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pThis, uint Adapter, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>> ppAdapter)
            => _proc(pThis, Adapter, ppAdapter);
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pThis, uint Adapter, out COM_PTR_IUNKNOWN<IDXGIAdapterImp> pAdapter)
            => _proc(pThis, Adapter, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>.FromOut(out pAdapter));

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
