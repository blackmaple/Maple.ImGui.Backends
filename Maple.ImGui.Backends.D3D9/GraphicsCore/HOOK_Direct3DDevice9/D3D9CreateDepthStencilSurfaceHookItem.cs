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
    internal class D3D9CreateDepthStencilSurfaceHookItem : HookItem<D3D9CreateDepthStencilSurfaceHookItem, Ptr_Func_CreateDepthStencilSurface_29, Ptr_Func_CreateDepthStencilSurface_29>, IGraphicsHookItem<D3D9CreateDepthStencilSurfaceHookItem>
    {
        public const string MethodName = Ptr_Func_CreateDepthStencilSurface_29.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, D3DFORMAT, D3DMULTISAMPLE_TYPE, uint, int, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, D3D9CreateDepthStencilSurfaceHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9CreateDepthStencilSurfaceHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9CreateDepthStencilSurfaceHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9CreateDepthStencilSurfaceHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, D3DFORMAT, D3DMULTISAMPLE_TYPE, uint, int, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, COM_HRESULT>
                _proc = &Hook_CreateDepthStencilSurface;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_CreateDepthStencilSurface(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint Width, uint Height, D3DFORMAT Format, D3DMULTISAMPLE_TYPE MultiSample, uint MultisampleQuality, int Discard, UnmanagedExtensions.UnsafeOut<nint> ppSurface, UnmanagedExtensions.UnsafeRef<HANDLE> pSharedHandle)
        {
            if (D3D9CreateDepthStencilSurfaceHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Width, Height, Format, MultiSample, MultisampleQuality, Discard, ppSurface, pSharedHandle, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Width, Height, Format, MultiSample, MultisampleQuality, Discard, ppSurface, pSharedHandle);
            }
            return 0;
        }
    }
}