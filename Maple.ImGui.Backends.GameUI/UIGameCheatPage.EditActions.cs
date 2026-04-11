using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;
using System.Globalization;
using System.Linq;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责编辑动作的触发、查询与提交。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private void HandleEditButtonClick<TDisplay>(TDisplay item) where TDisplay : GameDisplayDTO
        {
            if (GameSessionInfo is null)
            {
                AddToast(GetUiText("Toast.Session.NotReady"), UiToastKind.Error);
                return;
            }

            switch (item)
            {
                case GameCurrencyDisplayDTO currency when !_currencyEditRequest.IsRunning:
                    _currencyEditRequest.TryStart(() => GetCurrencyEditStateAsync(currency));
                    break;
                case GameInventoryDisplayDTO inventory when !_inventoryEditRequest.IsRunning:
                    _inventoryEditRequest.TryStart(() => GetInventoryEditStateAsync(inventory));
                    break;
                case GameCharacterDisplayDTO character when !_characterStatusRequest.IsRunning:
                    _characterStatusRequest.TryStart(() => GetCharacterStatusDialogStateAsync(character));
                    break;
                default:
                    AddToast(GetUiText("Toast.Edit.NotImplemented", item.DisplayName ?? item.ObjectId ?? "Item"), UiToastKind.Error);
                    break;
            }
        }

        private void HandleSkillButtonClick(GameCharacterDisplayDTO display)
        {
            if (GameSessionInfo is null)
            {
                AddToast(GetUiText("Toast.Session.NotReady"), UiToastKind.Error);
                return;
            }

            if (_characterSkillRequest.IsRunning)
            {
                return;
            }

            _characterSkillRequest.TryStart(() => GetCharacterSkillDialogStateAsync(display));
        }

        private void HandleCharacterSkillRemoveButtonClick(GameSkillInfoDTO skill)
        {
            if (GameSessionInfo is null || ViewingCharacterSkill is null || _characterSkillUpdateRequest.IsRunning)
            {
                return;
            }

            OpenCharacterSkillActionConfirm(
                skill.DisplayCategory,
                skill.ObjectId ?? string.Empty,
                string.Empty,
                skill.DisplayName ?? skill.ObjectId ?? GetUiText("Text.Skill"),
                false);
        }

        private void HandleCharacterSkillAddButtonClick(GameSkillInfoDTO skill)
        {
            CharacterSkillSelectionCategory = skill.DisplayCategory ?? string.Empty;
            CharacterSkillSelectorSearch.InputText = string.Empty;
            CharacterSkillSelectorSearch.AppliedText = string.Empty;
            ShowCharacterSkillSelectorDialog = true;
        }

        private void HandleCharacterSkillSelectionAddButtonClick(CharacterSkillSelectorItem skill)
        {
            if (GameSessionInfo is null || ViewingCharacterSkill is null || _characterSkillUpdateRequest.IsRunning)
            {
                return;
            }

            OpenCharacterSkillActionConfirm(
                skill.DisplayCategory,
                string.Empty,
                skill.ObjectId ?? string.Empty,
                skill.DisplayName ?? skill.ObjectId ?? GetUiText("Text.Skill"),
                true);
        }

        private void HandleMonsterInfoButtonClick(GameMonsterDisplayDTO monster)
        {
            ViewingMonsterInfo = new MonsterInfoDialogState(
                monster,
                monster.DisplayName ?? monster.ObjectId ?? GetUiText("Text.Monster"),
                monster.DisplayDesc ?? string.Empty);
            ShowMonsterInfoDialog = true;
        }

        private void HandleMonsterAddButtonClick(GameMonsterDisplayDTO monster)
        {
            if (GameSessionInfo is null || _monsterAddRequest.IsRunning)
            {
                return;
            }

            PendingMonsterAddAction = new MonsterAddConfirmState(
                monster,
                monster.DisplayName ?? monster.ObjectId ?? GetUiText("Text.Monster"),
                monster.DisplayDesc ?? string.Empty);
            PendingOpenMonsterAddPopup = true;
        }

        private void OpenCharacterSkillActionConfirm(string? modifyCategory, string oldSkill, string newSkill, string displayName, bool isAdd)
        {
            PendingCharacterSkillAction = new CharacterSkillActionConfirmState(modifyCategory, oldSkill, newSkill, displayName, isAdd);
            PendingOpenCharacterSkillActionPopup = true;
        }

        private void HandleCharacterStatusValueChanged(GameSwitchDisplayDTO attribute, string? originalContentValue)
        {
            if (GameSessionInfo is null || ViewingCharacterStatus is null || _characterStatusUpdateRequest.IsRunning)
            {
                return;
            }

            PendingCharacterStatusOriginalValue = CreateSwitchDisplayOriginalValueState(attribute, originalContentValue);
            _characterStatusUpdateRequest.TryStart(() => UpdateCharacterStatusDialogAsync(attribute));
        }

        private void HandleSwitchDisplayValueChanged(GameSwitchDisplayDTO attribute, string? originalContentValue)
        {
            if (GameSessionInfo is null || _switchDisplayUpdateRequest.IsRunning)
            {
                return;
            }

            PendingSwitchDisplayOriginalValue = CreateSwitchDisplayOriginalValueState(attribute, originalContentValue);
            _switchDisplayUpdateRequest.TryStart(() => UpdateSwitchDisplayAsync(attribute));
        }

        private async Task<AsyncFetchResult<CurrencyEditState>> GetCurrencyEditStateAsync(GameCurrencyDisplayDTO display)
        {
            if (GameSessionInfo is null || string.IsNullOrWhiteSpace(display.ObjectId))
            {
                return AsyncFetchResult<CurrencyEditState>.Failure(GetUiText("Request.Currency.Invalid"));
            }

            var result = await Service.GetCurrencyInfoAsync(GameSessionInfo, display.ObjectId, display.DisplayCategory).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<CurrencyEditState>.Success(new CurrencyEditState(data, display.DisplayCategory, display.DisplayName ?? display.ObjectId ?? GetUiText("Text.Currency"), display.DisplayDesc ?? string.Empty));
            }

            return AsyncFetchResult<CurrencyEditState>.Failure(result.MSG ?? GetUiText("Request.Currency.Failure"));
        }

        private async Task<AsyncFetchResult<InventoryEditState>> GetInventoryEditStateAsync(GameInventoryDisplayDTO display)
        {
            if (GameSessionInfo is null || string.IsNullOrWhiteSpace(display.ObjectId))
            {
                return AsyncFetchResult<InventoryEditState>.Failure(GetUiText("Request.Inventory.Invalid"));
            }

            var result = await Service.GetInventoryInfoAsync(GameSessionInfo, display.ObjectId, display.DisplayCategory).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<InventoryEditState>.Success(new InventoryEditState(data, display.DisplayCategory, display.DisplayName ?? display.ObjectId ?? GetUiText("Text.Inventory"), display.DisplayDesc ?? string.Empty));
            }

            return AsyncFetchResult<InventoryEditState>.Failure(result.MSG ?? GetUiText("Request.Inventory.Failure"));
        }

        private async Task<AsyncFetchResult<CharacterStatusDialogState>> GetCharacterStatusDialogStateAsync(GameCharacterDisplayDTO display)
        {
            if (GameSessionInfo is null)
            {
                return AsyncFetchResult<CharacterStatusDialogState>.Failure(GetUiText("Request.CharacterStatus.Invalid"));
            }

            var result = await Service.GetCharacterStatusAsync(GameSessionInfo, display).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<CharacterStatusDialogState>.Success(new CharacterStatusDialogState(data, display, display.DisplayName ?? display.ObjectId ?? GetUiText("Text.Character"), display.DisplayDesc ?? string.Empty));
            }

            return AsyncFetchResult<CharacterStatusDialogState>.Failure(result.MSG ?? GetUiText("Request.CharacterStatus.Failure"));
        }

        private async Task<AsyncFetchResult<CharacterSkillDialogState>> GetCharacterSkillDialogStateAsync(GameCharacterDisplayDTO display)
        {
            if (GameSessionInfo is null)
            {
                return AsyncFetchResult<CharacterSkillDialogState>.Failure(GetUiText("Request.CharacterSkill.Invalid"));
            }

            var result = await Service.GetCharacterSkillAsync(GameSessionInfo, display).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<CharacterSkillDialogState>.Success(new CharacterSkillDialogState(data, display, display.DisplayName ?? display.ObjectId ?? GetUiText("Text.Character"), display.DisplayDesc ?? string.Empty));
            }

            return AsyncFetchResult<CharacterSkillDialogState>.Failure(result.MSG ?? GetUiText("Request.CharacterSkill.Failure"));
        }

        private async Task<AsyncFetchResult<CharacterStatusDialogState>> UpdateCharacterStatusDialogAsync(GameSwitchDisplayDTO attribute)
        {
            if (GameSessionInfo is null || ViewingCharacterStatus is null)
            {
                return AsyncFetchResult<CharacterStatusDialogState>.Failure(GetUiText("Request.CharacterStatus.UpdateInvalid"));
            }

            var result = await Service.UpdateCharacterStatusAsync(
                GameSessionInfo,
                ViewingCharacterStatus.Character,
                attribute).ConfigureAwait(false);

            if (result.TryGet(out var data) && data is not null)
            {
                var updatedAttribute = FindUpdatedCharacterStatusAttribute(data, attribute);
                ReplaceCurrentCharacterStatusAttribute(updatedAttribute);
                return AsyncFetchResult<CharacterStatusDialogState>.Success(
                    new CharacterStatusDialogState(data, ViewingCharacterStatus.Character, ViewingCharacterStatus.DisplayName, ViewingCharacterStatus.DisplayDesc),
                    GetUiText(
                        "Format.DisplayValuePair",
                        attribute.DisplayName ?? attribute.ObjectId ?? GetUiText("Text.Attribute"),
                        GetCharacterStatusAttributeContent(updatedAttribute ?? attribute)));
            }

            return AsyncFetchResult<CharacterStatusDialogState>.Failure(result.MSG ?? GetUiText("Request.CharacterStatus.UpdateFailure"));
        }

        private async Task<AsyncFetchResult<CharacterSkillDialogState>> UpdateCharacterSkillDialogAsync(string? modifyCategory, string oldSkill, string newSkill, string successMessage)
        {
            if (GameSessionInfo is null || ViewingCharacterSkill is null)
            {
                return AsyncFetchResult<CharacterSkillDialogState>.Failure(GetUiText("Request.CharacterSkill.UpdateInvalid"));
            }

            var result = await Service.UpdateCharacterSkillAsync(
                GameSessionInfo,
                ViewingCharacterSkill.Character,
                modifyCategory,
                oldSkill,
                newSkill).ConfigureAwait(false);

            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<CharacterSkillDialogState>.Success(
                    new CharacterSkillDialogState(data, ViewingCharacterSkill.Character, ViewingCharacterSkill.DisplayName, ViewingCharacterSkill.DisplayDesc),
                    successMessage);
            }

            return AsyncFetchResult<CharacterSkillDialogState>.Failure(result.MSG ?? GetUiText("Request.CharacterSkill.UpdateFailure"));
        }

        private async Task<AsyncFetchResult<CharacterSkillDialogState>> UpdateMonsterMemberAsync(GameMonsterDisplayDTO monster)
        {
            if (GameSessionInfo is null || string.IsNullOrWhiteSpace(monster.ObjectId))
            {
                return AsyncFetchResult<CharacterSkillDialogState>.Failure(GetUiText("Request.MonsterAdd.Invalid"));
            }

            var result = await Service.AddMonsterMemberAsync(GameSessionInfo, monster.ObjectId).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<CharacterSkillDialogState>.Success(
                    new CharacterSkillDialogState(data, CreateCharacterDisplayFromMonster(monster, data.ObjectId), monster.DisplayName ?? monster.ObjectId ?? GetUiText("Text.Monster"), monster.DisplayDesc ?? string.Empty),
                    monster.DisplayName ?? monster.ObjectId);
            }

            return AsyncFetchResult<CharacterSkillDialogState>.Failure(result.MSG ?? GetUiText("Request.MonsterAdd.Failure"));
        }

        private async Task<AsyncFetchResult<SwitchDisplayUpdateState>> UpdateSwitchDisplayAsync(GameSwitchDisplayDTO attribute)
        {
            if (GameSessionInfo is null)
            {
                return AsyncFetchResult<SwitchDisplayUpdateState>.Failure(GetUiText("Request.SwitchDisplay.UpdateInvalid"));
            }

            var result = await Service.UpdateSwitchDisplayAsync(GameSessionInfo, attribute).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<SwitchDisplayUpdateState>.Success(
                    new SwitchDisplayUpdateState(data, GetSwitchDisplayUpdateMessage(data)));
            }

            return AsyncFetchResult<SwitchDisplayUpdateState>.Failure(result.MSG ?? GetUiText("Request.SwitchDisplay.UpdateFailure"));
        }

        private async Task<AsyncFetchResult<string>> UpdateCurrencyEditAsync()
        {
            if (GameSessionInfo is null || EditingCurrency is null)
            {
                return AsyncFetchResult<string>.Failure(GetUiText("Request.Currency.EditInvalid"));
            }

            EditingCurrency.Info.DisplayValue = CurrencyEditValue;
            var result = await Service.UpdateCurrencyInfoAsync(GameSessionInfo, EditingCurrency.Info, EditingCurrency.Category).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<string>.Success(GetUiText("Format.DisplayValuePair", EditingCurrency.DisplayName, data.DisplayValue));
            }

            return AsyncFetchResult<string>.Failure(result.MSG ?? GetUiText("Request.Currency.UpdateFailure"));
        }

        private async Task<AsyncFetchResult<string>> UpdateInventoryEditAsync()
        {
            if (GameSessionInfo is null || EditingInventory is null)
            {
                return AsyncFetchResult<string>.Failure(GetUiText("Request.Inventory.EditInvalid"));
            }

            EditingInventory.Info.DisplayValue = InventoryEditValue;
            var result = await Service.UpdateInventoryInfoAsync(GameSessionInfo, EditingInventory.Category, EditingInventory.Info).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return AsyncFetchResult<string>.Success(GetUiText("Format.DisplayValuePair", EditingInventory.DisplayName, data.DisplayValue));
            }

            return AsyncFetchResult<string>.Failure(result.MSG ?? GetUiText("Request.Inventory.UpdateFailure"));
        }

        private static string GetCharacterStatusAttributeContent(GameSwitchDisplayDTO attribute)
        {
            if (attribute.TextEditorType)
            {
                return attribute.DecimalValue.ToString(CultureInfo.InvariantCulture);
            }

            if (attribute.SelectsType)
            {
                var selectedOption = (attribute.SelectedContents ?? [])
                    .FirstOrDefault(x => string.Equals(x.DisplayValue, attribute.ContentValue, StringComparison.OrdinalIgnoreCase));
                return selectedOption?.DisplayName
                    ?? selectedOption?.DisplayValue
                    ?? attribute.ContentValue
                    ?? string.Empty;
            }

            if (attribute.MultipleType)
            {
                var selectedValues = (attribute.ContentValue ?? string.Empty)
                    .Split([',', ';', '|'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var selectedNames = (attribute.SelectedContents ?? [])
                    .Where(x => selectedValues.Any(v => string.Equals(v, x.DisplayValue, StringComparison.OrdinalIgnoreCase)))
                    .Select(x => x.DisplayName ?? x.DisplayValue ?? string.Empty)
                    .Where(static x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();
                return selectedNames.Length > 0
                    ? string.Join(',', selectedNames)
                    : attribute.ContentValue ?? string.Empty;
            }

            if (attribute.SwitchesType)
            {
                return attribute.SwitchValue.ToString();
            }

            if (attribute.ButtonType)
            {
                return attribute.ContentValue ?? GetUiText("Switch.Action");
            }

            return attribute.ContentValue ?? string.Empty;
        }

        private static GameSwitchDisplayDTO? FindUpdatedCharacterStatusAttribute(GameCharacterStatusDTO status, GameSwitchDisplayDTO sourceAttribute)
        {
            if (string.IsNullOrWhiteSpace(sourceAttribute.ObjectId))
            {
                return null;
            }

            return (status.CharacterAttributes ?? [])
                .FirstOrDefault(x => string.Equals(x.ObjectId, sourceAttribute.ObjectId, StringComparison.OrdinalIgnoreCase));
        }

        private void ReplaceCurrentCharacterStatusAttribute(GameSwitchDisplayDTO? updatedAttribute)
        {
            if (updatedAttribute is null || ViewingCharacterStatus?.Data.CharacterAttributes is not { } currentAttributes)
            {
                return;
            }

            for (var i = 0; i < currentAttributes.Length; i++)
            {
                var currentAttribute = currentAttributes[i];
                if (string.Equals(currentAttribute.ObjectId, updatedAttribute.ObjectId, StringComparison.OrdinalIgnoreCase))
                {
                    currentAttributes[i] = updatedAttribute;
                    return;
                }
            }
        }

        private static string GetSwitchDisplayUpdateMessage(GameSwitchDisplayDTO display)
        {
            var displayName = display.DisplayName ?? display.ObjectId ?? GetUiText("Text.Switch");
            if (display.ContentValue is null)
            {
                return displayName;
            }

            if (display.SelectedContents is { Count: > 0 })
            {
                var selectedNames = display.ContentValue
                    .Split([',', ';', '|'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(value => display.SelectedContents.FirstOrDefault(x => string.Equals(x.DisplayValue, value, StringComparison.OrdinalIgnoreCase))?.DisplayName ?? value)
                    .Where(static value => !string.IsNullOrWhiteSpace(value))
                    .ToArray();

                if (selectedNames.Length > 0)
                {
                    return GetUiText("Format.DisplayValuePair", displayName, string.Join(',', selectedNames));
                }
            }

            return GetUiText("Format.DisplayValuePair", displayName, display.ContentValue ?? string.Empty);
        }

        private void ReplaceCurrentSwitchDisplay(GameSwitchDisplayDTO updatedDisplay)
        {
            var switches = SessionCollections.Switches;
            for (var i = 0; i < switches.Length; i++)
            {
                if (string.Equals(switches[i].ObjectId, updatedDisplay.ObjectId, StringComparison.OrdinalIgnoreCase))
                {
                    switches[i] = updatedDisplay;
                    RefreshSwitchDisplayEditorCache(updatedDisplay);
                    return;
                }
            }
        }

        private void RefreshSwitchDisplayEditorCache(GameSwitchDisplayDTO updatedDisplay)
        {
            var cachePrefix = updatedDisplay.ObjectId ?? updatedDisplay.DisplayName;
            if (string.IsNullOrWhiteSpace(cachePrefix))
            {
                return;
            }

            var matchedKeys = SwitchDisplayEditorTexts.Keys
                .Where(key => key.StartsWith(cachePrefix + "_", StringComparison.Ordinal))
                .ToArray();

            foreach (var key in matchedKeys)
            {
                if (updatedDisplay.TextEditorType)
                {
                    SwitchDisplayEditorTexts[key] = updatedDisplay.ContentValue ?? string.Empty;
                }
                else
                {
                    SwitchDisplayEditorTexts.Remove(key);
                }
            }
        }

        private void RestorePendingSwitchDisplayOriginalValue()
        {
            RestoreSwitchDisplayOriginalValue(PendingSwitchDisplayOriginalValue);
            PendingSwitchDisplayOriginalValue = null;
        }

        private void RestorePendingCharacterStatusOriginalValue()
        {
            RestoreSwitchDisplayOriginalValue(PendingCharacterStatusOriginalValue);
            PendingCharacterStatusOriginalValue = null;
        }

        private static SwitchDisplayOriginalValueState CreateSwitchDisplayOriginalValueState(GameSwitchDisplayDTO attribute, string? originalContentValue)
        {
            return new SwitchDisplayOriginalValueState(
                attribute,
                originalContentValue);
        }

        private static GameCharacterDisplayDTO CreateCharacterDisplayFromMonster(GameMonsterDisplayDTO monster, string objectId)
        {
            return new GameCharacterDisplayDTO
            {
                ObjectId = objectId,
                DisplayCategory = monster.DisplayCategory,
                DisplayName = monster.DisplayName,
                DisplayDesc = monster.DisplayDesc,
                DisplayImage = monster.DisplayImage,
            };
        }

        private void RestoreSwitchDisplayOriginalValue(SwitchDisplayOriginalValueState? originalValue)
        {
            if (originalValue is null)
            {
                return;
            }

            originalValue.Target.ContentValue = originalValue.ContentValue;
            //         originalValue.Target.DecimalValue = originalValue.DecimalValue;
            //        originalValue.Target.SwitchValue = originalValue.SwitchValue;
            RefreshSwitchDisplayEditorCache(originalValue.Target);
        }
    }
}
