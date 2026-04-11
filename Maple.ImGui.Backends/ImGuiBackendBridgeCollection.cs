using Maple.UnmanagedExtensions;
using Microsoft.Extensions.DependencyInjection;
namespace Maple.ImGui.Backends
{
    public class ImGuiBackendBridgeCollection(IServiceProvider serviceProvider) : GCNormalSelf 
    {
        public IImGuiUnityInputBridge? UnityInputBridge { get; set; } = serviceProvider.GetService<IImGuiUnityInputBridge>();
        public IImGuiPlatformInputBridge? PlatformInputBridge { get; set; } = serviceProvider.GetService<IImGuiPlatformInputBridge>();
    }
}
