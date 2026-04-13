using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D9;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends;
using Maple.RenderSpy.Graphics.D3D9.COM_Direct3DDevice9;
using Maple.RenderSpy.Graphics.Windows.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D9
{
    public sealed class D3D9BackendImp(
        ImGuiContextPtr guiContextPtr,
        COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> D3D9DevicePtr,
        ImGuiBackendBridgeCollection controller,
        IImGuiUIView view) : ImGuiBackendImpBase(controller, view)

    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> ID3D9DevicePtr { get; set; } = D3D9DevicePtr;


        public unsafe static D3D9BackendImp CreateImp(COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pDevice, D3D9BackendService hostedService)
        {
            var hWnd = pDevice.GetFocusWindow();
            if (hWnd == nint.Zero)
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>("GetFocusWindow IS NULL");
            }


            var imguiContext = ImGuiApi.CreateContext();
            ImGuiApi.SetCurrentContext(imguiContext);
            ImGuiImplWin32.SetCurrentContext(imguiContext);
            if (false == hostedService.InitPlatform(imguiContext, hWnd))
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>($"InitPlatform INIT ERROR");
            }

            var pID3D9DevicePtr = new IDirect3DDevice9Ptr(pDevice.AsPointer<IDirect3DDevice9Imp, IDirect3DDevice9>());
            ImGuiImplD3D9.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D9.Init(pID3D9DevicePtr))
            {
                return ImGuiBackendException.Throw<D3D9BackendImp>($"ImGuiImplD3D9 INIT ERROR");
            }
            return new D3D9BackendImp(imguiContext, pDevice, hostedService.BridgeCollection, hostedService.View);
        }

        protected override void Starting(nint context)
        {
            Hexa.NET.ImGui.ImGui.EndFrame();
            Hexa.NET.ImGui.ImGui.Render();
        }
        protected override void Start(nint context)
        {
            this.View.RaiseRender();
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
