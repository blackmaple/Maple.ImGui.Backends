using Hexa.NET.ImGui;
using System.Runtime.InteropServices;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends
{
    public static class ImGuiSystemFontLoader
    {
        private static readonly string[] PreferredFontFiles =
        [
            "msyh.ttc",
            "msyh.ttf",
            "msyhbd.ttc",
            "msyhbd.ttf",
            "simsun.ttc",
            "simhei.ttf"
        ];

        private static readonly uint[] ChineseGlyphRanges =
        [
            0x0020, 0x00FF,
            0x2000, 0x206F,
            0x3000, 0x30FF,
            0x31F0, 0x31FF,
            0x3400, 0x4DBF,
            0x4E00, 0x9FFF,
            0xF900, 0xFAFF,
            0xFF00, 0xFFEF,
            0
        ];

        private static readonly GCHandle ChineseGlyphRangesHandle = GCHandle.Alloc(ChineseGlyphRanges, GCHandleType.Pinned);

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
            var glyphRanges = (uint*)ChineseGlyphRangesHandle.AddrOfPinnedObject();
            var font = ImGuiApi.AddFontFromFileTTF(io.Fonts, fontPath, fontSize, glyphRanges);
            if (font.IsNull)
            {
                return false;
            }

            io.FontDefault = font;
            return true;
        }
    }
}
