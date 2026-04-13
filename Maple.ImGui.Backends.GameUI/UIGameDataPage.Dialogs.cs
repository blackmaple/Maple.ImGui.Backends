using Hexa.NET.ImGui;
using Maple.MonoGameAssistant.GameDTO;
using System.Globalization;
using System.Linq;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责帮助弹窗与编辑弹窗的绘制。
    /// </summary>
    public partial class UIGameDataPage
    {
        private bool IsCharacterSkillDialogInteractive()
        {
            return !ShowCharacterSkillSelectorDialog && PendingCharacterSkillAction is null;
        }

        private bool IsCharacterSkillSelectorInteractive()
        {
            return PendingCharacterSkillAction is null;
        }

        private bool BeginStandardDialog(string overlayId, string dialogId, ref bool isOpen)
        {
            var mainViewport = ImGuiApi.GetMainViewport();
            ImGuiApi.SetNextWindowViewport(mainViewport.ID);
            ImGuiApi.SetNextWindowPos(MainWindowPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(MainWindowSize, ImGuiCond.Always);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
            ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0f, 0f, 0f, 0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0f, 0f, 0f, 0f));
            var overlayFlags = ImGuiWindowFlags.NoTitleBar
                | ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoSavedSettings
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse;
            var overlayOpen = true;
            var overlayVisible = ImGuiApi.Begin($"###{overlayId}", ref overlayOpen, overlayFlags);
            ImGuiApi.PopStyleColor(2);
            ImGuiApi.PopStyleVar(3);
            if (!overlayVisible)
            {
                ImGuiApi.End();
                isOpen = false;
                return false;
            }

            ImGuiApi.SetNextWindowViewport(mainViewport.ID);
            ImGuiApi.SetNextWindowPos(MainWindowPosition + MainWindowSize * 0.5f, ImGuiCond.Always, new Vector2(0.5f, 0.5f));
            ImGuiApi.SetNextWindowSizeConstraints(new Vector2(EditDialogWidth, 0.0f), new Vector2(EditDialogWidth, float.MaxValue));
            ImGuiApi.SetNextWindowFocus();
            PushPopupDialogStyle();
            var dialogFlags = ImGuiWindowFlags.NoTitleBar
                | ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse
                | ImGuiWindowFlags.AlwaysAutoResize;
            if (!ImGuiApi.Begin($"###{dialogId}", ref isOpen, dialogFlags))
            {
                ImGuiApi.End();
                PopPopupDialogStyle();
                ImGuiApi.End();
                return false;
            }

            return true;
        }

        private void EndStandardDialog()
        {
            ImGuiApi.End();
            PopPopupDialogStyle();
            ImGuiApi.End();
        }

        private void RenderGameSessionInfoHelpDialog()
        {
            var isOpen = ShowGameSessionHelpDialog;
            if (!isOpen || !ShowSessionWindow)
            {
                return;
            }

            if (!BeginStandardDialog("GameSessionHelpOverlay", "GameSessionHelpDialog", ref isOpen))
            {
                ShowGameSessionHelpDialog = isOpen;
                return;
            }

            var popupPos = ImGuiApi.GetWindowPos();
            var popupSize = ImGuiApi.GetWindowSize();
            var title = GameSessionInfo?.DisplayName ?? GetUiText("Dialog.Help.TitleFallback");
            var desc = string.IsNullOrWhiteSpace(GameSessionInfo?.DisplayDesc) ? GetUiText("Dialog.Help.Empty") : GameSessionInfo.DisplayDesc;
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
            drawList.AddText(tagMin + new Vector2(11.0f, 4.0f), ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.75f, 1.0f, 0.78f, 1.0f)), GetUiText("Dialog.Help.GameTag"));

            ImGuiApi.SetCursorPos(new Vector2(96.0f, 54.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.62f, 0.62f, 0.62f, 1.0f));
            ImGuiApi.PushTextWrapPos(popupPos.X + popupSize.X - 22.0f);
            ImGuiApi.TextUnformatted(GetTwoLineText(desc, popupSize.X - 130.0f));
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();

            ImGuiApi.SetCursorPos(new Vector2(22.0f, 128.0f));
            RenderInfoChip(GetUiText("Dialog.Help.ApiVersion", apiVer));

            ImGuiApi.SetCursorPos(new Vector2(popupSize.X - 40.0f, 16.0f));
            PushIconButtonStyle(
                new Vector4(0.18f, 0.20f, 0.24f, 1.0f),
                new Vector4(0.30f, 0.18f, 0.18f, 1.0f),
                new Vector4(0.42f, 0.16f, 0.16f, 1.0f),
                999.0f);
            if (ImGuiApi.Button("×##HelpPopupClose", new Vector2(24.0f, 24.0f)))
            {
                isOpen = false;
            }

            PopIconButtonStyle();

            if (!string.IsNullOrWhiteSpace(qq))
            {
                var qqText = qq;
                var qqSize = ImGuiApi.CalcTextSize(qqText);
                ImGuiApi.SetCursorPos(new Vector2(popupSize.X - qqSize.X - 24.0f, popupSize.Y - 38.0f));
                ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.92f, 0.92f, 1.0f, 1.0f));
                ImGuiApi.TextUnformatted(qqText);
                ImGuiApi.PopStyleColor();
            }

            EndStandardDialog();
            ShowGameSessionHelpDialog = isOpen;
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
            RenderDisplayGridCards(GetUiText("Tab.Misc"), characterAttributes);
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
            if (!IsCharacterSkillDialogInteractive())
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
                if (ImGuiApi.BeginChild(
                    "##CharacterSkillContent",
                    contentSize,
                    ImGuiChildFlags.AlwaysUseWindowPadding,
                    IsCharacterSkillDialogInteractive() ? ImGuiWindowFlags.None : ImGuiWindowFlags.NoInputs))
                {
                    RenderCharacterSkillTable(ViewingCharacterSkill.Data, ViewingCharacterSkill.DisplayDesc);
                }

                ImGuiApi.EndChild();
            }

            ImGuiApi.End();
            PopOverlayDialogStyle();

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
                ImGuiApi.TextUnformatted(GetUiText("Dialog.Skill.NoSkills"));
                return;
            }

            RenderCharacterSkillGridCards(skillInfos);
        }

        private void RenderCharacterSkillGridCards(GameSkillInfoDTO[] items)
        {
            const float cardSpacing = 12.0f;
            const float cardHeight = 112.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsCharacterSkillDialogInteractive() ? ImGuiWindowFlags.None : ImGuiWindowFlags.NoInputs;
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##CharacterSkillGridCards", childSize, ImGuiChildFlags.None, gridWindowFlags))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var cardWidth = GetInventoryCardWidth(childSize.X);
            var rowHeight = cardHeight + cardSpacing;
            var (columns, rowCount, startOffsetX) = GetCenteredGridLayout(childSize.X, childSize.Y, cardWidth, cardSpacing, rowHeight, items.Length);
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
            const float textStartX = 76.0f;
            var allowCardInteraction = IsCharacterSkillDialogInteractive();
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

            RenderCardThumbnail(iconMin, new Vector2(48.0f, 48.0f), item.DisplayCategory, item.ObjectId);
            RenderInventoryCardTextBlock(cardCategory, item.DisplayName ?? string.Empty, windowPos, textStartX, textWidth);

            var canEdit = allowCardInteraction && item.CanWrite && !_characterSkillUpdateRequest.IsRunning;
            if (RenderSkillActionButton($"##RemoveSkill_{item.ObjectId}_{index}", removeButtonPosition, false, canEdit))
            {
                HandleCharacterSkillRemoveButtonClick(item);
            }

            if (RenderSkillActionButton($"##AddSkill_{item.ObjectId}_{index}", addButtonPosition, true, canEdit))
            {
                HandleCharacterSkillAddButtonClick(item);
            }

            if (allowCardInteraction && (ImGuiApi.IsItemHovered() || ImGuiApi.IsMouseHoveringRect(windowPos, windowPos + new Vector2(cardWidth, cardHeight))))
            {
                var tooltipDesc = GetPlainText(item.DisplayDesc);
                if (string.IsNullOrWhiteSpace(tooltipDesc))
                {
                    tooltipDesc = GetUiText("Dialog.Text.Empty");
                }

                ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
                ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
                ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
                BeginStandardTooltip();
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
            var dialogWindowFlags = ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar;
            if (!IsCharacterSkillSelectorInteractive())
            {
                dialogWindowFlags |= ImGuiWindowFlags.NoInputs;
            }

            if (ImGuiApi.Begin("###CharacterSkillSelectorDialog", ref isOpen, dialogWindowFlags))
            {
                RenderSessionDialogTitleBar($"Select Skill - {CharacterSkillSelectionCategory}", ref isOpen, "CharacterSkillSelectorDialog");
                var contentPosition = new Vector2(SessionContentLeftMargin, SessionTitleBarHeight + SessionContentTopMargin);
                var contentSize = new Vector2(
                    MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin),
                    MathF.Max(1.0f, MainWindowSize.Y - SessionTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
                ImGuiApi.SetCursorPos(contentPosition);
                if (ImGuiApi.BeginChild(
                    "##CharacterSkillSelectorContent",
                    contentSize,
                    ImGuiChildFlags.AlwaysUseWindowPadding,
                    IsCharacterSkillSelectorInteractive() ? ImGuiWindowFlags.None : ImGuiWindowFlags.NoInputs))
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
            var isOpen = PendingCharacterSkillAction is not null;
            if (!isOpen)
            {
                return;
            }

            PendingOpenCharacterSkillActionPopup = false;
            if (!BeginStandardDialog("CharacterSkillActionOverlay", "CharacterSkillActionDialog", ref isOpen))
            {
                if (!isOpen)
                {
                    PendingCharacterSkillAction = null;
                }

                return;
            }

            if (PendingCharacterSkillAction is null)
            {
                EndStandardDialog();
                return;
            }

            var prompt = PendingCharacterSkillAction.IsAdd
                ? GetUiText("Dialog.Skill.ConfirmAdd", PendingCharacterSkillAction.DisplayName)
                : GetUiText("Dialog.Skill.ConfirmRemove", PendingCharacterSkillAction.DisplayName);
            RenderEditDialogHeaderCard(
                "CharacterSkillActionConfirm",
                null,
                PendingCharacterSkillAction.ModifyCategory,
                PendingCharacterSkillAction.DisplayName,
                prompt);

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            PushDialogActionButtonStyle();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.Yes")}##CharacterSkillActionConfirm", DialogActionButtonSize) && !_characterSkillUpdateRequest.IsRunning)
            {
                var action = PendingCharacterSkillAction;
                PendingCharacterSkillAction = null;
                isOpen = false;
                _characterSkillUpdateRequest.TryStart(() => UpdateCharacterSkillDialogAsync(
                    action.ModifyCategory,
                    action.OldSkill,
                    action.NewSkill,
                    action.DisplayName));
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.No")}##CharacterSkillActionConfirm", DialogActionButtonSize))
            {
                PendingCharacterSkillAction = null;
                isOpen = false;
            }

            PopDialogActionButtonStyle();

            EndStandardDialog();
        }

        private void RenderMonsterInfoDialog()
        {
            if (!ShowMonsterInfoDialog || ViewingMonsterInfo is null || !ShowSessionWindow)
            {
                return;
            }

            PushOverlayDialogStyle();

            var isOpen = true;
            ImGuiApi.SetNextWindowPos(MainWindowPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(MainWindowSize, ImGuiCond.Always);
            if (ImGuiApi.Begin("###MonsterInfoDialog", ref isOpen, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar))
            {
                RenderSessionDialogTitleBar(ViewingMonsterInfo.DisplayName, ref isOpen, "MonsterInfoDialog");
                var contentPosition = new Vector2(SessionContentLeftMargin, SessionTitleBarHeight + SessionContentTopMargin);
                var contentSize = new Vector2(
                    MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin),
                    MathF.Max(1.0f, MainWindowSize.Y - SessionTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
                ImGuiApi.SetCursorPos(contentPosition);
                if (ImGuiApi.BeginChild("##MonsterInfoContent", contentSize, ImGuiChildFlags.AlwaysUseWindowPadding))
                {
                    if (!string.IsNullOrWhiteSpace(ViewingMonsterInfo.Data.DisplayCategory))
                    {
                        RenderInfoChip(ViewingMonsterInfo.Data.DisplayCategory);
                        ImGuiApi.Spacing();
                    }

                    ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
                    ImGuiApi.TextUnformatted(ViewingMonsterInfo.DisplayName);
                    ImGuiApi.PopStyleColor();

                    if (!string.IsNullOrWhiteSpace(ViewingMonsterInfo.DisplayDesc))
                    {
                        ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.78f, 0.82f, 0.90f, 1.0f));
                        ImGuiApi.PushTextWrapPos(ImGuiApi.GetCursorScreenPos().X + ImGuiApi.GetContentRegionAvail().X);
                        ImGuiApi.TextUnformatted(ViewingMonsterInfo.DisplayDesc);
                        ImGuiApi.PopTextWrapPos();
                        ImGuiApi.PopStyleColor();
                    }

                    ImGuiApi.Spacing();
                    ImGuiApi.Separator();
                    RenderMonsterInfoTable(ViewingMonsterInfo.Data);
                }

                ImGuiApi.EndChild();
            }

            ImGuiApi.End();
            PopOverlayDialogStyle();
            if (!isOpen)
            {
                ShowMonsterInfoDialog = false;
                ViewingMonsterInfo = null;
            }
        }

        private void RenderMonsterAddConfirmDialog()
        {
            var isOpen = PendingMonsterAddAction is not null;
            if (!isOpen)
            {
                return;
            }

            PendingOpenMonsterAddPopup = false;
            if (!BeginStandardDialog("MonsterAddConfirmOverlay", "MonsterAddConfirmDialog", ref isOpen))
            {
                if (!isOpen)
                {
                    PendingMonsterAddAction = null;
                }

                return;
            }

            if (PendingMonsterAddAction is null)
            {
                EndStandardDialog();
                return;
            }

            var monsterDesc = GetPlainText(PendingMonsterAddAction.DisplayDesc);
            RenderEditDialogHeaderCard(
                "MonsterAddConfirm",
                PendingMonsterAddAction.Monster.ObjectId,
                PendingMonsterAddAction.Monster.DisplayCategory,
                PendingMonsterAddAction.DisplayName,
                string.IsNullOrWhiteSpace(monsterDesc)
                    ? GetUiText("Dialog.Monster.AddConfirm", PendingMonsterAddAction.DisplayName)
                    : monsterDesc);

            if (!string.IsNullOrWhiteSpace(monsterDesc))
            {
                ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
                ImGuiApi.TextUnformatted(GetUiText("Dialog.Monster.AddConfirm", PendingMonsterAddAction.DisplayName));
                ImGuiApi.PopStyleColor();
                ImGuiApi.Spacing();
            }

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            PushDialogActionButtonStyle();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.Yes")}##MonsterAddConfirm", DialogActionButtonSize) && !_monsterAddRequest.IsRunning)
            {
                var action = PendingMonsterAddAction;
                PendingMonsterAddAction = null;
                isOpen = false;
                _monsterAddRequest.TryStart(() => UpdateMonsterMemberAsync(action.Monster));
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.No")}##MonsterAddConfirm", DialogActionButtonSize))
            {
                PendingMonsterAddAction = null;
                isOpen = false;
            }

            PopDialogActionButtonStyle();

            EndStandardDialog();
        }

        private void RenderMonsterInfoTable(GameMonsterDisplayDTO monster)
        {
            const float cardSpacing = 12.0f;
            const float cardHeight = 112.0f;

            var attributes = monster.MonsterAttributes ?? [];
            var skillInfos = monster.SkillInfos ?? [];
            var totalCount = attributes.Length + skillInfos.Length;
            if (totalCount == 0)
            {
                ImGuiApi.TextUnformatted(GetUiText("Dialog.Monster.NoData"));
                return;
            }

            var childSize = ImGuiApi.GetContentRegionAvail();
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##MonsterInfoTable", childSize, ImGuiChildFlags.None, ImGuiWindowFlags.None))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var cardWidth = GetInventoryCardWidth(childSize.X);
            var rowHeight = cardHeight + cardSpacing;
            var (columns, rowCount, startOffsetX) = GetCenteredGridLayout(childSize.X, childSize.Y, cardWidth, cardSpacing, rowHeight, totalCount);
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
                        if (itemIndex >= totalCount)
                        {
                            break;
                        }

                        if (column > 0)
                        {
                            ImGuiApi.SameLine(0.0f, cardSpacing);
                        }

                        if (itemIndex < attributes.Length)
                        {
                            RenderMonsterInfoAttributeCard(attributes[itemIndex], itemIndex, cardWidth);
                        }
                        else
                        {
                            var skillIndex = itemIndex - attributes.Length;
                            RenderMonsterInfoSkillCard(skillInfos[skillIndex], skillIndex, cardWidth);
                        }
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

        private void RenderMonsterInfoAttributeCard(GameValueInfoDTO attribute, int index, float availableWidth)
        {
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            var cardWidth = MathF.Max(1.0f, availableWidth);
            if (ImGuiApi.BeginChild($"##MonsterAttribute_{attribute.ObjectId}_{index}", new Vector2(cardWidth, 112.0f), ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                var drawList = ImGuiApi.GetWindowDrawList();
                var cardPos = ImGuiApi.GetWindowPos();
                RenderCardThumbnail(cardPos + new Vector2(14.0f, 14.0f), new Vector2(48.0f, 48.0f), null, attribute.ObjectId);
                RenderInventoryCardTextBlock(GetUiText("Dialog.Monster.Category.Attributes"), attribute.DisplayName ?? string.Empty, cardPos, 76.0f, MathF.Max(1.0f, cardWidth - 92.0f));
                var displayValue = attribute.DisplayValue ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(displayValue))
                {
                    ImGuiApi.SetCursorPos(new Vector2(76.0f, 64.0f));
                    ImGuiApi.TextUnformatted(GetSingleLineText(displayValue, MathF.Max(1.0f, cardWidth - 92.0f)));
                }

                if (ImGuiApi.IsMouseHoveringRect(cardPos, cardPos + new Vector2(cardWidth, 112.0f)))
                {
                    ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
                    ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
                    ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
                    BeginStandardTooltip();
                    ImGuiApi.PushTextWrapPos(ImGuiApi.GetFontSize() * 24.0f);
                    ImGuiApi.TextUnformatted(GetUiText("Dialog.Monster.Category.Attributes"));
                    ImGuiApi.Separator();
                    ImGuiApi.TextUnformatted(attribute.DisplayName ?? string.Empty);
                    ImGuiApi.Spacing();
                    ImGuiApi.TextUnformatted(displayValue);
                    ImGuiApi.PopTextWrapPos();
                    ImGuiApi.EndTooltip();
                    ImGuiApi.PopStyleColor(2);
                    ImGuiApi.PopStyleVar();

                    drawList.AddRect(
                        cardPos + new Vector2(1.0f, 1.0f),
                        cardPos + new Vector2(cardWidth, 112.0f) - new Vector2(1.0f, 1.0f),
                        ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                        DisplayCardRounding,
                        ImDrawFlags.None,
                        1.6f);
                }
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
        }

        private void RenderMonsterInfoSkillCard(GameSkillInfoDTO skillInfo, int index, float availableWidth)
        {
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            var cardWidth = MathF.Max(1.0f, availableWidth);
            if (ImGuiApi.BeginChild($"##MonsterInfoSkill_{skillInfo.ObjectId}_{index}", new Vector2(cardWidth, 112.0f), ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                var drawList = ImGuiApi.GetWindowDrawList();
                var cardPos = ImGuiApi.GetWindowPos();
                RenderCardThumbnail(cardPos + new Vector2(14.0f, 14.0f), new Vector2(48.0f, 48.0f), skillInfo.DisplayCategory, skillInfo.ObjectId);
                RenderInventoryCardTextBlock(GetUiText("Dialog.Monster.Category.Skill"), skillInfo.DisplayName ?? string.Empty, cardPos, 76.0f, MathF.Max(1.0f, cardWidth - 92.0f));
                var tooltipDesc = GetPlainText(skillInfo.DisplayDesc);
                if (!string.IsNullOrWhiteSpace(tooltipDesc))
                {
                    ImGuiApi.SetCursorPos(new Vector2(76.0f, 64.0f));
                    ImGuiApi.TextUnformatted(GetSingleLineText(tooltipDesc, MathF.Max(1.0f, cardWidth - 92.0f)));
                }

                if (ImGuiApi.IsMouseHoveringRect(cardPos, cardPos + new Vector2(cardWidth, 112.0f)))
                {
                    var fullDesc = string.IsNullOrWhiteSpace(tooltipDesc) ? GetUiText("Dialog.Text.Empty") : tooltipDesc;
                    ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
                    ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
                    ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
                    BeginStandardTooltip();
                    ImGuiApi.PushTextWrapPos(ImGuiApi.GetFontSize() * 24.0f);
                    ImGuiApi.TextUnformatted(GetUiText("Dialog.Monster.Category.Skill"));
                    ImGuiApi.Separator();
                    ImGuiApi.TextUnformatted(skillInfo.DisplayName ?? string.Empty);
                    ImGuiApi.Spacing();
                    ImGuiApi.TextUnformatted(fullDesc);
                    RenderCharacterSkillTooltipAttributes(skillInfo);
                    ImGuiApi.PopTextWrapPos();
                    ImGuiApi.EndTooltip();
                    ImGuiApi.PopStyleColor(2);
                    ImGuiApi.PopStyleVar();

                    drawList.AddRect(
                        cardPos + new Vector2(1.0f, 1.0f),
                        cardPos + new Vector2(cardWidth, 112.0f) - new Vector2(1.0f, 1.0f),
                        ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                        DisplayCardRounding,
                        ImDrawFlags.None,
                        1.6f);
                }
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
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
                RenderCharacterSkillSelectorGridCards([.. items.Select(static item => new CharacterSkillSelectorItem(item.ObjectId, item.DisplayCategory, item.DisplayName, item.DisplayDesc))]);
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
                ImGuiApi.TextUnformatted(GetUiText("Dialog.Skill.Selector.NoSkillsInCategory"));
                return;
            }

            RenderCharacterSkillSelectorGridCards([.. fallbackItems.Select(static item => new CharacterSkillSelectorItem(item.ObjectId, item.DisplayCategory, item.DisplayName, item.DisplayDesc))]);
        }

        private void RenderCharacterSkillSelectorToolbar(SearchState searchState)
        {
            var allowInteraction = IsCharacterSkillSelectorInteractive();
            var toolbarWidth = ImGuiApi.GetContentRegionAvail().X;
            var totalWidth = MathF.Max(320.0f, toolbarWidth - 8.0f);
            var inputWidth = MathF.Max(220.0f, totalWidth - ToolbarIconButtonSize - 8.0f);
            var startX = MathF.Max(0.0f, (toolbarWidth - totalWidth) * 0.5f);

            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + startX);
            ImGuiApi.SetCursorPosY(ImGuiApi.GetCursorPosY() + 2.0f);
            ImGuiApi.BeginDisabled(!allowInteraction);
            PushToolbarSearchInputStyle();
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

            PopToolbarSearchInputStyle();
            ImGuiApi.SameLine();
            PushIconButtonStyle(EditorButtonBg, EditorButtonBgHovered, EditorButtonBgActive);
            if (ImGuiApi.Button("##CharacterSkillSelector_Search", new Vector2(ToolbarIconButtonSize, ToolbarIconButtonSize)))
            {
                searchState.AppliedText = searchState.InputText;
            }

            DrawSearchButtonIcon();
            PopIconButtonStyle();
            ImGuiApi.EndDisabled();
            ImGuiApi.Spacing();
        }

        private void RenderCharacterSkillSelectorGridCards(CharacterSkillSelectorItem[] items)
        {
            const float cardSpacing = 12.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsCharacterSkillSelectorInteractive() ? ImGuiWindowFlags.None : ImGuiWindowFlags.NoInputs;
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##CharacterSkillSelectorGridCards", childSize, ImGuiChildFlags.None, gridWindowFlags))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var cardWidth = GetInventoryCardWidth(childSize.X);
            const float cardHeight = 112.0f;
            var rowHeight = cardHeight + cardSpacing;
            var (columns, rowCount, startOffsetX) = GetCenteredGridLayout(childSize.X, childSize.Y, cardWidth, cardSpacing, rowHeight, items.Length);
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
            var allowCardInteraction = IsCharacterSkillSelectorInteractive();
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

            RenderCardThumbnail(iconMin, new Vector2(48.0f, 48.0f), item.DisplayCategory, item.ObjectId);
            RenderInventoryCardTextBlock(cardCategory, item.DisplayName ?? string.Empty, windowPos, textStartX, textWidth);

            if (RenderSkillActionButton($"##AddSelectedSkill_{item.ObjectId}_{index}", addButtonPosition, true, allowCardInteraction && !_characterSkillUpdateRequest.IsRunning))
            {
                HandleCharacterSkillSelectionAddButtonClick(item);
            }

            if (allowCardInteraction && (ImGuiApi.IsItemHovered() || ImGuiApi.IsMouseHoveringRect(windowPos, windowPos + new Vector2(cardWidth, cardHeight))))
            {
                var tooltipDesc = GetPlainText(item.DisplayDesc);
                if (string.IsNullOrWhiteSpace(tooltipDesc))
                {
                    tooltipDesc = GetUiText("Dialog.Text.Empty");
                }

                ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
                ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
                ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
                BeginStandardTooltip();
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
            var isOpen = EditingCurrency is not null;
            if (!isOpen)
            {
                return;
            }

            PendingOpenCurrencyEditPopup = false;
            if (!BeginStandardDialog("CurrencyEditOverlay", "CurrencyEditDialog", ref isOpen))
            {
                if (!isOpen)
                {
                    EditingCurrency = null;
                    CurrencyEditValue = string.Empty;
                }

                return;
            }

            if (PendingCloseCurrencyEditPopup)
            {
                PendingCloseCurrencyEditPopup = false;
                EditingCurrency = null;
                CurrencyEditValue = string.Empty;
                EndStandardDialog();
                return;
            }

            if (EditingCurrency is null)
            {
                EndStandardDialog();
                return;
            }

            RenderEditDialogHeaderCard("CurrencyEdit", EditingCurrency.Info.ObjectId, EditingCurrency.Category, EditingCurrency.DisplayName, EditingCurrency.DisplayDesc);

            ImGuiApi.SetNextItemWidth(ImGuiApi.GetContentRegionAvail().X);
            PushEditorInputStyle();
            CurrencyEditValue = RenderStepInput("##CurrencyEdit", "CurrencyEdit", CurrencyEditValue);
            PopEditorInputStyle();

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            PushDialogActionButtonStyle();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.Ok")}##CurrencyEdit", DialogActionButtonSize) && !_currencyUpdateRequest.IsRunning)
            {
                _currencyUpdateRequest.TryStart(UpdateCurrencyEditAsync);
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.Cancel")}##CurrencyEdit", DialogActionButtonSize))
            {
                isOpen = false;
            }

            PopDialogActionButtonStyle();

            EndStandardDialog();
            if (!isOpen)
            {
                EditingCurrency = null;
                CurrencyEditValue = string.Empty;
            }
        }

        private void RenderInventoryEditDialog()
        {
            var isOpen = EditingInventory is not null;
            if (!isOpen)
            {
                return;
            }

            PendingOpenInventoryEditPopup = false;
            if (!BeginStandardDialog("InventoryEditOverlay", "InventoryEditDialog", ref isOpen))
            {
                if (!isOpen)
                {
                    EditingInventory = null;
                    InventoryEditValue = string.Empty;
                }

                return;
            }

            if (PendingCloseInventoryEditPopup)
            {
                PendingCloseInventoryEditPopup = false;
                EditingInventory = null;
                InventoryEditValue = string.Empty;
                EndStandardDialog();
                return;
            }

            if (EditingInventory is null)
            {
                EndStandardDialog();
                return;
            }

            RenderEditDialogHeaderCard("InventoryEdit", EditingInventory.Info.ObjectId, EditingInventory.Category, EditingInventory.DisplayName, EditingInventory.DisplayDesc);

            ImGuiApi.SetNextItemWidth(ImGuiApi.GetContentRegionAvail().X);
            PushEditorInputStyle();
            InventoryEditValue = RenderStepInput("##InventoryEdit", "InventoryEdit", InventoryEditValue);
            PopEditorInputStyle();

            ImGuiApi.Spacing();
            var totalButtonWidth = DialogActionButtonSize.X * 2.0f + 8.0f;
            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + MathF.Max(0.0f, ImGuiApi.GetContentRegionAvail().X - totalButtonWidth));
            PushDialogActionButtonStyle();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.Ok")}##InventoryEdit", DialogActionButtonSize) && !_inventoryUpdateRequest.IsRunning)
            {
                _inventoryUpdateRequest.TryStart(UpdateInventoryEditAsync);
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button($"{GetUiText("Dialog.Action.Cancel")}##InventoryEdit", DialogActionButtonSize))
            {
                isOpen = false;
            }

            PopDialogActionButtonStyle();

            EndStandardDialog();
            if (!isOpen)
            {
                EditingInventory = null;
                InventoryEditValue = string.Empty;
            }
        }

        private void RenderCardThumbnail(Vector2 min, Vector2 size, string? category, string? objectId)
        {
            if (!string.IsNullOrWhiteSpace(objectId))
            {
                var cursorPos = ImGuiApi.GetCursorPos();
                ImGuiApi.SetCursorScreenPos(min);
                if (TryDrawImage?.Invoke(category, objectId) == true)
                {
                    ImGuiApi.SetCursorPos(cursorPos);
                    return;
                }

                ImGuiApi.SetCursorPos(cursorPos);
            }

            DrawThumbnailPreview(min, size);
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

        private void RenderEditDialogHeaderCard(string idSuffix, string? objectId, string? category, string title, string description)
        {
            const float thumbnailSize = 48.0f;
            var availableWidth = ImGuiApi.GetContentRegionAvail().X;
            var textStartX = 76.0f;
            var textWidth = MathF.Max(1.0f, availableWidth - textStartX - 12.0f);
            var textRegionStartX = ImGuiApi.GetCursorScreenPos().X + textStartX;
            var textLineHeight = ImGuiApi.GetTextLineHeight();
            var descAreaHeight = (textLineHeight * 2.0f) + ImGuiApi.GetStyle().ItemSpacing.Y;

            var rowStart = ImGuiApi.GetCursorScreenPos();
            RenderCardThumbnail(rowStart + new Vector2(14.0f, 0.0f), new Vector2(thumbnailSize, thumbnailSize), category, objectId);
            var drawList = ImGuiApi.GetWindowDrawList();
            if (!string.IsNullOrWhiteSpace(category))
            {
                var categoryText = GetSingleLineText(category, textWidth);
                var categorySize = ImGuiApi.CalcTextSize(categoryText);
                var chipPadding = new Vector2(7.0f, 2.0f);
                var categoryMin = new Vector2(textRegionStartX, rowStart.Y + 2.0f);
                var categoryMax = categoryMin + new Vector2(categorySize.X + (chipPadding.X * 2.0f), categorySize.Y + (chipPadding.Y * 2.0f) - 2.0f);
                drawList.AddRectFilled(
                    categoryMin,
                    categoryMax,
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.14f, 0.34f, 0.16f, 0.96f)),
                    10.0f);
                drawList.AddRect(
                    categoryMin,
                    categoryMax,
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.28f, 0.78f, 0.34f, 0.98f)),
                    10.0f,
                    ImDrawFlags.None,
                    1.0f);
                drawList.AddText(
                    categoryMin + chipPadding - new Vector2(0.0f, 1.0f),
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.88f, 1.0f, 0.90f, 1.0f)),
                    categoryText);

                ImGuiApi.SetCursorScreenPos(new Vector2(textRegionStartX, categoryMax.Y + 6.0f));
            }
            else
            {
                ImGuiApi.SetCursorScreenPos(new Vector2(textRegionStartX, rowStart.Y + 2.0f));
            }

            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.98f, 0.98f, 0.98f, 1.0f));
            ImGuiApi.TextUnformatted(GetSingleLineText(title, textWidth));
            ImGuiApi.PopStyleColor();

            var reservedHeight = MathF.Max(thumbnailSize, ImGuiApi.GetCursorScreenPos().Y - rowStart.Y);
            ImGuiApi.SetCursorScreenPos(new Vector2(rowStart.X, rowStart.Y + reservedHeight));
            ImGuiApi.Dummy(new Vector2(0.0f, 6.0f));

            var descStart = ImGuiApi.GetCursorScreenPos();
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.78f, 0.82f, 0.90f, 1.0f));
            ImGuiApi.PushTextWrapPos(descStart.X + MathF.Max(1.0f, availableWidth - 8.0f));
            ImGuiApi.TextUnformatted(GetTwoLineText(description, MathF.Max(1.0f, availableWidth - 8.0f)));
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();
            var usedDescHeight = ImGuiApi.GetCursorScreenPos().Y - descStart.Y;
            if (usedDescHeight < descAreaHeight)
            {
                ImGuiApi.Dummy(new Vector2(0.0f, descAreaHeight - usedDescHeight));
            }

            ImGuiApi.Spacing();
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
            PushUnifiedInteractiveStyle();
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

            PopUnifiedInteractiveStyle();

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

            PushIconButtonStyle(
                new Vector4(0.44f, 0.16f, 0.16f, 1.0f),
                new Vector4(0.76f, 0.22f, 0.22f, 1.0f),
                new Vector4(0.58f, 0.12f, 0.12f, 1.0f),
                999.0f);
            ImGuiApi.SetCursorPos(new Vector2(closeX, 8));
            if (ImGuiApi.Button($"×##{idSuffix}_Close", buttonSize))
            {
                isOpen = false;
            }

            PopIconButtonStyle();
        }

        private static bool TryParseNumericValue(string value, out decimal number)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out number)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out number);
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
