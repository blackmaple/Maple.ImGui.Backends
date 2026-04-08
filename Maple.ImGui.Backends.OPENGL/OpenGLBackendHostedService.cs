using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.OPENGL;
namespace Maple.ImGui.Backends.OPENGL
{
    public class OpenGLBackendHostedService : BackendHostedService
    {


        OpenGLBackendImp? BackendImp { get; set; }

        OPENGLwglSwapBuffersHookItem HookItem { get; set; }

        public OpenGLBackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiController controller)
            : base(hookFactory, winMsgHookFactory, controller)
        {

            this.HookItem = hookFactory.Create<OPENGLwglSwapBuffersHookItem>(EnumGraphicsType.OPENGL);
            this.HookItem.SyncCallback = Hook_wglSwapBuffers;


        }


        private bool Hook_wglSwapBuffers(HandleDeviceContext hdc, OPENGLwglSwapBuffersHookItem hookItem)
        {
            BackendImp ??= OpenGLBackendImp.CreateImp(hdc, WinMsgHookFactory, this.Controller);
            BackendImp.Run(hdc.HandleContext);
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
