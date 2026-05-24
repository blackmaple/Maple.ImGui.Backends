using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.OPENGL.GraphicsCore;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Microsoft.Extensions.Logging;
namespace Maple.ImGui.Backends.OPENGL.ImGuiCore
{
    public class OpenGLBackendService : Win32ImGuiBackendService
    {


 
        OPENGLwglSwapBuffersHookItem HookItem { get; set; }

        public OpenGLBackendService(ILogger<OpenGLBackendService> logger, IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection,IImGuiUIView view)
            : base(logger,hookFactory, winMsgHookFactory, bridgeCollection,view)
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
