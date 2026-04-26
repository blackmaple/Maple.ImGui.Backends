using Maple.ImGui.Backends.GraphicsCore;
using Microsoft.Extensions.DependencyInjection;


namespace Maple.ImGui.Backends.D3D9.GraphicsCore
{
    public static class D3D9HookExtensions
    {
        extension(IServiceCollection @this)
        {

            public IServiceCollection AddD3D9FunctionsProvider()
            {
                return @this.AddGraphicsFunctionsProvider<D3D9FunctionsProvider>(EnumGraphicsType.D3D9);
            }
        }
     
    }


}