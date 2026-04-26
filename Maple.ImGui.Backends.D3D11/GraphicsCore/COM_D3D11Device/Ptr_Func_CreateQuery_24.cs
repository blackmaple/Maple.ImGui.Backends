using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateQuery 函数指针 (VTable 索引 24)
    /// 创建查询
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_QUERY_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Query_unmanaged**, int> CreateQuery_24;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateQuery_24(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_QUERY_DESC>, UnsafeOut<UnsafePtr>, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, UnsafeIn<D3D11_QUERY_DESC>, UnsafeOut<UnsafePtr>, HRESULT>)ptr;
        public const string Name = "CreateQuery";
        /// <summary>
        /// 创建查询
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pQueryDesc">查询描述</param>
        /// <param name="ppQuery">接收 ID3D11Query 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            in D3D11_QUERY_DESC pQueryDesc,
            UnsafeOut<UnsafePtr> ppQuery) => _proc(
                pThis,
                UnsafeIn<D3D11_QUERY_DESC>.FromIn(in pQueryDesc),
                ppQuery);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
