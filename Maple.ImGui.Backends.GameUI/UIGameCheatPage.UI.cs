using Hexa.NET.ImGui;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责页面主窗口骨架与通用 UI 入口行为。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private const string GameSessionHelpPopupName = "Game Session Help";
        private const string CurrencyEditPopupName = "Currency Edit";
        private const string InventoryEditPopupName = "Inventory Edit";
        private const float SessionWindowRounding = 15.0f;
        private const float SessionTitleBarHeight = 52.0f;
        private const float SessionWindowViewportRatio = 0.9f;
        private const float SessionContentLeftMargin = 16.0f;
        private const float SessionContentTopMargin = 12.0f;
        private const float SessionContentRightMargin = 24.0f;
        private const float SessionContentBottomMargin = 24.0f;
        private const float DisplayCardHeight = 76.0f;
        private const float DisplayCardSpacing = 8.0f;
        private const float DisplayCardThumbnailWidth = 44.0f;
        private const float DisplayCardThumbnailHeight = 48.0f;
        private const float DisplayCardActionButtonOffset = 20.0f;
        private const float DisplayCardRounding = 14.0f;
        private const float ToolbarIconButtonSize = 32.0f;
        private const float TitleTabHeight = 28.0f;
        private const float TitleBarIconButtonSize = 28.0f;
        private const float SessionSideTabColumnWidth = 120.0f;
        private const float SessionSideTabColumnSpacing = 12.0f;
        private const int SearchInputBufferSize = 1024;
        private const float EditDialogWidth = 420.0f;
        private const float EditDialogThumbnailWidth = 58.0f;
        private const float EditDialogThumbnailHeight = 72.0f;
        private const float UiScaleMin = 0.75f;
        private const float UiScaleMax = 2.00f;
        private static readonly Vector4 LauncherButtonColor = new(0.00f, 0.76f, 0.46f, 0.50f);
        private static readonly Vector4 LauncherButtonHoveredColor = new(0.00f, 0.76f, 0.46f, 0.68f);
        private static readonly Vector4 LauncherButtonActiveColor = new(0.00f, 0.76f, 0.46f, 0.82f);

        private void UpdateMouseCursorVisibility()
        {
            ImGuiApi.GetIO().MouseDrawCursor = LauncherVisible || ShowSessionWindow;
        }

        private void ApplyUiScale()
        {
            if (MathF.Abs(UiScale - AppliedUiScale) < 0.001f)
            {
                return;
            }

            var style = ImGuiApi.GetStyle();
            style.FontScaleMain = UiScale;
            AppliedUiScale = UiScale;
        }

        private void RenderSessionWindow()
        {
            if (!ShowSessionWindow)
            {
                return;
            }

            var mainViewport = ImGuiApi.GetMainViewport();
            var sessionWindowSize = GetSessionWindowSize(mainViewport.WorkSize);
            var sessionWindowPos = GetSessionWindowPosition(mainViewport.WorkPos, mainViewport.WorkSize, sessionWindowSize);
            ImGuiApi.SetNextWindowPos(sessionWindowPos, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(sessionWindowSize, ImGuiCond.Always);
            ImGuiApi.SetNextWindowViewport(mainViewport.ID);
            ImGuiApi.SetNextWindowSizeConstraints(new Vector2(320.0f, 240.0f), new Vector2(float.MaxValue, float.MaxValue));

            var windowBg = new Vector4(0.07f, 0.08f, 0.10f, 0.98f);
            var borderColor = new Vector4(1f, 1f, 1f, 0.08f);
            var childBg = new Vector4(0f, 0f, 0f, 0f);
            var frameBg = new Vector4(0.18f, 0.19f, 0.22f, 1.0f);
            var frameBgHovered = new Vector4(0.22f, 0.23f, 0.27f, 1.0f);
            var frameBgActive = new Vector4(0.24f, 0.25f, 0.29f, 1.0f);
            var tabColor = new Vector4(0.14f, 0.15f, 0.18f, 1.0f);
            var tabHoverColor = new Vector4(0.22f, 0.23f, 0.27f, 1.0f);
            var tabSelectedColor = new Vector4(0.20f, 0.22f, 0.26f, 1.0f);
            var buttonColor = new Vector4(0.20f, 0.21f, 0.25f, 1.0f);
            var buttonHoverColor = new Vector4(0.27f, 0.29f, 0.34f, 1.0f);
            var buttonActiveColor = new Vector4(0.18f, 0.20f, 0.24f, 1.0f);

            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, SessionWindowRounding);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 1.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.ChildRounding, 16.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 12.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, windowBg);
            ImGuiApi.PushStyleColor(ImGuiCol.Border, borderColor);
            ImGuiApi.PushStyleColor(ImGuiCol.ChildBg, childBg);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBg, frameBg);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBgHovered, frameBgHovered);
            ImGuiApi.PushStyleColor(ImGuiCol.FrameBgActive, frameBgActive);
            ImGuiApi.PushStyleColor(ImGuiCol.Tab, tabColor);
            ImGuiApi.PushStyleColor(ImGuiCol.TabHovered, tabHoverColor);
            ImGuiApi.PushStyleColor(ImGuiCol.TabSelected, tabSelectedColor);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, buttonColor);
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, buttonHoverColor);
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, buttonActiveColor);

            var isOpen = true;
            var windowFlags = ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoTitleBar;

            if (PendingOpenCurrencyEditPopup
                || PendingOpenInventoryEditPopup
                || ShowCharacterStatusDialog
                || ShowCharacterSkillDialog
                || ShowCharacterSkillSelectorDialog
                || EditingCurrency is not null
                || EditingInventory is not null)
            {
                windowFlags |= ImGuiWindowFlags.NoInputs;
            }

            const string sessionWindowTitle = "###GameSessionWindow";

            if (!ImGuiApi.Begin(sessionWindowTitle, ref isOpen, windowFlags))
            {
                ImGuiApi.End();
                ImGuiApi.PopStyleColor(12);
                ImGuiApi.PopStyleVar(4);
                if (!isOpen)
                {
                    ShowSessionWindow = false;
                    LauncherVisible = true;
                }

                return;
            }

            MainWindowPosition = ImGuiApi.GetWindowPos();
            MainWindowSize = ImGuiApi.GetWindowSize();

            RenderSessionTitleBar(ref isOpen);
            var compactTitleBarHeight = SessionTitleBarHeight - 10.0f;
            var contentPosition = new Vector2(SessionContentLeftMargin + SessionSideTabColumnWidth + SessionSideTabColumnSpacing, compactTitleBarHeight + SessionContentTopMargin);
            var contentSize = new Vector2(
                MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin - SessionSideTabColumnWidth - SessionSideTabColumnSpacing),
                MathF.Max(1.0f, MainWindowSize.Y - compactTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
            ImGuiApi.SetCursorPos(contentPosition);

            if (_sessionCollectionsRequest.IsRunning)
            {
                ImGuiApi.TextUnformatted("Loading display lists...");
            }

            if (PendingOpenGameSessionHelpPopup)
            {
                ImGuiApi.OpenPopup(GameSessionHelpPopupName);
                PendingOpenGameSessionHelpPopup = false;
            }

            if (PendingOpenCurrencyEditPopup)
            {
                ImGuiApi.OpenPopup(CurrencyEditPopupName);
                PendingOpenCurrencyEditPopup = false;
            }

            if (PendingOpenInventoryEditPopup)
            {
                ImGuiApi.OpenPopup(InventoryEditPopupName);
                PendingOpenInventoryEditPopup = false;
            }

            if (ImGuiApi.BeginChild("##SessionWindowContent", contentSize, ImGuiChildFlags.AlwaysUseWindowPadding))
            {
                if (_sessionCollectionsRequest.IsRunning)
                {
                    ImGuiApi.Separator();
                }

                RenderSelectedTabContent();
            }

            ImGuiApi.EndChild();
            RenderGameSessionInfoHelpDialog();
            RenderCurrencyEditDialog();
            RenderInventoryEditDialog();

            ImGuiApi.End();
            ImGuiApi.PopStyleColor(12);
            ImGuiApi.PopStyleVar(4);

            if (!isOpen)
            {
                ShowSessionWindow = false;
                LauncherVisible = true;
            }
        }

        private void RenderSelectedTabContent()
        {
            switch (SelectedSessionTab)
            {
                case SessionTab.Currency:
                    RenderDisplayTabContent("Currency", SessionCollections.Currencies, CurrencySearch);
                    break;
                case SessionTab.Inventory:
                    RenderDisplayTabContent("Inventory", SessionCollections.Inventories, InventorySearch);
                    break;
                case SessionTab.Character:
                    RenderDisplayTabContent("Character", SessionCollections.Characters, CharacterSearch);
                    break;
                case SessionTab.Monster:
                    RenderDisplayTabContent("Monster", SessionCollections.Monsters, MonsterSearch);
                    break;
                case SessionTab.Skill:
                    RenderDisplayTabContent("Skill", SessionCollections.Skills, SkillSearch);
                    break;
                case SessionTab.Switch:
                    RenderDisplayTabContent("Misc", SessionCollections.Switches, SwitchSearch);
                    break;
            }
        }

        private static Vector2 GetSessionWindowSize(Vector2 availableSize)
        {
            return new Vector2(
                MathF.Max(1.0f, availableSize.X * SessionWindowViewportRatio),
                MathF.Max(1.0f, availableSize.Y * SessionWindowViewportRatio));
        }

        private static Vector2 GetSessionWindowPosition(Vector2 availablePos, Vector2 availableSize, Vector2 sessionWindowSize)
        {
            return new Vector2(
                availablePos.X + MathF.Max(0.0f, (availableSize.X - sessionWindowSize.X) * 0.5f),
                availablePos.Y + MathF.Max(0.0f, (availableSize.Y - sessionWindowSize.Y) * 0.5f));
        }
    }
}
