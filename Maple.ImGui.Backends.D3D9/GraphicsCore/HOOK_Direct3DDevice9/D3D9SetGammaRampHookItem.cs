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
    internal class D3D9SetGammaRampHookItem : HookItem<D3D9SetGammaRampHookItem, Ptr_Func_SetGammaRamp_21, Ptr_Func_SetGammaRamp_21>, IGraphicsHookItem<D3D9SetGammaRampHookItem>
    {
        public const string MethodName = Ptr_Func_SetGammaRamp_21.Name;

        public Action<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, UnmanagedExtensions.UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DGAMMARAMP>>? SyncCallback { get; set; }

        public static D3D9SetGammaRampHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9SetGammaRampHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9SetGammaRampHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, UnmanagedExtensions.UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DGAMMARAMP>, void>
                _proc = &Hook_SetGammaRamp;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static void Hook_SetGammaRamp(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint iSwapChain, uint Flags, UnmanagedExtensions.UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DGAMMARAMP> pRamp)
        {
            if (D3D9SetGammaRampHookItem.TryGet(out var hookItem))
            {
                hookItem.SyncCallback?.Invoke(@this, iSwapChain, Flags, pRamp);
                hookItem.OriginalMethod.Invoke(@this, iSwapChain, Flags, pRamp);
            }

        }
    }
}