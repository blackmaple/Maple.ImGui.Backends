using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreatePixelShader 函数指针 (VTable 索引 15)
    /// 创建像素着色器
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11PixelShader_unmanaged**, int> CreatePixelShader_15;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreatePixelShader_15(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, void*, nuint, void*, UnsafeOut<UnsafePtr>, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, void*, nuint, void*, UnsafeOut<UnsafePtr>, HRESULT>)ptr;
        public const string Name = "CreatePixelShader";
        /// <summary>
        /// 创建像素着色器
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pShaderBytecode">着色器字节码</param>
        /// <param name="BytecodeLength">字节码长度</param>
        /// <param name="pClassLinkage">类链接</param>
        /// <param name="ppPixelShader">接收 ID3D11PixelShader 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            void* pShaderBytecode,
            nuint BytecodeLength,
            void* pClassLinkage,
            UnsafeOut<UnsafePtr> ppPixelShader) => _proc(
                pThis,
                pShaderBytecode,
                BytecodeLength,
                pClassLinkage,
                ppPixelShader);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
