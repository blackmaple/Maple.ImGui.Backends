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
    internal class D3D9GetStreamSourceHookItem : HookItem<D3D9GetStreamSourceHookItem, Ptr_Func_GetStreamSource_101, Ptr_Func_GetStreamSource_101>, IGraphicsHookItem<D3D9GetStreamSourceHookItem>
    {
        public const string MethodName = Ptr_Func_GetStreamSource_101.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<int>, UnmanagedExtensions.UnsafeRef<int>, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9GetStreamSourceHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9GetStreamSourceHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9GetStreamSourceHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<int>, UnmanagedExtensions.UnsafeRef<int>, COM_HRESULT>
                _proc = &Hook_GetStreamSource;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_GetStreamSource(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint StreamNumber, UnmanagedExtensions.UnsafeOut<nint> ppStreamData, UnmanagedExtensions.UnsafeRef<int> pOffsetInBytes, UnmanagedExtensions.UnsafeRef<int> pStride)
        {
            if (D3D9GetStreamSourceHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, StreamNumber, ppStreamData, pOffsetInBytes, pStride);
                }
                return hookItem.OriginalMethod.Invoke(@this, StreamNumber, ppStreamData, pOffsetInBytes, pStride);
            }
            return 0;
        }
    }
}