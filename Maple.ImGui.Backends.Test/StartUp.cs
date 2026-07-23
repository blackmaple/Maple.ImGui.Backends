using ImGui.App.D3D11;
using Maple.Hook.Imp.Dobby.Static;
using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.D3D11.GraphicsCore;
using Maple.ImGui.Backends.D3D12.GraphicsCore;
using Maple.ImGui.Backends.GameUI;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.MonoGameAssistant.GameCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

services.AddHttpClient<GameHttpClientService>().ConfigurePrimaryHttpMessageHandler(p => new HttpClientHandler()
{
    AutomaticDecompression = System.Net.DecompressionMethods.Brotli,
    UseProxy = false,
}).ConfigureHttpClient(p => p.BaseAddress = new Uri("http://localhost:48749"));
services.AddSingleton<IGameDataService, GameCheatService_Http>();
//services.AddSingleton<IImGuiUIView, UIGameDataPage>();
//services.AddHostedService<D3D9BackendHostedService>();
////services.AddHostedService<D3D10BackendHostedService>();
//services.AddHostedService<D3D11BackendHostedService>();

//services.AddHostedService<OpenGLBackendHostedService>();

 services.AddHookD3D12();
//services.AddD3D12FunctionsProvider();
//services.AddD3D11FunctionsProvider();
//services.AddD3D9FunctionsProvider();
//services.AddD3D10FunctionsProvider();
//services.AddOPENGLFunctionsProvider();

//services.AddWindowsGraphicsHookFactory();
//services.AddDobbyHookNativeFactory();

#if DEBUG
Maple.Hook.Imp.Dobby.Dynamic.DobbyHookDynamicExtensions.AddDobbyHookDynamicFactory(services, @"C:\Users\Black\.nuget\packages\maple.hook.imp.dobby.dynamic\0.26.317.1-rc\build\runtimes\win-x64\dobby.dll");
#else
services.AddDobbyHookNativeFactory();

#endif


using var app = builder.Build();
await app.RunAsync().ConfigureAwait(false);


//Console.ReadLine();