using Hexa.NET.ImGui;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.Unity
{

    public interface IImGuiNativeBackend
    {
        ImTextureID CreateTexture(nint ptr, int w, int h);
        void ReleaseTexture(ImTextureID id);
    }

    public class ImGuiUnityInputBridge(IImGuiNativeBackend nativeBackend)
    {
        IImGuiNativeBackend NativeBackend { get; } = nativeBackend;
        private Dictionary<IntPtr, ImTextureID> TextureCache { get; } = new Dictionary<IntPtr, ImTextureID>();

        private ImTextureID GetOrCreateTextureId(ITexture2D texture)
        {
            if (texture == null) return IntPtr.Zero;

            IntPtr nativePtr = texture.GetNativeTexturePtr();

            // 检查缓存
            if (TextureCache.TryGetValue(nativePtr, out ImTextureID cachedId))
                return cachedId;

            // 创建新的纹理 ID
            ImTextureID newId = NativeBackend.CreateTexture(nativePtr, texture.Width, texture.Height);

            TextureCache[nativePtr] = newId;

            return newId;
        }

        private void ReleaseInvalidTextures()
        {


            foreach (var texId in TextureCache)
            {
                NativeBackend.ReleaseTexture(texId.Value);
            }


        }

        public virtual void PlatformSetImeDataFn(bool on) { }

        public unsafe virtual void Image(ISprite sprite, Vector2 size)
        {
            if (sprite == null) return;

            ITexture2D texture = sprite.Texture;
            ImTextureID textureId = GetOrCreateTextureId(texture);
            // ImTextureID id = new ImTextureID()
            Rect uvRect = GetSpriteUVRect(sprite);  // 完整纹理也会返回 (0,0,1,1)

          //  ImGuiApi.ImTextureRef(textureId).
           // .
           //   ImGuiApi.cre
           // 始终使用带 UV 参数的版本
           ImGuiApi.Image(new ImTextureRef(default, textureId), size,
                new Vector2(uvRect.XMin, uvRect.YMin),
                new Vector2(uvRect.XMax, uvRect.YMax));
        }

        /// <summary>
        /// 获取 Sprite 的 UV 坐标（裁剪区域）
        /// </summary>
        public static Rect GetSpriteUVRect(ISprite sprite)
        {
            if (sprite == null) return new Rect(0, 0, 1, 1);

            sprite.GetRect(out Rect rect);
            ITexture2D texture = sprite.Texture;

            // 计算 UV 坐标：将像素坐标转换为 0-1 范围
            float u = rect.X / texture.Width;
            float v = rect.Y / texture.Height;
            float w = rect.Width / texture.Width;
            float h = rect.Height / texture.Height;

            // 注意：Unity 的 V 坐标是从底部开始的，ImGui 也是从底部开始，无需翻转
            return new Rect(u, v, w, h);
        }


        public interface ISprite
        {
            ITexture2D Texture { get; }
            void GetRect(out Rect rect);
        }

        public interface ITexture2D
        {
            nint Value { get; }
            nint GetNativeTexturePtr();
            int Width { get; }
            int Height { get; }

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect(float x, float y, float w, float h)
        {

            // 内部存储：始终存储 x, y, width, height（width/height 可以是负数）
            [MarshalAs(UnmanagedType.R4)]
            private float m_X = x;
            [MarshalAs(UnmanagedType.R4)]
            private float m_Y = y;
            [MarshalAs(UnmanagedType.R4)]
            private float m_Width = w;
            [MarshalAs(UnmanagedType.R4)]
            private float m_Height = h;

            public float X
            {
                get => m_X;
                set => m_X = value;
            }

            public float Y
            {
                get => m_Y;
                set => m_Y = value;
            }

            public float Width
            {
                get => m_Width;
                set => m_Width = value;
            }

            public float Height
            {
                get => m_Height;
                set => m_Height = value;
            }

            #region 边界属性（核心计算）

            /// <summary>
            /// 左边界：始终返回较小的 x 值
            /// </summary>
            public float XMin
            {
                get => Math.Min(m_X, m_X + m_Width);
                set
                {
                    float xMax = this.XMax;
                    m_X = value;
                    m_Width = xMax - m_X;
                }
            }

            /// <summary>
            /// 右边界：始终返回较大的 x 值
            /// </summary>
            public float XMax
            {
                get => Math.Max(m_X, m_X + m_Width);
                set
                {
                    float xMin = this.XMin;
                    m_Width = value - xMin;
                    m_X = xMin;
                }
            }

            /// <summary>
            /// 下边界：始终返回较小的 y 值
            /// </summary>
            public float YMin
            {
                get => Math.Min(m_Y, m_Y + m_Height);
                set
                {
                    float yMax = this.YMax;
                    m_Y = value;
                    m_Height = yMax - m_Y;
                }
            }

            /// <summary>
            /// 上边界：始终返回较大的 y 值
            /// </summary>
            public float YMax
            {
                get => Math.Max(m_Y, m_Y + m_Height);
                set
                {
                    float yMin = this.YMin;
                    m_Height = value - yMin;
                    m_Y = yMin;
                }
            }

            #endregion
        }

    }
}
