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
    internal class D3D9ClearHookItem : HookItem<D3D9ClearHookItem, Ptr_Func_Clear_43, Ptr_Func_Clear_43>, IGraphicsHookItem<D3D9ClearHookItem>
    {
        public const string MethodName = Ptr_Func_Clear_43.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DRECT>, uint, uint, float, uint, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9ClearHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9ClearHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9ClearHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DRECT>, uint, uint, float, uint, COM_HRESULT>
                _proc = &Hook_Clear;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_Clear(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint Count, UnsafeRef<global::Windows.Win32.Graphics.Direct3D9.D3DRECT> pRects, uint Flags, uint Color, float Z, uint Stencil)
        {
            if (D3D9ClearHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Count, pRects, Flags, Color, Z, Stencil);
                }
                return hookItem.OriginalMethod.Invoke(@this, Count, pRects, Flags, Color, Z, Stencil);
            }
            return 0;
        }
    }
}