using ImGui.App.D3D11;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class WindowsFormsLifetime<TWindow>(IHostApplicationLifetime hostLifetime, IServiceProvider services) : BackgroundService
   where TWindow : ITestWindow
{
    private readonly IHostApplicationLifetime _hostLifetime = hostLifetime;
    private readonly IServiceProvider _services = services;


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            TWindow.Run();
            this._hostLifetime.StopApplication();
        }, stoppingToken);

    }
}