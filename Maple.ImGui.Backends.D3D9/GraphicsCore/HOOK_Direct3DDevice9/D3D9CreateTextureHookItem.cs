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
    internal class D3D9CreateTextureHookItem : HookItem<D3D9CreateTextureHookItem, Ptr_Func_CreateTexture_23, Ptr_Func_CreateTexture_23>, IGraphicsHookItem<D3D9CreateTextureHookItem>
    {
        public const string MethodName = Ptr_Func_CreateTexture_23.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, uint, uint, D3DFORMAT, D3DPOOL, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, D3D9CreateTextureHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9CreateTextureHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9CreateTextureHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9CreateTextureHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, uint, uint, D3DFORMAT, D3DPOOL, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, COM_HRESULT>
                _proc = &Hook_CreateTexture;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_CreateTexture(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint Width, uint Height, uint Levels, uint Usage, D3DFORMAT Format, D3DPOOL Pool, UnmanagedExtensions.UnsafeOut<nint> ppTexture, UnmanagedExtensions.UnsafeRef<HANDLE> pSharedHandle)
        {
            if (D3D9CreateTextureHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Width, Height, Levels, Usage, Format, Pool, ppTexture, pSharedHandle, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Width, Height, Levels, Usage, Format, Pool, ppTexture, pSharedHandle);
            }
            return 0;
        }
    }
}