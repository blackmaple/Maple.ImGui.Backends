using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
namespace Maple.ImGui.Backends
{
    public static class ImGuiBackendsExtensions
    {
        extension(IServiceCollection @this)
        {
            public IServiceCollection AddBridgeCollection()
                => @this.AddSingleton<ImGuiBackendBridgeCollection>();
            public IServiceCollection AddUnityInputBridge<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]T>()
                where T: class,IImGuiUnityInputBridge
                => @this.AddSingleton<IImGuiUnityInputBridge,T>();
            public IServiceCollection AddPlatformInputBridge<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
                where T : class, IImGuiPlatformInputBridge
                => @this.AddSingleton<IImGuiPlatformInputBridge, T>();
            public IServiceCollection AddBackendHostedService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
                where T : ImGuiBackendHostedService
                => @this.AddHostedService<T>();
            public IServiceCollection AddImGuiUIView<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
                where T : class,IImGuiUIView
                => @this.AddSingleton<IImGuiUIView,T>();



        }
    }
}
