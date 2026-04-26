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
    internal class D3D9SetTextureStageStateHookItem : HookItem<D3D9SetTextureStageStateHookItem, Ptr_Func_SetTextureStageState_67, Ptr_Func_SetTextureStageState_67>, IGraphicsHookItem<D3D9SetTextureStageStateHookItem>
    {
        public const string MethodName = Ptr_Func_SetTextureStageState_67.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, D3DTEXTURESTAGESTATETYPE, uint, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9SetTextureStageStateHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9SetTextureStageStateHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9SetTextureStageStateHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, D3DTEXTURESTAGESTATETYPE, uint, COM_HRESULT>
                _proc = &Hook_SetTextureStageState;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_SetTextureStageState(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint Stage, D3DTEXTURESTAGESTATETYPE Type, uint Value)
        {
            if (D3D9SetTextureStageStateHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Stage, Type, Value);
                }
                return hookItem.OriginalMethod.Invoke(@this, Stage, Type, Value);
            }
            return 0;
        }
    }
}