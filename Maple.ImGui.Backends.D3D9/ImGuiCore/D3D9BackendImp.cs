using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D9;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D9.ImGuiCore
{
    public sealed class D3D9BackendImp(
        ImGuiContextPtr guiContextPtr,
        COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> D3D9DevicePtr,
        ImGuiBackendBridgeCollection controller,
        IImGuiUIView view) : ImGuiBackendImpBase(controller, view)

    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = guiContextPtr;
        COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> ID3D9DevicePtr { get; set; } = D3D9DevicePtr;


        public unsafe static bool TryCreateImp(COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> pDevice, D3D9BackendService backendService, out D3D9BackendImp backendImp)
        {
            Unsafe.SkipInit(out backendImp);
            if (pDevice.GetCreationParameters(out var pDesc))
            {
                backendService.Logger.LogError("{NAME} IS ERROR", nameof(IDirect3DDevice9ImpExtension.GetCreationParameters));
                return false;
            }
            var hWnd = pDesc.hFocusWindow;
            if (hWnd == nint.Zero)
            {
                backendService.Logger.LogError("{NAME} IS NULL", nameof(D3DDEVICE_CREATION_PARAMETERS.hFocusWindow));
                return false;

            }

            var imguiContext = ImGuiApi.CreateContext();
            ImGuiApi.SetCurrentContext(imguiContext);
            if (false == backendService.InitPlatform(imguiContext, hWnd))
            {
                backendService.Logger.LogError("{NAME} IS ERROR", nameof(D3D9BackendService.InitPlatform));
                return false;
            }


            var pID3D9DevicePtr = new IDirect3DDevice9Ptr(pDevice.AsPointer<IDirect3DDevice9Imp, IDirect3DDevice9>());
            ImGuiImplD3D9.SetCurrentContext(imguiContext);
            if (!ImGuiImplD3D9.Init(pID3D9DevicePtr))
            {
                backendService.Logger.LogError("{NAME} IS ERROR", nameof(ImGuiImplD3D9.Init));
                return false;
            }
            backendImp = new D3D9BackendImp(imguiContext, pDevice, backendService.BridgeCollection, backendService.View);
            return true;
        }

        protected override void Starting(nint context)
        {
            ImGuiApi.EndFrame();
            ImGuiApi.Render();
        }
        protected override void Start(nint context)
        {
            this.View.RaiseRender();
        }
        protected override void Started(nint context)
        {
            ImGuiApi.EndFrame();
            ImGuiApi.Render();
        }
        protected override void Build(nint context)
        {
            ImGuiImplD3D9.RenderDrawData(ImGuiApi.GetDrawData());
        }


        protected override void Shutdown()
        {
            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                ImGuiImplWin32.Shutdown();
                ImGuiImplD3D9.Shutdown();
                ImGuiApi.DestroyContext(imguiContext);
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
