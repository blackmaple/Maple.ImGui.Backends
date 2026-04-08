using Hexa.NET.ImGui;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends
{
    public static class ImGuiSystemFontLoader
    {
        // Preferred Chinese font loading order:
        // 1. Microsoft YaHei collection/font
        // 2. Microsoft YaHei bold collection/font
        // 3. SimSun
        // 4. SimHei
        // The first existing file in this list will be loaded as the default ImGui font.
        private static readonly string[] PreferredFontFiles =
        [
                  "msyh.ttc",        // 微软雅黑
        "msyhbd.ttc",      // 微软雅黑粗体
        "simsun.ttc",      // 宋体
        "dengxian.ttf",    // 等线
        "PingFang.ttc",    // macOS 苹方
        "NotoSansCJK-Regular.ttf" // Linux Noto
        ];




        public static bool LoadPreferredChineseSystemFont(float fontSize = 18.0f)
        {
            var fontPath = GetPreferredChineseSystemFontPath();
            if (fontPath is null)
            {
                return false;
            }

            return TryLoadFont(fontPath, fontSize);
        }

        public static string? GetPreferredChineseSystemFontPath()
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

        public static unsafe bool TryLoadFont(string fontPath, float fontSize = 18.0f)
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
    }
}
