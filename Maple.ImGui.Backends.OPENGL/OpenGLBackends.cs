using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.OpenGL3;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics.OPENGL;

namespace Maple.ImGui.Backends.OPENGL
{
    public sealed class OpenGLBackends(
        ImGuiContextPtr guiContextPtr,
     
        WinMsgHookItem winMsgHookItem,
        IImGuiCustomRender customRender) : ImGuiRaiseRenderBase(customRender)
    {
        private const string _glslVersion = "#version 130"; // Default GLSL version, can be customized

        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
       
        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;

        public unsafe static OpenGLBackends Create(HandleDeviceContext hdc, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender customRender)
        {
            nint hWnd = hdc.WindowHandle;
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<OpenGLBackends>("WindowHandle NULL");
            }

            var winMsgHookItem = winMsgHookFactory.Create(hWnd);
            winMsgHookItem.SyncCallback += static (hWnd, uMsg, w, l, hookItem) => nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            winMsgHookItem.EnabledSyncCallback = true;


            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<OpenGLBackends>($"ImGuiImplWin32 INIT ERROR");
            }

            ImGuiImplOpenGL3.SetCurrentContext(imguiContext);
            if (!ImGuiImplOpenGL3.Init(_glslVersion))
            {
                return ImGuiBackendException.Throw<OpenGLBackends>($"ImGuiImplOpenGL INIT ERROR");
            }
            return new OpenGLBackends(imguiContext,  winMsgHookItem, customRender);
        }


        protected override void NewFrame()
        {
            ImGuiImplWin32.NewFrame();
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
