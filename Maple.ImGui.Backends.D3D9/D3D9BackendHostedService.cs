using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.Windows;
using Maple.RenderSpy.Graphics;
using Maple.RenderSpy.Graphics.D3D9.COM_Direct3DDevice9;
using Maple.RenderSpy.Graphics.D3D9.HOOK_Direct3DDevice9;
using Maple.RenderSpy.Graphics.Windows.COM;
using Maple.UnmanagedExtensions;
namespace Maple.ImGui.Backends.D3D9
{
    public sealed class D3D9BackendHostedService : Win32ImGuiBackendHostedService
    {
     
        D3D9EndSceneHookItem EndSceneHookItem { get; set; }
        D3D9ResetHookItem ResetHookItem { get; set; }

        public D3D9BackendHostedService(IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection,IImGuiUIView view)
         : base(hookFactory, winMsgHookFactory, bridgeCollection, view)
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

        private COM_HRESULT HookEndScene(COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, D3D9EndSceneHookItem hookItem)
        {
            BackendImp ??= D3D9BackendImp.CreateImp(@this, this);
            BackendImp.Run(@this);
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
