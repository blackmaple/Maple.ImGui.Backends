namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 集中管理页面中的用户可见文案，默认使用英文，便于后续扩展多语言。
    /// </summary>
    public partial class UIGameDataPage
    {
        private static readonly IReadOnlyDictionary<string, string> UiTextMap = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["Dialog.Help.TitleFallback"] = "Game Session",
            ["Window.Session.Loading"] = "Loading display lists...",
            ["Toast.Session.NotReady"] = "Game session is not ready.",
            ["Toast.Edit.NotImplemented"] = "Edit {0} not implemented.",
            ["Toast.Request.GameSession.Success"] = "Game session loaded successfully.",
            ["Toast.Request.GameSession.Failure"] = "Failed to load game session.",
            ["Toast.Request.SessionCollections.Reloaded"] = "{0} loaded {1} items.",
            ["Toast.Request.SessionCollections.Loaded"] = "Session display lists loaded: {0} items.",
            ["Toast.Request.CurrencyInfo.Failure"] = "Failed to load currency info.",
            ["Toast.Request.InventoryInfo.Failure"] = "Failed to load inventory info.",
            ["Toast.Request.CharacterStatus.Failure"] = "Failed to load character status.",
            ["Toast.Request.CharacterStatus.UpdateFailure"] = "Failed to update character status.",
            ["Toast.Request.CharacterStatus.Updated"] = "Character status updated.",
            ["Toast.Request.SwitchDisplay.UpdateFailure"] = "Failed to update switch display.",
            ["Toast.Request.MonsterAdd.Failure"] = "Failed to add monster.",
            ["Toast.Request.MonsterAdd.Success"] = "Monster added.",
            ["Toast.Request.CharacterSkill.Failure"] = "Failed to load character skill.",
            ["Toast.Request.CharacterSkill.UpdateFailure"] = "Failed to update character skill.",
            ["Toast.Request.CharacterSkill.Updated"] = "Character skill updated.",
            ["Toast.Request.Currency.UpdateFailure"] = "Failed to update currency.",
            ["Toast.Request.Currency.Updated"] = "Currency updated.",
            ["Toast.Request.Inventory.UpdateFailure"] = "Failed to update inventory.",
            ["Toast.Request.Inventory.Updated"] = "Inventory updated.",
            ["Format.DisplayValuePair"] = "{0}:{1}",
            ["Request.GameSession.Success"] = "Game session loaded successfully.",
            ["Request.GameSession.Failure"] = "Failed to load game session.",
            ["Request.Currency.Invalid"] = "Currency info request is invalid.",
            ["Request.Currency.Failure"] = "Failed to load currency info.",
            ["Request.Inventory.Invalid"] = "Inventory info request is invalid.",
            ["Request.Inventory.Failure"] = "Failed to load inventory info.",
            ["Request.CharacterStatus.Invalid"] = "Character status request is invalid.",
            ["Request.CharacterStatus.Failure"] = "Failed to load character status.",
            ["Request.CharacterStatus.UpdateInvalid"] = "Character status update request is invalid.",
            ["Request.CharacterStatus.UpdateFailure"] = "Failed to update character status.",
            ["Request.CharacterSkill.Invalid"] = "Character skill request is invalid.",
            ["Request.CharacterSkill.Failure"] = "Failed to load character skill.",
            ["Request.CharacterSkill.UpdateInvalid"] = "Character skill update request is invalid.",
            ["Request.CharacterSkill.UpdateFailure"] = "Failed to update character skill.",
            ["Request.MonsterAdd.Invalid"] = "Monster add request is invalid.",
            ["Request.MonsterAdd.Failure"] = "Failed to add monster.",
            ["Request.SwitchDisplay.UpdateInvalid"] = "Switch display update request is invalid.",
            ["Request.SwitchDisplay.UpdateFailure"] = "Failed to update switch display.",
            ["Request.Currency.EditInvalid"] = "Currency edit state is invalid.",
            ["Request.Currency.UpdateFailure"] = "Failed to update currency.",
            ["Request.Inventory.EditInvalid"] = "Inventory edit state is invalid.",
            ["Request.Inventory.UpdateFailure"] = "Failed to update inventory.",
            ["Tab.Currency"] = "Currency",
            ["Tab.Inventory"] = "Inventory",
            ["Tab.Character"] = "Character",
            ["Tab.Monster"] = "Monster",
            ["Tab.Skill"] = "Skill",
            ["Tab.Misc"] = "Misc",
            ["Dialog.Help.Empty"] = "Empty",
            ["Dialog.Help.GameTag"] = "Game",
            ["Dialog.Help.ApiVersion"] = "ApiVer:{0}",
            ["Dialog.Action.Ok"] = "OK",
            ["Dialog.Action.Cancel"] = "Cancel",
            ["Dialog.Action.Yes"] = "Yes",
            ["Dialog.Action.No"] = "Cancel",
            ["Dialog.Currency.TitleFallback"] = "Currency",
            ["Dialog.Inventory.TitleFallback"] = "Inventory",
            ["Dialog.Skill.NoSkills"] = "No skills.",
            ["Dialog.Skill.ConfirmAdd"] = "Confirm adding {0}?",
            ["Dialog.Skill.ConfirmRemove"] = "Confirm removing {0}?",
            ["Dialog.Monster.AddConfirm"] = "Confirm adding {0}?",
            ["Dialog.Monster.NoData"] = "No monster data.",
            ["Dialog.Monster.Category.Attributes"] = "Attributes",
            ["Dialog.Monster.Category.Skill"] = "Skill",
            ["Text.Item"] = "Item",
            ["Text.Currency"] = "Currency",
            ["Text.Inventory"] = "Inventory",
            ["Text.Character"] = "Character",
            ["Text.Skill"] = "Skill",
            ["Text.Monster"] = "Monster",
            ["Text.Attribute"] = "Attribute",
            ["Text.Switch"] = "Switch",
            ["Text.Misc"] = "Misc",
            ["Switch.Action"] = "Action",
            ["Switch.SelectPlaceholder"] = "Select...",
            ["Dialog.Skill.Selector.NoSkillsInCategory"] = "No skills in current category.",
            ["Dialog.Text.Empty"] = "Empty",
        };

        private static string GetUiText(string key)
        {
            return UiTextMap.TryGetValue(key, out var value)
                ? value
                : key;
        }

        private static string GetUiText(string key, params object?[] args)
        {
            var format = GetUiText(key);
            return args.Length == 0
                ? format
                : string.Format(format, args);
        }
    }
}
