using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10RenderTargetView;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D10;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10Device
{
    /*
    public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetConstantBuffers_3;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetShaderResources_4;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> PSSetShader_5;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetSamplers_6;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> VSSetShader_7;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, int, void> DrawIndexed_8;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, void> Draw_9;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetConstantBuffers_10;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> IASetInputLayout_11;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, uint*, uint*, void> IASetVertexBuffers_12;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, void> IASetIndexBuffer_13;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, int, uint, void> DrawIndexedInstanced_14;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, uint, void> DrawInstanced_15;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetConstantBuffers_16;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> GSSetShader_17;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_PRIMITIVE_TOPOLOGY, void> IASetPrimitiveTopology_18;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetShaderResources_19;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetSamplers_20;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, int, void> SetPredication_21;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetShaderResources_22;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetSamplers_23;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void*, void> OMSetRenderTargets_24;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, uint, void> OMSetBlendState_25;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> OMSetDepthStencilState_26;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, uint*, void> SOSetTargets_27;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> DrawAuto_28;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> RSSetState_29;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D10.D3D10_VIEWPORT*, void> RSSetViewports_30;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Foundation.RECT*, void> RSSetScissorRects_31;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, uint, uint, uint, void*, uint, global::Windows.Win32.Graphics.Direct3D10.D3D10_BOX*, void> CopySubresourceRegion_32;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void*, void> CopyResource_33;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, global::Windows.Win32.Graphics.Direct3D10.D3D10_BOX*, void*, uint, uint, void> UpdateSubresource_34;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, void> ClearRenderTargetView_35;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, float, byte, void> ClearDepthStencilView_36;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> GenerateMips_37;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void*, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, void> ResolveSubresource_38;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSGetConstantBuffers_39;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSGetShaderResources_40;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> PSGetShader_41;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSGetSamplers_42;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> VSGetShader_43;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSGetConstantBuffers_44;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> IAGetInputLayout_45;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, uint*, uint*, void> IAGetVertexBuffers_46;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.ID3D10Buffer_unmanaged**, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT*, uint*, void> IAGetIndexBuffer_47;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSGetConstantBuffers_48;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GSGetShader_49;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_PRIMITIVE_TOPOLOGY*, void> IAGetPrimitiveTopology_50;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSGetShaderResources_51;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSGetSamplers_52;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.ID3D10Predicate_unmanaged**, global::Windows.Win32.Foundation.BOOL*, void> GetPredication_53;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSGetShaderResources_54;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSGetSamplers_55;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, global::Windows.Win32.Graphics.Direct3D10.ID3D10DepthStencilView_unmanaged**, void> OMGetRenderTargets_56;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.ID3D10BlendState_unmanaged**, float*, uint*, void> OMGetBlendState_57;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.ID3D10DepthStencilState_unmanaged**, uint*, void> OMGetDepthStencilState_58;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, uint*, void> SOGetTargets_59;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> RSGetState_60;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, global::Windows.Win32.Graphics.Direct3D10.D3D10_VIEWPORT*, void> RSGetViewports_61;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, global::Windows.Win32.Foundation.RECT*, void> RSGetScissorRects_62;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> GetDeviceRemovedReason_63;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, int> SetExceptionMode_64;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetExceptionMode_65;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_66;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_67;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_68;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> ClearState_69;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> Flush_70;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_BUFFER_DESC*, global::Windows.Win32.Graphics.Direct3D10.D3D10_SUBRESOURCE_DATA*, global::Windows.Win32.Graphics.Direct3D10.ID3D10Buffer_unmanaged**, int> CreateBuffer_71;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_TEXTURE1D_DESC*, global::Windows.Win32.Graphics.Direct3D10.D3D10_SUBRESOURCE_DATA*, void**, int> CreateTexture1D_72;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_TEXTURE2D_DESC*, global::Windows.Win32.Graphics.Direct3D10.D3D10_SUBRESOURCE_DATA*, void**, int> CreateTexture2D_73;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_TEXTURE3D_DESC*, global::Windows.Win32.Graphics.Direct3D10.D3D10_SUBRESOURCE_DATA*, void**, int> CreateTexture3D_74;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D10.D3D10_SHADER_RESOURCE_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10ShaderResourceView_unmanaged**, int> CreateShaderResourceView_75;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D10.D3D10_RENDER_TARGET_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10RenderTargetView_unmanaged**, int> CreateRenderTargetView_76;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D10.D3D10_DEPTH_STENCIL_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10DepthStencilView_unmanaged**, int> CreateDepthStencilView_77;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_INPUT_ELEMENT_DESC*, uint, void*, nuint, global::Windows.Win32.Graphics.Direct3D10.ID3D10InputLayout_unmanaged**, int> CreateInputLayout_78;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, global::Windows.Win32.Graphics.Direct3D10.ID3D10VertexShader_unmanaged**, int> CreateVertexShader_79;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, global::Windows.Win32.Graphics.Direct3D10.ID3D10GeometryShader_unmanaged**, int> CreateGeometryShader_80;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, global::Windows.Win32.Graphics.Direct3D10.D3D10_SO_DECLARATION_ENTRY*, uint, uint, global::Windows.Win32.Graphics.Direct3D10.ID3D10GeometryShader_unmanaged**, int> CreateGeometryShaderWithStreamOutput_81;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, global::Windows.Win32.Graphics.Direct3D10.ID3D10PixelShader_unmanaged**, int> CreatePixelShader_82;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_BLEND_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10BlendState_unmanaged**, int> CreateBlendState_83;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_DEPTH_STENCIL_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10DepthStencilState_unmanaged**, int> CreateDepthStencilState_84;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_RASTERIZER_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10RasterizerState_unmanaged**, int> CreateRasterizerState_85;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_SAMPLER_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10SamplerState_unmanaged**, int> CreateSamplerState_86;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_QUERY_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10Query_unmanaged**, int> CreateQuery_87;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_QUERY_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10Predicate_unmanaged**, int> CreatePredicate_88;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_COUNTER_DESC*, global::Windows.Win32.Graphics.Direct3D10.ID3D10Counter_unmanaged**, int> CreateCounter_89;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint*, int> CheckFormatSupport_90;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, uint*, int> CheckMultisampleQualityLevels_91;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_COUNTER_INFO*, void> CheckCounterInfo_92;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D10.D3D10_COUNTER_DESC*, global::Windows.Win32.Graphics.Direct3D10.D3D10_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, int> CheckCounter_93;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetCreationFlags_94;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.Guid*, void**, int> OpenSharedResource_95;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, void> SetTextFilterSize_96;
public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, uint*, void> GetTextFilterSize_97;


*/


    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D10DeviceImp
    {

        public readonly static Guid GUID = new("9B7E4C0F-342C-4106-A19F-4F2704F689F0");

        // ID3D10Device 方法 (槽位 3-97)
        internal readonly Ptr_Func_VSSetConstantBuffers_3 VSSetConstantBuffers_3;
        internal readonly Ptr_Func_PSSetShaderResources_4 PSSetShaderResources_4;
        internal readonly Ptr_Func_PSSetShader_5 PSSetShader_5;
        internal readonly Ptr_Func_PSSetSamplers_6 PSSetSamplers_6;
        internal readonly Ptr_Func_VSSetShader_7 VSSetShader_7;
        internal readonly Ptr_Func_DrawIndexed_8 DrawIndexed_8;
        internal readonly Ptr_Func_Draw_9 Draw_9;
        internal readonly Ptr_Func_PSSetConstantBuffers_10 PSSetConstantBuffers_10;
        internal readonly Ptr_Func_IASetInputLayout_11 IASetInputLayout_11;
        internal readonly Ptr_Func_IASetVertexBuffers_12 IASetVertexBuffers_12;
        internal readonly Ptr_Func_IASetIndexBuffer_13 IASetIndexBuffer_13;
        internal readonly Ptr_Func_DrawIndexedInstanced_14 DrawIndexedInstanced_14;
        internal readonly Ptr_Func_DrawInstanced_15 DrawInstanced_15;
        internal readonly Ptr_Func_GSSetConstantBuffers_16 GSSetConstantBuffers_16;
        internal readonly Ptr_Func_GSSetShader_17 GSSetShader_17;
        internal readonly Ptr_Func_IASetPrimitiveTopology_18 IASetPrimitiveTopology_18;
        internal readonly Ptr_Func_VSSetShaderResources_19 VSSetShaderResources_19;
        internal readonly Ptr_Func_VSSetSamplers_20 VSSetSamplers_20;
        internal readonly Ptr_Func_SetPredication_21 SetPredication_21;
        internal readonly Ptr_Func_GSSetShaderResources_22 GSSetShaderResources_22;
        internal readonly Ptr_Func_GSSetSamplers_23 GSSetSamplers_23;
        internal readonly Ptr_Func_OMSetRenderTargets_24 OMSetRenderTargets_24;
        internal readonly Ptr_Func_OMSetBlendState_25 OMSetBlendState_25;
        internal readonly Ptr_Func_OMSetDepthStencilState_26 OMSetDepthStencilState_26;
        internal readonly Ptr_Func_SOSetTargets_27 SOSetTargets_27;
        internal readonly Ptr_Func_DrawAuto_28 DrawAuto_28;
        internal readonly Ptr_Func_RSSetState_29 RSSetState_29;
        internal readonly Ptr_Func_RSSetViewports_30 RSSetViewports_30;
        internal readonly Ptr_Func_RSSetScissorRects_31 RSSetScissorRects_31;
        internal readonly Ptr_Func_CopySubresourceRegion_32 CopySubresourceRegion_32;
        internal readonly Ptr_Func_CopyResource_33 CopyResource_33;
        internal readonly Ptr_Func_UpdateSubresource_34 UpdateSubresource_34;
        internal readonly Ptr_Func_ClearRenderTargetView_35 ClearRenderTargetView_35;
        internal readonly Ptr_Func_ClearDepthStencilView_36 ClearDepthStencilView_36;
        internal readonly Ptr_Func_GenerateMips_37 GenerateMips_37;
        internal readonly Ptr_Func_ResolveSubresource_38 ResolveSubresource_38;
        internal readonly Ptr_Func_VSGetConstantBuffers_39 VSGetConstantBuffers_39;
        internal readonly Ptr_Func_PSGetShaderResources_40 PSGetShaderResources_40;
        internal readonly Ptr_Func_PSGetShader_41 PSGetShader_41;
        internal readonly Ptr_Func_PSGetSamplers_42 PSGetSamplers_42;
        internal readonly Ptr_Func_VSGetShader_43 VSGetShader_43;
        internal readonly Ptr_Func_PSGetConstantBuffers_44 PSGetConstantBuffers_44;
        internal readonly Ptr_Func_IAGetInputLayout_45 IAGetInputLayout_45;
        internal readonly Ptr_Func_IAGetVertexBuffers_46 IAGetVertexBuffers_46;
        internal readonly Ptr_Func_IAGetIndexBuffer_47 IAGetIndexBuffer_47;
        internal readonly Ptr_Func_GSGetConstantBuffers_48 GSGetConstantBuffers_48;
        internal readonly Ptr_Func_GSGetShader_49 GSGetShader_49;
        internal readonly Ptr_Func_IAGetPrimitiveTopology_50 IAGetPrimitiveTopology_50;
        internal readonly Ptr_Func_VSGetShaderResources_51 VSGetShaderResources_51;
        internal readonly Ptr_Func_VSGetSamplers_52 VSGetSamplers_52;
        internal readonly Ptr_Func_GetPredication_53 GetPredication_53;
        internal readonly Ptr_Func_GSGetShaderResources_54 GSGetShaderResources_54;
        internal readonly Ptr_Func_GSGetSamplers_55 GSGetSamplers_55;
        internal readonly Ptr_Func_OMGetRenderTargets_56 OMGetRenderTargets_56;
        internal readonly Ptr_Func_OMGetBlendState_57 OMGetBlendState_57;
        internal readonly Ptr_Func_OMGetDepthStencilState_58 OMGetDepthStencilState_58;
        internal readonly Ptr_Func_SOGetTargets_59 SOGetTargets_59;
        internal readonly Ptr_Func_RSGetState_60 RSGetState_60;
        internal readonly Ptr_Func_RSGetViewports_61 RSGetViewports_61;
        internal readonly Ptr_Func_RSGetScissorRects_62 RSGetScissorRects_62;
        internal readonly Ptr_Func_GetDeviceRemovedReason_63 GetDeviceRemovedReason_63;
        internal readonly Ptr_Func_SetExceptionMode_64 SetExceptionMode_64;
        internal readonly Ptr_Func_GetExceptionMode_65 GetExceptionMode_65;
        internal readonly Ptr_Func_GetPrivateData_66 GetPrivateData_66;
        internal readonly Ptr_Func_SetPrivateData_67 SetPrivateData_67;
        internal readonly Ptr_Func_SetPrivateDataInterface_68 SetPrivateDataInterface_68;
        internal readonly Ptr_Func_ClearState_69 ClearState_69;
        internal readonly Ptr_Func_Flush_70 Flush_70;
        internal readonly Ptr_Func_CreateBuffer_71 CreateBuffer_71;
        internal readonly Ptr_Func_CreateTexture1D_72 CreateTexture1D_72;
        internal readonly Ptr_Func_CreateTexture2D_73 CreateTexture2D_73;
        internal readonly Ptr_Func_CreateTexture3D_74 CreateTexture3D_74;
        internal readonly Ptr_Func_CreateShaderResourceView_75 CreateShaderResourceView_75;
        internal readonly Ptr_Func_CreateRenderTargetView_76 CreateRenderTargetView_76;
        internal readonly Ptr_Func_CreateDepthStencilView_77 CreateDepthStencilView_77;
        internal readonly Ptr_Func_CreateInputLayout_78 CreateInputLayout_78;
        internal readonly Ptr_Func_CreateVertexShader_79 CreateVertexShader_79;
        internal readonly Ptr_Func_CreateGeometryShader_80 CreateGeometryShader_80;
        internal readonly Ptr_Func_CreateGeometryShaderWithStreamOutput_81 CreateGeometryShaderWithStreamOutput_81;
        internal readonly Ptr_Func_CreatePixelShader_82 CreatePixelShader_82;
        internal readonly Ptr_Func_CreateBlendState_83 CreateBlendState_83;
        internal readonly Ptr_Func_CreateDepthStencilState_84 CreateDepthStencilState_84;
        internal readonly Ptr_Func_CreateRasterizerState_85 CreateRasterizerState_85;
        internal readonly Ptr_Func_CreateSamplerState_86 CreateSamplerState_86;
        internal readonly Ptr_Func_CreateQuery_87 CreateQuery_87;
        internal readonly Ptr_Func_CreatePredicate_88 CreatePredicate_88;
        internal readonly Ptr_Func_CreateCounter_89 CreateCounter_89;
        internal readonly Ptr_Func_CheckFormatSupport_90 CheckFormatSupport_90;
        internal readonly Ptr_Func_CheckMultisampleQualityLevels_91 CheckMultisampleQualityLevels_91;
        internal readonly Ptr_Func_CheckCounterInfo_92 CheckCounterInfo_92;
        internal readonly Ptr_Func_CheckCounter_93 CheckCounter_93;
        internal readonly Ptr_Func_GetCreationFlags_94 GetCreationFlags_94;
        internal readonly Ptr_Func_OpenSharedResource_95 OpenSharedResource_95;
        internal readonly Ptr_Func_SetTextFilterSize_96 SetTextFilterSize_96;
        internal readonly Ptr_Func_GetTextFilterSize_97 GetTextFilterSize_97;
    }

    public static class ID3D10DeviceImpExtensions
    {
        extension(COM_PTR_IUNKNOWN<ID3D10DeviceImp> @this)
        {
            internal COM_HRESULT CreateRenderTargetView(COM_PTR_IUNKNOWN pResource, out COM_PTR_IUNKNOWN<ID3D10RenderTargetViewImp> pRTView)
            {
                var null_ptr_desc = new UnsafeIn<D3D10_RENDER_TARGET_VIEW_DESC>(nint.Zero);
                var hr = @this.Interface_VTable.CreateRenderTargetView_76.Invoke(@this, pResource, null_ptr_desc, UnsafeOut<COM_PTR_IUNKNOWN>.FromOut(out var pObject));
                pRTView = pObject.Get<ID3D10RenderTargetViewImp>();
                return hr;
            }

            internal void OMSetRenderTargets(ReadOnlySpan<COM_PTR_IUNKNOWN> ppRenderTargetViews, COM_PTR_IUNKNOWN pDepthStencilView)
            {
                @this.Interface_VTable.OMSetRenderTargets_24.Invoke(@this, (uint)ppRenderTargetViews.Length, UnsafeRef<COM_PTR_IUNKNOWN>.FromRef(ref MemoryMarshal.GetReference(ppRenderTargetViews)), pDepthStencilView);
            }
            internal void OMSetRenderTargets(params ReadOnlySpan<COM_PTR_IUNKNOWN> ppRenderTargetViews)
            {
                @this.Interface_VTable.OMSetRenderTargets_24.Invoke(@this, ppRenderTargetViews, default);
            }
            internal void ClearRenderTargetView(COM_PTR_IUNKNOWN ppRenderTargetViews, params ReadOnlySpan<float> colorRGBA)
            {
                @this.Interface_VTable.ClearRenderTargetView_35.Invoke(@this, ppRenderTargetViews, colorRGBA);
            }
            internal void RSSetViewports(params ReadOnlySpan<D3D10_VIEWPORT> views)
            {
                @this.Interface_VTable.RSSetViewports_30.Invoke(@this, views);
            }

            internal void Flush() => @this.Interface_VTable.Flush_70.Invoke(@this);
        }
    }



    /// <summary>
    /// ID3D10Device::VSSetConstantBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSSetConstantBuffers_3(nint ptr) : IHookMethod
    {
        public const string Name = "VSSetConstantBuffers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetConstantBuffers_3;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppConstantBuffers) => _proc(pThis, startSlot, numBuffers, ppConstantBuffers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSSetShaderResources
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSSetShaderResources_4(nint ptr) : IHookMethod
    {
        public const string Name = "PSSetShaderResources";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetShaderResources_4;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numViews, nint* ppShaderResourceViews) => _proc(pThis, startSlot, numViews, ppShaderResourceViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSSetShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSSetShader_5(nint ptr) : IHookMethod
    {
        public const string Name = "PSSetShader";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> PSSetShader_5;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pPixelShader) => _proc(pThis, pPixelShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSSetSamplers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSSetSamplers_6(nint ptr) : IHookMethod
    {
        public const string Name = "PSSetSamplers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetSamplers_6;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numSamplers, nint* ppSamplers) => _proc(pThis, startSlot, numSamplers, ppSamplers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::VSSetShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSSetShader_7(nint ptr) : IHookMethod
    {
        public const string Name = "VSSetShader";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> VSSetShader_7;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pVertexShader) => _proc(pThis, pVertexShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::DrawIndexed
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_DrawIndexed_8(nint ptr) : IHookMethod
    {
        public const string Name = "DrawIndexed";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, int, void> DrawIndexed_8;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, int, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, int, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint indexCount, uint startIndexLocation, int baseVertexLocation) => _proc(pThis, indexCount, startIndexLocation, baseVertexLocation);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::Draw
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Draw_9(nint ptr) : IHookMethod
    {
        public const string Name = "Draw";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, void> Draw_9;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint vertexCount, uint startVertexLocation) => _proc(pThis, vertexCount, startVertexLocation);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSSetConstantBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSSetConstantBuffers_10(nint ptr) : IHookMethod
    {
        public const string Name = "PSSetConstantBuffers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> PSSetConstantBuffers_10;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppConstantBuffers) => _proc(pThis, startSlot, numBuffers, ppConstantBuffers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IASetInputLayout
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IASetInputLayout_11(nint ptr) : IHookMethod
    {
        public const string Name = "IASetInputLayout";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> IASetInputLayout_11;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pInputLayout) => _proc(pThis, pInputLayout);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IASetVertexBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IASetVertexBuffers_12(nint ptr) : IHookMethod
    {
        public const string Name = "IASetVertexBuffers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, uint*, uint*, void> IASetVertexBuffers_12;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, uint*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, uint*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppVertexBuffers, uint* pStrides, uint* pOffsets) => _proc(pThis, startSlot, numBuffers, ppVertexBuffers, pStrides, pOffsets);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IASetIndexBuffer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IASetIndexBuffer_13(nint ptr) : IHookMethod
    {
        public const string Name = "IASetIndexBuffer";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, void> IASetIndexBuffer_13;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, DXGI_FORMAT, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, DXGI_FORMAT, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pIndexBuffer, DXGI_FORMAT format, uint offset) => _proc(pThis, pIndexBuffer, format, offset);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::DrawIndexedInstanced
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_DrawIndexedInstanced_14(nint ptr) : IHookMethod
    {
        public const string Name = "DrawIndexedInstanced";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, int, uint, void> DrawIndexedInstanced_14;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, uint, int, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, uint, int, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint indexCountPerInstance, uint instanceCount, uint startIndexLocation, int baseVertexLocation, uint startInstanceLocation) => _proc(pThis, indexCountPerInstance, instanceCount, startIndexLocation, baseVertexLocation, startInstanceLocation);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::DrawInstanced
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_DrawInstanced_15(nint ptr) : IHookMethod
    {
        public const string Name = "DrawInstanced";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, uint, void> DrawInstanced_15;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, uint, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, uint, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint vertexCountPerInstance, uint instanceCount, uint startVertexLocation, uint startInstanceLocation) => _proc(pThis, vertexCountPerInstance, instanceCount, startVertexLocation, startInstanceLocation);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSSetConstantBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSSetConstantBuffers_16(nint ptr) : IHookMethod
    {
        public const string Name = "GSSetConstantBuffers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetConstantBuffers_16;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppConstantBuffers) => _proc(pThis, startSlot, numBuffers, ppConstantBuffers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSSetShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSSetShader_17(nint ptr) : IHookMethod
    {
        public const string Name = "GSSetShader";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> GSSetShader_17;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pGeometryShader) => _proc(pThis, pGeometryShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IASetPrimitiveTopology
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IASetPrimitiveTopology_18(nint ptr) : IHookMethod
    {
        public const string Name = "IASetPrimitiveTopology";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_PRIMITIVE_TOPOLOGY, void> IASetPrimitiveTopology_18;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D_PRIMITIVE_TOPOLOGY, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D_PRIMITIVE_TOPOLOGY, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D_PRIMITIVE_TOPOLOGY topology) => _proc(pThis, topology);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::VSSetShaderResources
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSSetShaderResources_19(nint ptr) : IHookMethod
    {
        public const string Name = "VSSetShaderResources";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetShaderResources_19;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numViews, nint* ppShaderResourceViews) => _proc(pThis, startSlot, numViews, ppShaderResourceViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::VSSetSamplers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSSetSamplers_20(nint ptr) : IHookMethod
    {
        public const string Name = "VSSetSamplers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> VSSetSamplers_20;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numSamplers, nint* ppSamplers) => _proc(pThis, startSlot, numSamplers, ppSamplers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SetPredication
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPredication_21(nint ptr) : IHookMethod
    {
        public const string Name = "SetPredication";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, int, void> SetPredication_21;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, int, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, int, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pPredicate, int predicateValue) => _proc(pThis, pPredicate, predicateValue);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSSetShaderResources
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSSetShaderResources_22(nint ptr) : IHookMethod
    {
        public const string Name = "GSSetShaderResources";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetShaderResources_22;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numViews, nint* ppShaderResourceViews) => _proc(pThis, startSlot, numViews, ppShaderResourceViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSSetSamplers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSSetSamplers_23(nint ptr) : IHookMethod
    {
        public const string Name = "GSSetSamplers";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, global::System.IntPtr*, void> GSSetSamplers_23;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numSamplers, nint* ppSamplers) => _proc(pThis, startSlot, numSamplers, ppSamplers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OMSetRenderTargets
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMSetRenderTargets_24(nint ptr) : IHookMethod
    {
        public const string Name = "OMSetRenderTargets";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, void*, void> OMSetRenderTargets_24;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, COM_PTR_IUNKNOWN, void> _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, COM_PTR_IUNKNOWN, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint numRenderTargets, UnsafeRef<COM_PTR_IUNKNOWN> ppRenderTargetViews, COM_PTR_IUNKNOWN pDepthStencilView)
            => _proc(pThis, numRenderTargets, ppRenderTargetViews, pDepthStencilView);
        public void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, ReadOnlySpan<COM_PTR_IUNKNOWN> ppRenderTargetViews, COM_PTR_IUNKNOWN pDepthStencilView)
            => _proc(pThis, (uint)ppRenderTargetViews.Length, UnsafeRef<COM_PTR_IUNKNOWN>.FromRef(ref MemoryMarshal.GetReference(ppRenderTargetViews)), pDepthStencilView);


        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OMSetBlendState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMSetBlendState_25(nint ptr) : IHookMethod
    {
        public const string Name = "OMSetBlendState";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, uint, void> OMSetBlendState_25;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, float*, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, float*, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pBlendState, float* blendFactor, uint sampleMask) => _proc(pThis, pBlendState, blendFactor, sampleMask);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OMSetDepthStencilState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMSetDepthStencilState_26(nint ptr) : IHookMethod
    {
        public const string Name = "OMSetDepthStencilState";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void> OMSetDepthStencilState_26;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pDepthStencilState, uint stencilRef) => _proc(pThis, pDepthStencilState, stencilRef);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SOSetTargets
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SOSetTargets_27(nint ptr) : IHookMethod
    {
        public const string Name = "SOSetTargets";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.IntPtr*, uint*, void> SOSetTargets_27;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, nint*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, nint*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint numBuffers, nint* ppSOTargets, uint* pOffsets) => _proc(pThis, numBuffers, ppSOTargets, pOffsets);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::DrawAuto
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_DrawAuto_28(nint ptr) : IHookMethod
    {
        public const string Name = "DrawAuto";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void> DrawAuto_28;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::RSSetState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_RSSetState_29(nint ptr) : IHookMethod
    {
        public const string Name = "RSSetState";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> RSSetState_29;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pRasterizerState) => _proc(pThis, pRasterizerState);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::RSSetViewports
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_RSSetViewports_30(nint ptr) : IHookMethod
    {
        public const string Name = "RSSetViewports";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D10.D3D10_VIEWPORT*, void> RSSetViewports_30;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, UnsafeRef<D3D10_VIEWPORT>, void> _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, UnsafeRef<D3D10_VIEWPORT>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint numViewports, UnsafeRef<D3D10_VIEWPORT> pViewports)
            => _proc(pThis, numViewports, pViewports);
        public void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, ReadOnlySpan<D3D10_VIEWPORT> pViewports)
            => _proc(pThis, (uint)pViewports.Length, UnsafeRef<D3D10_VIEWPORT>.FromRef(ref MemoryMarshal.GetReference(pViewports)));


        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::RSSetScissorRects
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_RSSetScissorRects_31(nint ptr) : IHookMethod
    {
        public const string Name = "RSSetScissorRects";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Foundation.RECT*, void> RSSetScissorRects_31;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, RECT*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, RECT*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint numRects, RECT* pRects) => _proc(pThis, numRects, pRects);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CopySubresourceRegion
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CopySubresourceRegion_32(nint ptr) : IHookMethod
    {
        public const string Name = "CopySubresourceRegion";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, uint, uint, uint, void*, uint, global::Windows.Win32.Graphics.Direct3D10.D3D10_BOX*, void> CopySubresourceRegion_32;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, uint, uint, uint, void*, uint, D3D10_BOX*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, uint, uint, uint, void*, uint, D3D10_BOX*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pDstResource, uint dstSubresource, uint dstX, uint dstY, uint dstZ, void* pSrcResource, uint srcSubresource, D3D10_BOX* pSrcBox) => _proc(pThis, pDstResource, dstSubresource, dstX, dstY, dstZ, pSrcResource, srcSubresource, pSrcBox);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CopyResource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CopyResource_33(nint ptr) : IHookMethod
    {
        public const string Name = "CopyResource";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void*, void> CopyResource_33;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pDstResource, void* pSrcResource) => _proc(pThis, pDstResource, pSrcResource);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::UpdateSubresource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_UpdateSubresource_34(nint ptr) : IHookMethod
    {
        public const string Name = "UpdateSubresource";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, global::Windows.Win32.Graphics.Direct3D10.D3D10_BOX*, void*, uint, uint, void> UpdateSubresource_34;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, D3D10_BOX*, void*, uint, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, D3D10_BOX*, void*, uint, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pDstResource, uint dstSubresource, D3D10_BOX* pDstBox, void* pSrcData, uint srcRowPitch, uint srcDepthPitch) => _proc(pThis, pDstResource, dstSubresource, pDstBox, pSrcData, srcRowPitch, srcDepthPitch);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::ClearRenderTargetView
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ClearRenderTargetView_35(nint ptr) : IHookMethod
    {
        public const string Name = "ClearRenderTargetView";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, float*, void> ClearRenderTargetView_35;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, COM_PTR_IUNKNOWN, UnsafeRef<float>, void> _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, COM_PTR_IUNKNOWN, UnsafeRef<float>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, COM_PTR_IUNKNOWN pRenderTargetView, UnsafeRef<float> colorRGBA)
            => _proc(pThis, pRenderTargetView, colorRGBA);
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, COM_PTR_IUNKNOWN pRenderTargetView, ReadOnlySpan<float> colorRGBA)
            => _proc(pThis, pRenderTargetView, UnsafeRef<float>.FromRef(ref MemoryMarshal.GetReference(colorRGBA)));

        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::ClearDepthStencilView
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ClearDepthStencilView_36(nint ptr) : IHookMethod
    {
        public const string Name = "ClearDepthStencilView";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, float, byte, void> ClearDepthStencilView_36;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, float, byte, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, float, byte, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pDepthStencilView, uint flags, float depth, byte stencil) => _proc(pThis, pDepthStencilView, flags, depth, stencil);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GenerateMips
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GenerateMips_37(nint ptr) : IHookMethod
    {
        public const string Name = "GenerateMips";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, void> GenerateMips_37;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pShaderResourceView) => _proc(pThis, pShaderResourceView);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::ResolveSubresource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ResolveSubresource_38(nint ptr) : IHookMethod
    {
        public const string Name = "ResolveSubresource";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, void*, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, void> ResolveSubresource_38;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, void*, uint, DXGI_FORMAT, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, uint, void*, uint, DXGI_FORMAT, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pDstResource, uint dstSubresource, void* pSrcResource, uint srcSubresource, DXGI_FORMAT format) => _proc(pThis, pDstResource, dstSubresource, pSrcResource, srcSubresource, format);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    // 注意：后续 Get 方法（槽位 39-97）的模式相同，这里省略以保持响应长度
    // 如果需要完整的 3-97 所有函数，请告知
    /// <summary>
    /// ID3D10Device::VSGetConstantBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSGetConstantBuffers_39(nint ptr) : IHookMethod
    {
        public const string Name = "VSGetConstantBuffers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppConstantBuffers) => _proc(pThis, startSlot, numBuffers, ppConstantBuffers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSGetShaderResources
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSGetShaderResources_40(nint ptr) : IHookMethod
    {
        public const string Name = "PSGetShaderResources";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numViews, nint* ppShaderResourceViews) => _proc(pThis, startSlot, numViews, ppShaderResourceViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSGetShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSGetShader_41(nint ptr) : IHookMethod
    {
        public const string Name = "PSGetShader";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppPixelShader) => _proc(pThis, ppPixelShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSGetSamplers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSGetSamplers_42(nint ptr) : IHookMethod
    {
        public const string Name = "PSGetSamplers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numSamplers, nint* ppSamplers) => _proc(pThis, startSlot, numSamplers, ppSamplers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::VSGetShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSGetShader_43(nint ptr) : IHookMethod
    {
        public const string Name = "VSGetShader";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppVertexShader) => _proc(pThis, ppVertexShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::PSGetConstantBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_PSGetConstantBuffers_44(nint ptr) : IHookMethod
    {
        public const string Name = "PSGetConstantBuffers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppConstantBuffers) => _proc(pThis, startSlot, numBuffers, ppConstantBuffers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IAGetInputLayout
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IAGetInputLayout_45(nint ptr) : IHookMethod
    {
        public const string Name = "IAGetInputLayout";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppInputLayout) => _proc(pThis, ppInputLayout);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IAGetVertexBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IAGetVertexBuffers_46(nint ptr) : IHookMethod
    {
        public const string Name = "IAGetVertexBuffers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, uint*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, uint*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppVertexBuffers, uint* pStrides, uint* pOffsets) => _proc(pThis, startSlot, numBuffers, ppVertexBuffers, pStrides, pOffsets);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IAGetIndexBuffer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IAGetIndexBuffer_47(nint ptr) : IHookMethod
    {
        public const string Name = "IAGetIndexBuffer";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, DXGI_FORMAT*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, DXGI_FORMAT*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppIndexBuffer, DXGI_FORMAT* pFormat, uint* pOffset) => _proc(pThis, ppIndexBuffer, pFormat, pOffset);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSGetConstantBuffers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSGetConstantBuffers_48(nint ptr) : IHookMethod
    {
        public const string Name = "GSGetConstantBuffers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numBuffers, nint* ppConstantBuffers) => _proc(pThis, startSlot, numBuffers, ppConstantBuffers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSGetShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSGetShader_49(nint ptr) : IHookMethod
    {
        public const string Name = "GSGetShader";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppGeometryShader) => _proc(pThis, ppGeometryShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::IAGetPrimitiveTopology
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IAGetPrimitiveTopology_50(nint ptr) : IHookMethod
    {
        public const string Name = "IAGetPrimitiveTopology";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D_PRIMITIVE_TOPOLOGY*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D_PRIMITIVE_TOPOLOGY*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D_PRIMITIVE_TOPOLOGY* pTopology) => _proc(pThis, pTopology);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::VSGetShaderResources
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSGetShaderResources_51(nint ptr) : IHookMethod
    {
        public const string Name = "VSGetShaderResources";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numViews, nint* ppShaderResourceViews) => _proc(pThis, startSlot, numViews, ppShaderResourceViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::VSGetSamplers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_VSGetSamplers_52(nint ptr) : IHookMethod
    {
        public const string Name = "VSGetSamplers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numSamplers, nint* ppSamplers) => _proc(pThis, startSlot, numSamplers, ppSamplers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GetPredication
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetPredication_53(nint ptr) : IHookMethod
    {
        public const string Name = "GetPredication";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, int*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, int*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppPredicate, int* pPredicateValue) => _proc(pThis, ppPredicate, pPredicateValue);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSGetShaderResources
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSGetShaderResources_54(nint ptr) : IHookMethod
    {
        public const string Name = "GSGetShaderResources";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numViews, nint* ppShaderResourceViews) => _proc(pThis, startSlot, numViews, ppShaderResourceViews);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GSGetSamplers
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GSGetSamplers_55(nint ptr) : IHookMethod
    {
        public const string Name = "GSGetSamplers";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, nint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint startSlot, uint numSamplers, nint* ppSamplers) => _proc(pThis, startSlot, numSamplers, ppSamplers);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OMGetRenderTargets
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMGetRenderTargets_56(nint ptr) : IHookMethod
    {
        public const string Name = "OMGetRenderTargets";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, nint*, void**, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, nint*, void**, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint numRenderTargets, nint* ppRenderTargetViews, void** ppDepthStencilView) => _proc(pThis, numRenderTargets, ppRenderTargetViews, ppDepthStencilView);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OMGetBlendState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMGetBlendState_57(nint ptr) : IHookMethod
    {
        public const string Name = "OMGetBlendState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, float*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, float*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppBlendState, float* pBlendFactor, uint* pSampleMask) => _proc(pThis, ppBlendState, pBlendFactor, pSampleMask);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OMGetDepthStencilState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OMGetDepthStencilState_58(nint ptr) : IHookMethod
    {
        public const string Name = "OMGetDepthStencilState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppDepthStencilState, uint* pStencilRef) => _proc(pThis, ppDepthStencilState, pStencilRef);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SOGetTargets
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SOGetTargets_59(nint ptr) : IHookMethod
    {
        public const string Name = "SOGetTargets";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, nint*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, nint*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint numBuffers, nint* ppSOTargets, uint* pOffsets) => _proc(pThis, numBuffers, ppSOTargets, pOffsets);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::RSGetState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_RSGetState_60(nint ptr) : IHookMethod
    {
        public const string Name = "RSGetState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void**, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void** ppRasterizerState) => _proc(pThis, ppRasterizerState);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::RSGetViewports
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_RSGetViewports_61(nint ptr) : IHookMethod
    {
        public const string Name = "RSGetViewports";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint*, D3D10_VIEWPORT*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint*, D3D10_VIEWPORT*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint* pNumViewports, D3D10_VIEWPORT* pViewports) => _proc(pThis, pNumViewports, pViewports);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::RSGetScissorRects
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_RSGetScissorRects_62(nint ptr) : IHookMethod
    {
        public const string Name = "RSGetScissorRects";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint*, RECT*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint*, RECT*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint* pNumRects, RECT* pRects) => _proc(pThis, pNumRects, pRects);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GetDeviceRemovedReason
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDeviceRemovedReason_63(nint ptr) : IHookMethod
    {
        public const string Name = "GetDeviceRemovedReason";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SetExceptionMode
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetExceptionMode_64(nint ptr) : IHookMethod
    {
        public const string Name = "SetExceptionMode";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint raiseFlags) => _proc(pThis, raiseFlags);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GetExceptionMode
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetExceptionMode_65(nint ptr) : IHookMethod
    {
        public const string Name = "GetExceptionMode";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint>)ptr;
        public nint PtrMethod => (nint)_proc;
        public uint Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetPrivateData_66(nint ptr) : IHookMethod
    {
        public const string Name = "GetPrivateData";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, Guid*, uint*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, Guid*, uint*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, Guid* guid, uint* pDataSize, void* pData) => _proc(pThis, guid, pDataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateData_67(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateData";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, Guid*, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, Guid*, uint, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, Guid* guid, uint dataSize, void* pData) => _proc(pThis, guid, dataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SetPrivateDataInterface
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateDataInterface_68(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateDataInterface";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, Guid*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, Guid*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, Guid* guid, void* pData) => _proc(pThis, guid, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::ClearState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ClearState_69(nint ptr) : IHookMethod
    {
        public const string Name = "ClearState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::Flush
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Flush_70(nint ptr) : IHookMethod
    {
        public const string Name = "Flush";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateBuffer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateBuffer_71(nint ptr) : IHookMethod
    {
        public const string Name = "CreateBuffer";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_BUFFER_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_BUFFER_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_BUFFER_DESC* pDesc, D3D10_SUBRESOURCE_DATA* pInitialData, void** ppBuffer) => _proc(pThis, pDesc, pInitialData, ppBuffer);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateTexture1D
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateTexture1D_72(nint ptr) : IHookMethod
    {
        public const string Name = "CreateTexture1D";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_TEXTURE1D_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_TEXTURE1D_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_TEXTURE1D_DESC* pDesc, D3D10_SUBRESOURCE_DATA* pInitialData, void** ppTexture) => _proc(pThis, pDesc, pInitialData, ppTexture);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateTexture2D
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateTexture2D_73(nint ptr) : IHookMethod
    {
        public const string Name = "CreateTexture2D";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_TEXTURE2D_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_TEXTURE2D_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_TEXTURE2D_DESC* pDesc, D3D10_SUBRESOURCE_DATA* pInitialData, void** ppTexture) => _proc(pThis, pDesc, pInitialData, ppTexture);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateTexture3D
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateTexture3D_74(nint ptr) : IHookMethod
    {
        public const string Name = "CreateTexture3D";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_TEXTURE3D_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_TEXTURE3D_DESC*, D3D10_SUBRESOURCE_DATA*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_TEXTURE3D_DESC* pDesc, D3D10_SUBRESOURCE_DATA* pInitialData, void** ppTexture) => _proc(pThis, pDesc, pInitialData, ppTexture);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateShaderResourceView
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateShaderResourceView_75(nint ptr) : IHookMethod
    {
        public const string Name = "CreateShaderResourceView";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, D3D10_SHADER_RESOURCE_VIEW_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, D3D10_SHADER_RESOURCE_VIEW_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pResource, D3D10_SHADER_RESOURCE_VIEW_DESC* pDesc, void** ppSRView) => _proc(pThis, pResource, pDesc, ppSRView);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateRenderTargetView
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateRenderTargetView_76(nint ptr) : IHookMethod
    {
        public const string Name = "CreateRenderTargetView";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, COM_PTR_IUNKNOWN, UnsafeIn<D3D10_RENDER_TARGET_VIEW_DESC>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT> _proc
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, COM_PTR_IUNKNOWN, UnsafeIn<D3D10_RENDER_TARGET_VIEW_DESC>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, COM_PTR_IUNKNOWN pResource, UnsafeIn<D3D10_RENDER_TARGET_VIEW_DESC> pDesc, UnsafeOut<COM_PTR_IUNKNOWN> ppRTView)
            => _proc(pThis, pResource, pDesc, ppRTView);
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, COM_PTR_IUNKNOWN pResource, in D3D10_RENDER_TARGET_VIEW_DESC pDesc, out COM_PTR_IUNKNOWN pRTView)
            => _proc(pThis, pResource, UnsafeIn<D3D10_RENDER_TARGET_VIEW_DESC>.FromIn(in pDesc), UnsafeOut<COM_PTR_IUNKNOWN>.FromOut(out pRTView));

        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateDepthStencilView
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateDepthStencilView_77(nint ptr) : IHookMethod
    {
        public const string Name = "CreateDepthStencilView";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, D3D10_DEPTH_STENCIL_VIEW_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, D3D10_DEPTH_STENCIL_VIEW_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pResource, D3D10_DEPTH_STENCIL_VIEW_DESC* pDesc, void** ppDSView) => _proc(pThis, pResource, pDesc, ppDSView);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateInputLayout
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateInputLayout_78(nint ptr) : IHookMethod
    {
        public const string Name = "CreateInputLayout";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_INPUT_ELEMENT_DESC*, uint, void*, nuint, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_INPUT_ELEMENT_DESC*, uint, void*, nuint, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_INPUT_ELEMENT_DESC* pInputElementDescs, uint numElements, void* pShaderBytecodeWithInputSignature, nuint bytecodeLength, void** ppInputLayout) => _proc(pThis, pInputElementDescs, numElements, pShaderBytecodeWithInputSignature, bytecodeLength, ppInputLayout);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateVertexShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateVertexShader_79(nint ptr) : IHookMethod
    {
        public const string Name = "CreateVertexShader";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pShaderBytecode, nuint bytecodeLength, void** ppVertexShader) => _proc(pThis, pShaderBytecode, bytecodeLength, ppVertexShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateGeometryShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateGeometryShader_80(nint ptr) : IHookMethod
    {
        public const string Name = "CreateGeometryShader";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pShaderBytecode, nuint bytecodeLength, void** ppGeometryShader) => _proc(pThis, pShaderBytecode, bytecodeLength, ppGeometryShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateGeometryShaderWithStreamOutput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateGeometryShaderWithStreamOutput_81(nint ptr) : IHookMethod
    {
        public const string Name = "CreateGeometryShaderWithStreamOutput";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, D3D10_SO_DECLARATION_ENTRY*, uint, uint, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, D3D10_SO_DECLARATION_ENTRY*, uint, uint, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pShaderBytecode, nuint bytecodeLength, D3D10_SO_DECLARATION_ENTRY* pSODeclaration, uint numEntries, uint outputStreamStride, void** ppGeometryShader) => _proc(pThis, pShaderBytecode, bytecodeLength, pSODeclaration, numEntries, outputStreamStride, ppGeometryShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreatePixelShader
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreatePixelShader_82(nint ptr) : IHookMethod
    {
        public const string Name = "CreatePixelShader";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, nuint, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* pShaderBytecode, nuint bytecodeLength, void** ppPixelShader) => _proc(pThis, pShaderBytecode, bytecodeLength, ppPixelShader);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateBlendState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateBlendState_83(nint ptr) : IHookMethod
    {
        public const string Name = "CreateBlendState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_BLEND_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_BLEND_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_BLEND_DESC* pBlendStateDesc, void** ppBlendState) => _proc(pThis, pBlendStateDesc, ppBlendState);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateDepthStencilState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateDepthStencilState_84(nint ptr) : IHookMethod
    {
        public const string Name = "CreateDepthStencilState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_DEPTH_STENCIL_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_DEPTH_STENCIL_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_DEPTH_STENCIL_DESC* pDepthStencilDesc, void** ppDepthStencilState) => _proc(pThis, pDepthStencilDesc, ppDepthStencilState);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateRasterizerState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateRasterizerState_85(nint ptr) : IHookMethod
    {
        public const string Name = "CreateRasterizerState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_RASTERIZER_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_RASTERIZER_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_RASTERIZER_DESC* pRasterizerDesc, void** ppRasterizerState) => _proc(pThis, pRasterizerDesc, ppRasterizerState);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateSamplerState
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateSamplerState_86(nint ptr) : IHookMethod
    {
        public const string Name = "CreateSamplerState";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_SAMPLER_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_SAMPLER_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_SAMPLER_DESC* pSamplerDesc, void** ppSamplerState) => _proc(pThis, pSamplerDesc, ppSamplerState);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateQuery
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateQuery_87(nint ptr) : IHookMethod
    {
        public const string Name = "CreateQuery";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_QUERY_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_QUERY_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_QUERY_DESC* pQueryDesc, void** ppQuery) => _proc(pThis, pQueryDesc, ppQuery);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreatePredicate
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreatePredicate_88(nint ptr) : IHookMethod
    {
        public const string Name = "CreatePredicate";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_QUERY_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_QUERY_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_QUERY_DESC* pPredicateDesc, void** ppPredicate) => _proc(pThis, pPredicateDesc, ppPredicate);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CreateCounter
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CreateCounter_89(nint ptr) : IHookMethod
    {
        public const string Name = "CreateCounter";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_COUNTER_DESC*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_COUNTER_DESC*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_COUNTER_DESC* pCounterDesc, void** ppCounter) => _proc(pThis, pCounterDesc, ppCounter);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CheckFormatSupport
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CheckFormatSupport_90(nint ptr) : IHookMethod
    {
        public const string Name = "CheckFormatSupport";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, DXGI_FORMAT, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, DXGI_FORMAT, uint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, DXGI_FORMAT format, uint* pFormatSupport) => _proc(pThis, format, pFormatSupport);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CheckMultisampleQualityLevels
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CheckMultisampleQualityLevels_91(nint ptr) : IHookMethod
    {
        public const string Name = "CheckMultisampleQualityLevels";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, DXGI_FORMAT, uint, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, DXGI_FORMAT, uint, uint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, DXGI_FORMAT format, uint sampleCount, uint* pNumQualityLevels) => _proc(pThis, format, sampleCount, pNumQualityLevels);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CheckCounterInfo
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CheckCounterInfo_92(nint ptr) : IHookMethod
    {
        public const string Name = "CheckCounterInfo";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_COUNTER_INFO*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_COUNTER_INFO*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_COUNTER_INFO* pCounterInfo) => _proc(pThis, pCounterInfo);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::CheckCounter
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CheckCounter_93(nint ptr) : IHookMethod
    {
        public const string Name = "CheckCounter";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_COUNTER_DESC*, D3D10_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, D3D10_COUNTER_DESC*, D3D10_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, D3D10_COUNTER_DESC* pDesc, D3D10_COUNTER_TYPE* pType, uint* pActiveCounters, byte* szName, uint* pNameLength, byte* szUnits, uint* pUnitsLength, byte* szDescription, uint* pDescriptionLength) => _proc(pThis, pDesc, pType, pActiveCounters, szName, pNameLength, szUnits, pUnitsLength, szDescription, pDescriptionLength);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GetCreationFlags
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetCreationFlags_94(nint ptr) : IHookMethod
    {
        public const string Name = "GetCreationFlags";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint>)ptr;
        public nint PtrMethod => (nint)_proc;
        public uint Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::OpenSharedResource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_OpenSharedResource_95(nint ptr) : IHookMethod
    {
        public const string Name = "OpenSharedResource";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, void*, Guid*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, void* hResource, Guid* riid, void** ppResource) => _proc(pThis, hResource, riid, ppResource);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::SetTextFilterSize
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetTextFilterSize_96(nint ptr) : IHookMethod
    {
        public const string Name = "SetTextFilterSize";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint width, uint height) => _proc(pThis, width, height);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D10Device::GetTextFilterSize
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetTextFilterSize_97(nint ptr) : IHookMethod
    {
        public const string Name = "GetTextFilterSize";
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint*, uint*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D10DeviceImp>, uint*, uint*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D10DeviceImp> pThis, uint* pWidth, uint* pHeight) => _proc(pThis, pWidth, pHeight);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
