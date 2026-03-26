namespace Maple.ImGui.Backends
{
    public interface IImGuiBackendFactory<TImp>
        where TImp : IImGuiBackendFactory<TImp>
    {
        public abstract TImp Create(nint devicePtr, nint windowHandle);
    }

    public interface IImGuiCustomRender
    {
        void RaiseRender();
    }

    public abstract class ImGuiRaiseRender(IImGuiCustomRender customRender) : IDisposable
    {
        protected IImGuiCustomRender CustomRender { get; } = customRender;

        public abstract void NewFrame();
        public abstract void Render();
        public abstract void Shutdown();
        public abstract void OnLostDevice();
        public abstract void OnResetDevice();

        public void Dispose()
        {
            this.Shutdown();
            GC.SuppressFinalize(this);
        }
    }
}
