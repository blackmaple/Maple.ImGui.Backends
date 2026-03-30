using ImGui.App.D3D11;
using Maple.Hook.Imp.Dobby.Static;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends;
using Maple.ImGui.Backends.D3D10;
using Maple.ImGui.Backends.D3D11;
using Maple.ImGui.Backends.D3D9;
using Maple.ImGui.Backends.OPENGL;
using Maple.ImGui.Backends.Test;
using Maple.MonoGameAssistant.GameCore;
using Maple.RenderSpy.Graphics.D3D10;
using Maple.RenderSpy.Graphics.D3D11;
using Maple.RenderSpy.Graphics.D3D9;
using Maple.RenderSpy.Graphics.OPENGL;
using Maple.RenderSpy.Graphics.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

services.AddHttpClient< GameHttpClientService>().ConfigurePrimaryHttpMessageHandler(p=> new HttpClientHandler()
{
    AutomaticDecompression = System.Net.DecompressionMethods.Brotli,
    UseProxy = false,
}).ConfigureHttpClient(p=>p.BaseAddress = new Uri("http://localhost:30909"));

services.AddSingleton<IImGuiCustomRender, UIPageManager>();
services.AddHostedService<D3D9BackendHostedService>();
//services.AddHostedService<D3D10BackendHostedService>();
services.AddHostedService<D3D11BackendHostedService>();

services.AddHostedService<OpenGLBackendHostedService>();

services.AddHostedService<WindowsFormsLifetime<D3D11Window>>();
services.AddWinMsgHookFactory();
services.AddD3D11FunctionsProvider();
services.AddD3D9FunctionsProvider();
services.AddD3D10FunctionsProvider();
services.AddOPENGLFunctionsProvider();

services.AddWindowsGraphicsHookFactory();
services.AddDobbyHookNativeFactory();

#if DEBUG
Maple.Hook.Imp.Dobby.Dynamic.DobbyHookDynamicExtensions.AddDobbyHookDynamicFactory(services, @"C:\Users\Black\.nuget\packages\maple.hook.imp.dobby.dynamic\0.26.317.1-rc\build\runtimes\win-x64\dobby.dll");
#else
services.AddDobbyHookNativeFactory();

#endif


using var app = builder.Build();
await app.RunAsync().ConfigureAwait(false);
 

//Console.ReadLine();