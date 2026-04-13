using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.Windows;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.OPENGL;
namespace Maple.ImGui.Backends.OPENGL
{
    public class OpenGLBackendService : Win32ImGuiBackendService
    {


 
        OPENGLwglSwapBuffersHookItem HookItem { get; set; }

        public OpenGLBackendService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection,IImGuiUIView view)
            : base(hookFactory, winMsgHookFactory, bridgeCollection,view)
        {

            this.HookItem = hookFactory.Create<OPENGLwglSwapBuffersHookItem>(EnumGraphicsType.OPENGL);
            this.HookItem.SyncCallback = Hook_wglSwapBuffers;


        }


        private bool Hook_wglSwapBuffers(HandleDeviceContext hdc, OPENGLwglSwapBuffersHookItem hookItem)
        {
            BackendImp ??= OpenGLBackendImp.CreateImp(hdc, this);
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
            this.BridgeCollection.Dispose();
            return Task.CompletedTask;
        }
    }
}
