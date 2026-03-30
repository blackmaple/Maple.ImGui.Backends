using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.DXGI.HOOK_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
namespace Maple.ImGui.Backends.D3D10
{
    public sealed class D3D10BackendHostedService : BackendHostedService
    {

        DXGIPresentHookItem HookItem { get; }
        D3D10BackendImp? BackendImp { get; set; }

        public D3D10BackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender imGuiCustomRender)
            : base(hookFactory, winMsgHookFactory, imGuiCustomRender)
        {

            this.HookItem = hookFactory.Create<DXGIPresentHookItem>( EnumGraphicsType.D3D10);
            this.HookItem.SyncCallback = HookPresent;
         //   this.HookItem.Enable();
        }


        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            BackendImp ??= D3D10BackendImp.CreateImp(@this, WinMsgHookFactory, ImGuiCustomRender);
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
