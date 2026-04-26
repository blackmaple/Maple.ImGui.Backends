using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11RenderTargetView;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Resource;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11ShaderResourceView;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Texture2D;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D11DeviceImp
    {
        public readonly static Guid GUID = new("DB6F6DDB-AC77-4E88-8253-819DF9BBF140");
        //  ID3D11Device


        internal readonly Ptr_Func_CreateBuffer_3 CreateBuffer_3;
        internal readonly Ptr_Func_CreateTexture1D_4 CreateTexture1D_4;
        internal readonly Ptr_Func_CreateTexture2D_5 CreateTexture2D_5;
        internal readonly Ptr_Func_CreateTexture3D_6 CreateTexture3D_6;
        internal readonly Ptr_Func_CreateShaderResourceView_7 CreateShaderResourceView_7;
        internal readonly Ptr_Func_CreateUnorderedAccessView_8 CreateUnorderedAccessView_8;
        internal readonly Ptr_Func_CreateRenderTargetView_9 CreateRenderTargetView_9;
        internal readonly Ptr_Func_CreateDepthStencilView_10 CreateDepthStencilView_10;
        internal readonly Ptr_Func_CreateInputLayout_11 CreateInputLayout_11;
        internal readonly Ptr_Func_CreateVertexShader_12 CreateVertexShader_12;
        internal readonly Ptr_Func_CreateGeometryShader_13 CreateGeometryShader_13;
        internal readonly Ptr_Func_CreateGeometryShaderWithStreamOutput_14 CreateGeometryShaderWithStreamOutput_14;
        internal readonly Ptr_Func_CreatePixelShader_15 CreatePixelShader_15;
        internal readonly Ptr_Func_CreateHullShader_16 CreateHullShader_16;
        internal readonly Ptr_Func_CreateDomainShader_17 CreateDomainShader_17;
        internal readonly Ptr_Func_CreateComputeShader_18 CreateComputeShader_18;
        internal readonly Ptr_Func_CreateClassLinkage_19 CreateClassLinkage_19;
        internal readonly Ptr_Func_CreateBlendState_20 CreateBlendState_20;
        internal readonly Ptr_Func_CreateDepthStencilState_21 CreateDepthStencilState_21;
        internal readonly Ptr_Func_CreateRasterizerState_22 CreateRasterizerState_22;
        internal readonly Ptr_Func_CreateSamplerState_23 CreateSamplerState_23;
        internal readonly Ptr_Func_CreateQuery_24 CreateQuery_24;
        internal readonly Ptr_Func_CreatePredicate_25 CreatePredicate_25;
        internal readonly Ptr_Func_CreateCounter_26 CreateCounter_26;
        internal readonly Ptr_Func_CreateDeferredContext_27 CreateDeferredContext_27;
        internal readonly Ptr_Func_OpenSharedResource_28 OpenSharedResource_28;
        internal readonly Ptr_Func_CheckFormatSupport_29 CheckFormatSupport_29;
        internal readonly Ptr_Func_CheckMultisampleQualityLevels_30 CheckMultisampleQualityLevels_30;
        internal readonly Ptr_Func_CheckCounterInfo_31 CheckCounterInfo_31;
        internal readonly Ptr_Func_CheckCounter_32 CheckCounter_32;
        internal readonly Ptr_Func_CheckFeatureSupport_33 CheckFeatureSupport_33;
        internal readonly Ptr_Func_GetPrivateData_34 GetPrivateData_34;
        internal readonly Ptr_Func_SetPrivateData_35 SetPrivateData_35;
        internal readonly Ptr_Func_SetPrivateDataInterface_36 SetPrivateDataInterface_36;
        internal readonly Ptr_Func_GetFeatureLevel_37 GetFeatureLevel_37;
        internal readonly Ptr_Func_GetCreationFlags_38 GetCreationFlags_38;
        internal readonly Ptr_Func_GetDeviceRemovedReason_39 GetDeviceRemovedReason_39;
        internal readonly Ptr_Func_GetImmediateContext_40 GetImmediateContext_40;
        internal readonly Ptr_Func_SetExceptionMode_41 SetExceptionMode_41;
        internal readonly Ptr_Func_GetExceptionMode_42 GetExceptionMode_42;
    }

    public static class ID3D11DeviceImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D11DeviceImp> @this)
        {
            public void GetImmediateContext(out COM_PTR_IUNKNOWN<ID3D11DeviceContextImp> pContext)
                => @this.Interface_VTable.GetImmediateContext_40.Invoke(@this, out pContext);

            public COM_HRESULT CreateRenderTargetView(COM_PTR_IUNKNOWN pResource,
                UnsafeOut<COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp>> ppRTView)
            {
                var pdesc_nullptr = new UnsafeIn<D3D11_RENDER_TARGET_VIEW_DESC>(nint.Zero);
                return @this.Interface_VTable.CreateRenderTargetView_9.Invoke(@this, pResource, pdesc_nullptr, ppRTView);
            }

            public COM_HRESULT CreateRenderTargetView(COM_PTR_IUNKNOWN<ID3D11Texture2DImp> pResource,
                out COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> pRTView)
            {
                return @this.CreateRenderTargetView(pResource, UnsafeOut<COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp>>.FromOut(out pRTView));
            }

            public bool TryCreateBackbufferRTV(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, out COM_PTR_IUNKNOWN<ID3D11RenderTargetViewImp> pRTView)
            {
                Unsafe.SkipInit(out pRTView);
                if (pSwapChain.GetBuffer<ID3D11Texture2DImp>(in ID3D11Texture2DImp.GUID, out var pBackBuffer))
                {
                    using (pBackBuffer)
                    {
                        return @this.CreateRenderTargetView(pBackBuffer, out pRTView);
                    }
                }
                return false;
            }

            internal COM_HRESULT CreateShaderResourceView(COM_PTR_IUNKNOWN pResource, in D3D11_SHADER_RESOURCE_VIEW_DESC srvDesc,
               out COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp> pSRV)
            {
                return @this.Interface_VTable.CreateShaderResourceView_7.Invoke(@this, pResource, 
                    UnsafeIn<D3D11_SHADER_RESOURCE_VIEW_DESC>.FromIn(in srvDesc), UnsafeOut<COM_PTR_IUNKNOWN<ID3D11ShaderResourceViewImp>>.FromOut(out pSRV));
            }

        }

    }
    /*
        public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_BUFFER_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SUBRESOURCE_DATA*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Buffer_unmanaged**, int> CreateBuffer_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_TEXTURE1D_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SUBRESOURCE_DATA*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Texture1D_unmanaged**, int> CreateTexture1D_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_TEXTURE2D_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SUBRESOURCE_DATA*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Texture2D_unmanaged**, int> CreateTexture2D_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_TEXTURE3D_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SUBRESOURCE_DATA*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Texture3D_unmanaged**, int> CreateTexture3D_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SHADER_RESOURCE_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11ShaderResourceView_unmanaged**, int> CreateShaderResourceView_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D11.D3D11_UNORDERED_ACCESS_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11UnorderedAccessView_unmanaged**, int> CreateUnorderedAccessView_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D11.D3D11_RENDER_TARGET_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11RenderTargetView_unmanaged**, int> CreateRenderTargetView_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::Windows.Win32.Graphics.Direct3D11.D3D11_DEPTH_STENCIL_VIEW_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DepthStencilView_unmanaged**, int> CreateDepthStencilView_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_INPUT_ELEMENT_DESC*, uint, void*, nuint, global::Windows.Win32.Graphics.Direct3D11.ID3D11InputLayout_unmanaged**, int> CreateInputLayout_11;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11VertexShader_unmanaged**, int> CreateVertexShader_12;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11GeometryShader_unmanaged**, int> CreateGeometryShader_13;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, global::Windows.Win32.Graphics.Direct3D11.D3D11_SO_DECLARATION_ENTRY*, uint, uint*, uint, uint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11GeometryShader_unmanaged**, int> CreateGeometryShaderWithStreamOutput_14;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11PixelShader_unmanaged**, int> CreatePixelShader_15;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11HullShader_unmanaged**, int> CreateHullShader_16;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DomainShader_unmanaged**, int> CreateDomainShader_17;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, nuint, void*, global::Windows.Win32.Graphics.Direct3D11.ID3D11ComputeShader_unmanaged**, int> CreateComputeShader_18;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> CreateClassLinkage_19;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_BLEND_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11BlendState_unmanaged**, int> CreateBlendState_20;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_DEPTH_STENCIL_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11DepthStencilState_unmanaged**, int> CreateDepthStencilState_21;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_RASTERIZER_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11RasterizerState_unmanaged**, int> CreateRasterizerState_22;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_SAMPLER_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11SamplerState_unmanaged**, int> CreateSamplerState_23;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_QUERY_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Query_unmanaged**, int> CreateQuery_24;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_QUERY_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Predicate_unmanaged**, int> CreatePredicate_25;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_DESC*, global::Windows.Win32.Graphics.Direct3D11.ID3D11Counter_unmanaged**, int> CreateCounter_26;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D11.ID3D11DeviceContext_unmanaged**, int> CreateDeferredContext_27;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, global::System.Guid*, void**, int> OpenSharedResource_28;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint*, int> CheckFormatSupport_29;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, uint*, int> CheckMultisampleQualityLevels_30;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_INFO*, void> CheckCounterInfo_31;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_DESC*, global::Windows.Win32.Graphics.Direct3D11.D3D11_COUNTER_TYPE*, uint*, byte*, uint*, byte*, uint*, byte*, uint*, int> CheckCounter_32;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D11.D3D11_FEATURE, void*, uint, int> CheckFeatureSupport_33;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_34;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_35;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_36;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D.D3D_FEATURE_LEVEL> GetFeatureLevel_37;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetCreationFlags_38;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> GetDeviceRemovedReason_39;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, void> GetImmediateContext_40;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, int> SetExceptionMode_41;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetExceptionMode_42;

     */
}
