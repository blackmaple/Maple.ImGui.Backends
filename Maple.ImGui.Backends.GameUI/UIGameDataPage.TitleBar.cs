using Hexa.NET.ImGui;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责页面标题栏与顶部标签切换区域的绘制。
    /// </summary>
    public partial class UIGameDataPage
    {
        private void RenderSessionTitleBar(ref bool isOpen)
        {
            var compactTitleBarHeight = SessionTitleBarHeight - 10.0f;
            var title = string.IsNullOrWhiteSpace(GameSessionInfo?.DisplayName)
                ? GetUiText("Dialog.Help.TitleFallback")
                : GameSessionInfo.DisplayName;
            var titleTextColor = new Vector4(1f, 1f, 1f, 1f);
            var titleBarColor = new Vector4(0.10f, 0.12f, 0.16f, 0.98f);

            var drawList = ImGuiApi.GetWindowDrawList();
            var titleBarMin = MainWindowPosition;
            var titleBarMax = MainWindowPosition + new Vector2(MainWindowSize.X, compactTitleBarHeight);
            var tabsColumnMin = MainWindowPosition + new Vector2(SessionContentLeftMargin, compactTitleBarHeight + 8.0f);
            var tabsColumnMax = tabsColumnMin + new Vector2(SessionSideTabColumnWidth, MainWindowSize.Y - compactTitleBarHeight - SessionContentBottomMargin - 8.0f);
            var tabsLocalStart = new Vector2(SessionContentLeftMargin, compactTitleBarHeight + 8.0f);
            drawList.AddRectFilled(
                titleBarMin,
                titleBarMax,
                ImGuiApi.ColorConvertFloat4ToU32(titleBarColor),
                SessionWindowRounding,
                ImDrawFlags.RoundCornersTopLeft | ImDrawFlags.RoundCornersTopRight);
            drawList.AddLine(
                new Vector2(titleBarMin.X, titleBarMax.Y),
                new Vector2(titleBarMax.X, titleBarMax.Y),
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.12f)));
            drawList.AddLine(
                new Vector2(tabsColumnMax.X + (SessionSideTabColumnSpacing * 0.5f), titleBarMax.Y + 6.0f),
                new Vector2(tabsColumnMax.X + (SessionSideTabColumnSpacing * 0.5f), MainWindowPosition.Y + MainWindowSize.Y - SessionContentBottomMargin),
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.08f)));

            var buttonSize = new Vector2(TitleBarIconButtonSize, TitleBarIconButtonSize);
            var closeX = MainWindowSize.X - SessionContentRightMargin - buttonSize.X;
            var helpX = closeX - buttonSize.X - 8.0f;
            ImGuiApi.PushStyleColor(ImGuiCol.Text, titleTextColor);
            ImGuiApi.SetCursorPos(new Vector2(18.0f, 6.0f));
            ImGuiApi.TextUnformatted(title);
            ImGuiApi.PopStyleColor();

            PushIconButtonStyle(
                new Vector4(0.18f, 0.20f, 0.24f, 1.0f),
                new Vector4(0.26f, 0.30f, 0.38f, 1.0f),
                new Vector4(0.15f, 0.18f, 0.24f, 1.0f),
                999.0f);
            ImGuiApi.SetCursorPos(new Vector2(helpX, 6.0f));
            if (ImGuiApi.Button("?##Help", buttonSize))
            {
                ShowGameSessionHelpDialog = true;
            }

            PopIconButtonStyle();

            PushIconButtonStyle(
                new Vector4(0.44f, 0.16f, 0.16f, 1.0f),
                new Vector4(0.76f, 0.22f, 0.22f, 1.0f),
                new Vector4(0.58f, 0.12f, 0.12f, 1.0f),
                999.0f);
            ImGuiApi.SetCursorPos(new Vector2(closeX, 6.0f));
            if (ImGuiApi.Button("×##Close", buttonSize))
            {
                isOpen = false;
            }

            PopIconButtonStyle();

            RenderTitleTabsVertical(tabsLocalStart, SessionSideTabColumnWidth, tabsColumnMax.Y - tabsColumnMin.Y);
        }

        private void RenderTitleTabsVertical(Vector2 startPos, float availableWidth, float availableHeight)
        {
            var tabItems = new[]
            {
                (GetUiText("Tab.Currency"), SessionTab.Currency),
                (GetUiText("Tab.Inventory"), SessionTab.Inventory),
                (GetUiText("Tab.Character"), SessionTab.Character),
                (GetUiText("Tab.Monster"), SessionTab.Monster),
                (GetUiText("Tab.Misc"), SessionTab.Switch)
            };

            const float tabSpacing = 8.0f;
            var tabWidth = MathF.Max(72.0f, availableWidth - 8.0f);
            var tabHeight = MathF.Max(TitleTabHeight, (availableHeight - ((tabItems.Length - 1) * tabSpacing)) / tabItems.Length);
            var cursorX = startPos.X;
            var cursorY = startPos.Y;
            foreach (var (tabName, tab) in tabItems)
            {
                RenderTitleTabButton(tabName, tab, tabWidth, tabHeight, cursorX, ref cursorY, tabSpacing);
            }
        }

        private void RenderTitleTabButton(string tabName, SessionTab tab, float buttonWidth, float buttonHeight, float cursorX, ref float cursorY, float tabSpacing)
        {
            var isSelected = SelectedSessionTab == tab;
            ImGuiApi.SetCursorPos(new Vector2(cursorX, cursorY));
            if (ImGuiApi.InvisibleButton($"{tabName}##{tab}", new Vector2(buttonWidth, buttonHeight)))
            {
                SelectedSessionTab = tab;
            }

            var isHovered = ImGuiApi.IsItemHovered();
            var itemMin = ImGuiApi.GetItemRectMin();
            var itemMax = ImGuiApi.GetItemRectMax();
            var drawList = ImGuiApi.GetWindowDrawList();
            var bg = isHovered
                    ? new Vector4(0.16f, 0.18f, 0.22f, 1.0f)
                    : new Vector4(0.11f, 0.12f, 0.15f, 0.92f);
            drawList.AddRectFilled(
                itemMin,
                itemMax,
                ImGuiApi.ColorConvertFloat4ToU32(bg),
                8.0f,
                ImDrawFlags.RoundCornersAll);

            if (isSelected)
            {
                drawList.AddRect(
                    itemMin + new Vector2(1.0f, 1.0f),
                    itemMax - new Vector2(1.0f, 1.0f),
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                    8.0f,
                    ImDrawFlags.RoundCornersAll,
                    1.6f);
            }

            var textSize = ImGuiApi.CalcTextSize(tabName);
            drawList.AddText(
                new Vector2(itemMin.X + (buttonWidth - textSize.X) * 0.5f, itemMin.Y + (buttonHeight - textSize.Y) * 0.5f),
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.96f, 0.96f, 0.96f, 1.0f)),
                tabName);

            cursorY += buttonHeight + tabSpacing;
        }
    }
}
