using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11ShaderResourceView
{
    /// <summary>
    /// Wraps the ID3D11ShaderResourceViewImp::GetDesc function pointer (VTable slot 8).
    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SHADER_RESOURCE_VIEW_DESC*, void> GetDesc_8;
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_GetDesc_8(nint ptr) : Hook.Abstractions.IHookMethod
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp>, global::Windows.Win32.Graphics.Direct3D11.D3D11_SHADER_RESOURCE_VIEW_DESC*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp>, global::Windows.Win32.Graphics.Direct3D11.D3D11_SHADER_RESOURCE_VIEW_DESC*, void>)ptr;
        public const string Name = "GetDesc";
        /// <summary>
        /// Invokes ID3D11ShaderResourceViewImp::GetDesc.
        /// </summary>
        /// <param name="pThis">ID3D11ShaderResourceViewImp interface pointer.</param>
        /// <param name="arg1">Argument 1.</param>
        public void Invoke(COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp> pThis, global::Windows.Win32.Graphics.Direct3D11.D3D11_SHADER_RESOURCE_VIEW_DESC* arg1) => _proc(pThis, arg1);
        public nint PtrMethod => new(_proc);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
