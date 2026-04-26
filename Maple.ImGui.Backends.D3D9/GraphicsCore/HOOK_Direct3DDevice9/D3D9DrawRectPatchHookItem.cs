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
    internal class D3D9DrawRectPatchHookItem : HookItem<D3D9DrawRectPatchHookItem, Ptr_Func_DrawRectPatch_115, Ptr_Func_DrawRectPatch_115>, IGraphicsHookItem<D3D9DrawRectPatchHookItem>
    {
        public const string MethodName = Ptr_Func_DrawRectPatch_115.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnmanagedExtensions.UnsafeRef<float>, UnmanagedExtensions.UnsafeOut<global::Windows.Win32.Graphics.Direct3D9.D3DRECTPATCH_INFO>, D3D9DrawRectPatchHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9DrawRectPatchHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9DrawRectPatchHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9DrawRectPatchHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnmanagedExtensions.UnsafeRef<float>, UnmanagedExtensions.UnsafeOut<global::Windows.Win32.Graphics.Direct3D9.D3DRECTPATCH_INFO>, COM_HRESULT>
                _proc = &Hook_DrawRectPatch;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_DrawRectPatch(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint Handle, UnmanagedExtensions.UnsafeRef<float> pNumSegs, UnmanagedExtensions.UnsafeOut<global::Windows.Win32.Graphics.Direct3D9.D3DRECTPATCH_INFO> pRectPatchInfo)
        {
            if (D3D9DrawRectPatchHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Handle, pNumSegs, pRectPatchInfo, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Handle, pNumSegs, pRectPatchInfo);
            }
            return 0;
        }
    }
}