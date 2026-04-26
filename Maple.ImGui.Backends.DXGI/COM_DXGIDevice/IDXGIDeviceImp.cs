using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Maple.ImGui.Backends.DXGI.COM_DXGIAdapter;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIDevice
{

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGIDeviceImp
    {

        public static readonly Guid GUID = new("54EC77FA-1377-44E6-8C32-88FD5F44C84C");

        internal readonly Ptr_Func_SetPrivateData_3 SetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateDataInterface_4 SetPrivateDataInterface_4;
        internal readonly Ptr_Func_GetPrivateData_5 GetPrivateData_5;
        internal readonly Ptr_Func_GetParent_6 GetParent_6;
        internal readonly Ptr_Func_GetAdapter_7 GetAdapter_7;
        internal readonly Ptr_Func_CreateSurface_8 CreateSurface_8;
        internal readonly Ptr_Func_QueryResourceResidency_9 QueryResourceResidency_9;
        internal readonly Ptr_Func_SetGPUThreadPriority_10 SetGPUThreadPriority_10;
        internal readonly Ptr_Func_GetGPUThreadPriority_11 GetGPUThreadPriority_11;



    }

    public static class IDXGIDeviceImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGIDeviceImp> @this)
        {
            public COM_HRESULT GetAdapter(out COM_PTR_IUNKNOWN<IDXGIAdapterImp> pAdapter)
            {
                return @this.Interface_VTable.GetAdapter_7.Invoke(@this, out pAdapter);
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
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetAdapter_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SURFACE_DESC*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_USAGE, global::Windows.Win32.Graphics.Dxgi.DXGI_SHARED_RESOURCE*, global::System.IntPtr*, int> CreateSurface_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.IntPtr*, global::Windows.Win32.Graphics.Dxgi.DXGI_RESIDENCY*, uint, int> QueryResourceResidency_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int, int> SetGPUThreadPriority_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int*, int> GetGPUThreadPriority_11;

     */
}
