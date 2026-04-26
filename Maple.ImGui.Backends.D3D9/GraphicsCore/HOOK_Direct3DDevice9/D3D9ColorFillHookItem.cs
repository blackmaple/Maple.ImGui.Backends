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
    internal class D3D9ColorFillHookItem : HookItem<D3D9ColorFillHookItem,Ptr_Func_ColorFill_35, Ptr_Func_ColorFill_35>, IGraphicsHookItem<D3D9ColorFillHookItem>
    {
        public const string MethodName = Ptr_Func_ColorFill_35.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef< RECT>, uint, D3D9ColorFillHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9ColorFillHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9ColorFillHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9ColorFillHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef< RECT>, uint, COM_HRESULT>
                _proc = &Hook_ColorFill;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_ColorFill(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, nint pSurface, UnmanagedExtensions.UnsafeRef< RECT> pRect, uint color)
        {
            if (D3D9ColorFillHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pSurface, pRect, color, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pSurface, pRect, color);
            }
            return 0;
        }
    }
}