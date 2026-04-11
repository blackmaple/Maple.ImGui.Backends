using Maple.MonoGameAssistant.GameCore;
using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;

namespace Maple.ImGui.Backends.GameUI
{
    internal sealed class GameCheatService_Http(GameHttpClientService service) : IGameDataService
    {
        public Task<MonoResultDTO<GameSessionInfoDTO>> GetGameSessionInfoAsync() => service.GetGameSessionInfoAsync();
        public Task<MonoResultDTO<GameSessionInfoDTO>> LoadResourceAsync(GameSessionInfoDTO gameSessionInfo) => service.LoadResourceAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameCurrencyDisplayDTO[]>> GetListCurrencyDisplayAsync(GameSessionInfoDTO gameSessionInfo) => service.GetListCurrencyDisplayAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameCurrencyInfoDTO>> GetCurrencyInfoAsync(GameSessionInfoDTO gameSessionInfo, string gameCurrencyId, string? category = null) => service.GetCurrencyInfoAsync(gameSessionInfo, gameCurrencyId, category);
        public Task<MonoResultDTO<GameCurrencyInfoDTO>> UpdateCurrencyInfoAsync(GameSessionInfoDTO gameSessionInfo, GameCurrencyInfoDTO gameCurrency, string? category = null) => service.UpdateCurrencyInfoAsync(gameSessionInfo, gameCurrency, category);
        public Task<MonoResultDTO<GameInventoryDisplayDTO[]>> GetListInventoryDisplayAsync(GameSessionInfoDTO gameSessionInfo) => service.GetListInventoryDisplayAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameInventoryInfoDTO>> GetInventoryInfoAsync(GameSessionInfoDTO gameSessionInfo, string gameInventoryId, string? category) => service.GetInventoryInfoAsync(gameSessionInfo, gameInventoryId, category);
        public Task<MonoResultDTO<GameInventoryInfoDTO>> UpdateInventoryInfoAsync(GameSessionInfoDTO gameSessionInfo, string? category, GameInventoryInfoDTO gameInventory) => service.UpdateInventoryInfoAsync(gameSessionInfo, category, gameInventory);
        public Task<MonoResultDTO<GameCharacterDisplayDTO[]>> GetListCharacterDisplayAsync(GameSessionInfoDTO gameSessionInfo) => service.GetListCharacterDisplayAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameCharacterStatusDTO>> GetCharacterStatusAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay) => service.GetCharacterStatusAsync(gameSessionInfo, gameCharacterDisplay);
        public Task<MonoResultDTO<GameCharacterStatusDTO>> UpdateCharacterStatusAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, GameSwitchDisplayDTO gameValueInfo) => service.UpdateCharacterStatusAsync(gameSessionInfo, gameCharacterDisplay, gameValueInfo);
        public Task<MonoResultDTO<GameCharacterSkillDTO>> GetCharacterSkillAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay) => service.GetCharacterSkillAsync(gameSessionInfo, gameCharacterDisplay);
        public Task<MonoResultDTO<GameCharacterSkillDTO>> UpdateCharacterSkillAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, string? modifyCategory, string oldSkill, string newSkill) => service.UpdateCharacterSkillAsync(gameSessionInfo, gameCharacterDisplay, modifyCategory, oldSkill, newSkill);
        [Obsolete("remove...")]
        public Task<MonoResultDTO<GameCharacterEquipmentDTO>> GetCharacterEquipmentAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay) => service.GetCharacterEquipmentAsync(gameSessionInfo, gameCharacterDisplay);
        [Obsolete("remove...")]
        public Task<MonoResultDTO<GameCharacterEquipmentDTO>> UpdateCharacterEquipmentAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, string? modifyCategory, string oldEquip, string newEquip) => service.UpdateCharacterEquipmentAsync(gameSessionInfo, gameCharacterDisplay, modifyCategory, oldEquip, newEquip);
        public Task<MonoResultDTO<GameMonsterDisplayDTO[]>> GetListMonsterDisplayAsync(GameSessionInfoDTO gameSessionInfo) => service.GetListMonsterDisplayAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameCharacterSkillDTO>> AddMonsterMemberAsync(GameSessionInfoDTO gameSessionInfo, string monsterObject) => service.AddMonsterMemberAsync(gameSessionInfo, monsterObject);
        public Task<MonoResultDTO<GameSkillDisplayDTO[]>> GetListSkillDisplayAsync(GameSessionInfoDTO gameSessionInfo) => service.GetListSkillDisplayAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameSkillDisplayDTO>> AddSkillDisplayAsync(GameSessionInfoDTO gameSessionInfo, GameSkillDisplayDTO gameSkillDisplay) => service.AddSkillDisplayAsync(gameSessionInfo, gameSkillDisplay);
        public Task<MonoResultDTO<GameSwitchDisplayDTO[]>> GetListSwitchDisplayAsync(GameSessionInfoDTO gameSessionInfo) => service.GetListSwitchDisplayAsync(gameSessionInfo);
        public Task<MonoResultDTO<GameSwitchDisplayDTO>> UpdateSwitchDisplayAsync(GameSessionInfoDTO gameSessionInfo, GameSwitchDisplayDTO gameSwitchInfo) => service.UpdateSwitchDisplayAsync(gameSessionInfo, gameSwitchInfo);
    }
}
