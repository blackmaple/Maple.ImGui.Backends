using Microsoft.Extensions.DependencyInjection;
namespace Maple.ImGui.Backends.Unity
{
    [Obsolete("This class is deprecated. Use a different input bridge implementation.")]
    public static class ImGuiUnityInputBridgeExtensions
    {
        extension(IServiceCollection @this)
        {
            public IServiceCollection AddDefaultUnityInputBridge()
                 => @this.AddUnityInputBridge<  DefaultImGuiUnityInputBridge>();

        }
    }
}
