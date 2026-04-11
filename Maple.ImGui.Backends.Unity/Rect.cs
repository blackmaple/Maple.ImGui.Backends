using System.Runtime.InteropServices;
namespace Maple.ImGui.Backends.Unity
{
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
            readonly get => m_X;
            set => m_X = value;
        }

        public float Y
        {
            readonly get => m_Y;
            set => m_Y = value;
        }

        public float Width
        {
            readonly get => m_Width;
            set => m_Width = value;
        }

        public float Height
        {
            readonly get => m_Height;
            set => m_Height = value;
        }

        #region 边界属性（核心计算）

        /// <summary>
        /// 左边界：始终返回较小的 x 值
        /// </summary>
        public float XMin
        {
            readonly get => Math.Min(m_X, m_X + m_Width);
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
            readonly get => Math.Max(m_X, m_X + m_Width);
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
            readonly get => Math.Min(m_Y, m_Y + m_Height);
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
            readonly get => Math.Max(m_Y, m_Y + m_Height);
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
