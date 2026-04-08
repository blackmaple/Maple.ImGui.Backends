using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Windows.Win32;
using Windows.Win32.Foundation;
using UI = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends
{
    public class DefaultImGuiCustomRender(ILogger<DefaultImGuiCustomRender> logger) : IImGuiRender
    {
        public ILogger Logger { get; } = logger;
        string text = string.Empty;
        nint MainWindowHandle { get; } = Process.GetCurrentProcess().MainWindowHandle;
        public void RaiseRender()
        {
            UI.ShowDemoWindow();

            UI.Begin("Demo");

            UI.InputText("##CurrencyEdit", ref text, (nuint)1024);


            if (UI.Button("Test"))
            {
               
              //  PInvoke.SetFocus(fMain);
              //  ImGuiWin32InputBridge.SetImm(MainWindowHandle);
                //   PInvoke.SetForegroundWindow()
                //  PInvoke
            }

            UI.End();
        }
    }

}
