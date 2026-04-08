using Hexa.NET.ImGui;
using Maple.MonoGameAssistant.GameDTO;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责帮助弹窗与编辑弹窗的绘制。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private void RenderGameSessionInfoHelpDialog()
        {
            ImGuiApi.SetNextWindowPos(MainWindowPosition + MainWindowSize * 0.5f, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
            ImGuiApi.SetNextWindowSize(new Vector2(420.0f, 240.0f), ImGuiCond.Appearing);
            PushPopupDialogStyle();
            if (!ImGuiApi.BeginPopupModal(GameSessionHelpPopupName, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize))
            {
                PopPopupDialogStyle();
                return;
            }

            var popupPos = ImGuiApi.GetWindowPos();
            var popupSize = ImGuiApi.GetWindowSize();
            var title = GameSessionInfo?.DisplayName ?? "Game Session";
            var desc = string.IsNullOrWhiteSpace(GameSessionInfo?.DisplayDesc) ? "Empty" : GameSessionInfo.DisplayDesc;
            var apiVer = GameSessionInfo?.ApiVer ?? string.Empty;
            var qq = GameSessionInfo?.QQ ?? string.Empty;

            DrawThumbnailPreview(popupPos + new Vector2(22.0f, 26.0f), new Vector2(58.0f, 72.0f));

            ImGuiApi.SetCursorPos(new Vector2(96.0f, 20.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
            ImGuiApi.TextUnformatted(title);
            ImGuiApi.PopStyleColor();

            var drawList = ImGuiApi.GetWindowDrawList();
            var titleSize = ImGuiApi.CalcTextSize(title);
            var tagMin = popupPos + new Vector2(96.0f + titleSize.X + 14.0f, 20.0f);
            var tagMax = tagMin + new Vector2(54.0f, 22.0f);
            drawList.AddRectFilled(tagMin, tagMax, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.10f, 0.28f, 0.12f, 0.85f)), 11.0f);
            drawList.AddRect(tagMin, tagMax, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.20f, 0.62f, 0.26f, 0.90f)), 11.0f, ImDrawFlags.None, 1.0f);
            drawList.AddText(tagMin + new Vector2(11.0f, 4.0f), ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.75f, 1.0f, 0.78f, 1.0f)), "Game");

            ImGuiApi.SetCursorPos(new Vector2(96.0f, 54.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.62f, 0.62f, 0.62f, 1.0f));
            ImGuiApi.PushTextWrapPos(popupPos.X + popupSize.X - 22.0f);
            ImGuiApi.TextUnformatted(GetTwoLineText(desc, popupSize.X - 130.0f));
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();

            ImGuiApi.SetCursorPos(new Vector2(22.0f, 128.0f));
            RenderInfoChip($"ApiVer:{apiVer}");

            ImGuiApi.SetCursorPos(new Vector2(22.0f, 164.0f));
            ImGuiApi.SetNextItemWidth(180.0f);
            var uiScale = UiScale;
            if (ImGuiApi.SliderFloat("UI Scale", ref uiScale, UiScaleMin, UiScaleMax, "%.2f"))
            {
                UiScale = uiScale;
            }

            ImGuiApi.SetCursorPos(new Vector2(popupSize.X - 40.0f, 16.0f));
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 999.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, new Vector4(0.18f, 0.20f, 0.24f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.30f, 0.18f, 0.18f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.42f, 0.16f, 0.16f, 1.0f));
            if (ImGuiApi.Button("×##HelpPopupClose", new Vector2(24.0f, 24.0f)))
            {
                ImGuiApi.CloseCurrentPopup();
            }

            ImGuiApi.PopStyleColor(3);
            ImGuiApi.PopStyleVar();

            if (!string.IsNullOrWhiteSpace(qq))
            {
                var qqText = qq;
                var qqSize = ImGuiApi.CalcTextSize(qqText);
                ImGuiApi.SetCursorPos(new Vector2(popupSize.X - qqSize.X - 24.0f, popupSize.Y - 38.0f));
                ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.92f, 0.92f, 1.0f, 1.0f));
                ImGuiApi.TextUnformatted(qqText);
                ImGuiApi.PopStyleColor();
            }

            ImGuiApi.EndPopup();
            PopPopupDialogStyle();
        }

        private void RenderCharacterStatusDialog()
        {
            if (!ShowCharacterStatusDialog || ViewingCharacterStatus is null || !ShowSessionWindow)
            {
                return;
            }

            PushOverlayDialogStyle();

            var isOpen = true;
            ImGuiApi.SetNextWindowPos(MainWindowPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(MainWindowSize, ImGuiCond.Always);
            if (ImGuiApi.Begin("###CharacterStatusDialog", ref isOpen, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar))
            {
                RenderSessionDialogTitleBar(ViewingCharacterStatus.DisplayName, ref isOpen, "CharacterStatusDialog");
                var contentPosition = new Vector2(SessionContentLeftMargin, SessionTitleBarHeight + SessionContentTopMargin);
                var contentSize = new Vector2(
                    MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin),
                    MathF.Max(1.0f, MainWindowSize.Y - SessionTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
                ImGuiApi.SetCursorPos(contentPosition);
                if (ImGuiApi.BeginChild("##CharacterStatusContent", contentSize, ImGuiChildFlags.AlwaysUseWindowPadding))
                {
                    AllowDialogContentInput = true;
                    try
                    {
                        RenderCharacterStatusTable(ViewingCharacterStatus.Data, ViewingCharacterStatus.DisplayDesc);
                    }
                    finally
                    {
                        AllowDialogContentInput = false;
                    }
                }

                ImGuiApi.EndChild();
            }

            ImGuiApi.End();
            PopOverlayDialogStyle();
            if (!isOpen)
            {
                ShowCharacterStatusDialog = false;
                ViewingCharacterStatus = null;
                SwitchDisplayEditorTexts.Clear();
            }
        }

        private void RenderCharacterStatusTable(GameCharacterStatusDTO status, string displayDesc)
        {
            var characterAttributes = status.CharacterAttributes ?? [];
            RenderDisplayGridCards("Misc", characterAttributes);
        }

        private void RenderCharacterSkillDialog()
        {
            if (!ShowCharacterSkillDialog || ViewingCharacterSkill is null || !ShowSessionWindow)
            {
                return;
            }

            PushOverlayDialogStyle();

            var isOpen = true;
            ImGuiApi.SetNextWindowPos(MainWindowPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(MainWindowSize, ImGuiCond.Always);
            var dialogWindowFlags = ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar;
            if (ShowCharacterSkillSelectorDialog)
            {
                dialogWindowFlags |= ImGuiWindowFlags.NoInputs;
            }

            if (ImGuiApi.Begin("###CharacterSkillDialog", ref isOpen, dialogWindowFlags))
            {
                RenderSessionDialogTitleBar(ViewingCharacterSkill.DisplayName, ref isOpen, "CharacterSkillDialog");
                var contentPosition = new Vector2(SessionContentLeftMargin, SessionTitleBarHeight + SessionContentTopMargin);
                var contentSize = new Vector2(
                    MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin),
                    MathF.Max(1.0f, MainWindowSize.Y - SessionTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
                ImGuiApi.SetCursorPos(contentPosition);
                if (ImGuiApi.BeginChild("##CharacterSkillContent", contentSize, ImGuiChildFlags.AlwaysUseWindowPadding))
                {
                    RenderCharacterSkillTable(ViewingCharacterSkill.Data, ViewingCharacterSkill.DisplayDesc);
                }

                ImGuiApi.EndChild();
            }

            ImGuiApi.End();
            PopOverlayDialogStyle();
            if (isOpen)
            {
                RenderCharacterSkillActionConfirmDialog();
            }

            if (!isOpen)
            {
                ShowCharacterSkillDialog = false;
                ShowCharacterSkillSelectorDialog = false;
                CharacterSkillSelectionCategory = string.Empty;
                PendingOpenCharacterSkillActionPopup = false;
                PendingCharacterSkillAction = null;
                ViewingCharacterSkill = null;
            }
        }

        private void RenderCharacterSkillTable(GameCharacterSkillDTO data, string displayDesc)
        {
            var skillInfos = data.SkillInfos ?? [];
            if (skillInfos.Length == 0)
            {
                ImGuiApi.TextUnformatted("No skills.");
                return;
            }

            RenderCharacterSkillGridCards(skillInfos);
        }

        private void RenderCharacterSkillGridCards(GameSkillInfoDTO[] items)
        {
            const float cardSpacing = 12.0f;
            const float cardHeight = 112.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##CharacterSkillGridCards", childSize, ImGuiChildFlags.None, ImGuiWindowFlags.None))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var cardWidth = GetInventoryCardWidth(childSize.X);
            var columns = Math.Max(1, (int)((childSize.X + cardSpacing) / (cardWidth + cardSpacing)));
            var rowHeight = cardHeight + cardSpacing;
            var usedWidth = (columns * cardWidth) + ((columns - 1) * cardSpacing);
            var startOffsetX = MathF.Max(0.0f, (childSize.X - usedWidth) * 0.5f);
            var rowCount = (items.Length + columns - 1) / columns;
            var clipper = new ImGuiListClipper();
            clipper.Begin(rowCount, rowHeight);
            while (clipper.Step())
            {
                for (var row = clipper.DisplayStart; row < clipper.DisplayEnd; row++)
                {
                    ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + startOffsetX);
                    for (var column = 0; column < columns; column++)
                    {
                        var itemIndex = (row * columns) + column;
                        if (itemIndex >= items.Length)
                        {
                            break;
                        }

                        if (column > 0)
                        {
                            ImGuiApi.SameLine(0.0f, cardSpacing);
                        }

                        RenderCharacterSkillCard(items[itemIndex], itemIndex, cardWidth);
                    }

                    if (row < rowCount - 1)
                    {
                        ImGuiApi.Dummy(new Vector2(0.0f, cardSpacing));
                    }
                }
            }

            clipper.End();
            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
        }

        private void RenderCharacterSkillCard(GameSkillInfoDTO item, int index, float availableWidth)
        {
            const float cardControlMargin = 6.0f;
            const float actionButtonSize = 30.0f;
            const float actionButtonSpacing = 8.0f;
            const float textStartX = 76.0f;
            var cardWidth = MathF.Max(1.0f, availableWidth);
            const float cardHeight = 112.0f;
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            if (!ImGuiApi.BeginChild($"##CharacterSkillCard_{item.ObjectId}_{index}", new Vector2(cardWidth, cardHeight), ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                ImGuiApi.PopStyleVar();
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var windowPos = ImGuiApi.GetWindowPos();
            var iconMin = windowPos + new Vector2(14.0f, 14.0f);
            var cardCategory = string.IsNullOrWhiteSpace(item.DisplayCategory) ? "Skill" : item.DisplayCategory;
            var textRightBoundary = cardWidth - 16.0f;
            var textWidth = MathF.Max(1.0f, textRightBoundary - textStartX - 12.0f);
            var addButtonPosition = new Vector2(cardWidth - CardActionButtonSize - cardControlMargin, cardHeight - CardActionButtonSize - cardControlMargin);
            var removeButtonPosition = addButtonPosition - new Vector2(CardActionButtonSize + CardActionButtonSpacing, 0.0f);

            DrawThumbnailPreview(iconMin, new Vector2(48.0f, 48.0f));
            RenderInventoryCardTextBlock(cardCategory, item.DisplayName ?? string.Empty, windowPos, textStartX, textWidth);

            var canEdit = item.CanWrite && !_characterSkillUpdateRequest.IsRunning;
            if (RenderSkillActionButton($"##RemoveSkill_{item.ObjectId}_{index}", removeButtonPosition, false, canEdit))
            {
                HandleCharacterSkillRemoveButtonClick(item);
            }

            if (RenderSkillActionButton($"##AddSkill_{item.ObjectId}_{index}", addButtonPosition, true, canEdit))
            {
                HandleCharacterSkillAddButtonClick(item);
            }

            if (ImGuiApi.IsItemHovered() || ImGuiApi.IsMouseHoveringRect(windowPos, windowPos + new Vector2(cardWidth, cardHeight)))
            {
                var tooltipDesc = GetPlainText(item.DisplayDesc);
                if (string.IsNullOrWhiteSpace(tooltipDesc))
                {
                    tooltipDesc = "Empty";
                }

                ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
                ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
                ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
                ImGuiApi.BeginTooltip();
                ImGuiApi.PushTextWrapPos(ImGuiApi.GetFontSize() * 24.0f);
                ImGuiApi.TextUnformatted(cardCategory);
                ImGuiApi.Separator();
                ImGuiApi.TextUnformatted(item.DisplayName ?? string.Empty);
                ImGuiApi.Spacing();
                ImGuiApi.TextUnformatted(tooltipDesc);
                RenderCharacterSkillTooltipAttributes(item);
                ImGuiApi.PopTextWrapPos();
                ImGuiApi.EndTooltip();
                ImGuiApi.PopStyleColor(2);
                ImGuiApi.PopStyleVar();

                drawList.AddRect(
                    windowPos + new Vector2(1.0f, 1.0f),
                    windowPos + new Vector2(cardWidth, cardHeight) - new Vector2(1.0f, 1.0f),
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                    DisplayCardRounding,
                    ImDrawFlags.None,
                    1.6f);
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
        }

        private static void RenderCharacterSkillTooltipAttributes(GameSkillInfoDTO item)
        {
            var skillAttributes = item.SkillAttributes ?? [];
            var hasAttributes = false;
            foreach (var attribute in skillAttributes)
            {
                if (!hasAttributes)
                {
                    hasAttributes = true;
                    ImGuiApi.Spacing();
                    ImGuiApi.Separator();
                }

                ImGuiApi.TextUnformatted($"{attribute.DisplayName ?? string.Empty}:{attribute.DisplayValue ?? string.Empty}");
            }
        }

        private bool RenderSkillActionButton(string id, Vector2 position, bool isAdd, bool enabled)
        {
            var iconTint = enabled
                ? isAdd
                    ? SkillAddIconColor
                    : SkillRemoveIconColor
                : DisabledActionIconColor;
            var buttonColor = enabled
                ? isAdd
                    ? SkillAddButtonColor
                    : SkillRemoveButtonColor
                : DisabledActionButtonColor;
            var hoverColor = enabled
                ? isAdd
                    ? SkillAddButtonHoveredColor
                    : SkillRemoveButtonHoveredColor
                : DisabledActionButtonHoveredColor;
            var activeColor = enabled
                ? isAdd
                    ? SkillAddButtonActiveColor
                    : SkillRemoveButtonActiveColor
                : DisabledActionButtonActiveColor;

            ImGuiApi.BeginDisabled(!enabled);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, SharedButtonCornerRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, buttonColor);
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, hoverColor);
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, activeColor);
            ImGuiApi.SetCursorPos(position);
            var clicked = ImGuiApi.Button(id, new Vector2(CardActionButtonSize, CardActionButtonSize));
            if (isAdd)
            {
                DrawAddButtonIcon(iconTint);
            }
            else
            {
                DrawRemoveButtonIcon(iconTint);
            }

            ImGuiApi.PopStyleColor(3);
            ImGuiApi.PopStyleVar();
            ImGuiApi.EndDisabled();
            return enabled && clicked;
        }

        private void RenderCharacterSkillSelectorDialog()
        {
            if (!ShowCharacterSkillSelectorDialog || ViewingCharacterSkill is null || !ShowSessionWindow)
            {
                return;
            }

            PushOverlayDialogStyle();

            var isOpen = true;
            ImGuiApi.SetNextWindowPos(MainWindowPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(MainWindowSize, ImGuiCond.Always);
            if (ImGuiApi.Begin("###CharacterSkillSelectorDialog", ref isOpen, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar))
            {
                RenderSessionDialogTitleBar($"Select Skill - {CharacterSkillSelectionCategory}", ref isOpen, "CharacterSkillSelectorDialog");
                var contentPosition = new Vector2(SessionContentLeftMargin, SessionTitleBarHeight + SessionContentTopMargin);
                var contentSize = new Vector2(
                    MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin),
                    MathF.Max(1.0f, MainWindowSize.Y - SessionTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
                ImGuiApi.SetCursorPos(contentPosition);
                if (ImGuiApi.BeginChild("##CharacterSkillSelectorContent", contentSize, ImGuiChildFlags.AlwaysUseWindowPadding))
                {
                    RenderCharacterSkillSelectorTable();
                }

                ImGuiApi.EndChild();
            }

            ImGuiApi.End();
            PopOverlayDialogStyle();
            if (!isOpen)
            {
                ShowCharacterSkillSelectorDialog = false;
                CharacterSkillSelectionCategory = string.Empty;
            }
        }

        private void RenderCharacterSkillActionConfirmDialog()
        {
            const string characterSkillActionPopupName = "CharacterSkillActionConfirmPopup";

            if (PendingOpenCharacterSkillActionPopup)
            {
                ImGuiApi.OpenPopup(characterSkillActionPopupName);
                PendingOpenCharacterSkillActionPopup = false;
            }

            ImGuiApi.SetNextWindowPos(MainWindowPosition + MainWindowSize * 0.5f, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
            ImGuiApi.SetNextWindowSizeConstraints(new Vector2(360.0f, 0.0f), new Vector2(420.0f, float.MaxValue));
            PushPopupDialogStyle();
            if (!ImGuiApi.BeginPopupModal(characterSkillActionPopupName, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize))
            {
                PopPopupDialogStyle();
                return;
            }

            if (PendingCharacterSkillAction is null)
            {
                ImGuiApi.CloseCurrentPopup();
                ImGuiApi.EndPopup();
                PopPopupDialogStyle();
                return;
            }

            var prompt = PendingCharacterSkillAction.IsAdd
                ? $"是否选择 {PendingCharacterSkillAction.DisplayName}?"
                : $"是否移除 {PendingCharacterSkillAction.DisplayName}?";
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
            ImGuiApi.TextUnformatted(prompt);
            ImGuiApi.PopStyleColor();

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            if (ImGuiApi.Button("是##CharacterSkillActionConfirm", DialogActionButtonSize) && !_characterSkillUpdateRequest.IsRunning)
            {
                var action = PendingCharacterSkillAction;
                PendingCharacterSkillAction = null;
                ImGuiApi.CloseCurrentPopup();
                _characterSkillUpdateRequest.TryStart(() => UpdateCharacterSkillDialogAsync(
                    action.ModifyCategory,
                    action.OldSkill,
                    action.NewSkill,
                    action.DisplayName));
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button("取消##CharacterSkillActionConfirm", DialogActionButtonSize))
            {
                PendingCharacterSkillAction = null;
                ImGuiApi.CloseCurrentPopup();
            }

            ImGuiApi.EndPopup();
            PopPopupDialogStyle();
        }

        private void RenderCharacterSkillSelectorTable()
        {
            RenderCharacterSkillSelectorToolbar(CharacterSkillSelectorSearch);

            var items = SortDisplays(FilterDisplays(
                SessionCollections.Skills
                .Where(static x => x is not null)
                .Where(x => string.IsNullOrWhiteSpace(CharacterSkillSelectionCategory)
                    || string.Equals(x.DisplayCategory, CharacterSkillSelectionCategory, StringComparison.OrdinalIgnoreCase))
                .ToArray(),
                CharacterSkillSelectorSearch.AppliedText));

            if (items.Length > 0)
            {
                RenderCharacterSkillSelectorGridCards(items.Select(static item => new CharacterSkillSelectorItem(item.ObjectId, item.DisplayCategory, item.DisplayName, item.DisplayDesc)).ToArray());
                return;
            }

            var fallbackItems = SortDisplays(FilterDisplays(
                SessionCollections.Inventories
                .Where(static x => x is not null)
                .Where(x => string.IsNullOrWhiteSpace(CharacterSkillSelectionCategory)
                    || string.Equals(x.DisplayCategory, CharacterSkillSelectionCategory, StringComparison.OrdinalIgnoreCase))
                .ToArray(),
                CharacterSkillSelectorSearch.AppliedText));

            if (fallbackItems.Length == 0)
            {
                ImGuiApi.TextUnformatted("No skills in current category.");
                return;
            }

            RenderCharacterSkillSelectorGridCards(fallbackItems.Select(static item => new CharacterSkillSelectorItem(item.ObjectId, item.DisplayCategory, item.DisplayName, item.DisplayDesc)).ToArray());
        }

        private void RenderCharacterSkillSelectorToolbar(SearchState searchState)
        {
            var toolbarWidth = ImGuiApi.GetContentRegionAvail().X;
            var totalWidth = MathF.Max(320.0f, toolbarWidth - 8.0f);
            var inputWidth = MathF.Max(220.0f, totalWidth - ToolbarIconButtonSize - 8.0f);
            var startX = MathF.Max(0.0f, (toolbarWidth - totalWidth) * 0.5f);

            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + startX);
            ImGuiApi.SetCursorPosY(ImGuiApi.GetCursorPosY() + 2.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBg, new Vector4(0.13f, 0.14f, 0.17f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBgHovered, new Vector4(0.16f, 0.17f, 0.20f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBgActive, new Vector4(0.18f, 0.19f, 0.22f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(1f, 1f, 1f, 0.10f));
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FramePadding, new Vector2(12.0f, 9.0f));
            RenderSearchInput("##CharacterSkillSelector_SearchInput", searchState, inputWidth);
            var inputMin = ImGuiApi.GetItemRectMin();
            var inputMax = ImGuiApi.GetItemRectMax();
            var isSearchInputHovered = ImGuiApi.IsItemHovered();
            var isSearchInputActive = ImGuiApi.IsItemActive();
            if (isSearchInputHovered || isSearchInputActive)
            {
                ImGuiApi.GetWindowDrawList().AddRect(
                    inputMin + new Vector2(1.0f, 1.0f),
                    inputMax - new Vector2(1.0f, 1.0f),
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                    12.0f,
                    ImDrawFlags.RoundCornersAll,
                    1.6f);
            }

            ImGuiApi.PopStyleColor(4);
            ImGuiApi.PopStyleVar(2);
            ImGuiApi.SameLine();
            if (ImGuiApi.Button("##CharacterSkillSelector_Search", new Vector2(ToolbarIconButtonSize, ToolbarIconButtonSize)))
            {
                searchState.AppliedText = searchState.InputText;
            }

            DrawSearchButtonIcon();
            ImGuiApi.Spacing();
        }

        private void RenderCharacterSkillSelectorGridCards(CharacterSkillSelectorItem[] items)
        {
            const float cardSpacing = 12.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##CharacterSkillSelectorGridCards", childSize, ImGuiChildFlags.None, ImGuiWindowFlags.None))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var cardWidth = GetInventoryCardWidth(childSize.X);
            const float cardHeight = 112.0f;
            var columns = Math.Max(1, (int)((childSize.X + cardSpacing) / (cardWidth + cardSpacing)));
            var rowHeight = cardHeight + cardSpacing;
            var usedWidth = (columns * cardWidth) + ((columns - 1) * cardSpacing);
            var startOffsetX = MathF.Max(0.0f, (childSize.X - usedWidth) * 0.5f);
            var rowCount = (items.Length + columns - 1) / columns;
            var clipper = new ImGuiListClipper();
            clipper.Begin(rowCount, rowHeight);
            while (clipper.Step())
            {
                for (var row = clipper.DisplayStart; row < clipper.DisplayEnd; row++)
                {
                    ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + startOffsetX);
                    for (var column = 0; column < columns; column++)
                    {
                        var itemIndex = (row * columns) + column;
                        if (itemIndex >= items.Length)
                        {
                            break;
                        }

                        if (column > 0)
                        {
                            ImGuiApi.SameLine(0.0f, cardSpacing);
                        }

                        RenderCharacterSkillSelectorCard(items[itemIndex], itemIndex, cardWidth);
                    }

                    if (row < rowCount - 1)
                    {
                        ImGuiApi.Dummy(new Vector2(0.0f, cardSpacing));
                    }
                }
            }

            clipper.End();
            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
        }

        private void RenderCharacterSkillSelectorCard(CharacterSkillSelectorItem item, int index, float availableWidth)
        {
            const float cardControlMargin = 6.0f;
            const float textStartX = 76.0f;
            var cardWidth = MathF.Max(1.0f, availableWidth);
            const float cardHeight = 112.0f;
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            if (!ImGuiApi.BeginChild($"##CharacterSkillSelectorCard_{item.ObjectId}_{index}", new Vector2(cardWidth, cardHeight), ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                ImGuiApi.PopStyleVar();
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var windowPos = ImGuiApi.GetWindowPos();
            var iconMin = windowPos + new Vector2(14.0f, 14.0f);
            var cardCategory = string.IsNullOrWhiteSpace(item.DisplayCategory) ? "Skill" : item.DisplayCategory;
            var textRightBoundary = cardWidth - 16.0f;
            var textWidth = MathF.Max(1.0f, textRightBoundary - textStartX - 12.0f);
            var addButtonPosition = new Vector2(cardWidth - CardActionButtonSize - cardControlMargin, cardHeight - CardActionButtonSize - cardControlMargin);

            DrawThumbnailPreview(iconMin, new Vector2(48.0f, 48.0f));
            RenderInventoryCardTextBlock(cardCategory, item.DisplayName ?? string.Empty, windowPos, textStartX, textWidth);

            if (RenderSkillActionButton($"##AddSelectedSkill_{item.ObjectId}_{index}", addButtonPosition, true, !_characterSkillUpdateRequest.IsRunning))
            {
                HandleCharacterSkillSelectionAddButtonClick(item);
            }

            if (ImGuiApi.IsItemHovered() || ImGuiApi.IsMouseHoveringRect(windowPos, windowPos + new Vector2(cardWidth, cardHeight)))
            {
                var tooltipDesc = GetPlainText(item.DisplayDesc);
                if (string.IsNullOrWhiteSpace(tooltipDesc))
                {
                    tooltipDesc = "Empty";
                }

                ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
                ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
                ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
                ImGuiApi.BeginTooltip();
                ImGuiApi.PushTextWrapPos(ImGuiApi.GetFontSize() * 24.0f);
                ImGuiApi.TextUnformatted(cardCategory);
                ImGuiApi.Separator();
                ImGuiApi.TextUnformatted(item.DisplayName ?? string.Empty);
                ImGuiApi.Spacing();
                ImGuiApi.TextUnformatted(tooltipDesc);
                ImGuiApi.PopTextWrapPos();
                ImGuiApi.EndTooltip();
                ImGuiApi.PopStyleColor(2);
                ImGuiApi.PopStyleVar();

                drawList.AddRect(
                    windowPos + new Vector2(1.0f, 1.0f),
                    windowPos + new Vector2(cardWidth, cardHeight) - new Vector2(1.0f, 1.0f),
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                    DisplayCardRounding,
                    ImDrawFlags.None,
                    1.6f);
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
        }

        private void RenderCurrencyEditDialog()
        {
            ImGuiApi.SetNextWindowPos(MainWindowPosition + MainWindowSize * 0.5f, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
            ImGuiApi.SetNextWindowSizeConstraints(new Vector2(EditDialogWidth, 0.0f), new Vector2(EditDialogWidth, float.MaxValue));
            PushPopupDialogStyle();
            if (!ImGuiApi.BeginPopupModal(CurrencyEditPopupName, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize))
            {
                PopPopupDialogStyle();
                return;
            }

            if (PendingCloseCurrencyEditPopup)
            {
                PendingCloseCurrencyEditPopup = false;
                EditingCurrency = null;
                CurrencyEditValue = string.Empty;
                ImGuiApi.CloseCurrentPopup();
                ImGuiApi.EndPopup();
                PopPopupDialogStyle();
                return;
            }

            if (EditingCurrency is null)
            {
                ImGuiApi.CloseCurrentPopup();
                ImGuiApi.EndPopup();
                PopPopupDialogStyle();
                return;
            }

            var headerMin = ImGuiApi.GetCursorScreenPos();
            DrawThumbnailPreview(headerMin, new Vector2(EditDialogThumbnailWidth, EditDialogThumbnailHeight));
            ImGuiApi.Dummy(new Vector2(EditDialogThumbnailWidth, EditDialogThumbnailHeight));
            ImGuiApi.SameLine(0.0f, 16.0f);
            ImGuiApi.BeginGroup();
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
            ImGuiApi.TextUnformatted(EditingCurrency.DisplayName);
            ImGuiApi.PopStyleColor();
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.78f, 0.82f, 0.90f, 1.0f));
            var textWidth = EditDialogWidth - EditDialogThumbnailWidth - 74.0f;
            ImGuiApi.PushTextWrapPos(ImGuiApi.GetCursorScreenPos().X + textWidth);
            ImGuiApi.TextUnformatted(GetTwoLineText(EditingCurrency.DisplayDesc, textWidth));
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();
            ImGuiApi.EndGroup();

            ImGuiApi.Spacing();
            ImGuiApi.SetNextItemWidth(ImGuiApi.GetContentRegionAvail().X);
            PushEditorInputStyle();
            CurrencyEditValue = RenderStepInput("##CurrencyEdit", "CurrencyEdit", CurrencyEditValue);
            PopEditorInputStyle();

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            if (ImGuiApi.Button("OK##CurrencyEdit", DialogActionButtonSize) && !_currencyUpdateRequest.IsRunning)
            {
                _currencyUpdateRequest.TryStart(UpdateCurrencyEditAsync);
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button("Cancel##CurrencyEdit", DialogActionButtonSize))
            {
                EditingCurrency = null;
                CurrencyEditValue = string.Empty;
                ImGuiApi.CloseCurrentPopup();
            }

            ImGuiApi.EndPopup();
            PopPopupDialogStyle();
        }

        private void RenderInventoryEditDialog()
        {
            ImGuiApi.SetNextWindowPos(MainWindowPosition + MainWindowSize * 0.5f, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
            ImGuiApi.SetNextWindowSizeConstraints(new Vector2(EditDialogWidth, 0.0f), new Vector2(EditDialogWidth, float.MaxValue));
            PushPopupDialogStyle();
            if (!ImGuiApi.BeginPopupModal(InventoryEditPopupName, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.AlwaysAutoResize))
            {
                PopPopupDialogStyle();
                return;
            }

            if (PendingCloseInventoryEditPopup)
            {
                PendingCloseInventoryEditPopup = false;
                EditingInventory = null;
                InventoryEditValue = string.Empty;
                ImGuiApi.CloseCurrentPopup();
                ImGuiApi.EndPopup();
                PopPopupDialogStyle();
                return;
            }

            if (EditingInventory is null)
            {
                ImGuiApi.CloseCurrentPopup();
                ImGuiApi.EndPopup();
                PopPopupDialogStyle();
                return;
            }

            var headerMin = ImGuiApi.GetCursorScreenPos();
            DrawThumbnailPreview(headerMin, new Vector2(EditDialogThumbnailWidth, EditDialogThumbnailHeight));
            ImGuiApi.Dummy(new Vector2(EditDialogThumbnailWidth, EditDialogThumbnailHeight));
            ImGuiApi.SameLine(0.0f, 16.0f);
            ImGuiApi.BeginGroup();
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
            ImGuiApi.TextUnformatted(EditingInventory.DisplayName);
            ImGuiApi.PopStyleColor();
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.78f, 0.82f, 0.90f, 1.0f));
            var textWidth = EditDialogWidth - EditDialogThumbnailWidth - 74.0f;
            ImGuiApi.PushTextWrapPos(ImGuiApi.GetCursorScreenPos().X + textWidth);
            ImGuiApi.TextUnformatted(GetTwoLineText(EditingInventory.DisplayDesc, textWidth));
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();
            ImGuiApi.EndGroup();

            ImGuiApi.Spacing();
            ImGuiApi.SetNextItemWidth(ImGuiApi.GetContentRegionAvail().X);
            PushEditorInputStyle();
            InventoryEditValue = RenderStepInput("##InventoryEdit", "InventoryEdit", InventoryEditValue);
            PopEditorInputStyle();

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            if (ImGuiApi.Button("OK##InventoryEdit", DialogActionButtonSize) && !_inventoryUpdateRequest.IsRunning)
            {
                _inventoryUpdateRequest.TryStart(UpdateInventoryEditAsync);
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button("Cancel##InventoryEdit", DialogActionButtonSize))
            {
                EditingInventory = null;
                InventoryEditValue = string.Empty;
                ImGuiApi.CloseCurrentPopup();
            }

            ImGuiApi.EndPopup();
            PopPopupDialogStyle();
        }

        private static void DrawThumbnailPreview(Vector2 min, Vector2 size)
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var max = min + size;
            drawList.AddRectFilled(min, max, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.21f, 0.22f, 0.26f, 1.0f)), 12.0f);
            drawList.AddRect(min, max, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.08f)), 12.0f, ImDrawFlags.None, 1.0f);
            drawList.AddRectFilled(min + new Vector2(8.0f, 8.0f), new Vector2(max.X - 8.0f, min.Y + 28.0f), ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.32f, 0.34f, 0.40f, 1.0f)), 6.0f);
            drawList.AddRectFilled(new Vector2(min.X + 8.0f, min.Y + 36.0f), new Vector2(max.X - 8.0f, min.Y + 43.0f), ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.42f, 0.45f, 0.52f, 1.0f)), 3.0f);
            drawList.AddRectFilled(new Vector2(min.X + 8.0f, min.Y + 49.0f), new Vector2(max.X - 16.0f, min.Y + 55.0f), ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.35f, 0.37f, 0.43f, 1.0f)), 3.0f);
        }

        private static string RenderStepInput(string inputLabel, string inputId, string value, bool clampNegative = true)
        {
            var availableWidth = ImGuiApi.GetContentRegionAvail().X;
            var controlWidth = MathF.Max(
                1.0f,
                availableWidth - (StepButtonWidth * 2.0f) - (StepButtonSpacing * 2.0f));
            var totalWidth = controlWidth + (StepButtonWidth * 2.0f) + (StepButtonSpacing * 2.0f);
            var startX = ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, (availableWidth - totalWidth) * 0.5f);

            ImGuiApi.SetCursorPosX(startX);

            var editValue = value;
            if (ImGuiApi.Button($"-##{inputId}", new Vector2(StepButtonWidth, 0.0f)))
            {
                editValue = StepNumericValue(editValue, -1m, clampNegative);
            }

            ImGuiApi.SameLine(0.0f, StepButtonSpacing);
            ImGuiApi.SetNextItemWidth(controlWidth);
            ImGuiApi.InputText(inputLabel, ref editValue, (nuint)SearchInputBufferSize, ImGuiInputTextFlags.CharsDecimal);

            ImGuiApi.SameLine(0.0f, StepButtonSpacing);
            if (ImGuiApi.Button($"+##{inputId}", new Vector2(StepButtonWidth, 0.0f)))
            {
                editValue = StepNumericValue(editValue, 1m, clampNegative);
            }

            return editValue;
        }

        private static string StepNumericValue(string value, decimal delta, bool clampNegative)
        {
            if (!TryParseNumericValue(value, out var currentValue))
            {
                currentValue = 0m;
            }

            var nextValue = currentValue + delta;
            if (clampNegative)
            {
                nextValue = decimal.Max(0m, nextValue);
            }

            return nextValue % 1m == 0m
                ? decimal.Truncate(nextValue).ToString(CultureInfo.InvariantCulture)
                : nextValue.ToString(CultureInfo.InvariantCulture);
        }

        private void RenderSessionDialogTitleBar(string title, ref bool isOpen, string idSuffix)
        {
            var titleTextColor = new Vector4(1f, 1f, 1f, 1f);
            var titleBarColor = new Vector4(0.10f, 0.12f, 0.16f, 0.98f);
            var drawList = ImGuiApi.GetWindowDrawList();
            var windowPos = ImGuiApi.GetWindowPos();
            var windowSize = ImGuiApi.GetWindowSize();
            var titleBarMin = windowPos;
            var titleBarMax = windowPos + new Vector2(windowSize.X, SessionTitleBarHeight);

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

            ImGuiApi.PushStyleColor(ImGuiCol.Text, titleTextColor);
            ImGuiApi.SetCursorPos(new Vector2(18.0f, 8.0f));
            ImGuiApi.TextUnformatted(title);
            ImGuiApi.PopStyleColor();

            var buttonSize = new Vector2(TitleBarIconButtonSize, TitleBarIconButtonSize);
            var buttonRegionWidth = windowSize.X * 0.05f;
            var closeRegionStartX = windowSize.X - buttonRegionWidth;
            var closeX = closeRegionStartX + (buttonRegionWidth - buttonSize.X) * 0.5f;

            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 999.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, new Vector4(0.44f, 0.16f, 0.16f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.76f, 0.22f, 0.22f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.58f, 0.12f, 0.12f, 1.0f));
            ImGuiApi.SetCursorPos(new Vector2(closeX, 8));
            if (ImGuiApi.Button($"×##{idSuffix}_Close", buttonSize))
            {
                isOpen = false;
            }

            ImGuiApi.PopStyleColor(3);
            ImGuiApi.PopStyleVar();
        }

        private static bool TryParseNumericValue(string value, out decimal number)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out number)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out number);
        }

        private static object? GetDialogMemberValue(object source, string memberName)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            var sourceType = source.GetType();
            return sourceType.GetProperty(memberName, bindingFlags)?.GetValue(source)
                ?? sourceType.GetField(memberName, bindingFlags)?.GetValue(source);
        }

        private static void RenderJsonViewer(string id, object payload)
        {
            var json = BuildObjectDisplayText(payload);
            var size = ImGuiApi.GetContentRegionAvail();
            var bufferSize = (nuint)Math.Max(SearchInputBufferSize, json.Length + 64);
            ImGuiApi.InputTextMultiline($"##{id}", ref json, bufferSize, size, ImGuiInputTextFlags.ReadOnly);
        }

        private static string BuildObjectDisplayText(object payload)
        {
            var builder = new StringBuilder();
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
            AppendObjectDisplayText(builder, payload, 0, visited);
            return builder.ToString();
        }

        private static void AppendObjectDisplayText(StringBuilder builder, object? value, int depth, HashSet<object> visited)
        {
            if (value is null)
            {
                builder.Append("null");
                return;
            }

            var type = value.GetType();
            if (IsSimpleDisplayType(type))
            {
                builder.Append(value);
                return;
            }

            if (!type.IsValueType)
            {
                if (!visited.Add(value))
                {
                    builder.Append("<circular reference>");
                    return;
                }
            }

            if (value is System.Collections.IEnumerable enumerable and not string)
            {
                var hasAny = false;
                var index = 0;
                foreach (var item in enumerable)
                {
                    hasAny = true;
                    AppendIndent(builder, depth);
                    builder.Append('[').Append(index++).Append("] ");
                    AppendObjectDisplayText(builder, item, depth + 1, visited);
                    builder.AppendLine();
                }

                if (!hasAny)
                {
                    builder.Append("<empty>");
                }

                return;
            }

            var members = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(static x => x.GetIndexParameters().Length == 0)
                .Cast<MemberInfo>()
                .Concat(type.GetFields(BindingFlags.Instance | BindingFlags.Public))
                .ToArray();

            if (members.Length == 0)
            {
                builder.Append(value);
                return;
            }

            foreach (var member in members)
            {
                object? memberValue;
                try
                {
                    memberValue = member switch
                    {
                        PropertyInfo property => property.GetValue(value),
                        FieldInfo field => field.GetValue(value),
                        _ => null
                    };
                }
                catch (Exception ex)
                {
                    memberValue = $"<error: {ex.Message}>";
                }

                AppendIndent(builder, depth);
                builder.Append(member.Name).Append(": ");

                if (memberValue is not null && !IsSimpleDisplayType(memberValue.GetType()) && memberValue is not string)
                {
                    builder.AppendLine();
                    AppendObjectDisplayText(builder, memberValue, depth + 1, visited);
                    builder.AppendLine();
                    continue;
                }

                AppendObjectDisplayText(builder, memberValue, depth + 1, visited);
                builder.AppendLine();
            }
        }

        private static bool IsSimpleDisplayType(Type type)
        {
            var actualType = Nullable.GetUnderlyingType(type) ?? type;
            return actualType.IsPrimitive
                || actualType.IsEnum
                || actualType == typeof(string)
                || actualType == typeof(decimal)
                || actualType == typeof(DateTime)
                || actualType == typeof(DateTimeOffset)
                || actualType == typeof(TimeSpan)
                || actualType == typeof(Guid);
        }

        private static void AppendIndent(StringBuilder builder, int depth)
        {
            builder.Append(' ', depth * 2);
        }

        /// <summary>
        /// 为对象遍历过程提供基于引用的去重比较，避免循环引用导致死循环。
        /// </summary>
        private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            public static ReferenceEqualityComparer Instance { get; } = new();

            public new bool Equals(object? x, object? y)
            {
                return ReferenceEquals(x, y);
            }

            public int GetHashCode(object obj)
            {
                return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
            }
        }

        private static void RenderInfoChip(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var min = ImGuiApi.GetCursorScreenPos();
            var textSize = ImGuiApi.CalcTextSize(text);
            var max = min + new Vector2(textSize.X + 22.0f, 24.0f);
            drawList.AddRectFilled(min, max, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.14f, 0.15f, 0.18f, 1.0f)), 12.0f);
            drawList.AddRect(min, max, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.12f)), 12.0f, ImDrawFlags.None, 1.0f);
            drawList.AddText(min + new Vector2(11.0f, 4.0f), ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.98f, 0.98f, 0.98f, 1.0f)), text);
            ImGuiApi.Dummy(new Vector2(textSize.X + 22.0f, 24.0f));
        }
    }
}
