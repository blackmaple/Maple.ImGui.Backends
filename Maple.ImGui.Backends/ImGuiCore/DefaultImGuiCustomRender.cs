//using Microsoft.Extensions.Logging;
//using System.Diagnostics;
//using UI = Hexa.NET.ImGui.ImGui;
//namespace Maple.ImGui.Backends
//{
//    public class DefaultImGuiCustomRender(ILogger<DefaultImGuiCustomRender> logger) : IImGuiUIView
//    {
//        public ILogger Logger { get; } = logger;
//        string text = string.Empty;
//        nint MainWindowHandle { get; } = Process.GetCurrentProcess().MainWindowHandle;
//        public TryDrawImageDelegate? TryDrawImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


//        public bool ShowSessionWindow { get; set; }
//        public bool LauncherVisible { get; set; }

//        public bool SessionWindowVisible => throw new NotImplementedException();

//        public bool EnabledDraw { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//        public void RaiseRender()
//        {
//            UI.ShowDemoWindow();

//            UI.Begin("Demo");

//            UI.InputText("##CurrencyEdit", ref text, (nuint)1024);


//            if (UI.Button("Test"))
//            {

//                //  PInvoke.SetFocus(fMain);
//                //  ImGuiWin32InputBridge.SetImm(MainWindowHandle);
//                //   PInvoke.SetForegroundWindow()
//                //  PInvoke
//            }

//            UI.End();
//        }
//    }
//}
