using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device
{
    /// <summary>
    /// ID3D12Device::GetAdapterLuid
    /// 获取适配器 LUID
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetAdapterLuid_43(nint ptr) : IHookMethod
    {
        public const string Name = "GetAdapterLuid";

        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.LUID> GetAdapterLuid_43;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12DeviceImp>, LUID> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12DeviceImp>, LUID>)ptr;

        public nint PtrMethod => (nint)_proc;
        public unsafe LUID Invoke(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }
    /*
         public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, int> SetName_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetNodeCount_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_COMMAND_QUEUE_DESC*, global::System.Guid*, void**, int> CreateCommandQueue_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_COMMAND_LIST_TYPE, global::System.Guid*, void**, int> CreateCommandAllocator_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_GRAPHICS_PIPELINE_STATE_DESC*, global::System.Guid*, void**, int> CreateGraphicsPipelineState_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_COMPUTE_PIPELINE_STATE_DESC*, global::System.Guid*, void**, int> CreateComputePipelineState_11;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_COMMAND_LIST_TYPE, void*, void*, global::System.Guid*, void**, int> CreateCommandList_12;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_FEATURE, void*, uint, int> CheckFeatureSupport_13;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_DESCRIPTOR_HEAP_DESC*, global::System.Guid*, void**, int> CreateDescriptorHeap_14;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_DESCRIPTOR_HEAP_TYPE, uint> GetDescriptorHandleIncrementSize_15;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, void*, nuint, global::System.Guid*, void**, int> CreateRootSignature_16;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CONSTANT_BUFFER_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void> CreateConstantBufferView_17;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_SHADER_RESOURCE_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void> CreateShaderResourceView_18;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_UNORDERED_ACCESS_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void> CreateUnorderedAccessView_19;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RENDER_TARGET_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void> CreateRenderTargetView_20;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D12.D3D12_DEPTH_STENCIL_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void> CreateDepthStencilView_21;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_SAMPLER_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, void> CreateSampler_22;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE*, uint*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE*, uint*, global::Windows.Win32.Graphics.Direct3D12.D3D12_DESCRIPTOR_HEAP_TYPE, void> CopyDescriptors_23;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, global::Windows.Win32.Graphics.Direct3D12.D3D12_CPU_DESCRIPTOR_HANDLE, global::Windows.Win32.Graphics.Direct3D12.D3D12_DESCRIPTOR_HEAP_TYPE, void> CopyDescriptorsSimple_24;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_ALLOCATION_INFO> GetResourceAllocationInfo_25;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_TYPE, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_PROPERTIES> GetCustomHeapProperties_26;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_PROPERTIES*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_FLAGS, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_STATES, global::Windows.Win32.Graphics.Direct3D12.D3D12_CLEAR_VALUE*, global::System.Guid*, void**, int> CreateCommittedResource_27;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_DESC*, global::System.Guid*, void**, int> CreateHeap_28;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, ulong, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_STATES, global::Windows.Win32.Graphics.Direct3D12.D3D12_CLEAR_VALUE*, global::System.Guid*, void**, int> CreatePlacedResource_29;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_STATES, global::Windows.Win32.Graphics.Direct3D12.D3D12_CLEAR_VALUE*, global::System.Guid*, void**, int> CreateReservedResource_30;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Security.SECURITY_ATTRIBUTES*, uint, global::Windows.Win32.Foundation.PCWSTR, global::Windows.Win32.Foundation.HANDLE*, int> CreateSharedHandle_31;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.Guid*, void**, int> OpenSharedHandle_32;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, uint, global::Windows.Win32.Foundation.HANDLE*, int> OpenSharedHandleByName_33;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, int> MakeResident_34;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, int> Evict_35;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong, global::Windows.Win32.Graphics.Direct3D12.D3D12_FENCE_FLAGS, global::System.Guid*, void**, int> CreateFence_36;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> GetDeviceRemovedReason_37;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC*, uint, uint, ulong, global::Windows.Win32.Graphics.Direct3D12.D3D12_PLACED_SUBRESOURCE_FOOTPRINT*, uint*, ulong*, ulong*, void> GetCopyableFootprints_38;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_QUERY_HEAP_DESC*, global::System.Guid*, void**, int> CreateQueryHeap_39;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int, int> SetStablePowerState_40;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_COMMAND_SIGNATURE_DESC*, void*, global::System.Guid*, void**, int> CreateCommandSignature_41;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint*, global::Windows.Win32.Graphics.Direct3D12.D3D12_PACKED_MIP_INFO*, global::Windows.Win32.Graphics.Direct3D12.D3D12_TILE_SHAPE*, uint*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_SUBRESOURCE_TILING*, void> GetResourceTiling_42;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.LUID> GetAdapterLuid_43;

     
     */
}
