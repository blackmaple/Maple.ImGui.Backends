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
    internal class D3D9StretchRectHookItem : HookItem<D3D9StretchRectHookItem, Ptr_Func_StretchRect_34, Ptr_Func_StretchRect_34>, IGraphicsHookItem<D3D9StretchRectHookItem>
    {
        public const string MethodName = Ptr_Func_StretchRect_34.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef< RECT>, nint, UnmanagedExtensions.UnsafeRef< RECT>, D3DTEXTUREFILTERTYPE, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9StretchRectHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9StretchRectHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9StretchRectHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef<RECT>, nint, UnmanagedExtensions.UnsafeRef<RECT>, D3DTEXTUREFILTERTYPE, COM_HRESULT>
                _proc = &Hook_StretchRect;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_StretchRect(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, nint pSourceSurface, UnmanagedExtensions.UnsafeRef<RECT> pSourceRect, nint pDestSurface, UnmanagedExtensions.UnsafeRef<RECT> pDestRect, D3DTEXTUREFILTERTYPE Filter)
        {
            if (D3D9StretchRectHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pSourceSurface, pSourceRect, pDestSurface, pDestRect, Filter);
                }
                return hookItem.OriginalMethod.Invoke(@this, pSourceSurface, pSourceRect, pDestSurface, pDestRect, Filter);
            }
            return 0;
        }
    }
}