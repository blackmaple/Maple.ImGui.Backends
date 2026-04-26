using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    /// <summary>
    /// 封装 ID3D11DeviceContext::GetDevice 函数指针 (VTable 索引 3)
    /// 获取创建该设备上下文的设备。
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GetDevice_3;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetDevice_3(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceImp>>, void>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceImp>>, void>)ptr;
        public const string Name = "GetDevice";
        /// <summary>
        /// 获取创建该设备上下文的设备。
        /// </summary>
        /// <param name="pThis">ID3D11DeviceContext 接口指针</param>
        /// <param name="ppDevice">接收 ID3D11Device 接口指针的指针</param>
        public void Invoke(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pThis, out COM_PTR_IUNKNOWN<ID3D11DeviceImp> ppDevice)
            => _proc(pThis, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceImp>>.FromOut(out ppDevice));
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
