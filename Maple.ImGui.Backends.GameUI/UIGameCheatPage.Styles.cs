using Hexa.NET.ImGui;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 集中管理页面公共 UI 样式常量与样式辅助。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private const float PopupDialogWindowRounding = 14.0f;
        private const float PopupDialogBorderSize = 1.0f;
        private static readonly Vector2 PopupDialogPadding = new(18.0f, 18.0f);
        private static readonly Vector4 PopupDialogWindowBg = new(0.10f, 0.10f, 0.11f, 0.98f);
        private static readonly Vector4 PopupDialogBorder = new(1f, 1f, 1f, 0.08f);

        private const float OverlayDialogBorderSize = 1.0f;
        private const float OverlayDialogChildRounding = 16.0f;
        private const float OverlayDialogFrameRounding = 12.0f;
        private static readonly Vector4 OverlayDialogWindowBg = new(0.07f, 0.08f, 0.10f, 0.98f);
        private static readonly Vector4 OverlayDialogBorder = new(1f, 1f, 1f, 0.08f);
        private static readonly Vector4 OverlayDialogChildBg = new(0f, 0f, 0f, 0f);
        private static readonly Vector4 OverlayDialogFrameBg = new(0.18f, 0.19f, 0.22f, 1.0f);
        private static readonly Vector4 OverlayDialogFrameBgHovered = new(0.22f, 0.23f, 0.27f, 1.0f);
        private static readonly Vector4 OverlayDialogFrameBgActive = new(0.24f, 0.25f, 0.29f, 1.0f);

        private const float SharedButtonCornerRounding = 6.0f;
        private static readonly Vector2 DialogActionButtonSize = new(94.0f, 34.0f);
        private const float CardActionButtonSize = 30.0f;
        private const float CardActionButtonSpacing = 8.0f;

        private const float EditorFrameBorderSize = 1.0f;
        private static readonly Vector2 EditorFramePadding = new(12.0f, 5.0f);
        private static readonly Vector4 EditorFrameBg = new(0.13f, 0.14f, 0.17f, 1.0f);
        private static readonly Vector4 EditorFrameBorder = new(1f, 1f, 1f, 0.10f);

        private const float StepButtonWidth = 30.0f;
        private const float StepButtonSpacing = 6.0f;

        private static readonly Vector2 SwitchToggleSize = new(40.0f, 22.0f);
        private static readonly Vector4 SwitchToggleOnColor = new(0.24f, 0.72f, 0.38f, 0.95f);
        private static readonly Vector4 SwitchToggleOffColor = new(0.25f, 0.27f, 0.31f, 1.0f);

        private static readonly Vector4 SkillAddButtonColor = new(0.16f, 0.54f, 0.26f, 0.22f);
        private static readonly Vector4 SkillAddButtonHoveredColor = new(0.22f, 0.68f, 0.34f, 0.38f);
        private static readonly Vector4 SkillAddButtonActiveColor = new(0.12f, 0.48f, 0.22f, 0.56f);
        private static readonly Vector4 SkillAddIconColor = new(0.82f, 1.0f, 0.86f, 1.0f);

        private static readonly Vector4 SkillRemoveButtonColor = new(0.74f, 0.22f, 0.24f, 0.22f);
        private static readonly Vector4 SkillRemoveButtonHoveredColor = new(0.86f, 0.28f, 0.30f, 0.38f);
        private static readonly Vector4 SkillRemoveButtonActiveColor = new(0.70f, 0.16f, 0.18f, 0.56f);
        private static readonly Vector4 SkillRemoveIconColor = new(1.0f, 0.84f, 0.84f, 1.0f);

        private static readonly Vector4 DisabledActionButtonColor = new(0.30f, 0.32f, 0.36f, 0.56f);
        private static readonly Vector4 DisabledActionButtonHoveredColor = new(0.34f, 0.36f, 0.40f, 0.60f);
        private static readonly Vector4 DisabledActionButtonActiveColor = new(0.26f, 0.28f, 0.32f, 0.64f);
        private static readonly Vector4 DisabledActionIconColor = new(0.68f, 0.70f, 0.74f, 0.96f);

        private static void PushPopupDialogStyle()
        {
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, PopupDialogWindowRounding);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowPadding, PopupDialogPadding);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, PopupDialogBorderSize);
            ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, PopupDialogWindowBg);
            ImGuiApi.PushStyleColor(ImGuiCol.Border, PopupDialogBorder);
        }

        private static void PopPopupDialogStyle()
        {
            ImGuiApi.PopStyleColor(2);
            ImGuiApi.PopStyleVar(3);
        }

        private void PushOverlayDialogStyle()
        {
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, SessionWindowRounding);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, OverlayDialogBorderSize);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, OverlayDialogChildRounding);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, OverlayDialogFrameRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, OverlayDialogWindowBg);
            ImGuiApi.PushStyleColor(ImGuiCol.Border, OverlayDialogBorder);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, OverlayDialogChildBg);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBg, OverlayDialogFrameBg);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBgHovered, OverlayDialogFrameBgHovered);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBgActive, OverlayDialogFrameBgActive);
        }

        private static void PopOverlayDialogStyle()
        {
            ImGuiApi.PopStyleColor(6);
            ImGuiApi.PopStyleVar(4);
        }

        private static void PushEditorInputStyle()
        {
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameBorderSize, EditorFrameBorderSize);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FramePadding, EditorFramePadding);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBg, EditorFrameBg);
            ImGuiApi.PushStyleColor(ImGuiCol.Border, EditorFrameBorder);
        }

        private static void PopEditorInputStyle()
        {
            ImGuiApi.PopStyleColor(2);
            ImGuiApi.PopStyleVar(2);
        }
    }
}
