using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.Windows
{
    public abstract class Win32ImGuiBackendService(
        IGraphicsHookFactory hookFactory,
          WinMsgHookFactory winMsgHookFactory,
        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view) : ImGuiBackendService(hookFactory, bridgeCollection, view)
    {
        protected WinMsgHookFactory WinMsgHookFactory { get; } = winMsgHookFactory;

        public unsafe bool InitPlatform(ImGuiContextPtr imguiContext,nint hWnd)
        {
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return false;
            }

            var io = ImGuiApi.GetIO();
            if (this.BridgeCollection.UnityInputBridge is not null)
            {
                io.UserData = this.BridgeCollection.Handle.ToPointer();
                delegate* unmanaged[Cdecl]<UnsafePtr<ImGuiContext>, UnsafePtr<ImGuiViewport>, UnsafePtr<ImGuiPlatformImeData>, void> setImeDataProc = &SetImeData;
                var platformIO = ImGuiApi.GetPlatformIO();
                platformIO.PlatformSetImeDataFn = setImeDataProc;
                this.BridgeCollection.UnityInputBridge.BlockInput(this.View);
            }
            this.BridgeCollection.PlatformInputBridge?.LoadPreferredChineseSystemFont(18.0f);
            var winMsgHookItem = this.WinMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += WinProcCallback;
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe void SetImeData(UnsafePtr<ImGuiContext> ctx, UnsafePtr<ImGuiViewport> viewport, UnsafePtr<ImGuiPlatformImeData> data)
        {
            var ptr_userData = new nint(ctx.Raw.IO.UserData);
            if (ptr_userData == nint.Zero || false == ImGuiBackendBridgeCollection.TryGet<ImGuiBackendBridgeCollection>(ptr_userData, out var bridgeCollection))
            {
                return;
            }
            ref var ref_data = ref data.Raw;
            bool wantIME = ref_data.WantVisible == 1;
            bridgeCollection.UnityInputBridge?.PlatformSetImeDataFn(wantIME);
        }

        private static bool WinProcCallback(nint hWnd, uint uMsg, nuint w, nint l, WinMsgHookItem hooItem)
        {

            if (hooItem.AdditionalContent.TryGet<ImGuiBackendBridgeCollection>(nameof(ImGuiBackendBridgeCollection), out var bridgeCollection))
            {
                if (bridgeCollection.PlatformInputBridge?.TryHandleImeComposition(hWnd, uMsg, w, l) == true)
                {
                    return true;
                }
            }
            return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
        }
    }
    
}
