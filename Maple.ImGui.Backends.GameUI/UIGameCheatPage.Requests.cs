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
                        AddToast(result.Message ?? GetUiText("Toast.Request.GameSession.Success"), UiToastKind.Success);
                        if (!_sessionCollectionsRequest.IsRunning)
                        {
                            _sessionCollectionsRequest.TryStart(LoadSessionCollectionsAsync);
                        }
                    }
                    else
                    {
                        AddToast(result.Message ?? GetUiText("Toast.Request.GameSession.Failure"), UiToastKind.Error);
                    }
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _sessionCollectionsRequest.Update(
                onSuccess: collections =>
                {
                    SessionCollections = collections;
                    if (ReloadingTab is { } reloadingTab)
                    {
                        AddToast(GetUiText("Toast.Request.SessionCollections.Reloaded", reloadingTab, GetDisplayCount(collections, reloadingTab)), UiToastKind.Success);
                        ReloadingTab = null;
                    }
                    else
                    {
                        AddToast(GetUiText("Toast.Request.SessionCollections.Loaded", GetTotalDisplayCount(collections)), UiToastKind.Success);
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
                        AddToast(result.Message ?? GetUiText("Toast.Request.CurrencyInfo.Failure"), UiToastKind.Error);
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
                        AddToast(result.Message ?? GetUiText("Toast.Request.InventoryInfo.Failure"), UiToastKind.Error);
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
                        AddToast(result.Message ?? GetUiText("Toast.Request.CharacterStatus.Failure"), UiToastKind.Error);
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
                        AddToast(result.Message ?? GetUiText("Toast.Request.CharacterStatus.UpdateFailure"), UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterStatus = result.Data;
                    ShowCharacterStatusDialog = true;
                    PendingCharacterStatusOriginalValue = null;
                    AddToast(result.Message ?? GetUiText("Toast.Request.CharacterStatus.Updated"), UiToastKind.Success);
                },
                onError: exception =>
                {
                    RestorePendingCharacterStatusOriginalValue();
                    AddToast(exception.Message, UiToastKind.Error);
                });

            _switchDisplayUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        RestorePendingSwitchDisplayOriginalValue();
                        AddToast(result.Message ?? GetUiText("Toast.Request.SwitchDisplay.UpdateFailure"), UiToastKind.Error);
                        return;
                    }

                    ReplaceCurrentSwitchDisplay(result.Data.Display);
                    PendingSwitchDisplayOriginalValue = null;

                    AddToast(result.Data.Message, UiToastKind.Success);
                },
                onError: exception =>
                {
                    RestorePendingSwitchDisplayOriginalValue();
                    AddToast(exception.Message, UiToastKind.Error);
                });

            _monsterAddRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? GetUiText("Toast.Request.MonsterAdd.Failure"), UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterSkill = result.Data;
                    ShowCharacterSkillDialog = true;
                    ShowCharacterSkillSelectorDialog = false;
                    PendingMonsterAddAction = null;
                    AddToast(result.Message ?? GetUiText("Toast.Request.MonsterAdd.Success"), UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _characterSkillRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess || result.Data is null)
                    {
                        AddToast(result.Message ?? GetUiText("Toast.Request.CharacterSkill.Failure"), UiToastKind.Error);
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
                        AddToast(result.Message ?? GetUiText("Toast.Request.CharacterSkill.UpdateFailure"), UiToastKind.Error);
                        return;
                    }

                    ViewingCharacterSkill = result.Data;
                    ShowCharacterSkillDialog = true;
                    ShowCharacterSkillSelectorDialog = false;
                    AddToast(result.Message ?? GetUiText("Toast.Request.CharacterSkill.Updated"), UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _currencyUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess)
                    {
                        AddToast(result.Message ?? GetUiText("Toast.Request.Currency.UpdateFailure"), UiToastKind.Error);
                        return;
                    }

                    PendingCloseCurrencyEditPopup = true;
                    AddToast(result.Data ?? GetUiText("Toast.Request.Currency.Updated"), UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));

            _inventoryUpdateRequest.Update(
                onSuccess: result =>
                {
                    if (!result.IsSuccess)
                    {
                        AddToast(result.Message ?? GetUiText("Toast.Request.Inventory.UpdateFailure"), UiToastKind.Error);
                        return;
                    }

                    PendingCloseInventoryEditPopup = true;
                    AddToast(result.Data ?? GetUiText("Toast.Request.Inventory.Updated"), UiToastKind.Success);
                },
                onError: exception => AddToast(exception.Message, UiToastKind.Error));
        }

        private async Task<AsyncFetchResult<GameSessionInfoDTO?>> GetGameSessionInfoAsync()
        {
            var result = await Service.GetGameSessionInfoAsync().ConfigureAwait(false);
            if (result.TryGet(out var data))
            {
                return AsyncFetchResult<GameSessionInfoDTO?>.Success(data, GetUiText("Request.GameSession.Success"));
            }

            return AsyncFetchResult<GameSessionInfoDTO?>.Failure(result.MSG ?? GetUiText("Request.GameSession.Failure"));
        }

        private void AddToast(string message, UiToastKind kind)
        {
            Toasts.Add(new UiToast(NextToastId++, kind, message, DateTime.UtcNow.AddSeconds(3)));
        }
    }
}
