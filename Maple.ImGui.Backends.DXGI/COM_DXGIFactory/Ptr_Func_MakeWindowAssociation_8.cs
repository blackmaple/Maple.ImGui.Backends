using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIFactory
{
    /// <summary>
    /// 封装 IDXGIFactory::MakeWindowAssociation 函数指针 (VTable 索引 8)
    /// 建立窗口关联
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_MakeWindowAssociation_8(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, void*, DXGI_MWA_FLAGS, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, void*, DXGI_MWA_FLAGS, int>)ptr;

        public const string Name = "MakeWindowAssociation";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pThis, void* WindowHandle, DXGI_MWA_FLAGS Flags) => _proc(pThis, WindowHandle, Flags);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
