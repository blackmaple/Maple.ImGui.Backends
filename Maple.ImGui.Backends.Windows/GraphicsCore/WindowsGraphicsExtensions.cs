using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.Native;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Maple.ImGui.Backends.Windows.GraphicsCore
{
    public static class WindowsGraphicsExtensions
    {
        extension(IServiceCollection @this)
        {
            public IServiceCollection AddWindowsGraphicsHookFactory(bool jmpChain = false)
            {
                @this.TryAddSingleton<Win32WindowFactory>();
                @this.AddGraphicsHookFactory(jmpChain);
                return @this;
            }



        }

    }
}