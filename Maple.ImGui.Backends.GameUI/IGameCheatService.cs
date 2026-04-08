using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;

namespace Maple.ImGui.Backends.GameUI
{
    public interface IGameCheatService
    {
        Task<MonoResultDTO<GameSessionInfoDTO>> GetGameSessionInfoAsync();
        Task<MonoResultDTO<GameSessionInfoDTO>> LoadResourceAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameCurrencyDisplayDTO[]>> GetListCurrencyDisplayAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameCurrencyInfoDTO>> GetCurrencyInfoAsync(GameSessionInfoDTO gameSessionInfo, string gameCurrencyId, string? category = null);
        Task<MonoResultDTO<GameCurrencyInfoDTO>> UpdateCurrencyInfoAsync(GameSessionInfoDTO gameSessionInfo, GameCurrencyInfoDTO gameCurrency, string? category = null);
        Task<MonoResultDTO<GameInventoryDisplayDTO[]>> GetListInventoryDisplayAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameInventoryInfoDTO>> GetInventoryInfoAsync(GameSessionInfoDTO gameSessionInfo, string gameInventoryId, string? category);
        Task<MonoResultDTO<GameInventoryInfoDTO>> UpdateInventoryInfoAsync(GameSessionInfoDTO gameSessionInfo, string? category, GameInventoryInfoDTO gameInventory);
        Task<MonoResultDTO<GameCharacterDisplayDTO[]>> GetListCharacterDisplayAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameCharacterStatusDTO>> GetCharacterStatusAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay);
        Task<MonoResultDTO<GameCharacterStatusDTO>> UpdateCharacterStatusAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, GameSwitchDisplayDTO gameValueInfo);
        Task<MonoResultDTO<GameCharacterSkillDTO>> GetCharacterSkillAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay);
        Task<MonoResultDTO<GameCharacterSkillDTO>> UpdateCharacterSkillAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, string? modifyCategory, string oldSkill, string newSkill);
        [Obsolete("remove...")]
        Task<MonoResultDTO<GameCharacterEquipmentDTO>> GetCharacterEquipmentAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay);
        [Obsolete("remove...")]
        Task<MonoResultDTO<GameCharacterEquipmentDTO>> UpdateCharacterEquipmentAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, string? modifyCategory, string oldEquip, string newEquip);
        Task<MonoResultDTO<GameMonsterDisplayDTO[]>> GetListMonsterDisplayAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameCharacterSkillDTO>> AddMonsterMemberAsync(GameSessionInfoDTO gameSessionInfo, string monsterObject);
        Task<MonoResultDTO<GameSkillDisplayDTO[]>> GetListSkillDisplayAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameSkillDisplayDTO>> AddSkillDisplayAsync(GameSessionInfoDTO gameSessionInfo, GameSkillDisplayDTO gameSkillDisplay);
        Task<MonoResultDTO<GameSwitchDisplayDTO[]>> GetListSwitchDisplayAsync(GameSessionInfoDTO gameSessionInfo);
        Task<MonoResultDTO<GameSwitchDisplayDTO>> UpdateSwitchDisplayAsync(GameSessionInfoDTO gameSessionInfo, GameSwitchDisplayDTO gameSwitchInfo);
    }
}
