using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D12;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12CommandListImp
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12GraphicsCommandListImp
    {
        //  public readonly string s = nameof(ID3D12GraphicsCommandList.SetGraphicsRootUnorderedAccessView );
        public readonly static Guid GUID = new("5b160d0f-ac1b-4185-8ba8-b3ae42a5a455");



        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetDevice_7 GetDevice_7;
        internal readonly Ptr_Func_GetType_8 GetType_8;
        internal readonly Ptr_Func_Close_9 Close_9;
        internal readonly Ptr_Func_Reset_10 Reset_10;
        internal readonly Ptr_Func_ClearState_11 ClearState_11;
        internal readonly Ptr_Func_DrawInstanced_12 DrawInstanced_12;
        internal readonly Ptr_Func_DrawIndexedInstanced_13 DrawIndexedInstanced_13;
        internal readonly Ptr_Func_Dispatch_14 Dispatch_14;
        internal readonly Ptr_Func_CopyBufferRegion_15 CopyBufferRegion_15;
        internal readonly Ptr_Func_CopyTextureRegion_16 CopyTextureRegion_16;
        internal readonly Ptr_Func_CopyResource_17 CopyResource_17;
        internal readonly Ptr_Func_CopyTiles_18 CopyTiles_18;
        internal readonly Ptr_Func_ResolveSubresource_19 ResolveSubresource_19;
        internal readonly Ptr_Func_IASetPrimitiveTopology_20 IASetPrimitiveTopology_20;
        internal readonly Ptr_Func_RSSetViewports_21 RSSetViewports_21;
        internal readonly Ptr_Func_RSSetScissorRects_22 RSSetScissorRects_22;
        internal readonly Ptr_Func_OMSetBlendFactor_23 OMSetBlendFactor_23;
        internal readonly Ptr_Func_OMSetStencilRef_24 OMSetStencilRef_24;
        internal readonly Ptr_Func_SetPipelineState_25 SetPipelineState_25;
        internal readonly Ptr_Func_ResourceBarrier_26 ResourceBarrier_26;
        internal readonly Ptr_Func_ExecuteBundle_27 ExecuteBundle_27;
        internal readonly Ptr_Func_SetDescriptorHeaps_28 SetDescriptorHeaps_28;
        internal readonly Ptr_Func_SetComputeRootSignature_29 SetComputeRootSignature_29;
        internal readonly Ptr_Func_SetGraphicsRootSignature_30 SetGraphicsRootSignature_30;
        internal readonly Ptr_Func_SetComputeRootDescriptorTable_31 SetComputeRootDescriptorTable_31;
        internal readonly Ptr_Func_SetGraphicsRootDescriptorTable_32 SetGraphicsRootDescriptorTable_32;
        internal readonly Ptr_Func_SetComputeRoot32BitConstant_33 SetComputeRoot32BitConstant_33;
        internal readonly Ptr_Func_SetGraphicsRoot32BitConstant_34 SetGraphicsRoot32BitConstant_34;
        internal readonly Ptr_Func_SetComputeRoot32BitConstants_35 SetComputeRoot32BitConstants_35;
        internal readonly Ptr_Func_SetGraphicsRoot32BitConstants_36 SetGraphicsRoot32BitConstants_36;
        internal readonly Ptr_Func_SetComputeRootConstantBufferView_37 SetComputeRootConstantBufferView_37;
        internal readonly Ptr_Func_SetGraphicsRootConstantBufferView_38 SetGraphicsRootConstantBufferView_38;
        internal readonly Ptr_Func_SetComputeRootShaderResourceView_39 SetComputeRootShaderResourceView_39;
        internal readonly Ptr_Func_SetGraphicsRootShaderResourceView_40 SetGraphicsRootShaderResourceView_40;
        internal readonly Ptr_Func_SetComputeRootUnorderedAccessView_41 SetComputeRootUnorderedAccessView_41;
        internal readonly Ptr_Func_SetGraphicsRootUnorderedAccessView_42 SetGraphicsRootUnorderedAccessView_42;
        internal readonly Ptr_Func_IASetIndexBuffer_43 IASetIndexBuffer_43;
        internal readonly Ptr_Func_IASetVertexBuffers_44 IASetVertexBuffers_44;
        internal readonly Ptr_Func_SOSetTargets_45 SOSetTargets_45;
        internal readonly Ptr_Func_OMSetRenderTargets_46 OMSetRenderTargets_46;
        internal readonly Ptr_Func_ClearDepthStencilView_47 ClearDepthStencilView_47;
        internal readonly Ptr_Func_ClearRenderTargetView_48 ClearRenderTargetView_48;
        internal readonly Ptr_Func_ClearUnorderedAccessViewUint_49 ClearUnorderedAccessViewUint_49;
        internal readonly Ptr_Func_ClearUnorderedAccessViewFloat_50 ClearUnorderedAccessViewFloat_50;
        internal readonly Ptr_Func_DiscardResource_51 DiscardResource_51;
        internal readonly Ptr_Func_BeginQuery_52 BeginQuery_52;
        internal readonly Ptr_Func_EndQuery_53 EndQuery_53;
        internal readonly Ptr_Func_ResolveQueryData_54 ResolveQueryData_54;
        internal readonly Ptr_Func_SetPredication_55 SetPredication_55;
        internal readonly Ptr_Func_SetMarker_56 SetMarker_56;
        internal readonly Ptr_Func_BeginEvent_57 BeginEvent_57;
        internal readonly Ptr_Func_EndEvent_58 EndEvent_58;
        internal readonly Ptr_Func_ExecuteIndirect_59 ExecuteIndirect_59;
    }

    public static class ID3D12GraphicsCommandListImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> @this)
        {
            public COM_HRESULT Close() => @this.Interface_VTable.Close_9.Invoke(@this);

            public COM_HRESULT Reset(COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pAllocator, COM_PTR_IUNKNOWN pInitialState)
                => @this.Interface_VTable.Reset_10.Invoke(@this, pAllocator, pInitialState);

            internal void ResourceBarrier(params ReadOnlySpan<D3D12_RESOURCE_BARRIER> pBarriers)
            {
                @this.Interface_VTable.ResourceBarrier_26.Invoke(@this, pBarriers);
            }

            public void ResourceBarrier_Test<T>(ref T pBarriers) where T : unmanaged
            {
                var data = Unsafe.As<T, D3D12_RESOURCE_BARRIER>(ref pBarriers);
                @this.ResourceBarrier(data);
            }


            internal void OMSetRenderTargets(params ReadOnlySpan<D3D12_CPU_DESCRIPTOR_HANDLE> rtvHandles)
            {
                @this.Interface_VTable.OMSetRenderTargets_46.Invoke(@this, rtvHandles, false, default);
            }

            public void OMSetRenderTargets_Test(nuint rtvHandle)
            {
                Span<D3D12_CPU_DESCRIPTOR_HANDLE> rtvHandles = stackalloc D3D12_CPU_DESCRIPTOR_HANDLE[1] { new() { ptr = rtvHandle } };
                @this.Interface_VTable.OMSetRenderTargets_46.Invoke(@this, rtvHandles, false, default);
            }

            public void SetDescriptorHeaps(params ReadOnlySpan<COM_PTR_IUNKNOWN> ppDescriptorHeaps)
            {
                @this.Interface_VTable.SetDescriptorHeaps_28.Invoke(@this, ppDescriptorHeaps);
            }


        }
    }
}
