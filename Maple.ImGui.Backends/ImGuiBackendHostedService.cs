using Maple.RenderSpy.Graphics;
using Microsoft.Extensions.Hosting;

namespace Maple.ImGui.Backends
{
    public abstract class ImGuiBackendService(
        IGraphicsHookFactory hookFactory,
        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view)  
    {
       
        public IGraphicsHookFactory GraphicsHookFactory { get; } = hookFactory;
        public ImGuiBackendBridgeCollection BridgeCollection { get; } = bridgeCollection;
        public IImGuiUIView View { get; } = view;
        public ImGuiBackendImpBase? BackendImp { set; get; }

        public abstract Task StartAsync(CancellationToken cancellationToken);


        public abstract Task StopAsync(CancellationToken cancellationToken);

        
    }
}
