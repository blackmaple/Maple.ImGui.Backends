using Hexa.NET.ImGui;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

namespace Maple.ImGui.Backends
{
    public abstract class ImGuiRaiseRenderBase(ImGuiController controller) : IDisposable
    {

        protected ImGuiController Controller { get; } = controller;

        protected abstract void Starting(nint context);
        protected abstract void Start(nint context);
        protected abstract void Started(nint context);
        protected abstract void Build(nint context);

        public virtual void Run(nint context)
        {
            this.Starting(context);
            this.Start(context);
            this.Started(context);
            this.Build(context);
        }



        protected abstract void Shutdown();


        public virtual void Resetting(nint context) { }
        public virtual void Reset(nint context) { }
        public virtual void Resetted(nint context) { }



        public void Dispose()
        {
            this.Shutdown();
           
            GC.SuppressFinalize(this);
        }
    }





}
