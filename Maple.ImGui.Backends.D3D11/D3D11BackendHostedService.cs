using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.Windows;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.DXGI.HOOK_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
namespace Maple.ImGui.Backends.D3D11
{
    public class D3D11BackendHostedService : Win32ImGuiBackendHostedService
    {
        public DXGIPresentHookItem PresentHookItem { get; }
        public DXGIResizeBuffersHookItem ResizeBuffersHookItem { get; }

        public D3D11BackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view)
            : base(hookFactory, winMsgHookFactory, bridgeCollection, view)
        {
            this.PresentHookItem = hookFactory.Create<DXGIPresentHookItem>(EnumGraphicsType.D3D11);
            this.PresentHookItem.SyncCallback = HookPresent;
            this.ResizeBuffersHookItem = hookFactory.Create<DXGIResizeBuffersHookItem>(EnumGraphicsType.D3D11);
            this.ResizeBuffersHookItem.SyncCallback += (swapChainPtr, bufferCount, width, height, newFormat, swapChainFlags, hookItem) =>
            {
                if (BackendImp is not null)
                {
                    BackendImp.Resetting(swapChainPtr);
                    BackendImp.Reset(swapChainPtr);
                    var h = hookItem.InvokeOriginal(swapChainPtr, bufferCount, width, height, newFormat, swapChainFlags);
                    if (h)
                    {
                        BackendImp.Resetted(swapChainPtr);
                    }
                    return h;
                }
                return hookItem.InvokeOriginal(swapChainPtr, bufferCount, width, height, newFormat, swapChainFlags);
            };
        }


        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            BackendImp ??= D3D11BackendImp.CreateImp(@this, this);
            BackendImp.Run(@this);
            return hookItem.OriginalMethod.Invoke(@this, SyncInterval, Flags);
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
