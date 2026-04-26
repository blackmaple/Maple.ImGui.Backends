using Maple.ImGui.Backends.DXGI.COM_DXGIAdapter;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIFactory
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGIFactoryImp
    {
        public readonly static Guid GUID = new("7B7166EC-21C7-44AE-B21A-C9AE321AE369");
        internal readonly Ptr_Func_SetPrivateData_3 SetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateDataInterface_4 SetPrivateDataInterface_4;
        internal readonly Ptr_Func_GetPrivateData_5 GetPrivateData_5;
        internal readonly Ptr_Func_GetParent_6 GetParent_6;
        internal readonly Ptr_Func_EnumAdapters_7 EnumAdapters_7;
        internal readonly Ptr_Func_MakeWindowAssociation_8 MakeWindowAssociation_8;
        internal readonly Ptr_Func_GetWindowAssociation_9 GetWindowAssociation_9;
        internal readonly Ptr_Func_CreateSwapChain_10 CreateSwapChain_10;
        internal readonly Ptr_Func_CreateSoftwareAdapter_11 CreateSoftwareAdapter_11;
    }
    public static class IDXGIFactoryImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGIFactoryImp> @this)
        {
            internal COM_HRESULT CreateSwapChain(
            COM_PTR_IUNKNOWN pDevice,
            in DXGI_SWAP_CHAIN_DESC pDesc,
            out COM_PTR_IUNKNOWN<IDXGISwapChainImp> ppSwapChain)
            {
                return @this.Interface_VTable.CreateSwapChain_10.Invoke(@this, pDevice, in pDesc, out ppSwapChain);
            }

            internal COM_HRESULT EnumAdapters(uint index, out COM_PTR_IUNKNOWN<IDXGIAdapterImp> pAdapter)
            {
                return @this.Interface_VTable.EnumAdapters_7.Invoke(@this, index, out pAdapter);
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
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, void**, int> EnumAdapters_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Dxgi.DXGI_MWA_FLAGS, int> MakeWindowAssociation_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.HWND*, int> GetWindowAssociation_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC*, void**, int> CreateSwapChain_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void**, int> CreateSoftwareAdapter_11;

     */
}
