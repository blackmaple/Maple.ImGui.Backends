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
    internal class D3D9DrawPrimitiveHookItem : HookItem<D3D9DrawPrimitiveHookItem, Ptr_Func_DrawPrimitive_81, Ptr_Func_DrawPrimitive_81>, IGraphicsHookItem<D3D9DrawPrimitiveHookItem>
    {
        public const string MethodName = Ptr_Func_DrawPrimitive_81.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, D3DPRIMITIVETYPE, uint, uint, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9DrawPrimitiveHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9DrawPrimitiveHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9DrawPrimitiveHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, D3DPRIMITIVETYPE, uint, uint, COM_HRESULT>
                _proc = &Hook_DrawPrimitive;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_DrawPrimitive(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, D3DPRIMITIVETYPE PrimitiveType, uint StartVertex, uint PrimitiveCount)
        {
            if (D3D9DrawPrimitiveHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, PrimitiveType, StartVertex, PrimitiveCount);
                }
                return hookItem.OriginalMethod.Invoke(@this, PrimitiveType, StartVertex, PrimitiveCount);
            }
            return 0;
        }
    }
}