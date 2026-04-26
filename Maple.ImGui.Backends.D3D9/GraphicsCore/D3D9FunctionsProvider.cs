using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3D9;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.Windows.GraphicsCore.Native;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore
{
    internal partial class D3D9FunctionsProvider : GraphicsFunctionsProvider, IGraphicsFunctions<D3D9FunctionsProvider>
    {

        public static D3D9FunctionsProvider Create(IServiceProvider provider)
        {
            Win32WindowFactory tempWindowFactory = provider.GetRequiredService<Win32WindowFactory>();

            using var pDirect3D9 = CreateIDirect3D9Imp();
            using var pDirect3DDevice9 = CreateIDirect3DDevice9Imp(pDirect3D9, tempWindowFactory);
            var functionsProvider = new D3D9FunctionsProvider();
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_TestCooperativeLevel_3.Name, pDirect3DDevice9.Interface_VTable.TestCooperativeLevel_3.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetAvailableTextureMem_4.Name, pDirect3DDevice9.Interface_VTable.GetAvailableTextureMem_4.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_EvictManagedResources_5.Name, pDirect3DDevice9.Interface_VTable.EvictManagedResources_5.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetDirect3D_6.Name, pDirect3DDevice9.Interface_VTable.GetDirect3D_6.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetDeviceCaps_7.Name, pDirect3DDevice9.Interface_VTable.GetDeviceCaps_7.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetDisplayMode_8.Name, pDirect3DDevice9.Interface_VTable.GetDisplayMode_8.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetCreationParameters_9.Name, pDirect3DDevice9.Interface_VTable.GetCreationParameters_9.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetCursorProperties_10.Name, pDirect3DDevice9.Interface_VTable.SetCursorProperties_10.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetCursorPosition_11.Name, pDirect3DDevice9.Interface_VTable.SetCursorPosition_11.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_ShowCursor_12.Name, pDirect3DDevice9.Interface_VTable.ShowCursor_12.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateAdditionalSwapChain_13.Name, pDirect3DDevice9.Interface_VTable.CreateAdditionalSwapChain_13.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetSwapChain_14.Name, pDirect3DDevice9.Interface_VTable.GetSwapChain_14.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetNumberOfSwapChains_15.Name, pDirect3DDevice9.Interface_VTable.GetNumberOfSwapChains_15.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_Reset_16.Name, pDirect3DDevice9.Interface_VTable.Reset_16.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_Present_17.Name, pDirect3DDevice9.Interface_VTable.Present_17.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetBackBuffer_18.Name, pDirect3DDevice9.Interface_VTable.GetBackBuffer_18.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetRasterStatus_19.Name, pDirect3DDevice9.Interface_VTable.GetRasterStatus_19.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetDialogBoxMode_20.Name, pDirect3DDevice9.Interface_VTable.SetDialogBoxMode_20.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetGammaRamp_21.Name, pDirect3DDevice9.Interface_VTable.SetGammaRamp_21.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetGammaRamp_22.Name, pDirect3DDevice9.Interface_VTable.GetGammaRamp_22.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateTexture_23.Name, pDirect3DDevice9.Interface_VTable.CreateTexture_23.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateVolumeTexture_24.Name, pDirect3DDevice9.Interface_VTable.CreateVolumeTexture_24.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateCubeTexture_25.Name, pDirect3DDevice9.Interface_VTable.CreateCubeTexture_25.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateVertexBuffer_26.Name, pDirect3DDevice9.Interface_VTable.CreateVertexBuffer_26.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateIndexBuffer_27.Name, pDirect3DDevice9.Interface_VTable.CreateIndexBuffer_27.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateRenderTarget_28.Name, pDirect3DDevice9.Interface_VTable.CreateRenderTarget_28.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateDepthStencilSurface_29.Name, pDirect3DDevice9.Interface_VTable.CreateDepthStencilSurface_29.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_UpdateSurface_30.Name, pDirect3DDevice9.Interface_VTable.UpdateSurface_30.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_UpdateTexture_31.Name, pDirect3DDevice9.Interface_VTable.UpdateTexture_31.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetRenderTargetData_32.Name, pDirect3DDevice9.Interface_VTable.GetRenderTargetData_32.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetFrontBufferData_33.Name, pDirect3DDevice9.Interface_VTable.GetFrontBufferData_33.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_StretchRect_34.Name, pDirect3DDevice9.Interface_VTable.StretchRect_34.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_ColorFill_35.Name, pDirect3DDevice9.Interface_VTable.ColorFill_35.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateOffscreenPlainSurface_36.Name, pDirect3DDevice9.Interface_VTable.CreateOffscreenPlainSurface_36.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetRenderTarget_37.Name, pDirect3DDevice9.Interface_VTable.SetRenderTarget_37.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetRenderTarget_38.Name, pDirect3DDevice9.Interface_VTable.GetRenderTarget_38.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetDepthStencilSurface_39.Name, pDirect3DDevice9.Interface_VTable.SetDepthStencilSurface_39.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetDepthStencilSurface_40.Name, pDirect3DDevice9.Interface_VTable.GetDepthStencilSurface_40.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_BeginScene_41.Name, pDirect3DDevice9.Interface_VTable.BeginScene_41.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_EndScene_42.Name, pDirect3DDevice9.Interface_VTable.EndScene_42.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_Clear_43.Name, pDirect3DDevice9.Interface_VTable.Clear_43.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetTransform_44.Name, pDirect3DDevice9.Interface_VTable.SetTransform_44.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetTransform_45.Name, pDirect3DDevice9.Interface_VTable.GetTransform_45.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_MultiplyTransform_46.Name, pDirect3DDevice9.Interface_VTable.MultiplyTransform_46.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetViewport_47.Name, pDirect3DDevice9.Interface_VTable.SetViewport_47.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetViewport_48.Name, pDirect3DDevice9.Interface_VTable.GetViewport_48.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetMaterial_49.Name, pDirect3DDevice9.Interface_VTable.SetMaterial_49.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetMaterial_50.Name, pDirect3DDevice9.Interface_VTable.GetMaterial_50.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetLight_51.Name, pDirect3DDevice9.Interface_VTable.SetLight_51.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetLight_52.Name, pDirect3DDevice9.Interface_VTable.GetLight_52.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_LightEnable_53.Name, pDirect3DDevice9.Interface_VTable.LightEnable_53.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetLightEnable_54.Name, pDirect3DDevice9.Interface_VTable.GetLightEnable_54.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetClipPlane_55.Name, pDirect3DDevice9.Interface_VTable.SetClipPlane_55.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetClipPlane_56.Name, pDirect3DDevice9.Interface_VTable.GetClipPlane_56.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetRenderState_57.Name, pDirect3DDevice9.Interface_VTable.SetRenderState_57.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetRenderState_58.Name, pDirect3DDevice9.Interface_VTable.GetRenderState_58.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateStateBlock_59.Name, pDirect3DDevice9.Interface_VTable.CreateStateBlock_59.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_BeginStateBlock_60.Name, pDirect3DDevice9.Interface_VTable.BeginStateBlock_60.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_EndStateBlock_61.Name, pDirect3DDevice9.Interface_VTable.EndStateBlock_61.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetClipStatus_62.Name, pDirect3DDevice9.Interface_VTable.SetClipStatus_62.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetClipStatus_63.Name, pDirect3DDevice9.Interface_VTable.GetClipStatus_63.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetTexture_64.Name, pDirect3DDevice9.Interface_VTable.GetTexture_64.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetTexture_65.Name, pDirect3DDevice9.Interface_VTable.SetTexture_65.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetTextureStageState_66.Name, pDirect3DDevice9.Interface_VTable.GetTextureStageState_66.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetTextureStageState_67.Name, pDirect3DDevice9.Interface_VTable.SetTextureStageState_67.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetSamplerState_68.Name, pDirect3DDevice9.Interface_VTable.GetSamplerState_68.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetSamplerState_69.Name, pDirect3DDevice9.Interface_VTable.SetSamplerState_69.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_ValidateDevice_70.Name, pDirect3DDevice9.Interface_VTable.ValidateDevice_70.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetPaletteEntries_71.Name, pDirect3DDevice9.Interface_VTable.SetPaletteEntries_71.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetPaletteEntries_72.Name, pDirect3DDevice9.Interface_VTable.GetPaletteEntries_72.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetCurrentTexturePalette_73.Name, pDirect3DDevice9.Interface_VTable.SetCurrentTexturePalette_73.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetCurrentTexturePalette_74.Name, pDirect3DDevice9.Interface_VTable.GetCurrentTexturePalette_74.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetScissorRect_75.Name, pDirect3DDevice9.Interface_VTable.SetScissorRect_75.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetScissorRect_76.Name, pDirect3DDevice9.Interface_VTable.GetScissorRect_76.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetSoftwareVertexProcessing_77.Name, pDirect3DDevice9.Interface_VTable.SetSoftwareVertexProcessing_77.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetSoftwareVertexProcessing_78.Name, pDirect3DDevice9.Interface_VTable.GetSoftwareVertexProcessing_78.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetNPatchMode_79.Name, pDirect3DDevice9.Interface_VTable.SetNPatchMode_79.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetNPatchMode_80.Name, pDirect3DDevice9.Interface_VTable.GetNPatchMode_80.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DrawPrimitive_81.Name, pDirect3DDevice9.Interface_VTable.DrawPrimitive_81.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DrawIndexedPrimitive_82.Name, pDirect3DDevice9.Interface_VTable.DrawIndexedPrimitive_82.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DrawPrimitiveUP_83.Name, pDirect3DDevice9.Interface_VTable.DrawPrimitiveUP_83.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DrawIndexedPrimitiveUP_84.Name, pDirect3DDevice9.Interface_VTable.DrawIndexedPrimitiveUP_84.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_ProcessVertices_85.Name, pDirect3DDevice9.Interface_VTable.ProcessVertices_85.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateVertexDeclaration_86.Name, pDirect3DDevice9.Interface_VTable.CreateVertexDeclaration_86.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetVertexDeclaration_87.Name, pDirect3DDevice9.Interface_VTable.SetVertexDeclaration_87.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetVertexDeclaration_88.Name, pDirect3DDevice9.Interface_VTable.GetVertexDeclaration_88.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetFVF_89.Name, pDirect3DDevice9.Interface_VTable.SetFVF_89.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetFVF_90.Name, pDirect3DDevice9.Interface_VTable.GetFVF_90.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateVertexShader_91.Name, pDirect3DDevice9.Interface_VTable.CreateVertexShader_91.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetVertexShader_92.Name, pDirect3DDevice9.Interface_VTable.SetVertexShader_92.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetVertexShader_93.Name, pDirect3DDevice9.Interface_VTable.GetVertexShader_93.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetVertexShaderConstantF_94.Name, pDirect3DDevice9.Interface_VTable.SetVertexShaderConstantF_94.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetVertexShaderConstantF_95.Name, pDirect3DDevice9.Interface_VTable.GetVertexShaderConstantF_95.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetVertexShaderConstantI_96.Name, pDirect3DDevice9.Interface_VTable.SetVertexShaderConstantI_96.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetVertexShaderConstantI_97.Name, pDirect3DDevice9.Interface_VTable.GetVertexShaderConstantI_97.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetVertexShaderConstantB_98.Name, pDirect3DDevice9.Interface_VTable.SetVertexShaderConstantB_98.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetVertexShaderConstantB_99.Name, pDirect3DDevice9.Interface_VTable.GetVertexShaderConstantB_99.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetStreamSource_100.Name, pDirect3DDevice9.Interface_VTable.SetStreamSource_100.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetStreamSource_101.Name, pDirect3DDevice9.Interface_VTable.GetStreamSource_101.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetStreamSourceFreq_102.Name, pDirect3DDevice9.Interface_VTable.SetStreamSourceFreq_102.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetStreamSourceFreq_103.Name, pDirect3DDevice9.Interface_VTable.GetStreamSourceFreq_103.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetIndices_104.Name, pDirect3DDevice9.Interface_VTable.SetIndices_104.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetIndices_105.Name, pDirect3DDevice9.Interface_VTable.GetIndices_105.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreatePixelShader_106.Name, pDirect3DDevice9.Interface_VTable.CreatePixelShader_106.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetPixelShader_107.Name, pDirect3DDevice9.Interface_VTable.SetPixelShader_107.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetPixelShader_108.Name, pDirect3DDevice9.Interface_VTable.GetPixelShader_108.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetPixelShaderConstantF_109.Name, pDirect3DDevice9.Interface_VTable.SetPixelShaderConstantF_109.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetPixelShaderConstantF_110.Name, pDirect3DDevice9.Interface_VTable.GetPixelShaderConstantF_110.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetPixelShaderConstantI_111.Name, pDirect3DDevice9.Interface_VTable.SetPixelShaderConstantI_111.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetPixelShaderConstantI_112.Name, pDirect3DDevice9.Interface_VTable.GetPixelShaderConstantI_112.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_SetPixelShaderConstantB_113.Name, pDirect3DDevice9.Interface_VTable.SetPixelShaderConstantB_113.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_GetPixelShaderConstantB_114.Name, pDirect3DDevice9.Interface_VTable.GetPixelShaderConstantB_114.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DrawRectPatch_115.Name, pDirect3DDevice9.Interface_VTable.DrawRectPatch_115.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DrawTriPatch_116.Name, pDirect3DDevice9.Interface_VTable.DrawTriPatch_116.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_DeletePatch_117.Name, pDirect3DDevice9.Interface_VTable.DeletePatch_117.PtrMethod);
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_CreateQuery_118.Name, pDirect3DDevice9.Interface_VTable.CreateQuery_118.PtrMethod);

            return functionsProvider;
        }


        private static Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3D9Imp> CreateIDirect3D9Imp()
        {
            return Direct3DCreate9(PInvoke.D3D_SDK_VERSION);
        }

        private static Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> CreateIDirect3DDevice9Imp(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3D9Imp> pDirect3D9, Win32WindowFactory tempWindowFactory)
        {
            using var frm = tempWindowFactory.Create();
            HWND hWnd = new(frm.Handle);
            var createStatus = pDirect3D9.Interface_VTable.CreateDevice_16.Invoke(pDirect3D9,
                        PInvoke.D3DADAPTER_DEFAULT,
                    D3DDEVTYPE.D3DDEVTYPE_NULLREF,
                    hWnd,
                    PInvoke.D3DCREATE_SOFTWARE_VERTEXPROCESSING | PInvoke.D3DCREATE_DISABLE_DRIVER_MANAGEMENT,
                    new D3DPRESENT_PARAMETERS()
                    {
                        BackBufferWidth = 0,
                        BackBufferHeight = 0,
                        BackBufferFormat = D3DFORMAT.D3DFMT_UNKNOWN,
                        BackBufferCount = 0,
                        MultiSampleType = D3DMULTISAMPLE_TYPE.D3DMULTISAMPLE_NONE,
                        SwapEffect = D3DSWAPEFFECT.D3DSWAPEFFECT_DISCARD,
                        hDeviceWindow = hWnd,
                        Windowed = true,
                        EnableAutoDepthStencil = false,
                        AutoDepthStencilFormat = D3DFORMAT.D3DFMT_UNKNOWN,
                        FullScreen_RefreshRateInHz = 0,

                    },
                    out var pDirect3DDevice9
                    );
            if (createStatus == false)
            {
                return GraphicsException.Throw<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>>($"{nameof(CreateIDirect3DDevice9Imp)}:{createStatus}");
            }

            return pDirect3DDevice9;
        }



        const string LibraryName = "d3d9.dll";
        const string EntryPoint = "Direct3DCreate9";


        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        [LibraryImport(LibraryName, EntryPoint = EntryPoint)]
        internal static partial Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3D9Imp> Direct3DCreate9(uint SDKVersion);

    }
}
