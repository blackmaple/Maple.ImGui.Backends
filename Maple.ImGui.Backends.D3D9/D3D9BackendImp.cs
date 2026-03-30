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
        IDirect3DDevice9Ptr D3D9DevicePtr,
        WinMsgHookItem winMsgHookItem,
        IImGuiCustomRender customRender) : ImGuiRaiseRenderBase(customRender)

    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        IDirect3DDevice9Ptr ID3D9DevicePtr { get; set; } = D3D9DevicePtr;
        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;

        public unsafe static D3D9BackendImp CreateImp(COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pDevice, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender customRender)
        {
            var hWnd = pDevice.GetFocusWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>("GetFocusWindow IS NULL");
            }
            ImGuiWin32InputBridge.SetWindowHandle(hWnd);

            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += static (hWnd, uMsg, w, l, hookItem) => nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();

            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            var dpiScale = MathF.Max(1.0f, ImGuiImplWin32.GetDpiScaleForHwnd(hWnd.ToPointer()));
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f * dpiScale);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>($"ImGuiImplWin32 INIT ERROR");
            }

            var pID3D9DevicePtr = new IDirect3DDevice9Ptr(pDevice.AsPointer<IDirect3DDevice9Imp, IDirect3DDevice9>());
            ImGuiImplD3D9.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D9.Init(pID3D9DevicePtr))
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>($"ImGuiImplD3D9 INIT ERROR");
            }
            return new D3D9BackendImp(imguiContext, pID3D9DevicePtr, winMsgHookItem, customRender);
        }

        protected override void NewFrame()
        {
            ImGuiImplWin32.NewFrame();
            if (ImGuiWin32InputBridge.TryGetMousePosition(out var mousePosition))
            {
                Hexa.NET.ImGui.ImGui.GetIO().MousePos = mousePosition;
            }
            ImGuiImplD3D9.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }
        protected override void EndFrame()
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
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
        public override void OnLostDevice()
        {
            ImGuiImplD3D9.InvalidateDeviceObjects();
        }
        public override void OnResetDevice()
        {
            ImGuiImplD3D9.CreateDeviceObjects();
        }
    }
}
