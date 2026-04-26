using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Microsoft.Extensions.Logging;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D11.ImGuiCore
{
    public class D3D11BackendService : Win32ImGuiBackendService
    {
        public DXGIPresentHookItem PresentHookItem { get; }
        public DXGIResizeBuffersHookItem ResizeBuffersHookItem { get; }

        public D3D11BackendService(ILogger<D3D11BackendService> logger, IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view)
            : base(logger, hookFactory, winMsgHookFactory, bridgeCollection, view)
        {
            this.PresentHookItem = hookFactory.Create<DXGIPresentHookItem>(EnumGraphicsType.D3D11);
            this.PresentHookItem.SyncCallback = HookPresent;
            this.ResizeBuffersHookItem = hookFactory.Create<DXGIResizeBuffersHookItem>(EnumGraphicsType.D3D11);
            this.ResizeBuffersHookItem.SyncCallback = HookResizeBuffers;
        }


        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            if(BackendImp is not null)
            {
                BackendImp.Run(@this);
            }
            else if (D3D11BackendImp.TryCreateImp(@this, this, out var backendImp))
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
            this.PresentHookItem.Enable();
            this.ResizeBuffersHookItem.Enable();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.PresentHookItem.Dispose();
            this.ResizeBuffersHookItem.Dispose();
            this.BackendImp?.Dispose();
            this.BridgeCollection.Dispose();
            return Task.CompletedTask;
        }
    }
}
