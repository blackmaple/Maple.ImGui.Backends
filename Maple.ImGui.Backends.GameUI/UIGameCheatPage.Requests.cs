using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责处理页面异步请求结果与消息通知。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private void UpdateAsyncRequests()
        {
            _gameSessionInfoRequest.Update(
                onSuccess: result =>
                {
                    if (result.IsSuccess && result.Data is not null)
                    {
                        GameSessionInfo = result.Data;
                        ShowSessionWindow = true;
                        LauncherVisible = false;
                        AddToast(result.Message ?? "GetGameSessionInfoAsync success.", UiToastKind.Success);
                        if (!_sessionCollectionsRequest.IsRunning)
                        {
                            _sessionCollectionsRequest.TryStart(LoadSessionCollectionsAsync);
                        }
                    }
                    else
                    {
                        AddToast(result.Message ?? "GetGameSessionInfoAsync failed.", UiToastKind.Error);
                    }
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _sessionCollectionsRequest.Update(
                onSuccess: collections =>
                {
                    SessionCollections = collections;
                    if (ReloadingTab is { } reloadingTab)
                    {
                        AddToast($"{reloadingTab} loaded {GetDisplayCount(collections, reloadingTab)} items.", UiToastKind.Success);
                        ReloadingTab = null;
                    }
                    else
                    {
                        AddToast($"Session display lists loaded: {GetTotalDisplayCount(collections)} items.", UiToastKind.Success);
                    }
                },
                onError: exception =>
                {
                    SessionCollections = SessionCollectionsData.Empty;
                    ReloadingTab = null;
                    AddToast(exception.Message, UiToastKind.Error);
                });

            _currencyEditRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? "GetCurrencyInfoAsync failed.", UiToastKind.Error);
                        return;
                    }

                    EditingCurrency = result.Data;
                    CurrencyEditValue = result.Data.Info.DisplayValue ?? string.Empty;
                    PendingOpenCurrencyEditPopup = true;
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _inventoryEditRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? "GetInventoryInfoAsync failed.", UiToastKind.Error);
                        return;
                    }

                    EditingInventory = result.Data;
                    InventoryEditValue = result.Data.Info.DisplayValue ?? string.Empty;
                    PendingOpenInventoryEditPopup = true;
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _characterStatusRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? "GetCharacterStatusAsync failed.", UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterStatus = result.Data;
                    ShowCharacterStatusDialog = true;
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _characterStatusUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        RestorePendingCharacterStatusOriginalValue();
                        AddToast(result.Message ?? "UpdateCharacterStatusAsync failed.", UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterStatus = result.Data;
                    ShowCharacterStatusDialog = true;
                    PendingCharacterStatusOriginalValue = null;
                    AddToast(result.Message ?? "Character status updated.", UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _switchDisplayUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        RestorePendingSwitchDisplayOriginalValue();
                        AddToast(result.Message ?? "UpdateSwitchDisplayAsync failed.", UiToastKind.Error);
                        return;
                    }

                    ReplaceCurrentSwitchDisplay(result.Data.Display);
                    PendingSwitchDisplayOriginalValue = null;

                    AddToast(result.Data.Message, UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _characterSkillRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? "GetCharacterSkillAsync failed.", UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterSkill = result.Data;
                    ShowCharacterSkillDialog = true;
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _characterSkillUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? "UpdateCharacterSkillAsync failed.", UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterSkill = result.Data;
                    ShowCharacterSkillDialog = true;
                    ShowCharacterSkillSelectorDialog = false;
                    AddToast(result.Message ?? "Character skill updated.", UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _currencyUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess)
                    {
                        AddToast(result.Message ?? "UpdateCurrencyInfoAsync failed.", UiToastKind.Error);
                        return;
                    }

                    PendingCloseCurrencyEditPopup = true;
                    AddToast(result.Data ?? "Currency updated.", UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _inventoryUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess)
                    {
                        AddToast(result.Message ?? "UpdateInventoryInfoAsync failed.", UiToastKind.Error);
                        return;
                    }

                    PendingCloseInventoryEditPopup = true;
                    AddToast(result.Data ?? "Inventory updated.", UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));
        }

        private async Task<AsyncFetchResult<GameSessionInfoDTO?>> GetGameSessionInfoAsync()
        {
            var result = await Service.GetGameSessionInfoAsync().ConfigureAwait(false);
            if (result.TryGet(out var data))
            {
                return AsyncFetchResult<GameSessionInfoDTO?>.Success(data, "GetGameSessionInfoAsync success.");
            }

            return AsyncFetchResult<GameSessionInfoDTO?>.Failure(result.MSG ?? "GetGameSessionInfoAsync failed.");
        }

        private void AddToast(string message, UiToastKind kind)
        {
            Toasts.Add(new UiToast(NextToastId++, kind, message, DateTime.UtcNow.AddSeconds(3)));
        }
    }
}
