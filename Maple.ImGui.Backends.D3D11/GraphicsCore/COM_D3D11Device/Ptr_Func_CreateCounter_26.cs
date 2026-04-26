using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateCounter 函数指针 (VTable 索引 26)
    /// 创建计数器
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Counter_unmanaged**, int> CreateCounter_26;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateCounter_26(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_COUNTER_DESC>, UnsafeOut<UnsafePtr>, COM_HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_COUNTER_DESC>, UnsafeOut<UnsafePtr>, COM_HRESULT>)ptr;
        public const string Name = "CreateCounter";
        /// <summary>
        /// 创建计数器
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pCounterDesc">计数器描述</param>
        /// <param name="ppCounter">接收 ID3D11Counter 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public COM_HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            in D3D11_COUNTER_DESC pCounterDesc,
            UnsafeOut<UnsafePtr> ppCounter) => _proc(
                pThis,
                UnsafeIn<D3D11_COUNTER_DESC>.FromIn(in pCounterDesc),
                ppCounter);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
