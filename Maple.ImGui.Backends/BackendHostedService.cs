using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics;
using Microsoft.Extensions.Hosting;

namespace Maple.ImGui.Backends
{
    public abstract class BackendHostedService(
        IGraphicsHookFactory hookFactory,
        WinMsgHookFactory winMsgHookFactory,
        ImGuiController  imGuiController) : IHostedService
    {
        protected IGraphicsHookFactory GraphicsHookFactory { get; } = hookFactory;
        protected WinMsgHookFactory WinMsgHookFactory { get; } = winMsgHookFactory;
        protected ImGuiController  Controller { get; } = imGuiController;

        public abstract Task StartAsync(CancellationToken cancellationToken);


        public abstract Task StopAsync(CancellationToken cancellationToken);


    }
}
