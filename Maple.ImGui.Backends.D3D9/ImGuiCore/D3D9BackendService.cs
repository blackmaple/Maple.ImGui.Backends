using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.D3D9.GraphicsCore.HOOK_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Maple.UnmanagedExtensions;
using Microsoft.Extensions.Logging;
namespace Maple.ImGui.Backends.D3D9.ImGuiCore
{
    public sealed class D3D9BackendService : Win32ImGuiBackendService
    {

        D3D9EndSceneHookItem EndSceneHookItem { get; set; }
        D3D9ResetHookItem ResetHookItem { get; set; }

        public D3D9BackendService(ILogger<D3D9BackendService> logger, IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view)
         : base(logger, hookFactory, winMsgHookFactory, bridgeCollection, view)
        {


            this.EndSceneHookItem = hookFactory.Create<D3D9EndSceneHookItem>(EnumGraphicsType.D3D9);
            this.EndSceneHookItem.SyncCallback = HookEndScene;
            //this.EndSceneHookItem.Enable();

            this.ResetHookItem = hookFactory.Create<D3D9ResetHookItem>(EnumGraphicsType.D3D9);
            this.ResetHookItem.SyncCallback = HookReset;
            //   this.ResetHookItem.Enable();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.EndSceneHookItem.Enable();
            this.ResetHookItem.Enable();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.EndSceneHookItem.Dispose();
            this.ResetHookItem.Dispose();
            this.BackendImp?.Dispose();

            return Task.CompletedTask;
        }

        private COM_HRESULT HookEndScene(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, D3D9EndSceneHookItem hookItem)
        {
            if (BackendImp is not null)
            {
                BackendImp.Run(@this);
            }
            else if (D3D9BackendImp.TryCreateImp(@this, this, out var backendImp))
            {
                BackendImp = backendImp;
            }
            return hookItem.OriginalMethod.Invoke(@this);
        }

        private COM_HRESULT HookReset(COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, UnsafePtr ptr, D3D9ResetHookItem hookItem)
        {
            if (BackendImp is not null)
            {
                BackendImp.Resetting(@this);
                BackendImp.Reset(@this);
                var h = hookItem.OriginalMethod.Invoke(@this, ptr);
                BackendImp.Resetted(@this);
                return h;
            }
            return hookItem.OriginalMethod.Invoke(@this, ptr);
        }
    }
}
