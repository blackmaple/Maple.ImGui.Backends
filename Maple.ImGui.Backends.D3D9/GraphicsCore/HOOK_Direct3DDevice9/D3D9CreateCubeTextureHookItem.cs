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
    internal class D3D9CreateCubeTextureHookItem : HookItem<D3D9CreateCubeTextureHookItem,Ptr_Func_CreateCubeTexture_25, Ptr_Func_CreateCubeTexture_25>, IGraphicsHookItem<D3D9CreateCubeTextureHookItem>
    {
        public const string MethodName = Ptr_Func_CreateCubeTexture_25.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, uint,   D3DFORMAT, D3DPOOL, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, D3D9CreateCubeTextureHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9CreateCubeTextureHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9CreateCubeTextureHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9CreateCubeTextureHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<
                Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, 
                uint, uint, uint,  
                D3DFORMAT, D3DPOOL,
                UnmanagedExtensions.UnsafeOut<nint>,
                UnmanagedExtensions.UnsafeRef<HANDLE>, 
                COM_HRESULT>
                _proc = &Hook_CreateCubeTexture;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_CreateCubeTexture(
            Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, 
            uint EdgeLength, uint Levels, uint Usage, 
            D3DFORMAT Format, D3DPOOL Pool,
            UnmanagedExtensions.UnsafeOut<nint> ppCubeTexture,
            UnmanagedExtensions.UnsafeRef<HANDLE> pSharedHandle)
        {
            if (D3D9CreateCubeTextureHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, EdgeLength, Levels, Usage, Format, Pool, ppCubeTexture, pSharedHandle, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, EdgeLength, Levels, Usage, Format, Pool, ppCubeTexture, pSharedHandle);
            }
            return 0;
        }
    }
}