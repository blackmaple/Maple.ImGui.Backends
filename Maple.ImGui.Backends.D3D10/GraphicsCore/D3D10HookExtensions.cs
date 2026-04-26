using Maple.ImGui.Backends.GraphicsCore;
using Maple.RenderSpy.Graphics.D3D10;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.D3D10.GraphicsCore
{
    public static class D3D10HookExtensions
    {
        public static IServiceCollection AddD3D10FunctionsProvider(this IServiceCollection @this)
        {
            @this.AddGraphicsFunctionsProvider<D3D10FunctionsProvider>(EnumGraphicsType.D3D10);
            return @this;
        }

    }
}
