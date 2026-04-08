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
        ImGuiController controller) : ImGuiRaiseRenderBase(controller)
    {
        private const string _glslVersion = "#version 130"; // Default GLSL version, can be customized

        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;


        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;

        public unsafe static OpenGLBackendImp CreateImp(HandleDeviceContext hdc, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, ImGuiController controller)
        {
            var hWnd = hdc.WindowHandle;
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>("WindowHandle NULL");
            }

            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>($"ImGuiImplWin32 INIT ERROR");
            }
            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += (hWnd, uMsg, w, l, hookItem) =>
            {
                return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            };
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f);
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();

            ImGuiImplOpenGL3.SetCurrentContext(imguiContext);
            if (!ImGuiImplOpenGL3.Init(_glslVersion))
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>($"ImGuiImplOpenGL INIT ERROR");
            }
            return new OpenGLBackendImp(imguiContext, winMsgHookItem, controller);
        }

        protected override void Starting(nint context)
        {
            ImGuiImplWin32.NewFrame();
            ImGuiImplOpenGL3.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }
        protected override void Start(nint context)
        {
            this.Controller.CustomRender?.RaiseRender();
        }
        protected override void Started(nint context)
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
        }
        protected override void Build(nint context) { ImGuiImplOpenGL3.RenderDrawData(Hexa.NET.ImGui.ImGui.GetDrawData()); }


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

        public override void Reset(nint context)
        {


        }
        public override void Resetting(nint context)
        {


        }
        public override void Resetted(nint context)
        {


        }
    }

}
