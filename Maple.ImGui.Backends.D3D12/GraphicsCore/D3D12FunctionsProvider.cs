using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList;
using Maple.ImGui.Backends.DXGI.COM_DXGIAdapter;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.RenderSpy.Graphics.DXGI;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
namespace Maple.RenderSpy.Graphics.D3D12
{
    internal partial class D3D12FunctionsProvider : GraphicsFunctionsProvider, IGraphicsFunctions<D3D12FunctionsProvider>
    {
        public static D3D12FunctionsProvider Create(IServiceProvider provider)
        {
            using var pFactory = DXGIFunctionsProvider.CreateIDXGIFactoryImp();
            using var pAdapter = DXGIFunctionsProvider.CreateIDXGIAdapterImp(pFactory);
            using var pDevice = CreateID3D12DeviceImp(pAdapter);
            using var pCommandQueue = CreateID3D12CommandQueueImp(pDevice);
            using var pCommandAllocator = CreateID3D12CommandAllocatorImp(pDevice);
            using var pCommandList = CreateID3D12GraphicsCommandListImp(pDevice, pCommandAllocator);
            var functions = DXGIFunctionsProvider.CreateD3D12<D3D12FunctionsProvider>(pFactory, pCommandQueue, provider);
            functions.TryAddGraphicsFunctions(Ptr_Func_ExecuteCommandLists_10.Name, pCommandQueue.Interface_VTable.ExecuteCommandLists_10.PtrMethod);
            return functions;
        }

        private static COM_PTR_IUNKNOWN<ID3D12DeviceImp> CreateID3D12DeviceImp(COM_PTR_IUNKNOWN<IDXGIAdapterImp> pAdapter)
        {
            var hResult = D3D12CreateDevice(pAdapter, D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0, UnsafeIn<Guid>.FromIn(in ID3D12DeviceImp.GUID), UnsafeOut<COM_PTR_IUNKNOWN>.FromOut(out var ppDevice));
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<ID3D12DeviceImp>>($"{nameof(CreateID3D12DeviceImp)}:{hResult}");
            }
            return ppDevice.Get<ID3D12DeviceImp>();
        }
        private static COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> CreateID3D12CommandQueueImp(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            D3D12_COMMAND_QUEUE_DESC queueDesc;
            queueDesc.Type = D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT;
            queueDesc.Priority = (int)D3D12_COMMAND_QUEUE_PRIORITY.D3D12_COMMAND_QUEUE_PRIORITY_NORMAL;
            queueDesc.Flags = D3D12_COMMAND_QUEUE_FLAGS.D3D12_COMMAND_QUEUE_FLAG_NONE;
            queueDesc.NodeMask = 0;
            var hResult = pDevice.CreateCommandQueue(in queueDesc, in ID3D12CommandQueueImp.GUID, out var ppCommandQueue);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>>($"{nameof(CreateID3D12CommandQueueImp)}:{hResult}");
            }
            return ppCommandQueue.Get<ID3D12CommandQueueImp>();
        }
        private static COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> CreateID3D12CommandAllocatorImp(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var hResult = pDevice.CreateDirectCommandAllocator(out var ppCommandAllocator);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp>>($"{nameof(CreateID3D12CommandAllocatorImp)}:{hResult}");
            }
            return ppCommandAllocator;

        }
        private static COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> CreateID3D12GraphicsCommandListImp(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pCommandAllocator)
        {
            var hResult = pDevice.CreateCommandList(default, D3D12_COMMAND_LIST_TYPE.D3D12_COMMAND_LIST_TYPE_DIRECT, pCommandAllocator, default, in ID3D12GraphicsCommandListImp.GUID, out var ppCommandList);
            if (!hResult)
            {
                return GraphicsException.Throw<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>>($"{nameof(CreateID3D12GraphicsCommandListImp)}:{hResult}");
            }
            return ppCommandList.Get<ID3D12GraphicsCommandListImp>();

        }


        const string LibraryName = "d3d12.dll";
        const string EntryPoint = "D3D12CreateDevice";
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        [UnmanagedCallConv(CallConvs = [typeof(CallConvStdcall), typeof(CallConvSuppressGCTransition)])]
        [LibraryImport(LibraryName, EntryPoint = EntryPoint)]
        internal static partial COM_HRESULT D3D12CreateDevice(
        COM_PTR_IUNKNOWN pAdapter,
        D3D_FEATURE_LEVEL MinimumFeatureLevel,
        UnsafeIn<Guid> riid,
        UnsafeOut<COM_PTR_IUNKNOWN> ppDevice);

    }

}
