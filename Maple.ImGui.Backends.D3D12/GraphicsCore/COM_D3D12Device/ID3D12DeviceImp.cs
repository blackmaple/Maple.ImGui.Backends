using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D12;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device
{

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12DeviceImp
    {
        public readonly static Guid GUID = new("189819f1-1db6-4b57-be54-1821339b85f7");
        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetNodeCount_7 GetNodeCount_7;
        internal readonly Ptr_Func_CreateCommandQueue_8 CreateCommandQueue_8;
        internal readonly Ptr_Func_CreateCommandAllocator_9 CreateCommandAllocator_9;
        internal readonly Ptr_Func_CreateGraphicsPipelineState_10 CreateGraphicsPipelineState_10;
        internal readonly Ptr_Func_CreateComputePipelineState_11 CreateComputePipelineState_11;
        internal readonly Ptr_Func_CreateCommandList_12 CreateCommandList_12;
        internal readonly Ptr_Func_CheckFeatureSupport_13 CheckFeatureSupport_13;
        internal readonly Ptr_Func_CreateDescriptorHeap_14 CreateDescriptorHeap_14;
        internal readonly Ptr_Func_GetDescriptorHandleIncrementSize_15 GetDescriptorHandleIncrementSize_15;
        internal readonly Ptr_Func_CreateRootSignature_16 CreateRootSignature_16;
        internal readonly Ptr_Func_CreateConstantBufferView_17 CreateConstantBufferView_17;
        internal readonly Ptr_Func_CreateShaderResourceView_18 CreateShaderResourceView_18;
        internal readonly Ptr_Func_CreateUnorderedAccessView_19 CreateUnorderedAccessView_19;
        internal readonly Ptr_Func_CreateRenderTargetView_20 CreateRenderTargetView_20;
        internal readonly Ptr_Func_CreateDepthStencilView_21 CreateDepthStencilView_21;
        internal readonly Ptr_Func_CreateSampler_22 CreateSampler_22;
        internal readonly Ptr_Func_CopyDescriptors_23 CopyDescriptors_23;
        internal readonly Ptr_Func_CopyDescriptorsSimple_24 CopyDescriptorsSimple_24;
        internal readonly Ptr_Func_GetResourceAllocationInfo_25 GetResourceAllocationInfo_25;
        internal readonly Ptr_Func_GetCustomHeapProperties_26 GetCustomHeapProperties_26;
        internal readonly Ptr_Func_CreateCommittedResource_27 CreateCommittedResource_27;
        internal readonly Ptr_Func_CreateHeap_28 CreateHeap_28;
        internal readonly Ptr_Func_CreatePlacedResource_29 CreatePlacedResource_29;
        internal readonly Ptr_Func_CreateReservedResource_30 CreateReservedResource_30;
        internal readonly Ptr_Func_CreateSharedHandle_31 CreateSharedHandle_31;
        internal readonly Ptr_Func_OpenSharedHandle_32 OpenSharedHandle_32;
        internal readonly Ptr_Func_OpenSharedHandleByName_33 OpenSharedHandleByName_33;
        internal readonly Ptr_Func_MakeResident_34 MakeResident_34;
        internal readonly Ptr_Func_Evict_35 Evict_35;
        internal readonly Ptr_Func_CreateFence_36 CreateFence_36;
        internal readonly Ptr_Func_GetDeviceRemovedReason_37 GetDeviceRemovedReason_37;
        internal readonly Ptr_Func_GetCopyableFootprints_38 GetCopyableFootprints_38;
        internal readonly Ptr_Func_CreateQueryHeap_39 CreateQueryHeap_39;
        internal readonly Ptr_Func_SetStablePowerState_40 SetStablePowerState_40;
        internal readonly Ptr_Func_CreateCommandSignature_41 CreateCommandSignature_41;
        internal readonly Ptr_Func_GetResourceTiling_42 GetResourceTiling_42;
        internal readonly Ptr_Func_GetAdapterLuid_43 GetAdapterLuid_43;


    }


    public static class ID3D12DeviceImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D12DeviceImp> @this)
        {

            internal COM_HRESULT CreateCommandQueue(in D3D12_COMMAND_QUEUE_DESC pDesc, in Guid riid, out COM_PTR_IUNKNOWN ppCommandQueue)
            {
                return @this.Interface_VTable.CreateCommandQueue_8.Invoke(@this, in pDesc, in riid, out ppCommandQueue);
            }

            internal COM_HRESULT CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE type, in Guid riid, out COM_PTR_IUNKNOWN ppCommandAllocator)
            {
                return @this.Interface_VTable.CreateCommandAllocator_9.Invoke(@this, type, in riid, out ppCommandAllocator);
            }

            internal COM_HRESULT CreateCommandList(uint nodeMask, D3D12_COMMAND_LIST_TYPE type,
                COM_PTR_IUNKNOWN pCommandAllocator, COM_PTR_IUNKNOWN pInitialCommandList, in Guid riid, out COM_PTR_IUNKNOWN ppCommandList)
            {
                return @this.Interface_VTable.CreateCommandList_12.Invoke(@this, nodeMask, type, pCommandAllocator, pInitialCommandList, in riid, out ppCommandList);
            }


            internal COM_HRESULT CreateDescriptorHeap(D3D12_DESCRIPTOR_HEAP_TYPE type, D3D12_DESCRIPTOR_HEAP_FLAGS flags, uint buffersCounts, uint nodeMask, out COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> ppvHeap)
            {
                D3D12_DESCRIPTOR_HEAP_DESC desc = new()
                {
                    Type = type,//D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV,
                    Flags = flags,// D3D12_DESCRIPTOR_HEAP_FLAGS.D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE,
                    NumDescriptors = buffersCounts,
                    NodeMask = nodeMask
                };
                var hr = @this.Interface_VTable.CreateDescriptorHeap_14.Invoke(@this, in desc, in ID3D12DescriptorHeapImp.GUID, out var ppObject);
                ppvHeap = ppObject.Get<ID3D12DescriptorHeapImp>();
                return hr;
            }
            internal COM_HRESULT CreateDescriptorHeapForSRV(uint buffersCounts, out COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> ppvHeap)
                => @this.CreateDescriptorHeap(D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_CBV_SRV_UAV, D3D12_DESCRIPTOR_HEAP_FLAGS.D3D12_DESCRIPTOR_HEAP_FLAG_SHADER_VISIBLE, buffersCounts, 0, out ppvHeap);
            public COM_HRESULT CreateDirectCommandAllocator(out COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pCommandAllocator)
            {
                var hr = @this.CreateCommandAllocator(D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT, in ID3D12CommandAllocatorImp.GUID, out var ppObject);
                pCommandAllocator = ppObject.Get<ID3D12CommandAllocatorImp>();
                return hr;
            }
            public COM_HRESULT CreateGraphicsCommandList(uint nodeMask, COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pCommandAllocator, out COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pCommandList)
            {
                var hr = @this.CreateCommandList(nodeMask, D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT, pCommandAllocator, default, in ID3D12GraphicsCommandListImp.GUID, out var ppObject);
                pCommandList = ppObject.Get<ID3D12GraphicsCommandListImp>();
                return hr;
            }
            public COM_HRESULT CreateDescriptorHeapForRTV(uint buffersCounts, out COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> ppvHeap)
                => @this.CreateDescriptorHeap(D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_RTV, D3D12_DESCRIPTOR_HEAP_FLAGS.D3D12_DESCRIPTOR_HEAP_FLAG_NONE, buffersCounts, 1, out ppvHeap);

            public uint GetDescriptorHandleIncrementSize() => @this.Interface_VTable.GetDescriptorHandleIncrementSize_15.Invoke(@this, D3D12_DESCRIPTOR_HEAP_TYPE.D3D12_DESCRIPTOR_HEAP_TYPE_RTV);


            void CreateRenderTargetView(COM_PTR_IUNKNOWN pResource, D3D12_CPU_DESCRIPTOR_HANDLE cpuHandle)
            {
                var pdesc_nullptr = new UnsafeIn<D3D12_RENDER_TARGET_VIEW_DESC>(nint.Zero);
                @this.Interface_VTable.CreateRenderTargetView_20.Invoke(@this, pResource, pdesc_nullptr, cpuHandle);
            }

            internal void CreateRenderTargetView(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pResource, D3D12_CPU_DESCRIPTOR_HANDLE rtvHandle)
                => @this.CreateRenderTargetView(pResource, rtvHandle);



            public COM_HRESULT CreateFence(out COM_PTR_IUNKNOWN<ID3D12FenceImp> pFence)
            {
                var hr = @this.Interface_VTable.CreateFence_36.Invoke(@this, 0, D3D12_FENCE_FLAGS.D3D12_FENCE_FLAG_NONE, UnsafeIn<Guid>.FromIn(in ID3D12FenceImp.GUID), UnsafeOut<COM_PTR_IUNKNOWN>.FromOut(out var ppObject));
                pFence = ppObject.Get<ID3D12FenceImp>();
                return hr;
            }



        }
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
