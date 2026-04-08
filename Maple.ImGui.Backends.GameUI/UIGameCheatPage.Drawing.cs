using Hexa.NET.ImGui;
using System.Numerics;
using System.Text;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 提供页面绘制过程中复用的图形与文本辅助方法。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private static string GetTwoLineText(string text, float maxWidth)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var normalized = text.Replace("\r", string.Empty);
            var result = new StringBuilder();
            var line = new StringBuilder();
            var lineCount = 1;

            foreach (var ch in normalized)
            {
                if (ch == '\n')
                {
                    if (lineCount == 2)
                    {
                        return AppendEllipsis(result, line, maxWidth);
                    }

                    result.Append(line).Append('\n');
                    line.Clear();
                    lineCount++;
                    continue;
                }

                var candidate = line.ToString() + ch;
                if (line.Length == 0 || ImGuiApi.CalcTextSize(candidate).X <= maxWidth)
                {
                    line.Append(ch);
                    continue;
                }

                if (lineCount == 2)
                {
                    return AppendEllipsis(result, line, maxWidth);
                }

                result.Append(line).Append('\n');
                line.Clear();
                line.Append(ch);
                lineCount++;
            }

            result.Append(line);
            return result.ToString();
        }

        private void DrawSearchButtonIcon()
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var center = (min + max) * 0.5f + new Vector2(-1.0f, -1.0f);
            var color = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.97f, 0.97f, 0.97f, 1.0f));
            drawList.AddCircle(center - new Vector2(3.0f, 3.0f), 5.5f, color, 24, 1.6f);
            drawList.AddLine(center + new Vector2(1.0f, 1.0f), center + new Vector2(6.0f, 6.0f), color, 1.6f);
        }

        private void DrawReloadButtonIcon()
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var center = (min + max) * 0.5f;
            var color = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.97f, 0.48f, 0.10f, 1.0f));
            drawList.PathArcTo(center, 7.0f, 0.6f, 5.0f, 24);
            drawList.PathStroke(color, ImDrawFlags.None, 1.8f);
            drawList.AddTriangleFilled(center + new Vector2(6.0f, -5.0f), center + new Vector2(10.0f, -4.0f), center + new Vector2(7.0f, -1.0f), color);
        }

        private static void DrawActionButtonIcon()
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var color = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.97f, 0.48f, 0.10f, 1.0f));
            var center = (min + max) * 0.5f;
            var paperMin = center + new Vector2(-5.5f, -6.5f);
            var paperMax = center + new Vector2(3.5f, 6.5f);
            drawList.AddRect(paperMin, paperMax, color, 2.0f, ImDrawFlags.None, 1.5f);
            drawList.AddLine(paperMin + new Vector2(2.0f, 4.0f), paperMax - new Vector2(2.0f, 6.0f), color, 1.2f);
            drawList.AddLine(paperMin + new Vector2(2.0f, 7.0f), paperMax - new Vector2(3.0f, 3.0f), color, 1.2f);

            var pencilStart = center + new Vector2(-0.5f, 3.5f);
            var pencilEnd = center + new Vector2(6.5f, -2.5f);
            drawList.AddLine(pencilStart, pencilEnd, color, 1.8f);
            drawList.AddLine(pencilStart + new Vector2(1.5f, 1.5f), pencilEnd + new Vector2(1.5f, 1.5f), color, 1.8f);
            drawList.AddTriangleFilled(
                pencilEnd + new Vector2(1.5f, 1.5f),
                pencilEnd + new Vector2(-1.0f, 2.5f),
                pencilEnd + new Vector2(0.5f, -1.0f),
                color);
        }

        private static void DrawSkillButtonIcon()
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var center = (min + max) * 0.5f;
            var color = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.82f, 0.90f, 1.0f, 1.0f));

            drawList.AddCircle(center, 7.0f, color, 24, 1.5f);
            drawList.AddLine(center + new Vector2(-3.5f, -2.5f), center + new Vector2(-0.5f, 2.5f), color, 1.5f);
            drawList.AddLine(center + new Vector2(-0.5f, 2.5f), center + new Vector2(4.0f, -4.0f), color, 1.5f);
            drawList.AddLine(center + new Vector2(-2.0f, -4.0f), center + new Vector2(2.0f, -4.0f), color, 1.2f);
        }

        private static void DrawAddButtonIcon(Vector4? tint = null)
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var center = (min + max) * 0.5f;
            var color = ImGuiApi.ColorConvertFloat4ToU32(tint ?? new Vector4(0.82f, 1.0f, 0.86f, 1.0f));

            drawList.AddLine(center + new Vector2(-5.0f, 0.0f), center + new Vector2(5.0f, 0.0f), color, 1.8f);
            drawList.AddLine(center + new Vector2(0.0f, -5.0f), center + new Vector2(0.0f, 5.0f), color, 1.8f);
        }

        private static void DrawRemoveButtonIcon(Vector4? tint = null)
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            var center = (min + max) * 0.5f;
            var color = ImGuiApi.ColorConvertFloat4ToU32(tint ?? new Vector4(1.0f, 0.84f, 0.84f, 1.0f));

            drawList.AddLine(center + new Vector2(-5.0f, 0.0f), center + new Vector2(5.0f, 0.0f), color, 1.8f);
        }

        private static void DrawBottomBorderForLastItem()
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetItemRectMin();
            var max = ImGuiApi.GetItemRectMax();
            drawList.AddLine(
                new Vector2(min.X + 2.0f, max.Y),
                new Vector2(max.X - 2.0f, max.Y),
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.08f)),
                1.0f);
        }

        private static string AppendEllipsis(StringBuilder prefix, StringBuilder currentLine, float width)
        {
            var lineText = currentLine.ToString();
            while (lineText.Length > 0 && ImGuiApi.CalcTextSize(lineText + "...").X > width)
            {
                lineText = lineText[..^1];
            }

            return prefix + lineText + "...";
        }
    }
}
