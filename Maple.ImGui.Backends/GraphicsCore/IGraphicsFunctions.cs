namespace Maple.ImGui.Backends.GraphicsCore
{
    public interface IGraphicsFunctions<T> where T : GraphicsFunctionsProvider
    {
        static abstract T Create(IServiceProvider provider);

    }
}
