namespace Maple.ImGui.Backends
{
    public interface IImGuiBackendFactory<TImp>
        where TImp: IImGuiBackendFactory<TImp>
    {
        public abstract TImp Create(nint devicePtr, nint windowHandle);
    }



    public abstract class  ImGuiBackend
    {
        public abstract void NewFrame();
        public abstract void Render();
        public abstract void Shutdown();
    }
}
