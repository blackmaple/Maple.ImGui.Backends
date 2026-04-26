using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Maple.ImGui.Backends.GraphicsCore
{
    public static class GraphicsExtensions
    {
        extension(IServiceCollection @this)
        {
            public IServiceCollection AddGraphicsHookFactory(bool jmpChain = false)
            {
                if (jmpChain)
                {
                    @this.TryAddSingleton<ISupperHookFactory, JmpChainSupperHookFactory>();
                }
                else
                {
                    @this.TryAddSingleton<ISupperHookFactory, DefaultSupperHookFactory>();
                }

                @this.TryAddSingleton<IGraphicsHookFactory, DefaultGraphicsHookFactory>();
                return @this;
            }

            public IServiceCollection AddGraphicsFunctionsProvider<T>(EnumGraphicsType graphicsType)
                where T : GraphicsFunctionsProvider, IGraphicsFunctions<T>
            {
                @this.AddKeyedSingleton<GraphicsFunctionsProvider>(graphicsType, (p, key) => T.Create(p));
                return @this;
            }

        }

    }
}
