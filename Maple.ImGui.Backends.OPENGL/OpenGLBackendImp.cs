using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.OpenGL3;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends;
using Maple.RenderSpy.Graphics.OPENGL;

namespace Maple.ImGui.Backends.OPENGL
{
    internal sealed class OpenGLBackendImp(
        ImGuiContextPtr guiContextPtr,

        WinMsgHookItem winMsgHookItem,
        IImGuiCustomRender customRender) : ImGuiRaiseRenderBase(customRender)
    {
        private const string _glslVersion = "#version 130"; // Default GLSL version, can be customized

        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;

        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;

        public unsafe static OpenGLBackendImp CreateImp(HandleDeviceContext hdc, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender customRender)
        {
            nint hWnd = hdc.WindowHandle;
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>("WindowHandle NULL");
            }
            ImGuiWin32InputBridge.SetWindowHandle(hWnd);

            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += static (hWnd, uMsg, w, l, hookItem) => nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();

            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            var dpiScale = MathF.Max(1.0f, ImGuiImplWin32.GetDpiScaleForHwnd(hWnd.ToPointer()));
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f * dpiScale);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>($"ImGuiImplWin32 INIT ERROR");
            }

            ImGuiImplOpenGL3.SetCurrentContext(imguiContext);
            if (!ImGuiImplOpenGL3.Init(_glslVersion))
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>($"ImGuiImplOpenGL INIT ERROR");
            }
            return new OpenGLBackendImp(imguiContext, winMsgHookItem, customRender);
        }

        protected override void NewFrame()
        {
            ImGuiImplWin32.NewFrame();
            if (ImGuiWin32InputBridge.TryGetMousePosition(out var mousePosition))
            {
                Hexa.NET.ImGui.ImGui.GetIO().MousePos = mousePosition;
            }
            ImGuiImplOpenGL3.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }
        protected override void EndFrame()
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
            ImGuiImplOpenGL3.RenderDrawData(Hexa.NET.ImGui.ImGui.GetDrawData());
        }
        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                ImGuiImplWin32.Shutdown();
                ImGuiImplOpenGL3.Shutdown();
                Hexa.NET.ImGui.ImGui.DestroyContext(imguiContext);
            }
        }
        public override void OnLostDevice()
        {
            throw new NotImplementedException();
        }
        public override void OnResetDevice()
        {
            throw new NotImplementedException();
        }
    }

}
