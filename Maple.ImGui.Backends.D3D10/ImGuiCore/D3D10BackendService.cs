using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Microsoft.Extensions.Logging;
namespace Maple.ImGui.Backends.D3D10.ImGuiCore
{
    public sealed class D3D10BackendService : Win32ImGuiBackendService
    {
        DXGIPresentHookItem PresentHookItem { get; }
        DXGIResizeBuffersHookItem ResizeBuffersHookItem { get; }
        public D3D10BackendService(ILogger<D3D10BackendService> logger, IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view)
            : base(logger, hookFactory, winMsgHookFactory, bridgeCollection, view)
        {

            this.PresentHookItem = hookFactory.Create<DXGIPresentHookItem>(EnumGraphicsType.D3D10);
            this.PresentHookItem.SyncCallback = HookPresent;
            this.ResizeBuffersHookItem = hookFactory.Create<DXGIResizeBuffersHookItem>(EnumGraphicsType.D3D10);
            this.ResizeBuffersHookItem.SyncCallback = HookResizeBuffers;


            
        }


        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            if (BackendImp is not null)
            {
                BackendImp.Run(@this);
            }
            else if (D3D10BackendImp.TryCreateImp(@this, this, out var backendImp))
            {
                BackendImp = backendImp;
            }
            return hookItem.OriginalMethod.Invoke(@this, SyncInterval, Flags);
        }

        private COM_HRESULT HookResizeBuffers(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint BufferCount, uint Width, uint Height, uint NewFormat, uint SwapChainFlags, DXGIResizeBuffersHookItem hookItem)
        {
            if (BackendImp is not null)
            {
                BackendImp.Resetting(@this);
                BackendImp.Reset(@this);
                var h = hookItem.InvokeOriginal(@this, BufferCount, Width, Height, NewFormat, SwapChainFlags);
                if (h)
                {
                    BackendImp.Resetted(@this);
                }
                return h;
            }
            return hookItem.InvokeOriginal(@this, BufferCount, Width, Height, NewFormat, SwapChainFlags);
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.ResizeBuffersHookItem.Enable();
            this.PresentHookItem.Enable();
             
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.ResizeBuffersHookItem.Dispose();
            this.PresentHookItem.Dispose();
            this.BackendImp?.Dispose();
            this.BridgeCollection.Dispose();
            return Task.CompletedTask;

        }
    }
}
