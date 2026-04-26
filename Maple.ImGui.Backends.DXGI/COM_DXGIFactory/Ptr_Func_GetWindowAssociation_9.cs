using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIFactory
{
    /// <summary>
    /// 封装 IDXGIFactory::GetWindowAssociation 函数指针 (VTable 索引 9)
    /// 获取窗口关联
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetWindowAssociation_9(nint ptr): Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, HWND*, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIFactoryImp>, HWND*, int>)ptr;

        public const string Name = "GetWindowAssociation";

        public int Invoke(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pThis, HWND* pWindowHandle) => _proc(pThis, pWindowHandle);

        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
