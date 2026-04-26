using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11ShaderResourceView
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D11ShaderResourceViewImp
    {
        public static readonly Guid GUID = new("B0E06FE0-8192-4E1A-9BFD-042170F898D5");

        internal readonly Ptr_Func_GetDevice_3 GetDevice_3;
        internal readonly Ptr_Func_GetPrivateData_4 GetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateData_5 SetPrivateData_5;
        internal readonly Ptr_Func_SetPrivateDataInterface_6 SetPrivateDataInterface_6;
        internal readonly Ptr_Func_GetResource_7 GetResource_7;
        internal readonly Ptr_Func_GetDesc_8 GetDesc_8;
    }

    public static class ID3D11ShaderResourceViewImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp> @this)
        {
           
        }
 
    }
    /*
    public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GetDevice_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GetResource_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SHADER_RESOURCE_VIEW_DESC*, void> GetDesc_8;

     */
}
