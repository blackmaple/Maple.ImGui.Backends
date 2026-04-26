using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Texture2D;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Resource
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D11ResourceImp
    {
        public static readonly Guid GUID = new("DC8E63F3-D12B-4952-B47B-5E45026A862D");

        internal readonly Ptr_Func_GetDevice_3 GetDevice_3;
        internal readonly Ptr_Func_GetPrivateData_4 GetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateData_5 SetPrivateData_5;
        internal readonly Ptr_Func_SetPrivateDataInterface_6 SetPrivateDataInterface_6;
        internal readonly Ptr_Func_GetType_7 GetType_7;
        internal readonly Ptr_Func_SetEvictionPriority_8 SetEvictionPriority_8;
        internal readonly Ptr_Func_GetEvictionPriority_9 GetEvictionPriority_9;
    }

    public static class ID3D11ResourceImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D11ResourceImp> @this)
        {
            public COM_HRESULT QueryInterface<T>(in Guid riid, out COM_PTR_IUNKNOWN<T> pObject) where T : unmanaged
                => @this.IUnknown_VTable.QueryInterface_0.Invoke(@this, in riid, out pObject);

            public COM_HRESULT TryGetID3D11Texture2D(out COM_PTR_IUNKNOWN<ID3D11Texture2DImp> pResource)
            {
                return @this.QueryInterface(ID3D11Texture2DImp.GUID, out pResource);
            }
        }

        public static COM_PTR_IUNKNOWN<ID3D11ResourceImp> Create(nint nativeTexturePtr)
        {
            return new COM_PTR_IUNKNOWN<ID3D11ResourceImp>(nativeTexturePtr);
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
     */
}
