using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    /// <summary>
    /// 封装 ID3D11DeviceContext::OMSetRenderTargets 函数指针 (VTable 索引 33)
    /// 设置输出合并阶段的渲染目标和深度模板视图。
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void*, void> OMSetRenderTargets_33;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_OMSetRenderTargets_33(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, COM_PTR_IUNKNOWN, void>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, COM_PTR_IUNKNOWN, void>)ptr;
        public const string Name = "OMSetRenderTargets";
        public void Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, ReadOnlySpan<COM_PTR_IUNKNOWN> pRenderTargetViews, COM_PTR_IUNKNOWN pDepthStencilView)
        {
            _proc(pThis, (uint)pRenderTargetViews.Length, UnsafeRef<COM_PTR_IUNKNOWN>.FromRef(ref MemoryMarshal.GetReference(pRenderTargetViews)), pDepthStencilView);
        }
        /// <summary>
        /// 设置输出合并阶段的渲染目标和深度模板视图。
        /// </summary>
        /// <param name="pThis">ID3D11DeviceContext 接口指针</param>
        /// <param name="numViews">渲染目标视图数量</param>
        /// <param name="ppRenderTargetViews">渲染目标视图指针数组</param>
        /// <param name="pDepthStencilView">深度模板视图指针</param>
        public void Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, uint numViews = default, UnsafePtr ppRenderTargetViews = default, COM_PTR_IUNKNOWN pDepthStencilView = default)
        {
            _proc(pThis, numViews, new UnsafeRef<COM_PTR_IUNKNOWN>((nint)ppRenderTargetViews), pDepthStencilView);
        }
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
