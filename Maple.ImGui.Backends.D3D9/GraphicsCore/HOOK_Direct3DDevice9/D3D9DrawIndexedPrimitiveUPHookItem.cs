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
    internal class D3D9DrawIndexedPrimitiveUPHookItem : HookItem<D3D9DrawIndexedPrimitiveUPHookItem, Ptr_Func_DrawIndexedPrimitiveUP_84, Ptr_Func_DrawIndexedPrimitiveUP_84>, IGraphicsHookItem<D3D9DrawIndexedPrimitiveUPHookItem>
    {
        public const string MethodName = Ptr_Func_DrawIndexedPrimitiveUP_84.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, D3DPRIMITIVETYPE, uint, uint, uint, nint, D3DFORMAT,  nint, uint, D3D9DrawIndexedPrimitiveUPHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9DrawIndexedPrimitiveUPHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9DrawIndexedPrimitiveUPHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9DrawIndexedPrimitiveUPHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, D3DPRIMITIVETYPE, uint, uint, uint, nint, D3DFORMAT, nint, uint, COM_HRESULT>
                _proc = &Hook_DrawIndexedPrimitiveUP;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_DrawIndexedPrimitiveUP(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, D3DPRIMITIVETYPE PrimitiveType, uint MinVertexIndex, uint NumVertices, uint PrimitiveCount, nint pIndexData, D3DFORMAT IndexDataFormat, nint pVertexStreamZeroData, uint VertexStreamZeroStride)
        {
            if (D3D9DrawIndexedPrimitiveUPHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, PrimitiveType, MinVertexIndex, NumVertices, PrimitiveCount, pIndexData, IndexDataFormat, pVertexStreamZeroData, VertexStreamZeroStride, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, PrimitiveType, MinVertexIndex, NumVertices, PrimitiveCount, pIndexData, IndexDataFormat, pVertexStreamZeroData, VertexStreamZeroStride);
            }
            return 0;
        }
    }
}