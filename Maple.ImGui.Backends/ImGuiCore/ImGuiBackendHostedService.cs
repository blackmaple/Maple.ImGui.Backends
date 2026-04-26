using Maple.ImGui.Backends.GraphicsCore;

namespace Maple.ImGui.Backends.ImGuiCore
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
