using System.Runtime.CompilerServices;
namespace Maple.ImGui.Backends.Unity
{
    [Obsolete("This class is deprecated. Use a different input bridge implementation.")]
    public class DefaultImGuiUnityInputBridge : IImGuiUnityInputBridge
    {
        #region Image



        protected static (float u0, float v0, float u1, float v1) CacleImGuiImageUV(ISprite sprite, ITexture2D texture)
        {
            sprite.GetRect(out Rect rect);
            // 计算 UV 坐标：将像素坐标转换为 0-1 范围
            var u0 = rect.X / texture.Width;
            var v0 = rect.Y / texture.Height;
            var u1 = (rect.X + rect.Width) / texture.Width;
            var v1 = (rect.Y + rect.Height) / texture.Height;
            return (u0, v0, u1, v1);
        }

        public virtual bool TryGetImageInfo(string? category, string objectId, out nint nativePtr, out float u0, out float v0, out float u1, out float v1)
        {
            Unsafe.SkipInit(out nativePtr);
            Unsafe.SkipInit(out u0);
            Unsafe.SkipInit(out v0);
            Unsafe.SkipInit(out u1);
            Unsafe.SkipInit(out v1);
            return false;
        }



        #endregion

        #region 输入法

        public virtual void PlatformSetImeDataFn(bool on) { }

        #endregion


    }
}
