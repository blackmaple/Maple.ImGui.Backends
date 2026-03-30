using System.Numerics;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends
{
    public static class ImGuiWin32InputBridge
    {
        private static nint CurrentWindowHandle { get; set; }

        public static void SetWindowHandle(nint hWnd)
        {
            CurrentWindowHandle = hWnd;
        }

        public static bool TryGetClientSize(out Vector2 clientSize)
        {
            var hWnd = CurrentWindowHandle;
            if (hWnd == nint.Zero || !GetClientRect(hWnd, out var rect))
            {
                clientSize = default;
                return false;
            }

            clientSize = new Vector2(
                Math.Max(1, rect.Right - rect.Left),
                Math.Max(1, rect.Bottom - rect.Top));
            return true;
        }

        public static bool TryGetMousePosition(out Vector2 mousePosition)
        {
            var hWnd = CurrentWindowHandle;
            if (hWnd == nint.Zero || !GetCursorPos(out var point) || !ScreenToClient(hWnd, ref point))
            {
                mousePosition = default;
                return false;
            }

            mousePosition = new Vector2(point.X, point.Y);
            return true;
        }

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(nint hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool ScreenToClient(nint hWnd, ref POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }
    }
}
