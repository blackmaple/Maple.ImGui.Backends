using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11RenderTargetView;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateRenderTargetView 函数指针 (VTable 索引 9)
    /// 创建渲染目标视图
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D11.D3D11_RENDER_TARGET_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11RenderTargetView_unmanaged**, int> CreateRenderTargetView_9;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateRenderTargetView_9(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, COM_PTR_IUNKNOWN, UnsafeIn<D3D11_RENDER_TARGET_VIEW_DESC>, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp>>, COM_HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, COM_PTR_IUNKNOWN, UnsafeIn<D3D11_RENDER_TARGET_VIEW_DESC>, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp>>, COM_HRESULT>)ptr;
        public const string Name = "CreateRenderTargetView";
        /// <summary>
        /// 创建渲染目标视图
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pResource">资源指针</param>
        /// <param name="pDesc">视图描述</param>
        /// <param name="ppRTView">接收 ID3D11RenderTargetView 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(
    COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
    COM_PTR_IUNKNOWN pResource,
    UnsafeIn<D3D11_RENDER_TARGET_VIEW_DESC> pDesc,
    UnsafeOut<COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp>> ppRTView) =>
            _proc(pThis, pResource, pDesc, ppRTView);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
