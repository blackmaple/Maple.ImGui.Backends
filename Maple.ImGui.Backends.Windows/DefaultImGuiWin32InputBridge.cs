using Hexa.NET.ImGui;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.Ime;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.Windows
{
    public partial class DefaultImGuiWin32InputBridge : IImGuiPlatformInputBridge
    {
        #region 字体
        // Preferred Chinese font loading order:
        // 1. Microsoft YaHei collection/font
        // 2. Microsoft YaHei bold collection/font
        // 3. SimSun
        // 4. SimHei
        // The first existing file in this list will be loaded as the default ImGui font.
        private static string[] PreferredFontFiles { get; } =
        [
            "msyh.ttc",        // 微软雅黑
            "msyhbd.ttc",      // 微软雅黑粗体
            "simsun.ttc",      // 宋体
            "dengxian.ttf",    // 等线
            "PingFang.ttc",    // macOS 苹方
            "NotoSansCJK-Regular.ttf" // Linux Noto
        ];

        public bool LoadPreferredChineseSystemFont(float fontSize = 18.0f)
        {
            var fontPath = GetPreferredChineseSystemFontPath();
            if (fontPath is null)
            {
                return false;
            }

            return TryLoadFont(fontPath, fontSize);
        }

        private static string? GetPreferredChineseSystemFontPath()
        {
            var fontsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            if (string.IsNullOrWhiteSpace(fontsDirectory) || !Directory.Exists(fontsDirectory))
            {
                return null;
            }

            foreach (var fontFile in PreferredFontFiles)
            {
                var fontPath = Path.Combine(fontsDirectory, fontFile);
                if (File.Exists(fontPath))
                {
                    return fontPath;
                }
            }

            return null;
        }

        private static unsafe bool TryLoadFont(string fontPath, float fontSize = 18.0f)
        {
            if (string.IsNullOrWhiteSpace(fontPath) || !File.Exists(fontPath))
            {
                return false;
            }

            var io = ImGuiApi.GetIO();

            ImFontConfigPtr fontConfig = ImGuiApi.ImFontConfig(); //ImGuiNative.ImFontConfig_ImFontConfig();
            fontConfig.OversampleH = 3;  // 提高清晰度
            fontConfig.OversampleV = 1;
            fontConfig.PixelSnapH = false;
            var font = ImGuiApi.AddFontFromFileTTF(io.Fonts, fontPath, fontSize, fontConfig, io.Fonts.GetGlyphRangesDefault());
            if (font.IsNull)
            {
                return false;
            }

            io.FontDefault = font;

            return true;
        }

        #endregion

        #region 输入法

        [ThreadStatic]
        private static char[]? _chars;

        private static char[] RentChars(int minChars)
        {
            var buf = _chars;
            if (buf == null || buf.Length < minChars)
            {
                buf = new char[Math.Max(minChars, 256)];
                _chars = buf;
                return buf;
            }
            Array.Clear(buf, 0, buf.Length);
            return buf;
        }
        [ThreadStatic]
        private static byte[]? _bytes;

        private static byte[] RentBytes(int minBytes)
        {
            var buf = _bytes;
            if (buf == null || buf.Length < minBytes)
            {
                buf = new byte[Math.Max(minBytes, 256)];
                _bytes = buf;
                return buf;
            }
            Array.Clear(buf, 0, buf.Length);

            return buf;
        }

        public unsafe bool TryHandleImeComposition(nint handle, uint uMsg, nuint wParam, nint lParam)
        {

            if (uMsg != PInvoke.WM_IME_COMPOSITION || ((uint)lParam & (uint)IME_COMPOSITION_STRING.GCS_RESULTSTR) == 0)
            {
                return false;
            }
            var hWnd = new HWND(handle);
            var hImc = PInvoke.ImmGetContext(hWnd);
            if (hImc == nint.Zero)
            {
                return false;
            }

            try
            {
                var byteCount = PInvoke.ImmGetCompositionString(hImc, IME_COMPOSITION_STRING.GCS_RESULTSTR, default, default);
                if (byteCount <= 0)
                {
                    return false;
                }


                char[] charBuffer = RentChars(byteCount + 2);
                fixed (char* p = charBuffer)
                {
                    int copied = PInvoke.ImmGetCompositionString(hImc, IME_COMPOSITION_STRING.GCS_RESULTSTR, p, (uint)byteCount);
                    if (copied <= 0)
                    {
                        return false;
                    }

                    var realCopied = copied / 2;
                    byteCount = Encoding.UTF8.GetByteCount(charBuffer, 0, realCopied);
                    byte[] byteBuffer = RentBytes(byteCount + 2);
                    int written = Encoding.UTF8.GetBytes(charBuffer, 0, realCopied, byteBuffer, 0);
                //    Debug.WriteLine(Encoding.UTF8.GetString(byteBuffer.AsSpan(0, written)));
                    ImGuiApi.GetIO().AddInputCharactersUTF8(byteBuffer.AsSpan(0, written));

                    return true;
                }
            }
            finally
            {
                PInvoke.ImmReleaseContext(hWnd, hImc);
            }
        }

        public bool ShouldConsumeWindowMessage(uint uMsg)
        {
            var io = ImGuiApi.GetIO();
            if (io.WantCaptureMouse && IsMouseMessage(uMsg))
            {
                return true;
            }

            if ((io.WantCaptureKeyboard || io.WantTextInput) && IsKeyboardMessage(uMsg))
            {
                return true;
            }

            return false;
        }

        private static bool IsMouseMessage(uint uMsg)
        {
            return uMsg is PInvoke.WM_MOUSEMOVE
                or PInvoke.WM_MOUSEWHEEL
                or PInvoke.WM_MOUSEHWHEEL
                or PInvoke.WM_LBUTTONDOWN
                or PInvoke.WM_LBUTTONUP
                or PInvoke.WM_LBUTTONDBLCLK
                or PInvoke.WM_RBUTTONDOWN
                or PInvoke.WM_RBUTTONUP
                or PInvoke.WM_RBUTTONDBLCLK
                or PInvoke.WM_MBUTTONDOWN
                or PInvoke.WM_MBUTTONUP
                or PInvoke.WM_MBUTTONDBLCLK
                or PInvoke.WM_XBUTTONDOWN
                or PInvoke.WM_XBUTTONUP
                or PInvoke.WM_XBUTTONDBLCLK
                or PInvoke.WM_CAPTURECHANGED
                or PInvoke.WM_SETCURSOR;
        }

        private static bool IsKeyboardMessage(uint uMsg)
        {
            return uMsg is PInvoke.WM_KEYDOWN
                or PInvoke.WM_KEYUP
                or PInvoke.WM_SYSKEYDOWN
                or PInvoke.WM_SYSKEYUP
                or PInvoke.WM_CHAR
                or PInvoke.WM_SYSCHAR
                or PInvoke.WM_UNICHAR
                or PInvoke.WM_INPUTLANGCHANGE
                or PInvoke.WM_IME_STARTCOMPOSITION
                or PInvoke.WM_IME_ENDCOMPOSITION
                or PInvoke.WM_IME_COMPOSITION;
        }

        #endregion

    }
}
