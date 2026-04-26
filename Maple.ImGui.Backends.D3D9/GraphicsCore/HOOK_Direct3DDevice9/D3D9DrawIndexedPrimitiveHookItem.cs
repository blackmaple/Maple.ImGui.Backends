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
    internal class D3D9DrawIndexedPrimitiveHookItem : HookItem<D3D9DrawIndexedPrimitiveHookItem, Ptr_Func_DrawIndexedPrimitive_82, Ptr_Func_DrawIndexedPrimitive_82>, IGraphicsHookItem<D3D9DrawIndexedPrimitiveHookItem>
    {
        public const string MethodName = Ptr_Func_DrawIndexedPrimitive_82.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, D3DPRIMITIVETYPE, int, uint, uint, uint, uint, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9DrawIndexedPrimitiveHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9DrawIndexedPrimitiveHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9DrawIndexedPrimitiveHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, D3DPRIMITIVETYPE, int, uint, uint, uint, uint, COM_HRESULT>
                _proc = &Hook_DrawIndexedPrimitive;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_DrawIndexedPrimitive(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, D3DPRIMITIVETYPE Type, int BaseVertexIndex, uint MinVertexIndex, uint NumVertices, uint startIndex, uint primCount)
        {
            if (D3D9DrawIndexedPrimitiveHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Type, BaseVertexIndex, MinVertexIndex, NumVertices, startIndex, primCount);
                }
                return hookItem.OriginalMethod.Invoke(@this, Type, BaseVertexIndex, MinVertexIndex, NumVertices, startIndex, primCount);
            }
            return 0;
        }
    }
}