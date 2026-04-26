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
    internal class D3D9GetPaletteEntriesHookItem : HookItem<D3D9GetPaletteEntriesHookItem, Ptr_Func_GetPaletteEntries_72, Ptr_Func_GetPaletteEntries_72>, IGraphicsHookItem<D3D9GetPaletteEntriesHookItem>
    {
        public const string MethodName = Ptr_Func_GetPaletteEntries_72.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnsafeRef<global::Windows.Win32.Graphics.Gdi.PALETTEENTRY>, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9GetPaletteEntriesHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9GetPaletteEntriesHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9GetPaletteEntriesHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnsafeRef<global::Windows.Win32.Graphics.Gdi.PALETTEENTRY>, COM_HRESULT>
                _proc = &Hook_GetPaletteEntries;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetPaletteEntries(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint PaletteNumber, UnsafeRef<global::Windows.Win32.Graphics.Gdi.PALETTEENTRY> pEntries)
        {
            if (D3D9GetPaletteEntriesHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, PaletteNumber, pEntries);
                }
                return hookItem.OriginalMethod.Invoke(@this, PaletteNumber, pEntries);
            }
            return 0;
        }
    }
}