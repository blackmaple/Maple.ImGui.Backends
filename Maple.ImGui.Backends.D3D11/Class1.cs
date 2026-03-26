using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11Device;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11DeviceContext;
using Maple.RenderSpy.Graphics.D3D11.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;

namespace Maple.ImGui.Backends.D3D11
{
    public class D3D11Backends(
        ImGuiContextPtr  guiContextPtr,
        ID3D11DevicePtr d3D11DevicePtr,
        ID3D11DeviceContextPtr d3D11DeviceContextPtr,
        WinMsgHookItem winMsgHookItem)
    {
        ImGuiContextPtr _ImGuiContext = guiContextPtr;
        ID3D11DevicePtr _ID3D11DevicePtr = d3D11DevicePtr;
        ID3D11DeviceContextPtr _ID3D11DeviceContextPtr = d3D11DeviceContextPtr;
        WinMsgHookItem WinMsgHookItem { get; } = winMsgHookItem;

        public unsafe static D3D11Backends Create(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory)
        {
            nint hWnd = pSwapChain.GetOutputWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D11Backends>("GetOutputWindow NULL");
            }
            var hResult = pSwapChain.GetDevice(out var pDevice);
            if (!hResult)
            {
                return ImGuiBackendException.Throw<D3D11Backends>($"GetDevice ERROR:{hResult}");
            }
            pDevice.GetImmediateContext(out var pContext);

            var winMsgHookItem = winMsgHookFactory.Create(hWnd);
            winMsgHookItem.SyncCallback += (hWnd, uMsg, w, l, hookItem) =>
            {
                return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            };
            winMsgHookItem.EnabledSyncCallback = true;


            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (!ImGuiImplWin32.Init(hWnd.ToPointer()))
            {
                return ImGuiBackendException.Throw<D3D11Backends>($"ImGuiImplWin32 INIT ERROR");
            }

            var pID3D11DevicePtr = new ID3D11DevicePtr(pDevice.AsPointer<ID3D11DeviceImp, ID3D11Device>());
            var pID3D11DeviceContextPtr = new ID3D11DeviceContextPtr(pContext.AsPointer<ID3D11DeviceContextImp, ID3D11DeviceContext>());
            ImGuiImplD3D11.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D11.Init(pID3D11DevicePtr, pID3D11DeviceContextPtr))
            {
                return ImGuiBackendException.Throw<D3D11Backends>($"ImGuiImplD3D11 INIT ERROR");
            }
            return new D3D11Backends(imguiContext, pID3D11DevicePtr, pID3D11DeviceContextPtr, winMsgHookItem);
        }


        public void NewFrame()
        {
            ImGuiImplWin32.NewFrame();
            ImGuiImplD3D11.NewFrame();
            Hexa.NET.ImGui.ImGui.NewFrame();
        }

        public void RaiseRender()
        {

        }

        public void EndFrame()
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
            ImGuiImplD3D11.RenderDrawData(Hexa.NET.ImGui.ImGui.GetDrawData());
        }

        public void Shutdown()
        {
            ImGuiImplWin32.Shutdown();
            ImGuiImplD3D11.Shutdown();
            Hexa.NET.ImGui.ImGui.DestroyContext();
        }
    }
}
