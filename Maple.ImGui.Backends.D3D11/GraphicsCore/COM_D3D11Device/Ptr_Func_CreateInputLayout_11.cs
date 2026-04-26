using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    /// <summary>
    /// 封装 ID3D11Device::CreateInputLayout 函数指针 (VTable 索引 11)
    /// 创建输入布局
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_INPUT_ELEMENT_DESC*, uint, void*, nuint, global::Windows.Win32.Graphics.Direct3D11.ID3D11InputLayout_unmanaged**, int> CreateInputLayout_11;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateInputLayout_11(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_INPUT_ELEMENT_DESC*, uint, void*, nuint, UnsafeOut<UnsafePtr>, HRESULT>
            _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11DeviceImp>, D3D11_INPUT_ELEMENT_DESC*, uint, void*, nuint, UnsafeOut<UnsafePtr>, HRESULT>)ptr;
        public const string Name = "CreateInputLayout";
        /// <summary>
        /// 创建输入布局
        /// </summary>
        /// <param name="pThis">ID3D11Device 接口指针</param>
        /// <param name="pInputElementDescs">输入元素描述数组</param>
        /// <param name="NumElements">元素数量</param>
        /// <param name="pShaderBytecodeWithInputSignature">着色器字节码</param>
        /// <param name="BytecodeLength">字节码长度</param>
        /// <param name="ppInputLayout">接收 ID3D11InputLayout 接口指针的指针</param>
        /// <returns>HRESULT</returns>
        public HRESULT Invoke(
            COM_PTR_IUNKNOWN<ID3D11DeviceImp> pThis,
            D3D11_INPUT_ELEMENT_DESC* pInputElementDescs,
            uint NumElements,
            void* pShaderBytecodeWithInputSignature,
            nuint BytecodeLength,
            UnsafeOut<UnsafePtr> ppInputLayout) => _proc(
                pThis,
                pInputElementDescs,
                NumElements,
                pShaderBytecodeWithInputSignature,
                BytecodeLength,
                ppInputLayout);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
