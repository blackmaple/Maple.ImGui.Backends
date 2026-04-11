using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Maple.ImGui.Backends
{
    /// <summary>
    /// 利用后端绘画图片的委托，主要是为了让用户可以在不依赖特定平台输入桥接的情况下，单纯使用 ImGui 绘制功能，目前主要用于测试和调试，后续如果有其他需要单纯使用 ImGui 绘制功能的场景也可以使用这个接口
    /// </summary>
    /// <param name="category"></param>
    /// <param name="objectId"></param>
    /// <returns></returns>
    public delegate bool TryDrawImageDelegate(string? category, string objectId);

    /// <summary>
    /// 纯渲染接口，主要是为了让用户可以在不依赖特定平台输入桥接的情况下，单纯使用 ImGui 绘制功能，目前主要用于测试和调试，后续如果有其他需要单纯使用 ImGui 绘制功能的场景也可以使用这个接口
    /// </summary>
    public interface IImGuiUIView
    {
        

        void RaiseRender();

        /// <summary>
        /// bool TryDrawImage(string? category, string objectId) => false;
        /// </summary>
          TryDrawImageDelegate? TryDrawImage { get; set; }  

        bool ShowSessionWindow { get; }
    }
}
