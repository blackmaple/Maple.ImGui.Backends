using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateDeferredContext 函数指针 (VTable 索引 27)
    /// 创建延迟上下文
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D11.ID3D11DeviceContext_unmanaged**, int> CreateDeferredContext_27;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateDeferredContext_27(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, uint, UnsafeOut<UnsafePtr>, COM_HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, uint, UnsafeOut<UnsafePtr>, COM_HRESULT>)ptr;
        public const string Name = "CreateDeferredContext";
        /// <summary>
        /// 创建延迟上下文
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="ContextFlags">上下文标志</param>
        /// <param name="ppDeferredContext">接收 ID3D11DeviceContext 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            uint ContextFlags,
            UnsafeOut<UnsafePtr> ppDeferredContext) => _proc(pThis, ContextFlags, ppDeferredContext);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
