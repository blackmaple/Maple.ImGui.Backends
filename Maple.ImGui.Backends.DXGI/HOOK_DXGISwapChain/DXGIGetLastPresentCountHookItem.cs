using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGIGetLastPresentCountHookItem : HookItem<DXGIGetLastPresentCountHookItem, Ptr_Func_GetLastPresentCount_17, Ptr_Func_GetLastPresentCount_17>, IGraphicsHookItem<DXGIGetLastPresentCountHookItem>
    {
        public const string MethodName = Ptr_Func_GetLastPresentCount_17.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<uint>, DXGIGetLastPresentCountHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIGetLastPresentCountHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIGetLastPresentCountHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIGetLastPresentCountHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<uint>, COM_HRESULT>
                _proc = &Hook_GetLastPresentCount;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetLastPresentCount(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeOut<uint> pLastPresentCount)
        {
            if (DXGIGetLastPresentCountHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pLastPresentCount, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pLastPresentCount);
            }
            return 0;
        }
    }
}
