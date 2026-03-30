using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.DXGI.HOOK_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
namespace Maple.ImGui.Backends.D3D11
{
    public class D3D11BackendHostedService : BackendHostedService
    {
        DXGIPresentHookItem HookItem { get; }
        D3D11BackendImp? BackendImp { get; set; }

        public D3D11BackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender imGuiCustomRender)
            : base(hookFactory, winMsgHookFactory, imGuiCustomRender)
        {
            this.HookItem = hookFactory.Create<DXGIPresentHookItem>(EnumGraphicsType.D3D11);
            this.HookItem.SyncCallback = HookPresent;
        }


        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            BackendImp ??= D3D11BackendImp.CreateImp(@this, WinMsgHookFactory, ImGuiCustomRender);
            BackendImp.RaiseRender();
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

            return Task.CompletedTask;
        }
    }
}
