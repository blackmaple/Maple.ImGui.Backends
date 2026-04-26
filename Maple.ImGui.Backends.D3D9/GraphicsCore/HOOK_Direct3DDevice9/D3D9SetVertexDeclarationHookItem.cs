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
    internal class D3D9SetVertexDeclarationHookItem : HookItem<D3D9SetVertexDeclarationHookItem, Ptr_Func_SetVertexDeclaration_87, Ptr_Func_SetVertexDeclaration_87>, IGraphicsHookItem<D3D9SetVertexDeclarationHookItem>
    {
        public const string MethodName = Ptr_Func_SetVertexDeclaration_87.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9SetVertexDeclarationHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9SetVertexDeclarationHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9SetVertexDeclarationHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, COM_HRESULT>
                _proc = &Hook_SetVertexDeclaration;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_SetVertexDeclaration(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, nint pDecl)
        {
            if (D3D9SetVertexDeclarationHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pDecl);
                }
                return hookItem.OriginalMethod.Invoke(@this, pDecl);
            }
            return 0;
        }
    }
}