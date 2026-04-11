using Hexa.NET.ImGui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using UI = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends
{

    /// <summary>
    /// 系统平台相关的 接口放这里
    /// </summary>
    public interface IImGuiPlatformInputBridge
    {
        bool TryHandleImeComposition(nint handle, uint uMsg, nuint wParam, nint lParam);

        bool ShouldConsumeWindowMessage(uint uMsg);

        bool LoadPreferredChineseSystemFont(float fontSize = 18.0f);


       // void HookWindowMessage(nint hWnd );
    }
}
