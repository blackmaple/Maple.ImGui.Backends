using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11Device;
using Maple.ImGui.Backends.D3D11.GraphicsCore.COM_D3D11DeviceContext;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.RenderSpy.Graphics.DXGI;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D11;
namespace Maple.RenderSpy.Graphics.D3D11
{
    internal partial class D3D11FunctionsProvider : GraphicsFunctionsProvider, IGraphicsFunctions<D3D11FunctionsProvider>
    {
        public static D3D11FunctionsProvider Create(IServiceProvider provider)
        {
            using var pDevice = CreateID3D11DeviceImp();
            return DXGIFunctionsProvider.Create<D3D11FunctionsProvider>(pDevice, provider);
        }

        private static COM_PTR_IUNKNOWN<ID3D11DeviceImp> CreateID3D11DeviceImp()
        {

            var hResult = D3D11CreateDevice(
                default,
                D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE,
                 HMODULE.Null,
                default,
                default, 0U,
                PInvoke.D3D11_SDK_VERSION,

                UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceImp>>.FromOut(out var ppDevice),
                new UnsafePtr<D3D_FEATURE_LEVEL>(nint.Zero),
                new UnsafePtr<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>>(nint.Zero)

                );

            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<ID3D11DeviceImp>>($"{nameof(CreateID3D11DeviceImp)}:{hResult}");
            }
            return ppDevice;
        }
     
        const string LibraryName = "d3d11.dll";
        const string EntryPoint = "D3D11CreateDevice";
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        [LibraryImport(LibraryName, EntryPoint = EntryPoint)]
        private static partial COM_HRESULT D3D11CreateDevice(
            nint pAdapter,
            D3D_DRIVER_TYPE DriverType,
            HMODULE Software,
            D3D11_CREATE_DEVICE_FLAG Flags,
            UnsafeIn<D3D_FEATURE_LEVEL> pFeatureLevels,
            uint FeatureLevels,
            uint SDKVersion,
            UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceImp>> ppDevice,
            UnsafeOut<D3D_FEATURE_LEVEL> pFeatureLevel,
            UnsafeOut<COM_PTR_IUNKNOWN<ID3D11DeviceContextImp>> ppImmediateContext);

    }

}
