using Maple.ImGui.Backends.GraphicsCore;
using Maple.RenderSpy.Graphics.D3D11;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.D3D11.GraphicsCore
{
    public static class D3D11HookExtensions
    {
        public static IServiceCollection AddD3D11FunctionsProvider(this IServiceCollection @this)
        {
            return @this.AddGraphicsFunctionsProvider<D3D11FunctionsProvider>(EnumGraphicsType.D3D11);
        }

    }
}
