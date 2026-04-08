using Hexa.NET.ImGui;
using Maple.UnmanagedExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using UI = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends
{
    /// <summary>
    /// 纯渲染接口，主要是为了让用户可以在不依赖特定平台输入桥接的情况下，单纯使用 ImGui 绘制功能，目前主要用于测试和调试，后续如果有其他需要单纯使用 ImGui 绘制功能的场景也可以使用这个接口
    /// </summary>
    public interface IImGuiRender
    {
        void RaiseRender();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="provider"></param>
    public sealed class ImGuiController(ILogger<ImGuiController> logger, IServiceProvider provider) : GCNormalSelf, IDisposable
    {
        public ILogger Logger { get; } = logger;
        public IImGuiRender? CustomRender { get; } = provider.GetService<IImGuiRender>();
        public IImGuiUnityInputBridge? UnityInputBridge { get; } = provider.GetService<IImGuiUnityInputBridge>();
        public IImGuiWin32InputBridge? Win32InputBridge { get; } = provider.GetService<IImGuiWin32InputBridge>();


    }

    /// <summary>
    /// unity 输入接口放这里，主要是为了处理输入法相关的接口，目前仅有一个接口，后续如果有其他输入相关的接口也可以放在这里
    /// </summary>
    public interface IImGuiUnityInputBridge
    {
        void PlatformSetImeDataFn(bool on);
    }

    /// <summary>
    /// winapi 接口放这里
    /// </summary>
    public interface IImGuiWin32InputBridge
    {
        bool TryHandleImeComposition(nint handle, uint uMsg, nuint wParam, nint lParam);
    }
}
