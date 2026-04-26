using Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10Device;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.RenderSpy.Graphics.DXGI;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D10;
namespace Maple.RenderSpy.Graphics.D3D10
{
    public partial class D3D10FunctionsProvider : GraphicsFunctionsProvider, IGraphicsFunctions<D3D10FunctionsProvider>
    {

        public static D3D10FunctionsProvider Create(IServiceProvider provider)
        {
            using var pDevice = CreateID3D10DeviceImp();
            return DXGIFunctionsProvider.Create<D3D10FunctionsProvider>(pDevice, provider);
        }

        private static COM_PTR_IUNKNOWN<ID3D10DeviceImp> CreateID3D10DeviceImp()
        {
            var hResult = D3D10CreateDevice(
                default,
                D3D10_DRIVER_TYPE.D3D10_DRIVER_TYPE_HARDWARE,
                 HMODULE.Null
                , default, PInvoke.D3D10_SDK_VERSION, UnsafeOut<COM_PTR_IUNKNOWN<ID3D10DeviceImp>>.FromOut(out var ppDevice));

            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<ID3D10DeviceImp>>($"{nameof(CreateID3D10DeviceImp)}:{hResult}");
            }
            return ppDevice;
        }


        const string LibraryName = "d3d10.dll";
        const string EntryPoint = "D3D10CreateDevice";
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        [LibraryImport(LibraryName, EntryPoint = EntryPoint)]
        internal static partial COM_HRESULT D3D10CreateDevice(nint pAdapter, D3D10_DRIVER_TYPE DriverType, HMODULE Software, uint Flags, uint SDKVersion, UnsafeOut<COM_PTR_IUNKNOWN<ID3D10DeviceImp>> ppDevice);

    }
}


