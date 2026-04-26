using Maple.Hook.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.GraphicsCore
{
    internal class DefaultGraphicsHookFactory(IServiceProvider serviceProvider, ISupperHookFactory hookFactory) : IGraphicsHookFactory
    {
        ISupperHookFactory HookFactory { get; } = hookFactory;
        IServiceProvider Provider { get; } = serviceProvider;
        public T Create<T>(EnumGraphicsType graphicsType) where T : HookItem, IGraphicsHookItem<T>
        {
            var functionsProvider = Provider.GetRequiredKeyedService<GraphicsFunctionsProvider>(graphicsType);
            return T.Create(this.HookFactory, functionsProvider);

        }
    }
    public interface IGraphicsHookFactory
    {
        T Create<T>(EnumGraphicsType graphicsType) where T : HookItem, IGraphicsHookItem<T>;

    }
}
