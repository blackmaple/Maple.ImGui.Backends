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
    internal class D3D9SetMaterialHookItem : HookItem<D3D9SetMaterialHookItem, Ptr_Func_SetMaterial_49, Ptr_Func_SetMaterial_49>, IGraphicsHookItem<D3D9SetMaterialHookItem>
    {
        public const string MethodName = Ptr_Func_SetMaterial_49.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnmanagedExtensions.UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DMATERIAL9>, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9SetMaterialHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9SetMaterialHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9SetMaterialHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnmanagedExtensions.UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DMATERIAL9>, COM_HRESULT>
                _proc = &Hook_SetMaterial;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_SetMaterial(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, UnmanagedExtensions.UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DMATERIAL9> pMaterial)
        {
            if (D3D9SetMaterialHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pMaterial);
                }
                return hookItem.OriginalMethod.Invoke(@this, pMaterial);
            }
            return 0;
        }
    }
}