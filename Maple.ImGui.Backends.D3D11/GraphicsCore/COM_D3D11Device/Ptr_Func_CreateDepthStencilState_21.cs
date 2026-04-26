using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateDepthStencilState 函数指针 (VTable 索引 21)
    /// 创建深度模板状态
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_DEPTH_STENCIL_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DepthStencilState_unmanaged**, int> CreateDepthStencilState_21;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateDepthStencilState_21(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_DEPTH_STENCIL_DESC>, UnsafeOut<UnsafePtr>, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_DEPTH_STENCIL_DESC>, UnsafeOut<UnsafePtr>, HRESULT>)ptr;
        public const string Name = "CreateDepthStencilState";
        /// <summary>
        /// 创建深度模板状态
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pDepthStencilDesc">深度模板描述</param>
        /// <param name="ppDepthStencilState">接收 ID3D11DepthStencilState 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            in D3D11_DEPTH_STENCIL_DESC pDepthStencilDesc,
            UnsafeOut<UnsafePtr> ppDepthStencilState) => _proc(
                pThis,
                UnsafeIn<D3D11_DEPTH_STENCIL_DESC>.FromIn(in pDepthStencilDesc),
                ppDepthStencilState);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
