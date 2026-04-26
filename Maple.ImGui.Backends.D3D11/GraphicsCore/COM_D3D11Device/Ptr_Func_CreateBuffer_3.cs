using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateBuffer 函数指针 (VTable 索引 3)
    /// 创建缓冲区
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_BUFFER_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SUBRESOURCE_DATA*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Buffer_unmanaged**, int> CreateBuffer_3;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateBuffer_3(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_BUFFER_DESC>, UnsafeIn<D3D11_SUBRESOURCE_DATA>, UnsafeOut<UnsafePtr>, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_BUFFER_DESC>, UnsafeIn<D3D11_SUBRESOURCE_DATA>, UnsafeOut<UnsafePtr>, HRESULT>)ptr;
        public const string Name = "CreateBuffer";
        /// <summary>
        /// 创建缓冲区
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pDesc">缓冲区描述</param>
        /// <param name="pInitialData">初始数据</param>
        /// <param name="ppBuffer">接收 ID3D11Buffer 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            in D3D11_BUFFER_DESC pDesc,
            in D3D11_SUBRESOURCE_DATA pInitialData,
            UnsafeOut<UnsafePtr> ppBuffer) => _proc(
                pThis,
                UnsafeIn<D3D11_BUFFER_DESC>.FromIn(in pDesc),
                UnsafeIn<D3D11_SUBRESOURCE_DATA>.FromIn(in pInitialData),
                ppBuffer);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
