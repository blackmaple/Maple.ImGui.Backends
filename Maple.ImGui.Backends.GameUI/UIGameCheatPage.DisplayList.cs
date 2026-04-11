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
    public partial class UIGameCheatPage
    {
        private void RenderDisplayTabContent<TDisplay>(string tabName, TDisplay[] items, SearchState searchState) where TDisplay : GameDisplayDTO
        {
            RenderTabToolbar(tabName, searchState);
            var filteredItems = SortDisplays(FilterDisplays(items, searchState.AppliedText));
            if (typeof(TDisplay) == typeof(GameInventoryDisplayDTO))
            {
                RenderInventoryDisplayCards((GameInventoryDisplayDTO[])(object)filteredItems);
                return;
            }

            RenderDisplayGridCards(tabName, filteredItems);
        }

        private void RenderDisplayGridCards<TDisplay>(string tabName, TDisplay[] items) where TDisplay : GameDisplayDTO
        {
            const float cardSpacing = 12.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild($"##{tabName}GridCards", childSize, ImGuiChildFlags.None, gridWindowFlags))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var cardWidth = GetInventoryCardWidth(childSize.X);
            var gridCardHeight = typeof(TDisplay) == typeof(GameSwitchDisplayDTO)
                ? MathF.Max(112.0f, items.Cast<GameSwitchDisplayDTO>().Select(GetSwitchDisplayEditorCardHeight).DefaultIfEmpty(112.0f).Max())
                : 112.0f;
            var cardHeight = gridCardHeight;
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

                        RenderDisplayCard(tabName, items[itemIndex], itemIndex, cardWidth);
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

        private void RenderMonsterDisplayCards(GameMonsterDisplayDTO[] items)
        {
            const float cardSpacing = 12.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##MonsterGridCards", childSize, ImGuiChildFlags.None, gridWindowFlags))
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

                        RenderInventoryDisplayCard(items[itemIndex], itemIndex, new Vector2(cardWidth, cardHeight));
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

        private void RenderInventoryDisplayCards(GameInventoryDisplayDTO[] items)
        {
            const float inventoryCardSpacing = 12.0f;

            var childSize = ImGuiApi.GetContentRegionAvail();
            var gridWindowFlags = IsEditDialogBlockingInput() ? ImGuiWindowFlags.NoInputs : ImGuiWindowFlags.None;
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
            if (!ImGuiApi.BeginChild("##InventoryGridCards", childSize, ImGuiChildFlags.None, gridWindowFlags))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                return;
            }

            var inventoryCardWidth = GetInventoryCardWidth(childSize.X);
            const float inventoryCardHeight = 112.0f;
            var rowHeight = inventoryCardHeight + inventoryCardSpacing;
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

            var (columns, rowCount, startOffsetX) = GetCenteredGridLayout(childSize.X, childSize.Y, inventoryCardWidth, inventoryCardSpacing, rowHeight, items.Length);
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
                            ImGuiApi.SameLine(0.0f, inventoryCardSpacing);
                        }

                        RenderInventoryDisplayCard(items[itemIndex], itemIndex, new Vector2(inventoryCardWidth, inventoryCardHeight));
                    }

                    if (row < rowCount - 1)
                    {
                        ImGuiApi.Dummy(new Vector2(0.0f, inventoryCardSpacing));
                    }
                }
            }

            clipper.End();

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
        }

        private void RenderInventoryDisplayCard(GameDisplayDTO item, int index, Vector2 cardSize)
        {
            var allowCardInteraction = !IsEditDialogBlockingInput();
            const float cardControlMargin = 6.0f;
       //     const float imageColumnWidth = 62.0f;
            const float textStartX = 76.0f;
            var switchDisplay = item as GameSwitchDisplayDTO;
            var objectDisplay = item as GameObjectDisplayDTO;
            var monsterDisplay = item as GameMonsterDisplayDTO;
            var textColumnWidth = switchDisplay is null
                ? MathF.Max(1.0f, cardSize.X - textStartX - 50.0f)
                : MathF.Max(1.0f, cardSize.X - textStartX - 12.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            if (!ImGuiApi.BeginChild($"##InventoryGridCard_{item.ObjectId}_{index}", cardSize, ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                ImGuiApi.PopStyleVar();
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var cardPos = ImGuiApi.GetWindowPos();
            var isCardHovered = allowCardInteraction && ImGuiApi.IsMouseHoveringRect(cardPos, cardPos + cardSize);
            var cardCategory = switchDisplay is null
                ? (string.IsNullOrWhiteSpace(objectDisplay?.DisplayCategory) ? GetUiText("Text.Inventory") : objectDisplay.DisplayCategory)
                : GetUiText("Text.Misc");
            var visibleCardCategory = switchDisplay is null ? cardCategory : string.Empty;
            var thumbnailSize = new Vector2(48.0f, 48.0f);
            var thumbnailMin = cardPos + new Vector2(14.0f, 14.0f);
            RenderCardThumbnail(thumbnailMin, thumbnailSize, switchDisplay is null ? objectDisplay?.DisplayCategory : cardCategory, item.ObjectId);

            if (monsterDisplay is not null)
            {
                var addButtonPosition = new Vector2(cardSize.X - CardActionButtonSize - cardControlMargin, cardSize.Y - CardActionButtonSize - cardControlMargin);
                var infoButtonPosition = addButtonPosition - new Vector2(CardActionButtonSize + CardActionButtonSpacing, 0.0f);

                PushIconButtonStyle(
                    new Vector4(0.92f, 0.40f, 0.02f, 0.18f),
                    new Vector4(0.92f, 0.40f, 0.02f, 0.35f),
                    new Vector4(0.92f, 0.40f, 0.02f, 0.50f));
                ImGuiApi.SetCursorPos(infoButtonPosition);
                var infoClicked = ImGuiApi.Button($"##MonsterInfo_{item.ObjectId}_{index}", new Vector2(CardActionButtonSize, CardActionButtonSize));
                DrawActionButtonIcon();
                PopIconButtonStyle();
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
                PushIconButtonStyle(
                    new Vector4(0.92f, 0.40f, 0.02f, 0.18f),
                    new Vector4(0.92f, 0.40f, 0.02f, 0.35f),
                    new Vector4(0.92f, 0.40f, 0.02f, 0.50f));
                ImGuiApi.SetCursorPos(new Vector2(cardSize.X - 30.0f - cardControlMargin, cardSize.Y - 30.0f - cardControlMargin));
                if (ImGuiApi.Button($"##InventoryGridEdit_{item.ObjectId}_{index}", new Vector2(30.0f, 30.0f)))
                {
                    HandleEditButtonClick(item);
                }

                DrawActionButtonIcon();
                PopIconButtonStyle();
            }
            else
            {
                const float switchEditorWidth = 154.0f;
                const float switchEditorBottomMargin = cardControlMargin;
                var switchEditorX = MathF.Max(0.0f, (cardSize.X - switchEditorWidth) * 0.5f);
                var switchEditorY = cardSize.Y - 30.0f - switchEditorBottomMargin;
                var switchEditorHeight = MathF.Max(30.0f, cardSize.Y - switchEditorY - switchEditorBottomMargin);
                ImGuiApi.SetCursorPos(new Vector2(switchEditorX, switchEditorY));
                ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
                if (ImGuiApi.BeginChild($"##SwitchEditorHost_{item.ObjectId}_{index}", new Vector2(switchEditorWidth, switchEditorHeight), ImGuiChildFlags.None, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
                {
                    RenderSwitchDisplayEditor(switchDisplay, index);
                }

                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
            }

            RenderInventoryCardTextBlock(visibleCardCategory, item.DisplayName ?? string.Empty, cardPos, textStartX, textColumnWidth, switchDisplay is not null);

            if (allowCardInteraction && isCardHovered)
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
                RenderTooltipAttributes(item);
                ImGuiApi.PopTextWrapPos();
                ImGuiApi.EndTooltip();
                ImGuiApi.PopStyleColor(2);
                ImGuiApi.PopStyleVar();
            }

            if (isCardHovered)
            {
                drawList.AddRect(
                    cardPos + new Vector2(1.0f, 1.0f),
                    cardPos + cardSize - new Vector2(1.0f, 1.0f),
                    ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.24f, 0.72f, 0.38f, 0.95f)),
                    DisplayCardRounding,
                    ImDrawFlags.None,
                    1.6f);
            }

            ImGuiApi.EndChild();
            ImGuiApi.PopStyleColor();
            ImGuiApi.PopStyleVar();
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

        private void RenderDisplayCards<TDisplay>(string tabName, TDisplay[] items) where TDisplay : GameDisplayDTO
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

        private void RenderDisplayCard<TDisplay>(string tabName, TDisplay item, int index, float availableWidth) where TDisplay : GameDisplayDTO
        {
            var objectDisplay = item as GameObjectDisplayDTO;
            var switchDisplay = item as GameSwitchDisplayDTO;
            var characterDisplay = item as GameCharacterDisplayDTO;
            var allowCardInteraction = !IsEditDialogBlockingInput();
            const float cardControlMargin = 6.0f;
            var cardWidth = MathF.Max(1.0f, availableWidth);
            var cardHeight = switchDisplay is null
                ? 112.0f
                : MathF.Max(112.0f, GetSwitchDisplayEditorCardHeight(switchDisplay));
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, DisplayCardRounding);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.11f, 0.12f, 0.15f, 0.72f));
            if (!ImGuiApi.BeginChild($"##{tabName}_Card_{item.ObjectId}_{index}", new Vector2(cardWidth, cardHeight), ImGuiChildFlags.Borders, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();
                ImGuiApi.PopStyleVar();
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var windowPos = ImGuiApi.GetWindowPos();
            var iconMin = windowPos + new Vector2(14.0f, 14.0f);
            var compactThumbnailSize = new Vector2(48.0f, 48.0f);

            const float actionButtonSize = 30.0f;
            const float actionButtonSpacing = 8.0f;
            var actionButtonPosition = new Vector2(cardWidth - actionButtonSize - cardControlMargin, cardHeight - actionButtonSize - cardControlMargin);
            var skillButtonPosition = actionButtonPosition - new Vector2(actionButtonSize + actionButtonSpacing, 0.0f);
            const float textStartX = 76.0f;
            var textRightBoundary = cardWidth - 16.0f;
            var textWidth = MathF.Max(1.0f, textRightBoundary - textStartX - 12.0f);
            var cardCategory = switchDisplay is null
                ? (string.IsNullOrWhiteSpace(objectDisplay?.DisplayCategory) ? tabName : objectDisplay.DisplayCategory)
                : GetUiText("Text.Misc");
            var visibleCardCategory = switchDisplay is null ? cardCategory : string.Empty;

            RenderCardThumbnail(iconMin, compactThumbnailSize, switchDisplay is null ? objectDisplay?.DisplayCategory : GetUiText("Text.Misc"), item.ObjectId);

            RenderInventoryCardTextBlock(visibleCardCategory, item.DisplayName ?? string.Empty, windowPos, textStartX, textWidth, switchDisplay is not null);

            if (switchDisplay is not null)
            {
                const float switchEditorWidth = 154.0f;
                const float switchEditorBottomMargin = cardControlMargin;
                var switchEditorX = MathF.Max(0.0f, (cardWidth - switchEditorWidth) * 0.5f);
                var switchEditorY = cardHeight - 30.0f - switchEditorBottomMargin;
                var switchEditorHeight = MathF.Max(30.0f, cardHeight - switchEditorY - switchEditorBottomMargin);
                ImGuiApi.SetCursorPos(new Vector2(switchEditorX, switchEditorY));
                ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0f, 0f, 0f, 0f));
                if (ImGuiApi.BeginChild($"##SwitchGridEditorHost_{item.ObjectId}_{index}", new Vector2(switchEditorWidth, switchEditorHeight), ImGuiChildFlags.None, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
                {
                    RenderSwitchDisplayEditor(switchDisplay, index);
                }

                ImGuiApi.EndChild();
                ImGuiApi.PopStyleColor();

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
                    RenderTooltipAttributes(item);
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
                return;
            }

            if (characterDisplay is not null)
            {
                PushIconButtonStyle(
                    new Vector4(0.16f, 0.34f, 0.78f, 0.20f),
                    new Vector4(0.20f, 0.42f, 0.92f, 0.36f),
                    new Vector4(0.14f, 0.30f, 0.78f, 0.52f));
                ImGuiApi.SetCursorPos(skillButtonPosition);
                if (ImGuiApi.Button($"##Skill_{tabName}_{item.ObjectId}_{index}", new Vector2(actionButtonSize, actionButtonSize)))
                {
                    HandleSkillButtonClick(characterDisplay);
                }

                DrawSkillButtonIcon();
                PopIconButtonStyle();
            }

            PushIconButtonStyle(
                new Vector4(0.92f, 0.40f, 0.02f, 0.18f),
                new Vector4(0.92f, 0.40f, 0.02f, 0.35f),
                new Vector4(0.92f, 0.40f, 0.02f, 0.50f));
            ImGuiApi.SetCursorPos(actionButtonPosition);
            if (ImGuiApi.Button($"##Action_{tabName}_{item.ObjectId}_{index}", new Vector2(actionButtonSize, actionButtonSize)))
            {
                HandleEditButtonClick(item);
            }

            DrawActionButtonIcon();
            PopIconButtonStyle();

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
                RenderTooltipAttributes(item);
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

        private static void RenderSearchInput(string label, SearchState searchState, float width)
        {
            ImGuiApi.SetNextItemWidth(width);
            ImGuiApi.InputText(label, ref searchState.InputText, (nuint)SearchInputBufferSize);
        }
    }
}
