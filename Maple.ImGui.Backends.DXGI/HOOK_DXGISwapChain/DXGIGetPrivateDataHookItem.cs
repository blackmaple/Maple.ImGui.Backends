using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGIGetPrivateDataHookItem : HookItem<DXGIGetPrivateDataHookItem, Ptr_Func_GetPrivateData_5, Ptr_Func_GetPrivateData_5>, IGraphicsHookItem<DXGIGetPrivateDataHookItem>
    {
        public const string MethodName = Ptr_Func_GetPrivateData_5.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<global::System.Guid>, UnsafeOut<uint>, UnsafePtr, DXGIGetPrivateDataHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIGetPrivateDataHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIGetPrivateDataHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIGetPrivateDataHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<global::System.Guid>, UnsafeOut<uint>, UnsafeOut<nint>, COM_HRESULT>
                _proc = &Hook_GetPrivateData;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetPrivateData(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeIn<global::System.Guid> Name, UnsafeOut<uint> pDataSize, UnsafeOut<nint> pData)
        {
            if (DXGIGetPrivateDataHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Name, pDataSize, pData, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Name, pDataSize, pData);
            }
            return 0;
        }
    }
}
