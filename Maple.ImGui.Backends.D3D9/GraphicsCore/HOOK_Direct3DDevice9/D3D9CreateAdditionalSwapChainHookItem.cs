using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.UnmanagedExtensions;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore.HOOK_Direct3DDevice9
{
    internal class D3D9CreateAdditionalSwapChainHookItem : HookItem<D3D9CreateAdditionalSwapChainHookItem,Ptr_Func_CreateAdditionalSwapChain_13, Ptr_Func_CreateAdditionalSwapChain_13>, IGraphicsHookItem<D3D9CreateAdditionalSwapChainHookItem>
    {
        public const string MethodName = Ptr_Func_CreateAdditionalSwapChain_13.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DPRESENT_PARAMETERS>, UnsafeOut<nint>, D3D9CreateAdditionalSwapChainHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9CreateAdditionalSwapChainHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9CreateAdditionalSwapChainHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9CreateAdditionalSwapChainHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DPRESENT_PARAMETERS>, UnsafeOut<nint>, COM_HRESULT>
                _proc = &Hook_CreateAdditionalSwapChain;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_CreateAdditionalSwapChain(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DPRESENT_PARAMETERS> pPresentationParameters, UnsafeOut<nint> ppSwapChain)
        {
            if (D3D9CreateAdditionalSwapChainHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pPresentationParameters, ppSwapChain, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pPresentationParameters, ppSwapChain);
            }
            return 0;
        }
    }
}