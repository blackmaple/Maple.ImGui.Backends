using ImGui.App.D3D11;
using Maple.ImGui.Backends.GameUI;
using Maple.MonoGameAssistant.GameCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
var services = builder.Services;
services.AddHostedService<WindowsFormsLifetime<D3D11Window>>();
Maple.Hook.Imp.Dobby.Dynamic.DobbyHookDynamicExtensions.AddDobbyHookDynamicFactory(services, @"C:\Users\Black\.nuget\packages\maple.hook.imp.dobby.dynamic\0.26.317.1-rc\build\runtimes\win-x64\dobby.dll");
services.AddSingleton<IGameCheatService, GameCheatService_Http>();
services.AddHttpClient<GameHttpClientService>().ConfigurePrimaryHttpMessageHandler(p => new HttpClientHandler()
{
    AutomaticDecompression = System.Net.DecompressionMethods.Brotli,
    UseProxy = false,
}).ConfigureHttpClient(p => p.BaseAddress = new Uri("http://localhost:24962"));
services.AddGameCheatPage();
using var app = builder.Build();
app.Run();
//Console.ReadLine();