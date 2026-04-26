using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGIResizeTargetHookItem : HookItem<DXGIResizeTargetHookItem, Ptr_Func_ResizeTarget_14, Ptr_Func_ResizeTarget_14>, IGraphicsHookItem<DXGIResizeTargetHookItem>
    {
        public const string MethodName = Ptr_Func_ResizeTarget_14.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafePtr, DXGIResizeTargetHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIResizeTargetHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIResizeTargetHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIResizeTargetHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<DXGI_MODE_DESC>, COM_HRESULT>
                _proc = &Hook_ResizeTarget;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_ResizeTarget(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeIn<DXGI_MODE_DESC> pNewTargetParameters)
        {
            if (DXGIResizeTargetHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pNewTargetParameters, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pNewTargetParameters);
            }
            return 0;
        }
    }
}
