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
    internal class D3D9UpdateSurfaceHookItem : HookItem<D3D9UpdateSurfaceHookItem,Ptr_Func_UpdateSurface_30, Ptr_Func_UpdateSurface_30>, IGraphicsHookItem<D3D9UpdateSurfaceHookItem>
    {
        public const string MethodName = Ptr_Func_UpdateSurface_30.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef<RECT>, nint, UnmanagedExtensions.UnsafeRef<System.Drawing.Point>, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9UpdateSurfaceHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9UpdateSurfaceHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9UpdateSurfaceHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, nint, UnmanagedExtensions.UnsafeRef<RECT>, nint, UnmanagedExtensions.UnsafeRef<System.Drawing.Point>, COM_HRESULT>
                _proc = &Hook_UpdateSurface;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_UpdateSurface(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, nint pSourceSurface, UnmanagedExtensions.UnsafeRef<RECT> pSourceRect, nint pDestinationSurface, UnmanagedExtensions.UnsafeRef<System.Drawing.Point> pDestPoint)
        {
            if (D3D9UpdateSurfaceHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pSourceSurface, pSourceRect, pDestinationSurface, pDestPoint);
                }
                return hookItem.OriginalMethod.Invoke(@this, pSourceSurface, pSourceRect, pDestinationSurface, pDestPoint);
            }
            return 0;
        }
    }
}