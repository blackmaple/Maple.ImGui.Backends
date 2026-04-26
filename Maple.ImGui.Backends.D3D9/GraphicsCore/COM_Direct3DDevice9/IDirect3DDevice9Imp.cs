using Maple.ImGui.Backends.Windows.GraphicsCore.COM;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IDirect3DDevice9Imp
    {
         

        public static readonly Guid GUID = new("D0223B96-BF7A-43fd-92BD-A43B0D82B9EB");
        internal Ptr_Func_TestCooperativeLevel_3 TestCooperativeLevel_3;
        internal Ptr_Func_GetAvailableTextureMem_4 GetAvailableTextureMem_4;
        internal Ptr_Func_EvictManagedResources_5 EvictManagedResources_5;
        internal Ptr_Func_GetDirect3D_6 GetDirect3D_6;
        internal Ptr_Func_GetDeviceCaps_7 GetDeviceCaps_7;
        internal Ptr_Func_GetDisplayMode_8 GetDisplayMode_8;
        internal Ptr_Func_GetCreationParameters_9 GetCreationParameters_9;
        internal Ptr_Func_SetCursorProperties_10 SetCursorProperties_10;
        internal Ptr_Func_SetCursorPosition_11 SetCursorPosition_11;
        internal Ptr_Func_ShowCursor_12 ShowCursor_12;
        internal Ptr_Func_CreateAdditionalSwapChain_13 CreateAdditionalSwapChain_13;
        internal Ptr_Func_GetSwapChain_14 GetSwapChain_14;
        internal Ptr_Func_GetNumberOfSwapChains_15 GetNumberOfSwapChains_15;
        internal Ptr_Func_Reset_16 Reset_16;
        internal Ptr_Func_Present_17 Present_17;
        internal Ptr_Func_GetBackBuffer_18 GetBackBuffer_18;
        internal Ptr_Func_GetRasterStatus_19 GetRasterStatus_19;
        internal Ptr_Func_SetDialogBoxMode_20 SetDialogBoxMode_20;
        internal Ptr_Func_SetGammaRamp_21 SetGammaRamp_21;
        internal Ptr_Func_GetGammaRamp_22 GetGammaRamp_22;
        internal Ptr_Func_CreateTexture_23 CreateTexture_23;
        internal Ptr_Func_CreateVolumeTexture_24 CreateVolumeTexture_24;
        internal Ptr_Func_CreateCubeTexture_25 CreateCubeTexture_25;
        internal Ptr_Func_CreateVertexBuffer_26 CreateVertexBuffer_26;
        internal Ptr_Func_CreateIndexBuffer_27 CreateIndexBuffer_27;
        internal Ptr_Func_CreateRenderTarget_28 CreateRenderTarget_28;
        internal Ptr_Func_CreateDepthStencilSurface_29 CreateDepthStencilSurface_29;
        internal Ptr_Func_UpdateSurface_30 UpdateSurface_30;
        internal Ptr_Func_UpdateTexture_31 UpdateTexture_31;
        internal Ptr_Func_GetRenderTargetData_32 GetRenderTargetData_32;
        internal Ptr_Func_GetFrontBufferData_33 GetFrontBufferData_33;
        internal Ptr_Func_StretchRect_34 StretchRect_34;
        internal Ptr_Func_ColorFill_35 ColorFill_35;
        internal Ptr_Func_CreateOffscreenPlainSurface_36 CreateOffscreenPlainSurface_36;
        internal Ptr_Func_SetRenderTarget_37 SetRenderTarget_37;
        internal Ptr_Func_GetRenderTarget_38 GetRenderTarget_38;
        internal Ptr_Func_SetDepthStencilSurface_39 SetDepthStencilSurface_39;
        internal Ptr_Func_GetDepthStencilSurface_40 GetDepthStencilSurface_40;
        internal Ptr_Func_BeginScene_41 BeginScene_41;
        internal Ptr_Func_EndScene_42 EndScene_42;
        internal Ptr_Func_Clear_43 Clear_43;
        internal Ptr_Func_SetTransform_44 SetTransform_44;
        internal Ptr_Func_GetTransform_45 GetTransform_45;
        internal Ptr_Func_MultiplyTransform_46 MultiplyTransform_46;
        internal Ptr_Func_SetViewport_47 SetViewport_47;
        internal Ptr_Func_GetViewport_48 GetViewport_48;
        internal Ptr_Func_SetMaterial_49 SetMaterial_49;
        internal Ptr_Func_GetMaterial_50 GetMaterial_50;
        internal Ptr_Func_SetLight_51 SetLight_51;
        internal Ptr_Func_GetLight_52 GetLight_52;
        internal Ptr_Func_LightEnable_53 LightEnable_53;
        internal Ptr_Func_GetLightEnable_54 GetLightEnable_54;
        internal Ptr_Func_SetClipPlane_55 SetClipPlane_55;
        internal Ptr_Func_GetClipPlane_56 GetClipPlane_56;
        internal Ptr_Func_SetRenderState_57 SetRenderState_57;
        internal Ptr_Func_GetRenderState_58 GetRenderState_58;
        internal Ptr_Func_CreateStateBlock_59 CreateStateBlock_59;
        internal Ptr_Func_BeginStateBlock_60 BeginStateBlock_60;
        internal Ptr_Func_EndStateBlock_61 EndStateBlock_61;
        internal Ptr_Func_SetClipStatus_62 SetClipStatus_62;
        internal Ptr_Func_GetClipStatus_63 GetClipStatus_63;
        internal Ptr_Func_GetTexture_64 GetTexture_64;
        internal Ptr_Func_SetTexture_65 SetTexture_65;
        internal Ptr_Func_GetTextureStageState_66 GetTextureStageState_66;
        internal Ptr_Func_SetTextureStageState_67 SetTextureStageState_67;
        internal Ptr_Func_GetSamplerState_68 GetSamplerState_68;
        internal Ptr_Func_SetSamplerState_69 SetSamplerState_69;
        internal Ptr_Func_ValidateDevice_70 ValidateDevice_70;
        internal Ptr_Func_SetPaletteEntries_71 SetPaletteEntries_71;
        internal Ptr_Func_GetPaletteEntries_72 GetPaletteEntries_72;
        internal Ptr_Func_SetCurrentTexturePalette_73 SetCurrentTexturePalette_73;
        internal Ptr_Func_GetCurrentTexturePalette_74 GetCurrentTexturePalette_74;
        internal Ptr_Func_SetScissorRect_75 SetScissorRect_75;
        internal Ptr_Func_GetScissorRect_76 GetScissorRect_76;
        internal Ptr_Func_SetSoftwareVertexProcessing_77 SetSoftwareVertexProcessing_77;
        internal Ptr_Func_GetSoftwareVertexProcessing_78 GetSoftwareVertexProcessing_78;
        internal Ptr_Func_SetNPatchMode_79 SetNPatchMode_79;
        internal Ptr_Func_GetNPatchMode_80 GetNPatchMode_80;
        internal Ptr_Func_DrawPrimitive_81 DrawPrimitive_81;
        internal Ptr_Func_DrawIndexedPrimitive_82 DrawIndexedPrimitive_82;
        internal Ptr_Func_DrawPrimitiveUP_83 DrawPrimitiveUP_83;
        internal Ptr_Func_DrawIndexedPrimitiveUP_84 DrawIndexedPrimitiveUP_84;
        internal Ptr_Func_ProcessVertices_85 ProcessVertices_85;
        internal Ptr_Func_CreateVertexDeclaration_86 CreateVertexDeclaration_86;
        internal Ptr_Func_SetVertexDeclaration_87 SetVertexDeclaration_87;
        internal Ptr_Func_GetVertexDeclaration_88 GetVertexDeclaration_88;
        internal Ptr_Func_SetFVF_89 SetFVF_89;
        internal Ptr_Func_GetFVF_90 GetFVF_90;
        internal Ptr_Func_CreateVertexShader_91 CreateVertexShader_91;
        internal Ptr_Func_SetVertexShader_92 SetVertexShader_92;
        internal Ptr_Func_GetVertexShader_93 GetVertexShader_93;
        internal Ptr_Func_SetVertexShaderConstantF_94 SetVertexShaderConstantF_94;
        internal Ptr_Func_GetVertexShaderConstantF_95 GetVertexShaderConstantF_95;
        internal Ptr_Func_SetVertexShaderConstantI_96 SetVertexShaderConstantI_96;
        internal Ptr_Func_GetVertexShaderConstantI_97 GetVertexShaderConstantI_97;
        internal Ptr_Func_SetVertexShaderConstantB_98 SetVertexShaderConstantB_98;
        internal Ptr_Func_GetVertexShaderConstantB_99 GetVertexShaderConstantB_99;
        internal Ptr_Func_SetStreamSource_100 SetStreamSource_100;
        internal Ptr_Func_GetStreamSource_101 GetStreamSource_101;
        internal Ptr_Func_SetStreamSourceFreq_102 SetStreamSourceFreq_102;
        internal Ptr_Func_GetStreamSourceFreq_103 GetStreamSourceFreq_103;
        internal Ptr_Func_SetIndices_104 SetIndices_104;
        internal Ptr_Func_GetIndices_105 GetIndices_105;
        internal Ptr_Func_CreatePixelShader_106 CreatePixelShader_106;
        internal Ptr_Func_SetPixelShader_107 SetPixelShader_107;
        internal Ptr_Func_GetPixelShader_108 GetPixelShader_108;
        internal Ptr_Func_SetPixelShaderConstantF_109 SetPixelShaderConstantF_109;
        internal Ptr_Func_GetPixelShaderConstantF_110 GetPixelShaderConstantF_110;
        internal Ptr_Func_SetPixelShaderConstantI_111 SetPixelShaderConstantI_111;
        internal Ptr_Func_GetPixelShaderConstantI_112 GetPixelShaderConstantI_112;
        internal Ptr_Func_SetPixelShaderConstantB_113 SetPixelShaderConstantB_113;
        internal Ptr_Func_GetPixelShaderConstantB_114 GetPixelShaderConstantB_114;
        internal Ptr_Func_DrawRectPatch_115 DrawRectPatch_115;
        internal Ptr_Func_DrawTriPatch_116 DrawTriPatch_116;
        internal Ptr_Func_DeletePatch_117 DeletePatch_117;
        internal Ptr_Func_CreateQuery_118 CreateQuery_118;
    }


    public static class IDirect3DDevice9ImpExtension
    {
        extension(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this)
        {
            internal COM_HRESULT GetCreationParameters(out D3DDEVICE_CREATION_PARAMETERS pParameters)
            {
                return @this.Interface_VTable.GetCreationParameters_9.Invoke(@this,  out   pParameters);
            }

            //public nint GetFocusWindow()
            //    => @this.GetCreationParameters(out var pParameters) ? pParameters.hFocusWindow : nint.Zero;
        }
    }
}
