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
    internal class D3D9ProcessVerticesHookItem : HookItem<D3D9ProcessVerticesHookItem, Ptr_Func_ProcessVertices_85, Ptr_Func_ProcessVertices_85>, IGraphicsHookItem<D3D9ProcessVerticesHookItem>
    {
        public const string MethodName = Ptr_Func_ProcessVertices_85.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, uint, nint, nint, uint, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9ProcessVerticesHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9ProcessVerticesHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9ProcessVerticesHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, uint, nint, nint, uint, COM_HRESULT>
                _proc = &Hook_ProcessVertices;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_ProcessVertices(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint SrcStartIndex, uint DestIndex, uint VertexCount, nint pDestBuffer, nint pVertexDeclaration, uint Flags)
        {
            if (D3D9ProcessVerticesHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, SrcStartIndex, DestIndex, VertexCount, pDestBuffer, pVertexDeclaration, Flags);
                }
                return hookItem.OriginalMethod.Invoke(@this, SrcStartIndex, DestIndex, VertexCount, pDestBuffer, pVertexDeclaration, Flags);
            }
            return 0;
        }
    }
}