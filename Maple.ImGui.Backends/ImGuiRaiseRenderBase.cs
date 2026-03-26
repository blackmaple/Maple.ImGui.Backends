namespace Maple.ImGui.Backends
{
    public abstract class ImGuiRaiseRenderBase(IImGuiCustomRender customRender) : IDisposable
    {
        protected IImGuiCustomRender CustomRender { get; } = customRender;

        protected abstract void NewFrame();
        protected abstract void EndFrame();
        public virtual void RaiseRender()
        { 
            this.NewFrame();
            this.CustomRender.RaiseRender();
            this.EndFrame();
        }
        protected abstract void Shutdown();
        public abstract void OnLostDevice();
        public abstract void OnResetDevice();

        public void Dispose()
        {
            this.Shutdown();
            GC.SuppressFinalize(this);
        }
    }
}
