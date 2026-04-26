using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi.Common;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain
{
    public class DXGIResizeBuffersHookItem : HookItem<DXGIResizeBuffersHookItem, Ptr_Func_ResizeBuffers_13, Ptr_Func_ResizeBuffers_13>, IGraphicsHookItem<DXGIResizeBuffersHookItem>
    {
        public const string MethodName = Ptr_Func_ResizeBuffers_13.Name;

        public Func<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, uint, uint, uint, uint, DXGIResizeBuffersHookItem, COM_HRESULT>? SyncCallback { get; set; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public COM_HRESULT InvokeOriginal(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint bufferCount, uint width, uint height, uint newFormat, uint swapChainFlags)
        {
            return OriginalMethod.Invoke(@this, bufferCount, width, height, (DXGI_FORMAT)newFormat, swapChainFlags);
        }

        public static DXGIResizeBuffersHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<DXGIResizeBuffersHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<DXGIResizeBuffersHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChainImp>, uint, uint, uint, DXGI_FORMAT, uint, COM_HRESULT>
                _proc = &Hook_ResizeBuffers;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_ResizeBuffers(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint BufferCount, uint Width, uint Height, DXGI_FORMAT NewFormat, uint SwapChainFlags)
        {
            if (DXGIResizeBuffersHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, BufferCount, Width, Height, (uint)NewFormat, SwapChainFlags, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, BufferCount, Width, Height, NewFormat, SwapChainFlags);
            }
            return 0;
        }
    }
}
