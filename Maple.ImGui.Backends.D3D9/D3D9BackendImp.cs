using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D9;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends;
using Maple.RenderSpy.Graphics.D3D9.COM_Direct3DDevice9;
using Maple.RenderSpy.Graphics.Windows.COM;

namespace Maple.ImGui.Backends.D3D9
{
    public sealed class D3D9BackendImp(
        ImGuiContextPtr guiContextPtr,
        COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> D3D9DevicePtr,
       
        WinMsgHookItem winMsgHookItem,
        ImGuiController controller) : ImGuiRaiseRenderBase(controller)

    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> ID3D9DevicePtr { get; set; } = D3D9DevicePtr;
       
        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;

        public unsafe static D3D9BackendImp CreateImp(COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pDevice, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, ImGuiController controller)
        {
            var hWnd = pDevice.GetFocusWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>("GetFocusWindow IS NULL");
            }
 
      
            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                
                return ImGuiBackendException.Throw<D3D9BackendImp>($"ImGuiImplWin32 INIT ERROR");
            }
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f);

            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += (hWnd, uMsg, w, l, hookItem) =>
            {
                return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            };
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();


            var pID3D9DevicePtr = new IDirect3DDevice9Ptr(pDevice.AsPointer<IDirect3DDevice9Imp, IDirect3DDevice9>());
            ImGuiImplD3D9.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D9.Init(pID3D9DevicePtr))
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>($"ImGuiImplD3D9 INIT ERROR");
            }
            return new D3D9BackendImp(imguiContext, pDevice,  winMsgHookItem, controller);
        }

        protected override void Starting(nint context)
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
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
        protected override void Build(nint context)
        {
            ImGuiImplD3D9.RenderDrawData(Hexa.NET.ImGui.ImGui.GetDrawData());
        }


        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                ImGuiImplWin32.Shutdown();
                ImGuiImplD3D9.Shutdown();
                Hexa.NET.ImGui.ImGui.DestroyContext(imguiContext);
            }
            this.WinMsgHookItem.Dispose();
        }


        public override void Resetting(nint context)
        {
          
        }
        public override void Reset(nint context)
        {
            ImGuiImplD3D9.InvalidateDeviceObjects();
        }
        public override void Resetted(nint context)
        {
            ImGuiImplD3D9.CreateDeviceObjects();
        }
    }
}
