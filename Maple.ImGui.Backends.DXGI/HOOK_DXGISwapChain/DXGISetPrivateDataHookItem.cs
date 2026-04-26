using Maple.Hook.Abstractions;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGISetPrivateDataHookItem : HookItem<DXGISetPrivateDataHookItem, Ptr_Func_SetPrivateData_3, Ptr_Func_SetPrivateData_3>, IGraphicsHookItem<DXGISetPrivateDataHookItem>
    {
        public const string MethodName = Ptr_Func_SetPrivateData_3.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<global::System.Guid>, uint, UnsafePtr, DXGISetPrivateDataHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGISetPrivateDataHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGISetPrivateDataHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGISetPrivateDataHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<global::System.Guid>, uint, UnsafePtr, COM_HRESULT>
                _proc = &Hook_SetPrivateData;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_SetPrivateData(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeIn<global::System.Guid> Name, uint DataSize, UnsafePtr pData)
        {
            if (DXGISetPrivateDataHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Name, DataSize, pData, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Name, DataSize, pData);
            }
            return 0;
        }
    }
}
