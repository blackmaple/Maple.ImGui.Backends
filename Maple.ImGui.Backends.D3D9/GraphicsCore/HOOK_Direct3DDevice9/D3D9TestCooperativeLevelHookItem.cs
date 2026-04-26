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
    internal class D3D9TestCooperativeLevelHookItem : HookItem<D3D9TestCooperativeLevelHookItem, Ptr_Func_TestCooperativeLevel_3, Ptr_Func_TestCooperativeLevel_3>, IGraphicsHookItem<D3D9TestCooperativeLevelHookItem>
    {
        public const string MethodName = Ptr_Func_TestCooperativeLevel_3.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9TestCooperativeLevelHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9TestCooperativeLevelHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9TestCooperativeLevelHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, COM_HRESULT>
                _proc = &Hook_TestCooperativeLevel;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_TestCooperativeLevel(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this)
        {
            if (D3D9TestCooperativeLevelHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this);
                }
                return hookItem.OriginalMethod.Invoke(@this);
            }
            return COM_HRESULT.S_FALSE;
        }
    }
}