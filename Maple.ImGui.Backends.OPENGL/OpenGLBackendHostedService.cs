using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.OPENGL;
namespace Maple.ImGui.Backends.OPENGL
{
    public class OpenGLBackendHostedService : BackendHostedService
    {


        OpenGLBackendImp? BackendImp { get; set; }

        OPENGLwglSwapBuffersHookItem HookItem { get; set; }

        public OpenGLBackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender imGuiCustomRender)
            : base(hookFactory, winMsgHookFactory, imGuiCustomRender)
        {

            this.HookItem = hookFactory.Create<OPENGLwglSwapBuffersHookItem>(EnumGraphicsType.OPENGL);
            this.HookItem.SyncCallback = Hook_wglSwapBuffers;


        }


        private bool Hook_wglSwapBuffers(HandleDeviceContext hdc, OPENGLwglSwapBuffersHookItem hookItem)
        {
            BackendImp ??= OpenGLBackendImp.CreateImp(hdc, WinMsgHookFactory, ImGuiCustomRender);
            BackendImp.RaiseRender();
            return hookItem.OriginalMethod.Invoke(hdc.HandleContext);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.HookItem.Enable();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.HookItem.Dispose();
            this.BackendImp?.Dispose();

            return Task.CompletedTask;
        }
    }
}
