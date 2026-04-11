using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.OpenGL3;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics.OPENGL;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.OPENGL
{
    internal sealed class OpenGLBackendImp(
        ImGuiContextPtr guiContextPtr,
         ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view) : ImGuiBackendImpBase(bridgeCollection, view)
    {
        private const string _glslVersion = "#version 130"; // Default GLSL version, can be customized

        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;


 
        public unsafe static OpenGLBackendImp CreateImp(HandleDeviceContext hdc, OpenGLBackendHostedService hostedService)
        {
            var hWnd = hdc.WindowHandle;
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>("WindowHandle NULL");
            }

         
            var imguiContext = ImGuiApi.CreateContext();
            ImGuiApi.SetCurrentContext(imguiContext);
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (false == hostedService.InitPlatform(imguiContext, hWnd))
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>($"InitPlatform INIT ERROR");
            }

            ImGuiImplOpenGL3.SetCurrentContext(imguiContext);
            if (!ImGuiImplOpenGL3.Init(_glslVersion))
            {
                return ImGuiBackendException.Throw<OpenGLBackendImp>($"ImGuiImplOpenGL INIT ERROR");
            }
            return new OpenGLBackendImp(imguiContext, hostedService.BridgeCollection, hostedService.View);
        }

        protected override void Starting(nint context)
        {
            ImGuiImplWin32.NewFrame();
            ImGuiImplOpenGL3.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }
        protected override void Start(nint context)
        {
            this.View.RaiseRender();
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
