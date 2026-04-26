using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Texture2D
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D11Texture2DImp
    {
        public static readonly Guid GUID = new("6F15AAF2-D208-4E89-9AB4-489535D34F9C");

        internal readonly Ptr_Func_GetDevice_3 GetDevice_3;
        internal readonly Ptr_Func_GetPrivateData_4 GetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateData_5 SetPrivateData_5;
        internal readonly Ptr_Func_SetPrivateDataInterface_6 SetPrivateDataInterface_6;
        internal readonly Ptr_Func_GetType_7 GetType_7;
        internal readonly Ptr_Func_SetEvictionPriority_8 SetEvictionPriority_8;
        internal readonly Ptr_Func_GetEvictionPriority_9 GetEvictionPriority_9;
        internal readonly Ptr_Func_GetDesc_10 GetDesc_10;
    }

    public static class ID3D11Texture2DImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D11Texture2DImp> @this)
        {
            internal void GetDesc(out D3D11_TEXTURE2D_DESC pDesc)
            {
                @this.Interface_VTable.GetDesc_10.Invoke(@this, out pDesc);
            }
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
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_RESOURCE_DIMENSION*, void> GetType_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, void> SetEvictionPriority_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetEvictionPriority_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_TEXTURE2D_DESC*, void> GetDesc_10;
     */
}
