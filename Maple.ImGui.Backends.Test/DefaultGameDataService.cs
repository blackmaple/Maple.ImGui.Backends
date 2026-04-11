using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;

namespace Maple.ImGui.Backends.GameUI
{
    public class DefaultGameDataService(IGameWebApiControllers apiControllers) : IGameDataService
    {
        IGameWebApiControllers Controllers { get; } = apiControllers;

        private static MonoResultDTO<T> GetErrorResult<T>(Exception ex)
        {
            return ex switch
            {
                MonoCommonException bizEx => MonoResultDTO.GetBizError<T>(bizEx),
                _ => MonoResultDTO.GetSystemError<T>(ex.Message)
            };
        }

        public async Task<MonoResultDTO<GameCharacterSkillDTO>> AddMonsterMemberAsync(GameSessionInfoDTO gameSessionInfo, string monsterObject)
        {
            try
            {

                var data = await this.Controllers.AddMonsterMemberAsync(new GameMonsterObjectDTO() { Session = gameSessionInfo.ObjectId, MonsterObject = monsterObject }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (MonoCommonException ex)
            {
                return MonoResultDTO.GetBizError<GameCharacterSkillDTO>(ex);
            }
            catch (Exception ex)
            {
                return MonoResultDTO.GetSystemError<GameCharacterSkillDTO>(ex.Message);
            }
        }

        public async Task<MonoResultDTO<GameSkillDisplayDTO>> AddSkillDisplayAsync(GameSessionInfoDTO gameSessionInfo, GameSkillDisplayDTO gameSkillDisplay)
        {
            try
            {
                var data = await this.Controllers.AddSkillDisplayAsync(new GameSkillObjectDTO() { Session = gameSessionInfo.ObjectId, SkillObject = gameSkillDisplay.ObjectId, SkillCategory = gameSkillDisplay.DisplayCategory }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameSkillDisplayDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterEquipmentDTO>> GetCharacterEquipmentAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay)
        {
            try
            {
                var data = await this.Controllers.GetCharacterEquipmentAsync(new GameCharacterObjectDTO() { Session = gameSessionInfo.ObjectId, CharacterId = gameCharacterDisplay.ObjectId, CharacterCategory = gameCharacterDisplay.DisplayCategory }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterEquipmentDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterSkillDTO>> GetCharacterSkillAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay)
        {
            try
            {
                var data = await this.Controllers.GetCharacterSkillAsync(new GameCharacterObjectDTO() { Session = gameSessionInfo.ObjectId, CharacterId = gameCharacterDisplay.ObjectId, CharacterCategory = gameCharacterDisplay.DisplayCategory }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterSkillDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterStatusDTO>> GetCharacterStatusAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay)
        {
            try
            {
                var data = await this.Controllers.GetCharacterStatusAsync(new GameCharacterObjectDTO() { Session = gameSessionInfo.ObjectId, CharacterId = gameCharacterDisplay.ObjectId, CharacterCategory = gameCharacterDisplay.DisplayCategory }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterStatusDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCurrencyInfoDTO>> GetCurrencyInfoAsync(GameSessionInfoDTO gameSessionInfo, string gameCurrencyId, string? category = null)
        {
            try
            {
                var data = await this.Controllers.GetCurrencyInfoAsync(new GameCurrencyObjectDTO() { Session = gameSessionInfo.ObjectId, CurrencyObject = gameCurrencyId, CurrencyCategory = category }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCurrencyInfoDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameSessionInfoDTO>> GetGameSessionInfoAsync()
        {
            try
            {
                var data = await this.Controllers.GetSessionInfoAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameSessionInfoDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameInventoryInfoDTO>> GetInventoryInfoAsync(GameSessionInfoDTO gameSessionInfo, string gameInventoryId, string? category)
        {
            try
            {
                var data = await this.Controllers.GetInventoryInfoAsync(new GameInventoryObjectDTO() { Session = gameSessionInfo.ObjectId, InventoryObject = gameInventoryId, InventoryCategory = category }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameInventoryInfoDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterDisplayDTO[]>> GetListCharacterDisplayAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.GetListCharacterDisplayAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterDisplayDTO[]>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCurrencyDisplayDTO[]>> GetListCurrencyDisplayAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.GetListCurrencyDisplayAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCurrencyDisplayDTO[]>(ex);
            }
        }

        public async Task<MonoResultDTO<GameInventoryDisplayDTO[]>> GetListInventoryDisplayAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.GetListInventoryDisplayAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameInventoryDisplayDTO[]>(ex);
            }
        }

        public async Task<MonoResultDTO<GameMonsterDisplayDTO[]>> GetListMonsterDisplayAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.GetListMonsterDisplayAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameMonsterDisplayDTO[]>(ex);
            }
        }

        public async Task<MonoResultDTO<GameSkillDisplayDTO[]>> GetListSkillDisplayAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.GetListSkillDisplayAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameSkillDisplayDTO[]>(ex);
            }
        }

        public async Task<MonoResultDTO<GameSwitchDisplayDTO[]>> GetListSwitchDisplayAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.GetListSwitchDisplayAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameSwitchDisplayDTO[]>(ex);
            }
        }

        public async Task<MonoResultDTO<GameSessionInfoDTO>> LoadResourceAsync(GameSessionInfoDTO gameSessionInfo)
        {
            try
            {
                var data = await this.Controllers.LoadResourceAsync().ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameSessionInfoDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterEquipmentDTO>> UpdateCharacterEquipmentAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, string? modifyCategory, string oldEquip, string newEquip)
        {
            try
            {
                var data = await this.Controllers.UpdateCharacterEquipmentAsync(new GameCharacterModifyDTO() { Session = gameSessionInfo.ObjectId, CharacterId = gameCharacterDisplay.ObjectId, CharacterCategory = gameCharacterDisplay.DisplayCategory, ModifyCategory = modifyCategory, ModifyObject = oldEquip, NewValue = newEquip }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterEquipmentDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterSkillDTO>> UpdateCharacterSkillAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, string? modifyCategory, string oldSkill, string newSkill)
        {
            try
            {
                var data = await this.Controllers.UpdateCharacterSkillAsync(new GameCharacterModifyDTO() { Session = gameSessionInfo.ObjectId, CharacterId = gameCharacterDisplay.ObjectId, CharacterCategory = gameCharacterDisplay.DisplayCategory, ModifyCategory = modifyCategory, ModifyObject = oldSkill, NewValue = newSkill }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterSkillDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCharacterStatusDTO>> UpdateCharacterStatusAsync(GameSessionInfoDTO gameSessionInfo, GameCharacterDisplayDTO gameCharacterDisplay, GameSwitchDisplayDTO gameValueInfo)
        {
            try
            {
                var data = await this.Controllers.UpdateCharacterStatusAsync(new GameCharacterModifyDTO() { Session = gameSessionInfo.ObjectId, CharacterId = gameCharacterDisplay.ObjectId, CharacterCategory = gameCharacterDisplay.DisplayCategory, ModifyCategory = gameValueInfo.DisplayCategory, ModifyObject = gameValueInfo.ObjectId, NewValue = gameValueInfo.ContentValue }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCharacterStatusDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameCurrencyInfoDTO>> UpdateCurrencyInfoAsync(GameSessionInfoDTO gameSessionInfo, GameCurrencyInfoDTO gameCurrency, string? category = null)
        {
            try
            {
                var data = await this.Controllers.UpdateCurrencyInfoAsync(new GameCurrencyModifyDTO() { Session = gameSessionInfo.ObjectId, CurrencyObject = gameCurrency.ObjectId, CurrencyCategory = category, NewValue = gameCurrency.DisplayValue, IntValue = int.TryParse(gameCurrency.DisplayValue, out var intValue) ? intValue : 0 }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameCurrencyInfoDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameInventoryInfoDTO>> UpdateInventoryInfoAsync(GameSessionInfoDTO gameSessionInfo, string? category, GameInventoryInfoDTO gameInventory)
        {
            try
            {
                var data = await this.Controllers.UpdateInventoryInfoAsync(new GameInventoryModifyDTO() { Session = gameSessionInfo.ObjectId, InventoryObject = gameInventory.ObjectId, InventoryCategory = category, NewValue = gameInventory.InventoryCount.ToString(), InventoryCount = gameInventory.InventoryCount }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameInventoryInfoDTO>(ex);
            }
        }

        public async Task<MonoResultDTO<GameSwitchDisplayDTO>> UpdateSwitchDisplayAsync(GameSessionInfoDTO gameSessionInfo, GameSwitchDisplayDTO gameSwitchInfo)
        {
            try
            {
                var data = await this.Controllers.UpdateSwitchDisplayAsync(new GameSwitchModifyDTO() { Session = gameSessionInfo.ObjectId, SwitchObjectId = gameSwitchInfo.ObjectId, ContentValue = gameSwitchInfo.ContentValue }).ConfigureAwait(false);
                return MonoResultDTO.GetOk(data);
            }
            catch (Exception ex)
            {
                return GetErrorResult<GameSwitchDisplayDTO>(ex);
            }
        }
    }
}
