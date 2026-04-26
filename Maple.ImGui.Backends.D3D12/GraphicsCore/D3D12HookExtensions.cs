using Maple.ImGui.Backends.GraphicsCore;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.D3D12;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore
{
    public static class D3D12HookExtensions
    {
        public static IServiceCollection AddD3D12FunctionsProvider(this IServiceCollection @this)
        {
            return @this.AddGraphicsFunctionsProvider<D3D12FunctionsProvider>(EnumGraphicsType.D3D12);
        }

    }
}
