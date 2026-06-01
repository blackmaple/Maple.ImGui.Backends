using ImGui.App.D3D11;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class WindowsFormsLifetime<TWindow>(IHostApplicationLifetime hostLifetime, IServiceProvider services)
    : BackgroundService
   where TWindow : ITestWindow
{
    private readonly IHostApplicationLifetime _hostLifetime = hostLifetime;
    private readonly IServiceProvider _services = services;




    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var win32ImGuiBackendService = _services.GetRequiredService<Win32ImGuiBackendService>();
        await win32ImGuiBackendService.StartAsync(stoppingToken).ConfigureAwait(false);
        await Task.Run(() =>
        {
            TWindow.Run();
            this._hostLifetime.StopApplication();
        }, stoppingToken).ConfigureAwait(false);

    }
}