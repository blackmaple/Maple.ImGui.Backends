using Maple.ImGui.Backends.GraphicsCore;
using Microsoft.Extensions.DependencyInjection;


namespace Maple.ImGui.Backends.OPENGL.GraphicsCore
{
    public static class OPENGLHookExtensions
    {
        extension(IServiceCollection @this)
        {

            public IServiceCollection AddOPENGLFunctionsProvider()
            {
                return @this.AddGraphicsFunctionsProvider<OPENGLFunctionsProvider>(EnumGraphicsType.OPENGL);

            }
        }

    }


}