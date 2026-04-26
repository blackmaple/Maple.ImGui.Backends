using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGISetFullscreenStateHookItem : HookItem<DXGISetFullscreenStateHookItem, Ptr_Func_SetFullscreenState_10, Ptr_Func_SetFullscreenState_10>, IGraphicsHookItem<DXGISetFullscreenStateHookItem>
    {
        public const string MethodName = Ptr_Func_SetFullscreenState_10.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, int, UnsafePtr, DXGISetFullscreenStateHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGISetFullscreenStateHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGISetFullscreenStateHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGISetFullscreenStateHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, int, UnsafePtr, COM_HRESULT>
                _proc = &Hook_SetFullscreenState;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_SetFullscreenState(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, int Fullscreen, UnsafePtr pTarget)
        {
            if (DXGISetFullscreenStateHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Fullscreen, pTarget, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Fullscreen, pTarget);
            }
            return 0;
        }
    }
}
