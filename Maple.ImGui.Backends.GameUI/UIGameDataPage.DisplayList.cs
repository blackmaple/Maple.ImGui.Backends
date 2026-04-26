using Hexa.NET.ImGui;
using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;
using System.Linq;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责列表工具栏、卡片列表和搜索展示区域的绘制。
    /// </summary>
    public partial class UIGameDataPage
    {
        private void RenderDisplayTabContent<TDisplay>(string tabName, TDisplay[] items, SearchState searchState) where TDisplay : GameObjectDisplayDTO
        {
            RenderTabToolbar(tabName, searchState);
            var filteredItems = SortDisplays(FilterDisplays(items, searchState.AppliedText));
            if (filteredItems is GameInventoryDisplayDTO[] inventoryItems)
            {
                RenderInventoryDisplayCards(inventoryItems);
                return;
            }

            RenderDisplayGridCards(tabName, filteredItems);
        }

        private void RenderDisplayGridCards<TDisplay>(string tabName, TDisplay[] items) where TDisplay : GameObjectDisplayDTO
        {
            const float cardSpacing = GridCardVerticalSpacing;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            var cardWidth = GetInventoryCardWidth(childSize.X);
            var gridCardHeight = items is GameSwitchDisplayDTO[] switchItems
                ? MathF.Max(GridCardHeight, switchItems.Select(GetSwitchDisplayEditorCardHeight).DefaultIfEmpty(GridCardHeight).Max())
                : GridCardHeight;
            RenderGridCardsCore(
                $"##{tabName}GridCards",
                childSize,
                gridWindowFlags,
                cardWidth,
                gridCardHeight,
                cardSpacing,
                items.Length,
                itemIndex => RenderDisplayCard(tabName, items[itemIndex], itemIndex, cardWidth));
        }

        private void RenderMonsterDisplayCards(GameMonsterDisplayDTO[] items)
        {
            const float cardSpacing = GridCardVerticalSpacing;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            var cardWidth = GetInventoryCardWidth(childSize.X);
            RenderGridCardsCore(
                "##MonsterGridCards",
                childSize,
                gridWindowFlags,
                cardWidth,
                GridCardHeight,
                cardSpacing,
                items.Length,
                itemIndex => RenderInventoryDisplayCard(items[itemIndex], itemIndex, new Vector2(cardWidth, GridCardHeight)));
        }

        private void RenderInventoryDisplayCards(GameInventoryDisplayDTO[] items)
        {
            const float inventoryCardSpacing = GridCardVerticalSpacing;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            var inventoryCardWidth = GetInventoryCardWidth(childSize.X);
            var rowHeight = GridCardHeight + inventoryCardSpacing;
            var estimatedColumns = GetGridColumns(childSize.X, inventoryCardWidth, inventoryCardSpacing);
            if (LastInventoryGridColumns > 0 && LastInventoryGridColumns != estimatedColumns)
            {
                var currentScrollY = ImGuiApi.GetScrollY();
                var currentTopRow = currentScrollY / rowHeight;
                var currentTopItemIndex = currentTopRow * LastInventoryGridColumns;
                var nextTopRow = currentTopItemIndex / estimatedColumns;
                PendingInventoryGridScrollY = nextTopRow * rowHeight;
            }

            LastInventoryGridColumns = estimatedColumns;

            if (PendingInventoryGridScrollY >= 0.0f)
            {
                ImGuiApi.SetScrollY(PendingInventoryGridScrollY);
                PendingInventoryGridScrollY = -1.0f;
            }

            RenderGridCardsCore(
                "##InventoryGridCards",
                childSize,
                gridWindowFlags,
                inventoryCardWidth,
                GridCardHeight,
                inventoryCardSpacing,
                items.Length,
                itemIndex => RenderInventoryDisplayCard(items[itemIndex], itemIndex, new Vector2(inventoryCardWidth, GridCardHeight)));
        }

        private void RenderGridCardsCore(string childId, Vector2 childSize, ImGuiWindowFlags gridWindowFlags, float cardWidth, float cardHeight, float cardSpacing, int itemCount, Action<int> renderCard)
        {
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild(childId, childSize, ImGuiChildFlags.None, gridWindowFlags))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var rowHeight = cardHeight + cardSpacing;
            var (columns, rowCount, startOffsetX) = GetCenteredGridLayout(childSize.X, childSize.Y, cardWidth, cardSpacing, rowHeight, itemCount);
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
                        if (itemIndex >= itemCount)
                        {
                            break;
                        }

                        if (column > 0)
                        {
                            ImGuiApi.SameLine(0.0f, cardSpacing);
                        }

                        renderCard(itemIndex);
                    }

                    if (row < rowCount - 1)
                    {
                        ImGuiApi.Dummy(new Vector2(0.0f, cardSpacing));
                    }
                }
            }

            clipper.End();
            ImGuiApi.Dummy(new Vector2(0.0f, GridBottomPadding));
            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
        }

        private void RenderInventoryDisplayCard(GameObjectDisplayDTO item, int index, Vector2 cardSize)
        {
            var allowCardInteraction = !IsEditDialogBlockingInput();
            var switchDisplay = item as GameSwitchDisplayDTO;
            var objectDisplay = item;// as GameObjectDisplayDTO;
            var monsterDisplay = item as GameMonsterDisplayDTO;
            var textColumnWidth = switchDisplay is null
                ? MathF.Max(1.0f, cardSize.X - GridCardTextStartX - 50.0f)
                : MathF.Max(1.0f, cardSize.X - GridCardTextStartX - 12.0f);
            if (!BeginDisplayCard($"##InventoryGridCard_{item.ObjectId}_{index}", cardSize))
            {
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var cardPos = ImGuiApi.GetWindowPos();
            var isCardHovered = IsCardInteractionHovered(allowCardInteraction, cardPos, cardSize);
            var (cardCategory, visibleCardCategory) = GetCardCategoryTexts(objectDisplay.DisplayCategory, switchDisplay is not null, GetUiText("Text.Inventory"));
            RenderDisplayCardHeader(
                cardPos,
                objectDisplay.DisplayCategory,
                item.ObjectId,
                objectDisplay.DisplayImage,
                visibleCardCategory,
                item.DisplayName ?? string.Empty,
                textColumnWidth,
                switchDisplay is not null);

            if (monsterDisplay is not null)
            {
                var addButtonPosition = GetCardActionButtonPosition(cardSize.X, cardSize.Y);
                var infoButtonPosition = GetSecondaryCardActionButtonPosition(addButtonPosition);

                var infoClicked = RenderActionIconButton(
                    infoButtonPosition,
                    $"##MonsterInfo_{item.ObjectId}_{index}",
                    CardActionButtonSize);
                if (infoClicked)
                {
                    HandleMonsterInfoButtonClick(monsterDisplay);
                }

                if (RenderSkillActionButton($"##MonsterAdd_{item.ObjectId}_{index}", addButtonPosition, true, !_monsterAddRequest.IsRunning))
                {
                    HandleMonsterAddButtonClick(monsterDisplay);
                }
            }
            else if (switchDisplay is null)
            {
                if (RenderActionIconButton(
                    GetCardActionButtonPosition(cardSize.X, cardSize.Y),
                    $"##InventoryGridEdit_{item.ObjectId}_{index}",
                    GridCardActionButtonSize))
                {
                    HandleEditButtonClick(item);
                }
            }
            else
            {
                RenderSwitchEditorHost(switchDisplay, item.ObjectId, index, cardSize.X, cardSize.Y);
            }

            if (allowCardInteraction && isCardHovered)
            {
                RenderDisplayCardHoverFeedback(drawList, cardPos, cardSize, cardCategory, item.DisplayName ?? string.Empty, item.DisplayDesc, item);
            }

            EndDisplayCard();
        }

        private void RenderInventoryCardTextBlock(string category, string title, Vector2 cardPos, float textStartX, float textColumnWidth, bool preserveCategorySpace = false)
        {
            var textWidth = MathF.Max(1.0f, textColumnWidth);
            var categoryText = GetSingleLineText(category, textWidth);
            var titleText = GetSingleLineText(title, textWidth);
            var lineHeight = ImGuiApi.GetTextLineHeight();
            var drawList = ImGuiApi.GetWindowDrawList();
            var hasCategory = !string.IsNullOrWhiteSpace(categoryText);
            var categorySize = hasCategory ? ImGuiApi.CalcTextSize(categoryText) : Vector2.Zero;
            var blockSpacing = 6.0f;
            var titleSize = ImGuiApi.CalcTextSize(titleText);
            var chipPadding = new Vector2(7.0f, 2.0f);
            var chipSize = hasCategory
                ? new Vector2(categorySize.X + (chipPadding.X * 2.0f), categorySize.Y + (chipPadding.Y * 2.0f) - 2.0f)
                : Vector2.Zero;
            var blockHeight = hasCategory
                ? chipSize.Y + blockSpacing + titleSize.Y
                : titleSize.Y;
            var blockStartY = cardPos.Y + 14.0f;
            var textColumnStartX = cardPos.X + textStartX;
            if (hasCategory)
            {
                var categoryMin = new Vector2(textColumnStartX, blockStartY);
                var categoryMax = categoryMin + chipSize;
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
            }

            var textColor = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.96f, 0.96f, 0.96f, 1.0f));
            var titleX = textColumnStartX;
            var reservedChipHeight = ImGuiApi.GetTextLineHeight() + (chipPadding.Y * 2.0f) - 2.0f;
            var titleY = hasCategory
                ? blockStartY + chipSize.Y + blockSpacing
                : preserveCategorySpace
                    ? blockStartY + reservedChipHeight + blockSpacing
                    : blockStartY + 4.0f;
            drawList.AddText(new Vector2(titleX, titleY), textColor, titleText);
        }

        private static float GetInventoryCardWidth(float availableWidth)
        {
            return availableWidth switch
            {
                >= 1500.0f => 260.0f,
                >= 1200.0f => 240.0f,
                >= 900.0f => 220.0f,
                >= 640.0f => 220.0f,
                _ => 220.0f
            };
        }

        private static void RenderTooltipAttributes(GameDisplayDTO item)
        {
            if (item is GameInventoryDisplayDTO inventoryDisplay)
            {
                var itemAttributes = inventoryDisplay.ItemAttributes ?? [];
                var hasInventoryAttributes = false;
                foreach (var attribute in itemAttributes)
                {
                    if (!hasInventoryAttributes)
                    {
                        hasInventoryAttributes = true;
                        ImGuiApi.Spacing();
                        ImGuiApi.Separator();
                    }

                    var name = attribute.DisplayName ?? string.Empty;
                    var value = attribute.DisplayValue ?? string.Empty;
                    ImGuiApi.TextUnformatted($"{name}:{value}");
                }

                return;
            }

            if (item is not GameCharacterDisplayDTO characterDisplay)
            {
                return;
            }

            var characterAttributes = characterDisplay.CharacterAttributes ?? [];
            var hasCharacterAttributes = false;
            foreach (var attribute in characterAttributes)
            {
                if (!hasCharacterAttributes)
                {
                    hasCharacterAttributes = true;
                    ImGuiApi.Spacing();
                    ImGuiApi.Separator();
                }

                var name = attribute.DisplayName ?? string.Empty;
                var value = attribute.DisplayValue ?? string.Empty;
                ImGuiApi.TextUnformatted($"{name}:{value}");
            }
        }

        private static string GetSingleLineText(string value, float maxWidth)
        {
            if (string.IsNullOrEmpty(value) || ImGuiApi.CalcTextSize(value).X <= maxWidth)
            {
                return value;
            }

            const string ellipsis = "...";
            var candidate = value;
            while (candidate.Length > 0)
            {
                candidate = candidate[..^1];
                var truncated = candidate + ellipsis;
                if (ImGuiApi.CalcTextSize(truncated).X <= maxWidth)
                {
                    return truncated;
                }
            }

            return ellipsis;
        }

        private static int GetGridColumns(float availableWidth, float cardWidth, float cardSpacing)
        {
            return Math.Max(1, (int)((availableWidth + cardSpacing) / (cardWidth + cardSpacing)));
        }

        private static (int Columns, int RowCount, float StartOffsetX) GetCenteredGridLayout(float availableWidth, float availableHeight, float cardWidth, float cardSpacing, float rowHeight, int itemCount)
        {
            var columns = GetGridColumns(availableWidth, cardWidth, cardSpacing);
            var rowCount = (itemCount + columns - 1) / columns;
            if ((rowCount * rowHeight) <= availableHeight)
            {
                return (columns, rowCount, GetCenteredGridStartOffset(availableWidth, cardWidth, cardSpacing, columns));
            }

            var effectiveWidth = MathF.Max(1.0f, availableWidth - ImGuiApi.GetStyle().ScrollbarSize);
            columns = GetGridColumns(effectiveWidth, cardWidth, cardSpacing);
            rowCount = (itemCount + columns - 1) / columns;
            return (columns, rowCount, GetCenteredGridStartOffset(effectiveWidth, cardWidth, cardSpacing, columns));
        }

        private static float GetCenteredGridStartOffset(float availableWidth, float cardWidth, float cardSpacing, int columns)
        {
            var usedWidth = (columns * cardWidth) + ((columns - 1) * cardSpacing);
            return MathF.Max(0.0f, (availableWidth - usedWidth) * 0.5f);
        }

        private bool IsEditDialogBlockingInput()
        {
            return IsAnyPopupDialogOpen()
                || (!AllowDialogContentInput && (
                    ShowCharacterStatusDialog
                    || ShowCharacterSkillDialog
                    || ShowCharacterSkillSelectorDialog
                    || ShowMonsterInfoDialog))
                || EditingCurrency is not null
                || EditingInventory is not null;
        }

        private static string GetPlainText(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var builder = new System.Text.StringBuilder(value.Length);
            var insideTag = false;
            foreach (var ch in value)
            {
                if (ch == '<')
                {
                    insideTag = true;
                    continue;
                }

                if (ch == '>')
                {
                    insideTag = false;
                    continue;
                }

                if (!insideTag)
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString().Replace("&nbsp;", " ").Trim();
        }

        private void RenderTabToolbar(string tabName, SearchState searchState)
        {
            var toolbarWidth = ImGuiApi.GetContentRegionAvail().X;
            var totalWidth = MathF.Max(320.0f, toolbarWidth - 8.0f);
            var inputWidth = MathF.Max(220.0f, totalWidth - (ToolbarIconButtonSize * 2.0f) - 16.0f);
            var startX = MathF.Max(0.0f, (toolbarWidth - totalWidth) * 0.5f);

            ImGuiApi.SetCursorPosX(ImGuiApi.GetCursorPosX() + startX);
            ImGuiApi.SetCursorPosY(ImGuiApi.GetCursorPosY() + 2.0f);
            PushToolbarSearchInputStyle();
            RenderSearchInput($"##{tabName}_SearchInput", searchState, inputWidth);
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
            if (ImGuiApi.Button($"##Search_{tabName}", new Vector2(ToolbarIconButtonSize, ToolbarIconButtonSize)))
            {
                searchState.AppliedText = searchState.InputText;
            }

            DrawSearchButtonIcon();
            PopIconButtonStyle();

            ImGuiApi.SameLine();
            PushIconButtonStyle(EditorButtonBg, EditorButtonBgHovered, EditorButtonBgActive);
            if (ImGuiApi.Button($"##Reload_{tabName}", new Vector2(ToolbarIconButtonSize, ToolbarIconButtonSize)) && GameSessionInfo is not null && !_sessionCollectionsRequest.IsRunning)
            {
                searchState.InputText = string.Empty;
                searchState.AppliedText = string.Empty;
                ReloadingTab = SelectedSessionTab;
                _sessionCollectionsRequest.TryStart(() => ReloadCurrentTabAsync(SelectedSessionTab));
            }

            DrawReloadButtonIcon();
            PopIconButtonStyle();
        }

        private void RenderDisplayCards<TDisplay>(string tabName, TDisplay[] items) where TDisplay : GameObjectDisplayDTO
        {
            var childSize = ImGuiApi.GetContentRegionAvail();
            var listWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild($"##{tabName}Cards", childSize, ImGuiChildFlags.None, listWindowFlags))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var clipper = new ImGuiListClipper();
            clipper.Begin(items.Length, DisplayCardHeight + DisplayCardSpacing);
            while (clipper.Step())
            {
                for (var i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
                {
                    RenderDisplayCard(tabName, items[i], i, childSize.X);
                    var separatorMin = ImGuiApi.GetCursorScreenPos() + new Vector2(12.0f, 2.0f);
                    var separatorMax = separatorMin + new Vector2(MathF.Max(1.0f, childSize.X - 24.0f), 0.0f);
                    ImGuiApi.GetWindowDrawList().AddLine(
                        separatorMin,
                        separatorMax,
                        ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.06f)),
                        1.0f);
                    ImGuiApi.Dummy(new Vector2(0, DisplayCardSpacing));
                }
            }

            clipper.End();

            ImGuiApi.PopStyleColor();
            ImGuiApi.EndChild();
        }

        private void RenderDisplayCard<TDisplay>(string tabName, TDisplay item, int index, float availableWidth) where TDisplay : GameObjectDisplayDTO
        {
            var objectDisplay = item;//as GameObjectDisplayDTO;
            var switchDisplay = item as GameSwitchDisplayDTO;
            var characterDisplay = item as GameCharacterDisplayDTO;
            var allowCardInteraction = !IsEditDialogBlockingInput();
            var cardWidth = MathF.Max(1.0f, availableWidth);
            var cardHeight = switchDisplay is null
                ? GridCardHeight
                : MathF.Max(GridCardHeight, GetSwitchDisplayEditorCardHeight(switchDisplay));
            if (!BeginDisplayCard($"##{tabName}_Card_{item.ObjectId}_{index}", new Vector2(cardWidth, cardHeight)))
            {
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var windowPos = ImGuiApi.GetWindowPos();

            var actionButtonPosition = GetCardActionButtonPosition(cardWidth, cardHeight);
            var skillButtonPosition = GetSecondaryCardActionButtonPosition(actionButtonPosition);
            var textWidth = GetStandardCardTextWidth(cardWidth);
            var (cardCategory, visibleCardCategory) = GetCardCategoryTexts(objectDisplay.DisplayCategory, switchDisplay is not null, tabName);

            RenderDisplayCardHeader(
                windowPos,
                objectDisplay.DisplayCategory,
                item.ObjectId,
                objectDisplay.DisplayImage,
                visibleCardCategory,
                item.DisplayName ?? string.Empty,
                textWidth,
                switchDisplay is not null);

            if (switchDisplay is not null)
            {
                RenderSwitchEditorHost(switchDisplay, item.ObjectId, index, cardWidth, cardHeight);

                if (IsCardInteractionHovered(allowCardInteraction, windowPos, new Vector2(cardWidth, cardHeight), true))
                {
                    RenderDisplayCardHoverFeedback(drawList, windowPos, new Vector2(cardWidth, cardHeight), cardCategory, item.DisplayName ?? string.Empty, item.DisplayDesc, item);
                }

                EndDisplayCard();
                return;
            }

            if (characterDisplay is not null)
            {
                if (RenderSkillIconButton(
                    skillButtonPosition,
                    $"##Skill_{tabName}_{item.ObjectId}_{index}"))
                {
                    HandleSkillButtonClick(characterDisplay);
                }
            }

            if (RenderActionIconButton(
                actionButtonPosition,
                $"##Action_{tabName}_{item.ObjectId}_{index}",
                GridCardActionButtonSize))
            {
                HandleEditButtonClick(item);
            }

            if (IsCardInteractionHovered(allowCardInteraction, windowPos, new Vector2(cardWidth, cardHeight), true))
            {
                RenderDisplayCardHoverFeedback(drawList, windowPos, new Vector2(cardWidth, cardHeight), cardCategory, item.DisplayName ?? string.Empty, item.DisplayDesc, item);
            }

            EndDisplayCard();
        }

        private static bool BeginDisplayCard(string id, Vector2 cardSize)
        {
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            if (ImGuiApi.BeginChild(id, cardSize, ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                return true;
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
            return false;
        }

        private static void EndDisplayCard()
        {
            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
        }

        private static bool IsCardInteractionHovered(bool allowCardInteraction, Vector2 cardPos, Vector2 cardSize, bool includeLastItemHover = false)
        {
            return allowCardInteraction
                && ((includeLastItemHover && ImGuiApi.IsItemHovered())
                    || ImGuiApi.IsMouseHoveringRect(cardPos, cardPos + cardSize));
        }

        private bool RenderActionIconButton(Vector2 position, string id, float size)
        {
            return RenderIconActionButton(position, id, size, ActionIconButtonStyle, DrawActionButtonIcon);
        }

        private bool RenderSkillIconButton(Vector2 position, string id)
        {
            return RenderIconActionButton(position, id, GridCardActionButtonSize, SkillIconButtonStyle, DrawSkillButtonIcon);
        }

        private bool RenderIconActionButton(Vector2 position, string id, float size, (Vector4 ButtonColor, Vector4 HoverColor, Vector4 ActiveColor) style, Action drawIcon)
        {
            PushIconButtonStyle(style.ButtonColor, style.HoverColor, style.ActiveColor);
            ImGuiApi.SetCursorPos(position);
            var clicked = ImGuiApi.Button(id, new Vector2(size, size));
            drawIcon();
            PopIconButtonStyle();
            return clicked;
        }

        private static (Vector4 ButtonColor, Vector4 HoverColor, Vector4 ActiveColor) ActionIconButtonStyle =>
        (
            new Vector4(0.92f, 0.40f, 0.02f, 0.18f),
            new Vector4(0.92f, 0.40f, 0.02f, 0.35f),
            new Vector4(0.92f, 0.40f, 0.02f, 0.50f)
        );

        private static (Vector4 ButtonColor, Vector4 HoverColor, Vector4 ActiveColor) SkillIconButtonStyle =>
        (
            new Vector4(0.16f, 0.34f, 0.78f, 0.20f),
            new Vector4(0.20f, 0.42f, 0.92f, 0.36f),
            new Vector4(0.14f, 0.30f, 0.78f, 0.52f)
        );

        private void RenderSwitchEditorHost(GameSwitchDisplayDTO switchDisplay, string objectId, int index, float cardWidth, float cardHeight)
        {
            var switchEditorX = MathF.Max(0.0f, (cardWidth - SwitchEditorHostWidth) * 0.5f);
            var switchEditorY = cardHeight - GridCardActionButtonSize - GridCardControlMargin;
            var switchEditorHeight = MathF.Max(GridCardActionButtonSize, cardHeight - switchEditorY - GridCardControlMargin);
            ImGuiApi.SetCursorPos(new Vector2(switchEditorX, switchEditorY));
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (ImGuiApi.BeginChild($"##SwitchEditorHost_{objectId}_{index}", new Vector2(SwitchEditorHostWidth, switchEditorHeight), ImGuiChildFlags.None, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                RenderSwitchDisplayEditor(switchDisplay, index);
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
        }

        private void RenderDisplayCardHeader(Vector2 cardPos, string? category, string objectId, string? image, string visibleCategory, string title, float textWidth, bool preserveCategorySpace)
        {
            var thumbnailSize = new Vector2(GridCardThumbnailSize, GridCardThumbnailSize);
            var thumbnailMin = cardPos + new Vector2(GridCardContentInset, GridCardContentInset);
            RenderCardThumbnail(thumbnailMin, thumbnailSize, category, objectId, image);
            RenderInventoryCardTextBlock(visibleCategory, title, cardPos, GridCardTextStartX, textWidth, preserveCategorySpace);
        }

        private static Vector2 GetCardActionButtonPosition(float cardWidth, float cardHeight)
        {
            return new Vector2(cardWidth - GridCardActionButtonSize - GridCardControlMargin, cardHeight - GridCardActionButtonSize - GridCardControlMargin);
        }

        private static Vector2 GetSecondaryCardActionButtonPosition(Vector2 primaryButtonPosition)
        {
            return primaryButtonPosition - new Vector2(GridCardActionButtonSize + GridCardActionButtonSpacing, 0.0f);
        }

        private static float GetStandardCardTextWidth(float cardWidth)
        {
            var textRightBoundary = cardWidth - 16.0f;
            return MathF.Max(1.0f, textRightBoundary - GridCardTextStartX - 12.0f);
        }

        private (string CardCategory, string VisibleCardCategory) GetCardCategoryTexts(string? displayCategory, bool isSwitchDisplay, string emptyCategoryText)
        {
            if (isSwitchDisplay)
            {
                return (GetUiText("Text.Misc"), string.Empty);
            }

            var cardCategory = string.IsNullOrWhiteSpace(displayCategory) ? emptyCategoryText : displayCategory;
            return (cardCategory, cardCategory);
        }

        private static void RenderSearchInput(string label, SearchState searchState, float width)
        {
            ImGuiApi.SetNextItemWidth(width);
            ImGuiApi.InputText(label, ref searchState.InputText, (nuint)SearchInputBufferSize);
        }

        private void RenderDisplayCardTooltip(string category, string title, string? description, GameDisplayDTO item)
        {
            var tooltipDesc = GetPlainText(description);
            if (string.IsNullOrWhiteSpace(tooltipDesc))
            {
                tooltipDesc = GetUiText("Dialog.Text.Empty");
            }

            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.PopupBg, new Vector4(0.09f, 0.10f, 0.12f, 0.98f));
            ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(0.20f, 0.62f, 0.26f, 0.92f));
            BeginStandardTooltip();
            ImGuiApi.PushTextWrapPos(ImGuiApi.GetFontSize() * 24.0f);
            ImGuiApi.TextUnformatted(category);
            ImGuiApi.Separator();
            ImGuiApi.TextUnformatted(title);
            ImGuiApi.Spacing();
            ImGuiApi.TextUnformatted(tooltipDesc);
            RenderTooltipAttributes(item);
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.EndTooltip();
            ImGuiApi.PopStyleColor(2);
            ImGuiApi.PopStyleVar();
        }

        private void RenderDisplayCardHoverFeedback(ImDrawListPtr drawList, Vector2 cardPos, Vector2 cardSize, string category, string title, string? description, GameDisplayDTO item)
        {
            RenderDisplayCardTooltip(category, title, description, item);
            DrawHoveredCardBorder(drawList, cardPos, cardSize);
        }

        private static void DrawHoveredCardBorder(ImDrawListPtr drawList, Vector2 cardPos, Vector2 cardSize)
        {
            drawList.AddRect(
                cardPos + new Vector2(1.0f, 1.0f),
                cardPos + cardSize - new Vector2(1.0f, 1.0f),
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                DisplayCardRounding,
                ImDrawFlags.None,
                1.6f);
        }
}
}
