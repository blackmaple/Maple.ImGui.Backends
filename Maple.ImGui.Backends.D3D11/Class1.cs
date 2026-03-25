using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D11;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.RenderSpy.Graphics.D3D11.COM_D3D11Device;
using Maple.RenderSpy.Graphics.D3D11.COM_DXGISwapChain;
using Maple.RenderSpy.Graphics.Windows.COM;

namespace Maple.ImGui.Backends.D3D11
{
    public class D3D11Backends
    {

        public unsafe static D3D11Backends Create(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, Maple.Hook.WinMsg.WinMsgHookFactory winMsgHookFactory)
        {
            nint hWnd = pSwapChain.GetOutputWindow();
            pSwapChain.GetDevice(out var pDevice);
            pDevice.GetImmediateContext(out var pContext);

            var winMsgHookItem = winMsgHookFactory.Create(hWnd);
            winMsgHookItem.SyncCallback += (hWnd, uMsg, w, l, hookItem) =>
            {
                return nint.Zero != ImGuiImplWin32.WndProcHandler(hWnd, uMsg, w, l);
            };
            winMsgHookItem.EnabledSyncCallback = true;


            var imguiContext = Hexa.NET.ImGui.ImGui.CreateContext();
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            ImGuiImplWin32.Init(hWnd.ToPointer());

            ImGuiImplD3D11.SetCurrentContext(imguiContext);
            ImGuiImplD3D11.Init(new ID3D11DevicePtr((ID3D11Device*)pDevice.PTR_IUNKNOWN.Pointer),
                new ID3D11DeviceContextPtr((ID3D11DeviceContext*)pContext.PTR_IUNKNOWN.Pointer));

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
          
            Hexa.NET.ImGui.ImGui.DestroyContext
        }
    }
}
