using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11Device;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11DeviceContext;
using Maple.RenderSpy.Graphics.DXGI.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;
namespace Maple.ImGui.Backends.D3D11
{
    internal sealed class D3D11BackendImp(
        ImGuiContextPtr guiContextPtr,
        ID3D11DevicePtr d3D11DevicePtr,
        ID3D11DeviceContextPtr d3D11DeviceContextPtr,
        WinMsgHookItem winMsgHookItem,
        IImGuiCustomRender customRender) : ImGuiRaiseRenderBase(customRender)
    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        ID3D11DevicePtr ID3D11DevicePtr { get; set; } = d3D11DevicePtr;
        ID3D11DeviceContextPtr ID3D11DeviceContextPtr { get; set; } = d3D11DeviceContextPtr;
        WinMsgHookItem WinMsgHookItem { get; set; } = winMsgHookItem;


        public unsafe static D3D11BackendImp CreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory, IImGuiCustomRender customRender)
        {
            var hWnd = pSwapChain.GetOutputWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>("GetOutputWindow IS NULL");
            }
            ImGuiWin32InputBridge.SetWindowHandle(hWnd);
            var hResult = pSwapChain.GetDevice<ID3D11DeviceImp>(ID3D11DeviceImp.GUID, out var pDevice);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"GetDevice ERROR:{hResult}");
            }
            pDevice.GetImmediateContext(out var pContext);

            var winMsgHookItem = winMsgHookFactory.CreateRequiresNew(hWnd);
            winMsgHookItem.SyncCallback += static (hWnd, uMsg, w, l, hookItem) => ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l) != nint.Zero;
            winMsgHookItem.EnabledSyncCallback = true;
            winMsgHookItem.Start();

            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            var dpiScale = MathF.Max(1.0f, ImGuiImplWin32.GetDpiScaleForHwnd(hWnd.ToPointer()));
            ImGuiSystemFontLoader.LoadPreferredChineseSystemFont(18.0f * dpiScale);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"ImGuiImplWin32 INIT ERROR");
            }

            var pID3D11DevicePtr = new ID3D11DevicePtr(pDevice.AsPointer<ID3D11DeviceImp, ID3D11Device>());
            var pID3D11DeviceContextPtr = new ID3D11DeviceContextPtr(pContext.AsPointer<ID3D11DeviceContextImp, ID3D11DeviceContext>());
            ImGuiImplD3D11.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D11.Init(pID3D11DevicePtr, pID3D11DeviceContextPtr))
            {
                return ImGuiBackendException.Throw<D3D11BackendImp>($"ImGuiImplD3D11 INIT ERROR");
            }
            return new D3D11BackendImp(imguiContext, pID3D11DevicePtr, pID3D11DeviceContextPtr, winMsgHookItem, customRender);
        }

        protected override void NewFrame()
        {
            ImGuiImplWin32.NewFrame();
            if (ImGuiWin32InputBridge.TryGetMousePosition(out var mousePosition))
            {
                Hexa.NET.ImGui.ImGui.GetIO().MousePos = mousePosition;
            }
            ImGuiImplD3D11.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }
        protected override void EndFrame()
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
            ImGuiImplD3D11.RenderDrawData(Hexa.NET.ImGui.ImGui.GetDrawData());
        }
        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                ImGuiImplWin32.Shutdown();
                ImGuiImplD3D11.Shutdown();
                Hexa.NET.ImGui.ImGui.DestroyContext(imguiContext);
            }
            this.WinMsgHookItem.Dispose();
        }
        public override void OnLostDevice()
        {
            ImGuiImplD3D11.InvalidateDeviceObjects();
        }
        public override void OnResetDevice()
        {
            throw new NotImplementedException();
        }


    }
}
