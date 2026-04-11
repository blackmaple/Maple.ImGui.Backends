namespace Maple.ImGui.Backends.Unity
{
    public interface ISprite
    {
        bool IsNull { get; }
        ITexture2D Texture { get; }
        void GetRect(out Rect rect);
    }

}
