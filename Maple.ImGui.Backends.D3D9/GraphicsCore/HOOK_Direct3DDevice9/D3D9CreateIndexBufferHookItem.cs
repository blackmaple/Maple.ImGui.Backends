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
    internal class D3D9CreateIndexBufferHookItem : HookItem<D3D9CreateIndexBufferHookItem, Ptr_Func_CreateIndexBuffer_27, Ptr_Func_CreateIndexBuffer_27>, IGraphicsHookItem<D3D9CreateIndexBufferHookItem>
    {
        public const string MethodName = Ptr_Func_CreateIndexBuffer_27.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, D3DFORMAT, D3DPOOL, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, D3D9CreateIndexBufferHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9CreateIndexBufferHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9CreateIndexBufferHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9CreateIndexBufferHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, uint, uint, D3DFORMAT, D3DPOOL, UnmanagedExtensions.UnsafeOut<nint>, UnmanagedExtensions.UnsafeRef<HANDLE>, COM_HRESULT>
                _proc = &Hook_CreateIndexBuffer;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_CreateIndexBuffer(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, uint Length, uint Usage, D3DFORMAT Format, D3DPOOL Pool, UnmanagedExtensions.UnsafeOut<nint> ppIndexBuffer, UnmanagedExtensions.UnsafeRef<HANDLE> pSharedHandle)
        {
            if (D3D9CreateIndexBufferHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, Length, Usage, Format, Pool, ppIndexBuffer, pSharedHandle, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, Length, Usage, Format, Pool, ppIndexBuffer, pSharedHandle);
            }
            return 0;
        }
    }
}