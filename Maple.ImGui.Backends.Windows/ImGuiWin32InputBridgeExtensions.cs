using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.Windows
{
    public static class ImGuiWin32InputBridgeExtensions
    {
        extension(IServiceCollection @this)
        { 
            public IServiceCollection AddDefaultWin32InputBridge()
                 => @this.AddPlatformInputBridge<DefaultImGuiWin32InputBridge>();

        }
    }
}
