using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    internal class DXGIGetDeviceHookItem : HookItem<DXGIGetDeviceHookItem, Ptr_Func_GetDevice_7, Ptr_Func_GetDevice_7>, IGraphicsHookItem<DXGIGetDeviceHookItem>
    {
        public const string MethodName = Ptr_Func_GetDevice_7.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<global::System.Guid>, UnsafeOut<COM_PTR_IUNKNOWN>, DXGIGetDeviceHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static DXGIGetDeviceHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIGetDeviceHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIGetDeviceHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, UnsafeIn<global::System.Guid>, UnsafeOut<COM_PTR_IUNKNOWN>, COM_HRESULT>
                _proc = &Hook_GetDevice;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetDevice(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, UnsafeIn<global::System.Guid> riid, UnsafeOut<COM_PTR_IUNKNOWN> ppDevice)
        {
            if (DXGIGetDeviceHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, riid, ppDevice, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, riid, ppDevice);
            }
            return 0;
        }
    }
}
