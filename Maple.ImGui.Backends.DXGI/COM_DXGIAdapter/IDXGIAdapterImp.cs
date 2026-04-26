using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIAdapter
{

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGIAdapterImp
    {
        public static readonly Guid GUID = new("2411E7E1-12AC-4CCF-BD14-9798E8534DC0");

        internal readonly Ptr_Func_SetPrivateData_3 SetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateDataInterface_4 SetPrivateDataInterface_4;
        internal readonly Ptr_Func_GetPrivateData_5 GetPrivateData_5;
        internal readonly Ptr_Func_GetParent_6 GetParent_6;
        internal readonly Ptr_Func_EnumOutputs_7 EnumOutputs_7;
        internal readonly Ptr_Func_GetDesc_8 GetDesc_8;
        internal readonly Ptr_Func_CheckInterfaceSupport_9 CheckInterfaceSupport_9;
    }

    public static class IDXGIAdapterImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGIAdapterImp> @this)
        {
            public COM_HRESULT GetParent<T>(in Guid guid, out COM_PTR_IUNKNOWN<T> pDXGIFactory)
                where T : unmanaged
            {
                var hResult = @this.Interface_VTable.GetParent_6.Invoke(@this, in guid, out var ppParent);
                pDXGIFactory = ppParent.Get<T>();
                return new COM_HRESULT(hResult);
            }
        }
    }

    /*
         public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetParent_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, void**, int> EnumOutputs_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_ADAPTER_DESC*, int> GetDesc_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, long*, int> CheckInterfaceSupport_9;

     */
}
