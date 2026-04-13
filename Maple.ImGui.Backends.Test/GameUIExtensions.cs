using Maple.Hook.Imp.Dobby.Dynamic;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.D3D11;
using Maple.ImGui.Backends.Windows;
using Maple.RenderSpy.Graphics.D3D11;
using Maple.RenderSpy.Graphics.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Maple.ImGui.Backends.GameUI
{
    public static class GameUIExtensions
    {
        public static IServiceCollection AddGameCheatPage_Test(this IServiceCollection @this)
        {
            //   EnsureRenderSpyAssembliesLoaded();
            @this.AddDefaultWin32InputBridge();
            @this.AddBridgeCollection();
            @this.AddSingleton<IImGuiUIView, UIGameDataPage>();

            //     @this.AddHostedService<D3D11BackendHostedService>();


            @this.AddWinMsgHookFactory();
            @this.AddD3D11FunctionsProvider();

            @this.AddWindowsGraphicsHookFactory(true);
             @this.AddDobbyHookDynamicFactory("Dobby.dll");
            //  @this.AddDobbyHookDynamicFactory();
            return @this;
        }



        public static IServiceCollection AddGameCheatPage(this IServiceCollection @this)
        {
            //   EnsureRenderSpyAssembliesLoaded();
            @this.AddDefaultWin32InputBridge();
            @this.AddBridgeCollection();
            @this.AddSingleton<IImGuiUIView, UIGameDataPage>();

            @this.AddSingleton<D3D11BackendService>();


            @this.AddWinMsgHookFactory();
            @this.AddD3D11FunctionsProvider();

            @this.AddWindowsGraphicsHookFactory();
           @this.AddDobbyHookDynamicFactory("Dobby.dll");
            //     @this.AddDobbyHookNativeFactory();
            return @this;
        }


    }
}
