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
    public class D3D9PresentHookItem : HookItem<D3D9PresentHookItem, Ptr_Func_Present_17, Ptr_Func_Present_17>, IGraphicsHookItem<D3D9PresentHookItem>
    {
        public const string MethodName = Ptr_Func_Present_17.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnsafePtr, UnsafePtr, nint, UnsafePtr, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9PresentHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9PresentHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9PresentHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>,
                UnsafeRef<RECT>,
                UnsafeRef<RECT>,
                HWND,
                UnsafeRef<global::Windows.Win32.Graphics.Gdi.RGNDATA>, 
                COM_HRESULT> _proc = &Hook_Present;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_Present(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this,
            UnsafeRef<RECT> pSourceRect,
            UnsafeRef<RECT> pDestRect,
            HWND hDestWindowOverride,
            UnsafeRef<global::Windows.Win32.Graphics.Gdi.RGNDATA> pDirtyRegion)
        {
            
            if (D3D9PresentHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pSourceRect, pDestRect, hDestWindowOverride, pDirtyRegion);
                }
                return hookItem.OriginalMethod.Invoke(@this, pSourceRect, pDestRect, hDestWindowOverride, pDirtyRegion);
            }
            return COM_HRESULT.S_FALSE;
        }
    }
}