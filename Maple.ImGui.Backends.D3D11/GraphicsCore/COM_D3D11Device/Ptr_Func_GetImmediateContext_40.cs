using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::GetImmediateContext 函数指针 (VTable 索引 40)
    /// 获取立即上下文
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GetImmediateContext_40;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetImmediateContext_40(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>>, void>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>>, void>)ptr;
        public const string Name = "GetImmediateContext";
        /// <summary>
        /// 获取立即上下文
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="ppImmediateContext">接收 ID3D11DeviceContext 接口指针的指针</param>
        public void Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            out COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> ppImmediateContext) => _proc(pThis, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>>.FromOut(out ppImmediateContext));
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
