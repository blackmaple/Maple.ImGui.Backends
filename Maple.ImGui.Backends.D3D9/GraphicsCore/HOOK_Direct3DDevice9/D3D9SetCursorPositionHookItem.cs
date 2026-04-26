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
    internal class D3D9SetCursorPositionHookItem : HookItem<D3D9SetCursorPositionHookItem, Ptr_Func_SetCursorPosition_11, Ptr_Func_SetCursorPosition_11>, IGraphicsHookItem<D3D9SetCursorPositionHookItem>
    {
        public const string MethodName = Ptr_Func_SetCursorPosition_11.Name;

        public Action<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, int, int, uint>? SyncCallback { get; set; }

        public static D3D9SetCursorPositionHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9SetCursorPositionHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9SetCursorPositionHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, int, int, uint, void>
                _proc = &Hook_SetCursorPosition;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static void Hook_SetCursorPosition(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, int X, int Y, uint Flags)
        {
            if (D3D9SetCursorPositionHookItem.TryGet(out var hookItem))
            {
                hookItem.SyncCallback?.Invoke(@this, X, Y, Flags);
                hookItem.OriginalMethod.Invoke(@this, X, Y, Flags);
            }

        }
    }
}