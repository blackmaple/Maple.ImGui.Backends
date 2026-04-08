using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 游戏作弊页面的核心宿主，维护页面状态并驱动各个 UI/业务分区。
    /// </summary>
    public partial class UIGameCheatPage(ILogger<UIGameCheatPage> logger, IGameCheatService service) : IImGuiRender
    {
        public ILogger Logger { get; } = logger;
        IGameCheatService Service { get; } = service;
        List<UiToast> Toasts { get; } = [];

        private readonly ImGuiAsyncRequest<AsyncFetchResult<GameSessionInfoDTO?>> _gameSessionInfoRequest = new();
        private readonly ImGuiAsyncRequest<SessionCollectionsData> _sessionCollectionsRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<CurrencyEditState>> _currencyEditRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<InventoryEditState>> _inventoryEditRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<CharacterStatusDialogState>> _characterStatusRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<CharacterStatusDialogState>> _characterStatusUpdateRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<SwitchDisplayUpdateState>> _switchDisplayUpdateRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<CharacterSkillDialogState>> _characterSkillRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<CharacterSkillDialogState>> _characterSkillUpdateRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<string>> _currencyUpdateRequest = new();
        private readonly ImGuiAsyncRequest<AsyncFetchResult<string>> _inventoryUpdateRequest = new();

        bool LauncherVisible { get; set; } = true;
        bool ShowSessionWindow { get; set; }
        SessionTab SelectedSessionTab { get; set; } = SessionTab.Currency;
        SessionTab? ReloadingTab { get; set; }
        GameSessionInfoDTO? GameSessionInfo { get; set; }
        SessionCollectionsData SessionCollections { get; set; } = SessionCollectionsData.Empty;
        int NextToastId { get; set; }
        Vector2 MainWindowPosition { get; set; }
        Vector2 MainWindowSize { get; set; }
        int LoadingFrame { get; set; }
        Vector2 LauncherPosition { get; set; } = new(30, 30);
        bool IsDraggingLauncher { get; set; }
        Vector2 LauncherDragOffset { get; set; }
        bool PendingOpenGameSessionHelpPopup { get; set; }
        float UiScale { get; set; } = 1.0f;
        float AppliedUiScale { get; set; } = 1.0f;
        bool PendingOpenCurrencyEditPopup { get; set; }
        bool PendingOpenInventoryEditPopup { get; set; }
        bool PendingCloseCurrencyEditPopup { get; set; }
        bool PendingCloseInventoryEditPopup { get; set; }
        bool PendingOpenCharacterSkillActionPopup { get; set; }
        bool ShowCharacterStatusDialog { get; set; }
        bool ShowCharacterSkillDialog { get; set; }
        bool ShowCharacterSkillSelectorDialog { get; set; }
        CurrencyEditState? EditingCurrency { get; set; }
        InventoryEditState? EditingInventory { get; set; }
        CharacterStatusDialogState? ViewingCharacterStatus { get; set; }
        CharacterSkillDialogState? ViewingCharacterSkill { get; set; }
        CharacterSkillActionConfirmState? PendingCharacterSkillAction { get; set; }
        SwitchDisplayOriginalValueState? PendingCharacterStatusOriginalValue { get; set; }
        SwitchDisplayOriginalValueState? PendingSwitchDisplayOriginalValue { get; set; }
        bool AllowDialogContentInput { get; set; }
        string CharacterSkillSelectionCategory { get; set; } = string.Empty;
        string? SelectedInventoryCardId { get; set; }
        int LastInventoryGridColumns { get; set; }
        float PendingInventoryGridScrollY { get; set; } = -1.0f;
        string CurrencyEditValue { get; set; } = string.Empty;
        string InventoryEditValue { get; set; } = string.Empty;

        SearchState CurrencySearch { get; } = new();
        SearchState InventorySearch { get; } = new();
        SearchState CharacterSearch { get; } = new();
        SearchState CharacterSkillSelectorSearch { get; } = new();
        SearchState MonsterSearch { get; } = new();
        SearchState SkillSearch { get; } = new();
        SearchState SwitchSearch { get; } = new();
        Dictionary<string, string> SwitchDisplayEditorTexts { get; } = [];

        public void RaiseRender()
        {
            try
            {
                ApplyUiScale();
                UpdateMouseCursorVisibility();
                UpdateAsyncRequests();
                RenderSessionWindow();
                RenderCharacterStatusDialog();
                RenderCharacterSkillDialog();
                RenderCharacterSkillSelectorDialog();
                RenderLauncherWindow();
                RenderToasts();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "");
            }
        }
    }
}
