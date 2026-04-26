using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Direct3D12;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap
{
    /*
    public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, int> SetName_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_DESCRIPTOR_HEAP_DESC> GetDesc_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE> GetCPUDescriptorHandleForHeapStart_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_GPU_DESCRIPTOR_HANDLE> GetGPUDescriptorHandleForHeapStart_10;
     
     */

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12DescriptorHeapImp
    {
        public static readonly Guid GUID = new("8EFB471D-616C-4F49-90F7-127BB763FA51");

        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetDevice_7 GetDevice_7;
        internal readonly Ptr_Func_GetDesc_8 GetDesc_8;
        internal readonly Ptr_Func_GetCPUDescriptorHandleForHeapStart_9 GetCPUDescriptorHandleForHeapStart_9;
        internal readonly Ptr_Func_GetGPUDescriptorHandleForHeapStart_10 GetGPUDescriptorHandleForHeapStart_10;
    }

    public static class ID3D12DescriptorHeapImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> @this)
        {
            internal D3D12_CPU_DESCRIPTOR_HANDLE GetCPUDescriptorHandleForHeapStart() => @this.Interface_VTable.GetCPUDescriptorHandleForHeapStart_9.Invoke(@this);
            internal D3D12_GPU_DESCRIPTOR_HANDLE GetGPUDescriptorHandleForHeapStart() => @this.Interface_VTable.GetGPUDescriptorHandleForHeapStart_10.Invoke(@this);
        }
    }
}
