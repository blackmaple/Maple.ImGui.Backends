using Maple.ImGui.Backends.DXGI.COM_DXGIAdapter;
using Maple.ImGui.Backends.DXGI.COM_DXGIDevice;
using Maple.ImGui.Backends.DXGI.COM_DXGIFactory;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.Windows.GraphicsCore.Native;
using Maple.UnmanagedExtensions;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.RenderSpy.Graphics.DXGI
{
    public partial class DXGIFunctionsProvider
    {
        #region D3D10&D3D11
        public static T Create<T>(COM_PTR_IUNKNOWN pDevice, IServiceProvider provider)
            where T : GraphicsFunctionsProvider, new()
        {

            using var pDXGIDevice = CreateIDXGIDeviceImp(pDevice);
            using var pAdapter = CreateIDXGIAdapterImp(pDXGIDevice);
            using var pFactory = CreateIDXGIFactoryImp(pAdapter);
            using var pSwapChain = CreateIDXGISwapChainImp(pFactory, pDevice, provider.GetRequiredService<Win32WindowFactory>());
            return BuildGraphicsFunctionsProvider<T>(pSwapChain);
        }


        private static COM_PTR_IUNKNOWN<IDXGIDeviceImp> CreateIDXGIDeviceImp(COM_PTR_IUNKNOWN pDevice)
        {
            var hResult = pDevice.QueryInterface<IDXGIDeviceImp>(IDXGIDeviceImp.GUID, out var pDXGIDevice);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGIDeviceImp>>($"{nameof(CreateIDXGIDeviceImp)}:{hResult}");
            }
            return pDXGIDevice;
        }
        private static COM_PTR_IUNKNOWN<IDXGIAdapterImp> CreateIDXGIAdapterImp(COM_PTR_IUNKNOWN<IDXGIDeviceImp> pDXGIDevice)
        {
            var hResult = pDXGIDevice.GetAdapter(out var pAdapter);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>($"{nameof(CreateIDXGIAdapterImp)}:{hResult}");
            }
            return pAdapter;
        }
        private static COM_PTR_IUNKNOWN<IDXGIFactoryImp> CreateIDXGIFactoryImp(COM_PTR_IUNKNOWN<IDXGIAdapterImp> pAdapter)
        {

            var hResult = pAdapter.GetParent<IDXGIFactoryImp>(in IDXGIFactoryImp.GUID, out var ppParent);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGIFactoryImp>>($"{nameof(CreateIDXGIFactoryImp)}:{hResult}");
            }
            return ppParent;
        }
        private static COM_PTR_IUNKNOWN<IDXGISwapChainImp> CreateIDXGISwapChainImp(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pFactory,COM_PTR_IUNKNOWN pDevice,Win32WindowFactory windowFactory)
        {
            using var from = windowFactory.Create();
            var swapChainDesc = new DXGI_SWAP_CHAIN_DESC
            {
                BufferDesc = new DXGI_MODE_DESC
                {
                    Width = 100,
                    Height = 100,
                    Format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM,
                    Scaling = DXGI_MODE_SCALING.DXGI_MODE_SCALING_UNSPECIFIED,
                    ScanlineOrdering = DXGI_MODE_SCANLINE_ORDER.DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED,
                    RefreshRate = new DXGI_RATIONAL { Numerator = 60, Denominator = 1 }
                },
                SampleDesc = new DXGI_SAMPLE_DESC { Count = 1, Quality = 0 },
                BufferUsage = DXGI_USAGE.DXGI_USAGE_RENDER_TARGET_OUTPUT,
                BufferCount = 1,
                OutputWindow = new HWND(from.Handle),
                Windowed = true,
                SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_DISCARD,
       //         Flags = DXGI_SWAP_CHAIN_FLAG.DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH

            };
            var hResult = pFactory.CreateSwapChain(pDevice, in swapChainDesc, out var ppSwapChain);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGISwapChainImp>>($"{nameof(CreateIDXGISwapChainImp)}:{hResult}");
            }
            return ppSwapChain;
        }
        private static COM_PTR_IUNKNOWN<IDXGISwapChainImp> CreateIDXGISwapChainImpD3D12(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pFactory, COM_PTR_IUNKNOWN pDevice, Win32WindowFactory windowFactory)
        {
            using var from = windowFactory.Create();
            var swapChainDesc = new DXGI_SWAP_CHAIN_DESC
            {
                BufferDesc = new DXGI_MODE_DESC
                {
                    Width = 100,
                    Height = 100,
                    Format = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM,
                    Scaling = DXGI_MODE_SCALING.DXGI_MODE_SCALING_UNSPECIFIED,
                    ScanlineOrdering = DXGI_MODE_SCANLINE_ORDER.DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED,
                    RefreshRate = new DXGI_RATIONAL { Numerator = 60, Denominator = 1 }
                },
                SampleDesc = new DXGI_SAMPLE_DESC { Count = 1, Quality = 0 },
                BufferUsage = DXGI_USAGE.DXGI_USAGE_RENDER_TARGET_OUTPUT,
                BufferCount = 2,
                OutputWindow = new HWND(from.Handle),
                Windowed = true,
                SwapEffect = DXGI_SWAP_EFFECT.DXGI_SWAP_EFFECT_FLIP_DISCARD,
                Flags = DXGI_SWAP_CHAIN_FLAG.DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH

            };
            var hResult = pFactory.CreateSwapChain(pDevice, in swapChainDesc, out var ppSwapChain);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGISwapChainImp>>($"{nameof(CreateIDXGISwapChainImpD3D12)}:{hResult}");
            }
            return ppSwapChain;
        }

        private static T BuildGraphicsFunctionsProvider<T>(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
            where T : GraphicsFunctionsProvider, new()
        {
            var functions = new T();
            functions.TryAddGraphicsFunctions(ImGui.Backends.DXGI.COM_DXGISwapChain.Ptr_Func_SetPrivateData_3.Name, pSwapChain.Interface_VTable.SetPrivateData_3.PtrMethod);
            functions.TryAddGraphicsFunctions(ImGui.Backends.DXGI.COM_DXGISwapChain.Ptr_Func_SetPrivateDataInterface_4.Name, pSwapChain.Interface_VTable.SetPrivateDataInterface_4.PtrMethod);
            functions.TryAddGraphicsFunctions(ImGui.Backends.DXGI.COM_DXGISwapChain.Ptr_Func_GetPrivateData_5.Name, pSwapChain.Interface_VTable.GetPrivateData_5.PtrMethod);
            functions.TryAddGraphicsFunctions(ImGui.Backends.DXGI.COM_DXGISwapChain.Ptr_Func_GetParent_6.Name, pSwapChain.Interface_VTable.GetParent_6.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetDevice_7.Name, pSwapChain.Interface_VTable.GetDevice_7.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_Present_8.Name, pSwapChain.Interface_VTable.Present_8.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetBuffer_9.Name, pSwapChain.Interface_VTable.GetBuffer_9.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_SetFullscreenState_10.Name, pSwapChain.Interface_VTable.SetFullscreenState_10.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetFullscreenState_11.Name, pSwapChain.Interface_VTable.GetFullscreenState_11.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetDesc_12.Name, pSwapChain.Interface_VTable.GetDesc_12.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_ResizeBuffers_13.Name, pSwapChain.Interface_VTable.ResizeBuffers_13.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_ResizeTarget_14.Name, pSwapChain.Interface_VTable.ResizeTarget_14.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetContainingOutput_15.Name, pSwapChain.Interface_VTable.GetContainingOutput_15.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetFrameStatistics_16.Name, pSwapChain.Interface_VTable.GetFrameStatistics_16.PtrMethod);
            functions.TryAddGraphicsFunctions(Ptr_Func_GetLastPresentCount_17.Name, pSwapChain.Interface_VTable.GetLastPresentCount_17.PtrMethod);

            return functions;

        }
        #endregion

        #region D3D12

        const string LibraryName = "dxgi.dll";
        const string EntryPoint = "CreateDXGIFactory";
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall), typeof(CallConvSuppressGCTransition)])]
        [LibraryImport(LibraryName, EntryPoint = EntryPoint)]
        private static partial COM_HRESULT CreateDXGIFactory(UnsafeIn<Guid> riid, UnsafeOut<COM_PTR_IUNKNOWN<IDXGIFactoryImp>> ppFactory);
        public static COM_PTR_IUNKNOWN<IDXGIFactoryImp> CreateIDXGIFactoryImp()
        {
            var hResult = CreateDXGIFactory(UnsafeIn<Guid>.FromIn(in IDXGIFactoryImp.GUID), UnsafeOut<COM_PTR_IUNKNOWN<IDXGIFactoryImp>>.FromOut(out var pFactory));
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGIFactoryImp>>($"{nameof(CreateIDXGIFactoryImp)}:{hResult}");
            }
            return pFactory;
        }
        public static COM_PTR_IUNKNOWN<IDXGIAdapterImp> CreateIDXGIAdapterImp(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pFactory)
        {

            var hResult = pFactory.EnumAdapters(0, out var pAdapter);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<IDXGIAdapterImp>>($"{nameof(CreateIDXGIAdapterImp)}:{hResult}");
            }
            return pAdapter;
        }

        public static T CreateD3D12<T>(COM_PTR_IUNKNOWN<IDXGIFactoryImp> pFactory, COM_PTR_IUNKNOWN pCommandQueue, IServiceProvider provider)
            where T : GraphicsFunctionsProvider, new()
        {
            using var pSwapChain = CreateIDXGISwapChainImpD3D12(pFactory, pCommandQueue, provider.GetRequiredService<Win32WindowFactory>());
            return BuildGraphicsFunctionsProvider<T>(pSwapChain);
        }
        #endregion

    }
}
