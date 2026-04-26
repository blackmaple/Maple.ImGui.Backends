using Maple.Hook.Abstractions;

namespace Maple.ImGui.Backends.GraphicsCore
{
    public interface IGraphicsHookItem<T> where T : HookItem
    {
        /// <summary>
        /// 要求实现类提供静态 Create 方法，返回 T
        /// </summary>
        /// <param name="hookFactory"></param>
        /// <param name="functionsProvider"></param>
        /// <returns></returns>
        static abstract T Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider);  

    }
}
