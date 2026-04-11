using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.Windows;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.DXGI.HOOK_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
namespace Maple.ImGui.Backends.D3D10
{
    public sealed class D3D10BackendHostedService : Win32ImGuiBackendHostedService
    {
        DXGIPresentHookItem HookItem { get; }

        public D3D10BackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection,IImGuiUIView view)
            : base(hookFactory, winMsgHookFactory, bridgeCollection, view)
        {

            this.HookItem = hookFactory.Create<DXGIPresentHookItem>(EnumGraphicsType.D3D10);
            this.HookItem.SyncCallback = HookPresent;
            //   this.HookItem.Enable();
        }


        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            BackendImp ??= D3D10BackendImp.CreateImp(@this, this);
            BackendImp.Run(@this);
            return hookItem.OriginalMethod.Invoke(@this, SyncInterval, Flags);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.HookItem.Enable();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.HookItem.Dispose();
            this.BackendImp?.Dispose();
            this.BridgeCollection.Dispose();
            return Task.CompletedTask;

        }
    }
}
