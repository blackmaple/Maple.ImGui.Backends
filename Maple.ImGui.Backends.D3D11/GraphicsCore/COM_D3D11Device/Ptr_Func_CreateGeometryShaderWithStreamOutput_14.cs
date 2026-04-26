using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateGeometryShaderWithStreamOutput 函数指针 (VTable 索引 14)
    /// 创建带流输出的几何着色器
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, global::Windows.Win32.Graphics.Direct3D11.D3D11_SO_DECLARATION_ENTRY*, uint, uint*, uint, uint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11GeometryShader_unmanaged**, int> CreateGeometryShaderWithStreamOutput_14;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateGeometryShaderWithStreamOutput_14(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, void*, nuint, D3D11_SO_DECLARATION_ENTRY*, uint, uint*, uint, uint, void*, UnsafeOut<UnsafePtr>, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, void*, nuint, D3D11_SO_DECLARATION_ENTRY*, uint, uint*, uint, uint, void*, UnsafeOut<UnsafePtr>, HRESULT>)ptr;
        public const string Name = "CreateGeometryShaderWithStreamOutput";
        /// <summary>
        /// 创建带流输出的几何着色器
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pShaderBytecode">着色器字节码</param>
        /// <param name="BytecodeLength">字节码长度</param>
        /// <param name="pSODeclaration">流输出声明</param>
        /// <param name="NumEntries">条目数量</param>
        /// <param name="pBufferStrides">缓冲区跨度</param>
        /// <param name="NumStrides">跨度数量</param>
        /// <param name="RasterizedStream">光栅化流</param>
        /// <param name="pClassLinkage">类链接</param>
        /// <param name="ppGeometryShader">接收 ID3D11GeometryShader 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            void* pShaderBytecode,
            nuint BytecodeLength,
            D3D11_SO_DECLARATION_ENTRY* pSODeclaration,
            uint NumEntries,
            uint* pBufferStrides,
            uint NumStrides,
            uint RasterizedStream,
            void* pClassLinkage,
            UnsafeOut<UnsafePtr> ppGeometryShader) => _proc(
                pThis,
                pShaderBytecode,
                BytecodeLength,
                pSODeclaration,
                NumEntries,
                pBufferStrides,
                NumStrides,
                RasterizedStream,
                pClassLinkage,
                ppGeometryShader);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
