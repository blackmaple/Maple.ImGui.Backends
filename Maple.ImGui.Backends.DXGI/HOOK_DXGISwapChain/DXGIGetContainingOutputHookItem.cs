using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGIGetContainingOutputHookItem : HookItem<DXGIGetContainingOutputHookItem, Ptr_Func_GetContainingOutput_15, Ptr_Func_GetContainingOutput_15>, IGraphicsHookItem<DXGIGetContainingOutputHookItem>
    {
        public const string MethodName = Ptr_Func_GetContainingOutput_15.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafePtr, DXGIGetContainingOutputHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIGetContainingOutputHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIGetContainingOutputHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIGetContainingOutputHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<nint>, COM_HRESULT>
                _proc = &Hook_GetContainingOutput;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetContainingOutput(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeOut<nint> ppOutput)
        {
            if (DXGIGetContainingOutputHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, ppOutput, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, ppOutput);
            }
            return COM_HRESULT.S_FALSE;
        }
    }
}
