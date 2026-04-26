using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11RenderTargetView;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Direct3D11;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct ID3D11DeviceContextImp
    {
        //     ID3D11DeviceContext
        public static readonly Guid GUID = new("C0BFA96C-E089-44FB-8EAF-26F8796190DA");


        internal readonly Ptr_Func_GetDevice_3 GetDevice_3;
        internal readonly Ptr_Func_GetPrivateData_4 GetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateData_5 SetPrivateData_5;
        internal readonly Ptr_Func_SetPrivateDataInterface_6 SetPrivateDataInterface_6;
        internal readonly Ptr_Func_VSSetConstantBuffers_7 VSSetConstantBuffers_7;
        internal readonly Ptr_Func_PSSetShaderResources_8 PSSetShaderResources_8;
        internal readonly Ptr_Func_PSSetShader_9 PSSetShader_9;
        internal readonly Ptr_Func_PSSetSamplers_10 PSSetSamplers_10;
        internal readonly Ptr_Func_VSSetShader_11 VSSetShader_11;
        internal readonly Ptr_Func_DrawIndexed_12 DrawIndexed_12;
        internal readonly Ptr_Func_Draw_13 Draw_13;
        internal readonly Ptr_Func_Map_14 Map_14;
        internal readonly Ptr_Func_Unmap_15 Unmap_15;
        internal readonly Ptr_Func_PSSetConstantBuffers_16 PSSetConstantBuffers_16;
        internal readonly Ptr_Func_IASetInputLayout_17 IASetInputLayout_17;
        internal readonly Ptr_Func_IASetVertexBuffers_18 IASetVertexBuffers_18;
        internal readonly Ptr_Func_IASetIndexBuffer_19 IASetIndexBuffer_19;
        internal readonly Ptr_Func_DrawIndexedInstanced_20 DrawIndexedInstanced_20;
        internal readonly Ptr_Func_DrawInstanced_21 DrawInstanced_21;
        internal readonly Ptr_Func_GSSetConstantBuffers_22 GSSetConstantBuffers_22;
        internal readonly Ptr_Func_GSSetShader_23 GSSetShader_23;
        internal readonly Ptr_Func_IASetPrimitiveTopology_24 IASetPrimitiveTopology_24;
        internal readonly Ptr_Func_VSSetShaderResources_25 VSSetShaderResources_25;
        internal readonly Ptr_Func_VSSetSamplers_26 VSSetSamplers_26;
        internal readonly Ptr_Func_Begin_27 Begin_27;
        internal readonly Ptr_Func_End_28 End_28;
        internal readonly Ptr_Func_GetData_29 GetData_29;
        internal readonly Ptr_Func_SetPredication_30 SetPredication_30;
        internal readonly Ptr_Func_GSSetShaderResources_31 GSSetShaderResources_31;
        internal readonly Ptr_Func_GSSetSamplers_32 GSSetSamplers_32;
        internal readonly Ptr_Func_OMSetRenderTargets_33 OMSetRenderTargets_33;
        internal readonly Ptr_Func_OMSetRenderTargetsAndUnorderedAccessViews_34 OMSetRenderTargetsAndUnorderedAccessViews_34;
        internal readonly Ptr_Func_OMSetBlendState_35 OMSetBlendState_35;
        internal readonly Ptr_Func_OMSetDepthStencilState_36 OMSetDepthStencilState_36;
        internal readonly Ptr_Func_SOSetTargets_37 SOSetTargets_37;
        internal readonly Ptr_Func_DrawAuto_38 DrawAuto_38;
        internal readonly Ptr_Func_DrawIndexedInstancedIndirect_39 DrawIndexedInstancedIndirect_39;
        internal readonly Ptr_Func_DrawInstancedIndirect_40 DrawInstancedIndirect_40;
        internal readonly Ptr_Func_Dispatch_41 Dispatch_41;
        internal readonly Ptr_Func_DispatchIndirect_42 DispatchIndirect_42;
        internal readonly Ptr_Func_RSSetState_43 RSSetState_43;
        internal readonly Ptr_Func_RSSetViewports_44 RSSetViewports_44;
        internal readonly Ptr_Func_RSSetScissorRects_45 RSSetScissorRects_45;
        internal readonly Ptr_Func_CopySubresourceRegion_46 CopySubresourceRegion_46;
        internal readonly Ptr_Func_CopyResource_47 CopyResource_47;
        internal readonly Ptr_Func_UpdateSubresource_48 UpdateSubresource_48;
        internal readonly Ptr_Func_CopyStructureCount_49 CopyStructureCount_49;
        internal readonly Ptr_Func_ClearRenderTargetView_50 ClearRenderTargetView_50;
        internal readonly Ptr_Func_ClearUnorderedAccessViewUint_51 ClearUnorderedAccessViewUint_51;
        internal readonly Ptr_Func_ClearUnorderedAccessViewFloat_52 ClearUnorderedAccessViewFloat_52;
        internal readonly Ptr_Func_ClearDepthStencilView_53 ClearDepthStencilView_53;
        internal readonly Ptr_Func_GenerateMips_54 GenerateMips_54;
        internal readonly Ptr_Func_SetResourceMinLOD_55 SetResourceMinLOD_55;
        internal readonly Ptr_Func_GetResourceMinLOD_56 GetResourceMinLOD_56;
        internal readonly Ptr_Func_ResolveSubresource_57 ResolveSubresource_57;
        internal readonly Ptr_Func_ExecuteCommandList_58 ExecuteCommandList_58;
        internal readonly Ptr_Func_HSSetShaderResources_59 HSSetShaderResources_59;
        internal readonly Ptr_Func_HSSetShader_60 HSSetShader_60;
        internal readonly Ptr_Func_HSSetSamplers_61 HSSetSamplers_61;
        internal readonly Ptr_Func_HSSetConstantBuffers_62 HSSetConstantBuffers_62;
        internal readonly Ptr_Func_DSSetShaderResources_63 DSSetShaderResources_63;
        internal readonly Ptr_Func_DSSetShader_64 DSSetShader_64;
        internal readonly Ptr_Func_DSSetSamplers_65 DSSetSamplers_65;
        internal readonly Ptr_Func_DSSetConstantBuffers_66 DSSetConstantBuffers_66;
        internal readonly Ptr_Func_CSSetShaderResources_67 CSSetShaderResources_67;
        internal readonly Ptr_Func_CSSetUnorderedAccessViews_68 CSSetUnorderedAccessViews_68;
        internal readonly Ptr_Func_CSSetShader_69 CSSetShader_69;
        internal readonly Ptr_Func_CSSetSamplers_70 CSSetSamplers_70;
        internal readonly Ptr_Func_CSSetConstantBuffers_71 CSSetConstantBuffers_71;
        internal readonly Ptr_Func_VSGetConstantBuffers_72 VSGetConstantBuffers_72;
        internal readonly Ptr_Func_PSGetShaderResources_73 PSGetShaderResources_73;
        internal readonly Ptr_Func_PSGetShader_74 PSGetShader_74;
        internal readonly Ptr_Func_PSGetSamplers_75 PSGetSamplers_75;
        internal readonly Ptr_Func_VSGetShader_76 VSGetShader_76;
        internal readonly Ptr_Func_PSGetConstantBuffers_77 PSGetConstantBuffers_77;
        internal readonly Ptr_Func_IAGetInputLayout_78 IAGetInputLayout_78;
        internal readonly Ptr_Func_IAGetVertexBuffers_79 IAGetVertexBuffers_79;
        internal readonly Ptr_Func_IAGetIndexBuffer_80 IAGetIndexBuffer_80;
        internal readonly Ptr_Func_GSGetConstantBuffers_81 GSGetConstantBuffers_81;
        internal readonly Ptr_Func_GSGetShader_82 GSGetShader_82;
        internal readonly Ptr_Func_IAGetPrimitiveTopology_83 IAGetPrimitiveTopology_83;
        internal readonly Ptr_Func_VSGetShaderResources_84 VSGetShaderResources_84;
        internal readonly Ptr_Func_VSGetSamplers_85 VSGetSamplers_85;
        internal readonly Ptr_Func_GetPredication_86 GetPredication_86;
        internal readonly Ptr_Func_GSGetShaderResources_87 GSGetShaderResources_87;
        internal readonly Ptr_Func_GSGetSamplers_88 GSGetSamplers_88;
        internal readonly Ptr_Func_OMGetRenderTargets_89 OMGetRenderTargets_89;
        internal readonly Ptr_Func_OMGetRenderTargetsAndUnorderedAccessViews_90 OMGetRenderTargetsAndUnorderedAccessViews_90;
        internal readonly Ptr_Func_OMGetBlendState_91 OMGetBlendState_91;
        internal readonly Ptr_Func_OMGetDepthStencilState_92 OMGetDepthStencilState_92;
        internal readonly Ptr_Func_SOGetTargets_93 SOGetTargets_93;
        internal readonly Ptr_Func_RSGetState_94 RSGetState_94;
        internal readonly Ptr_Func_RSGetViewports_95 RSGetViewports_95;
        internal readonly Ptr_Func_RSGetScissorRects_96 RSGetScissorRects_96;
        internal readonly Ptr_Func_HSGetShaderResources_97 HSGetShaderResources_97;
        internal readonly Ptr_Func_HSGetShader_98 HSGetShader_98;
        internal readonly Ptr_Func_HSGetSamplers_99 HSGetSamplers_99;
        internal readonly Ptr_Func_HSGetConstantBuffers_100 HSGetConstantBuffers_100;
        internal readonly Ptr_Func_DSGetShaderResources_101 DSGetShaderResources_101;
        internal readonly Ptr_Func_DSGetShader_102 DSGetShader_102;
        internal readonly Ptr_Func_DSGetSamplers_103 DSGetSamplers_103;
        internal readonly Ptr_Func_DSGetConstantBuffers_104 DSGetConstantBuffers_104;
        internal readonly Ptr_Func_CSGetShaderResources_105 CSGetShaderResources_105;
        internal readonly Ptr_Func_CSGetUnorderedAccessViews_106 CSGetUnorderedAccessViews_106;
        internal readonly Ptr_Func_CSGetShader_107 CSGetShader_107;
        internal readonly Ptr_Func_CSGetSamplers_108 CSGetSamplers_108;
        internal readonly Ptr_Func_CSGetConstantBuffers_109 CSGetConstantBuffers_109;
        internal readonly Ptr_Func_ClearState_110 ClearState_110;
        internal readonly Ptr_Func_Flush_111 Flush_111;
        internal readonly Ptr_Func_GetType_112 GetType_112;
        internal readonly Ptr_Func_GetContextFlags_113 GetContextFlags_113;
        internal readonly Ptr_Func_FinishCommandList_114 FinishCommandList_114;
    }
    /*
         public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GetDevice_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetConstantBuffers_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetShaderResources_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.IntPtr*, uint, void> PSSetShader_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetSamplers_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.IntPtr*, uint, void> VSSetShader_11;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, int, void> DrawIndexed_12;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, void> Draw_13;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAP, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_MAPPED_SUBRESOURCE*, int> Map_14;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> Unmap_15;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetConstantBuffers_16;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> IASetInputLayout_17;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, uint*, uint*, void> IASetVertexBuffers_18;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, void> IASetIndexBuffer_19;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, int, uint, void> DrawIndexedInstanced_20;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, uint, void> DrawInstanced_21;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetConstantBuffers_22;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.IntPtr*, uint, void> GSSetShader_23;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_PRIMITIVE_TOPOLOGY, void> IASetPrimitiveTopology_24;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetShaderResources_25;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetSamplers_26;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> Begin_27;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> End_28;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void*, uint, uint, int> GetData_29;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, int, void> SetPredication_30;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetShaderResources_31;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetSamplers_32;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void*, void> OMSetRenderTargets_33;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void*, uint, uint, global::System.IntPtr*, uint*, void> OMSetRenderTargetsAndUnorderedAccessViews_34;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, uint, void> OMSetBlendState_35;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> OMSetDepthStencilState_36;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, uint*, void> SOSetTargets_37;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> DrawAuto_38;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> DrawIndexedInstancedIndirect_39;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> DrawInstancedIndirect_40;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, void> Dispatch_41;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> DispatchIndirect_42;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> RSSetState_43;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_VIEWPORT*, void> RSSetViewports_44;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Foundation.RECT*, void> RSSetScissorRects_45;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, uint, uint, uint, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_BOX*, void> CopySubresourceRegion_46;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void*, void> CopyResource_47;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, global::Windows.Win32.Graphics.Direct3D11.D3D11_BOX*, void*, uint, uint, void> UpdateSubresource_48;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void*, void> CopyStructureCount_49;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, void> ClearRenderTargetView_50;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint*, void> ClearUnorderedAccessViewUint_51;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, void> ClearUnorderedAccessViewFloat_52;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, float, byte, void> ClearDepthStencilView_53;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> GenerateMips_54;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float, void> SetResourceMinLOD_55;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float> GetResourceMinLOD_56;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void*, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, void> ResolveSubresource_57;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, int, void> ExecuteCommandList_58;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> HSSetShaderResources_59;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.IntPtr*, uint, void> HSSetShader_60;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> HSSetSamplers_61;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> HSSetConstantBuffers_62;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> DSSetShaderResources_63;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.IntPtr*, uint, void> DSSetShader_64;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> DSSetSamplers_65;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> DSSetConstantBuffers_66;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSSetShaderResources_67;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, uint*, void> CSSetUnorderedAccessViews_68;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.IntPtr*, uint, void> CSSetShader_69;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSSetSamplers_70;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSSetConstantBuffers_71;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSGetConstantBuffers_72;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSGetShaderResources_73;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, global::Windows.Win32.Graphics.Direct3D11.ID3D11ClassInstance_unmanaged**, uint*, void> PSGetShader_74;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSGetSamplers_75;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, global::Windows.Win32.Graphics.Direct3D11.ID3D11ClassInstance_unmanaged**, uint*, void> VSGetShader_76;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSGetConstantBuffers_77;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> IAGetInputLayout_78;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, uint*, uint*, void> IAGetVertexBuffers_79;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Buffer_unmanaged**, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT*, uint*, void> IAGetIndexBuffer_80;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSGetConstantBuffers_81;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, global::Windows.Win32.Graphics.Direct3D11.ID3D11ClassInstance_unmanaged**, uint*, void> GSGetShader_82;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_PRIMITIVE_TOPOLOGY*, void> IAGetPrimitiveTopology_83;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSGetShaderResources_84;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSGetSamplers_85;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Predicate_unmanaged**, global::Windows.Win32.Foundation.BOOL*, void> GetPredication_86;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSGetShaderResources_87;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSGetSamplers_88;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DepthStencilView_unmanaged**, void> OMGetRenderTargets_89;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DepthStencilView_unmanaged**, uint, uint, global::System.IntPtr*, void> OMGetRenderTargetsAndUnorderedAccessViews_90;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.ID3D11BlendState_unmanaged**, float*, uint*, void> OMGetBlendState_91;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DepthStencilState_unmanaged**, uint*, void> OMGetDepthStencilState_92;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void> SOGetTargets_93;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> RSGetState_94;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, global::Windows.Win32.Graphics.Direct3D11.D3D11_VIEWPORT*, void> RSGetViewports_95;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, global::Windows.Win32.Foundation.RECT*, void> RSGetScissorRects_96;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> HSGetShaderResources_97;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, global::Windows.Win32.Graphics.Direct3D11.ID3D11ClassInstance_unmanaged**, uint*, void> HSGetShader_98;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> HSGetSamplers_99;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> HSGetConstantBuffers_100;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> DSGetShaderResources_101;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, global::Windows.Win32.Graphics.Direct3D11.ID3D11ClassInstance_unmanaged**, uint*, void> DSGetShader_102;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> DSGetSamplers_103;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> DSGetConstantBuffers_104;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSGetShaderResources_105;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSGetUnorderedAccessViews_106;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, global::Windows.Win32.Graphics.Direct3D11.ID3D11ClassInstance_unmanaged**, uint*, void> CSGetShader_107;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSGetSamplers_108;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> CSGetConstantBuffers_109;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> ClearState_110;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> Flush_111;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_DEVICE_CONTEXT_TYPE> GetType_112;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetContextFlags_113;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int, global::Windows.Win32.Graphics.Direct3D11.ID3D11CommandList_unmanaged**, int> FinishCommandList_114;
*/
    public static class ID3D11DeviceContextImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> @this)
        {
            public void GetDevice(out COM_PTR_IUNKNOWN<ID3D11DeviceImp> pDevice)
                => @this.Interface_VTable.GetDevice_3.Invoke(@this, out pDevice);

            public void OMSetRenderTargets(params ReadOnlySpan<COM_PTR_IUNKNOWN> pRenderTargetViews)
                => @this.Interface_VTable.OMSetRenderTargets_33.Invoke(@this, pRenderTargetViews, default);
          

            internal void RSSetViewports(params ReadOnlySpan<D3D11_VIEWPORT> views)
            {
                @this.Interface_VTable.RSSetViewports_44.Invoke(@this, views);
            }

            internal void ClearRenderTargetView(COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> pRenderTargetView, params ReadOnlySpan<float> color)
            {
                @this.Interface_VTable.ClearRenderTargetView_50.Invoke(@this, pRenderTargetView, color);
            }

            internal void Flush()=>@this.Interface_VTable.Flush_111.Invoke(@this);
        }
    }
}