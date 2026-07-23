using ImGui.App.D3D11;
using Maple.Hook.Imp.Dobby.Dynamic;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.D3D11.GraphicsCore;
using Maple.ImGui.Backends.D3D11.ImGuiCore;
using Maple.ImGui.Backends.D3D12.GraphicsCore;
using Maple.ImGui.Backends.D3D12.ImGuiCore;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.GameUI
{
    public static class GameUIExtensions
    {
        public static IServiceCollection AddHookD3D11(this IServiceCollection @this)
        {
            @this.AddHostedService<WindowsFormsLifetime<D3D11Window>>();

            @this.AddDefaultWin32InputBridge();
            @this.AddBridgeCollection();
            @this.AddSingleton<IImGuiUIView, UIGameDataPage>();

            @this.AddSingleton<Win32ImGuiBackendService,D3D11BackendService>();


            @this.AddWinMsgHookFactory();
            @this.AddD3D11FunctionsProvider();

            @this.AddWindowsGraphicsHookFactory();
            @this.AddDobbyHookDynamicFactory("Dobby.dll");
            return @this;
        }



        public static IServiceCollection AddHookD3D12(this IServiceCollection @this)
        {
            @this.AddHostedService<WindowsFormsLifetime<D3D12Window>>();

            @this.AddDefaultWin32InputBridge();
            @this.AddBridgeCollection();
            @this.AddSingleton<IImGuiUIView, UIGameDataPage>();

            @this.AddSingleton<Win32ImGuiBackendService,D3D12BackendService>();


            @this.AddWinMsgHookFactory();
            @this.AddD3D12FunctionsProvider();

            @this.AddWindowsGraphicsHookFactory();
            @this.AddDobbyHookDynamicFactory("Dobby.dll");
             return @this;
        }


    }
}
