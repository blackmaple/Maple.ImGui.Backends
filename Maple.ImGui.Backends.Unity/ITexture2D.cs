namespace Maple.ImGui.Backends.Unity
{
    public interface ITexture2D
    {
        nint Value { get; }
        nint GetNativeTexturePtr();
        int Width { get; }
        int Height { get; }

    }

}
