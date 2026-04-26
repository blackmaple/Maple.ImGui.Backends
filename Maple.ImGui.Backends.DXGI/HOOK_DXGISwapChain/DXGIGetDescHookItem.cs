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
    internal class DXGIGetDescHookItem : HookItem<DXGIGetDescHookItem, Ptr_Func_GetDesc_12, Ptr_Func_GetDesc_12>, IGraphicsHookItem<DXGIGetDescHookItem>
    {
        public const string MethodName = Ptr_Func_GetDesc_12.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<DXGI_SWAP_CHAIN_DESC>, DXGIGetDescHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIGetDescHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIGetDescHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIGetDescHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeOut<DXGI_SWAP_CHAIN_DESC>, COM_HRESULT>
                _proc = &Hook_GetDesc;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetDesc(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeOut<DXGI_SWAP_CHAIN_DESC> pDesc)
        {
            if (DXGIGetDescHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pDesc, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pDesc);
            }
            return 0;
        }
    }
}
