using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGIGetFrameStatisticsHookItem : HookItem<DXGIGetFrameStatisticsHookItem, Ptr_Func_GetFrameStatistics_16, Ptr_Func_GetFrameStatistics_16>, IGraphicsHookItem<DXGIGetFrameStatisticsHookItem>
    {
        public const string MethodName = Ptr_Func_GetFrameStatistics_16.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafePtr, DXGIGetFrameStatisticsHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIGetFrameStatisticsHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIGetFrameStatisticsHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIGetFrameStatisticsHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<DXGI_FRAME_STATISTICS>, COM_HRESULT>
                _proc = &Hook_GetFrameStatistics;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetFrameStatistics(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeOut<DXGI_FRAME_STATISTICS> pStats)
        {
            if (DXGIGetFrameStatisticsHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pStats, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pStats);
            }
            return 0;
        }
    }
}
