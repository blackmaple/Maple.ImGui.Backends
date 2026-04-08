using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责会话列表数据的加载、刷新、筛选与排序。
    /// </summary>
    public partial class UIGameCheatPage
    {
        private static TDisplay[] FilterDisplays<TDisplay>(TDisplay[] items, string searchText) where TDisplay : GameDisplayDTO
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return items;
            }

            return [.. items.Where(x =>
                (x.DisplayName?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false)
                || (x.DisplayDesc?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false)
                || (x.ObjectId?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false))];
        }

        private static TDisplay[] SortDisplays<TDisplay>(TDisplay[] items) where TDisplay : GameDisplayDTO
        {
            return
            [
                .. items
                    .OrderBy(static x => x is GameObjectDisplayDTO objectDisplay ? objectDisplay.DisplayCategory ?? string.Empty : string.Empty, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(x => x.DisplayName ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(x => x.ObjectId ?? string.Empty, StringComparer.OrdinalIgnoreCase)
            ];
        }

        private static int GetDisplayCount(SessionCollectionsData collections, SessionTab tab)
        {
            return tab switch
            {
                SessionTab.Currency => collections.Currencies.Length,
                SessionTab.Inventory => collections.Inventories.Length,
                SessionTab.Character => collections.Characters.Length,
                SessionTab.Monster => collections.Monsters.Length,
                SessionTab.Skill => collections.Skills.Length,
                SessionTab.Switch => collections.Switches.Length,
                _ => 0
            };
        }

        private static int GetTotalDisplayCount(SessionCollectionsData collections)
        {
            return collections.Currencies.Length
                + collections.Inventories.Length
                + collections.Characters.Length
                + collections.Monsters.Length
                + collections.Skills.Length
                + collections.Switches.Length;
        }

        private async Task<SessionCollectionsData> LoadSessionCollectionsAsync()
        {
            var session = GameSessionInfo;
            if (session is null)
            {
                return SessionCollectionsData.Empty;
            }

            var currencyTask = LoadCollectionAsync(session, Service.GetListCurrencyDisplayAsync);
            var inventoryTask = LoadCollectionAsync(session, Service.GetListInventoryDisplayAsync);
            var characterTask = LoadCollectionAsync(session, Service.GetListCharacterDisplayAsync);
            var monsterTask = LoadCollectionAsync(session, Service.GetListMonsterDisplayAsync);
            var skillTask = LoadCollectionAsync(session, Service.GetListSkillDisplayAsync);
            var switchTask = LoadCollectionAsync(session, Service.GetListSwitchDisplayAsync);

            await Task.WhenAll(currencyTask, inventoryTask, characterTask, monsterTask, skillTask, switchTask).ConfigureAwait(false);

            return new SessionCollectionsData
            {
                Currencies = currencyTask.Result,
                Inventories = inventoryTask.Result,
                Characters = characterTask.Result,
                Monsters = monsterTask.Result,
                Skills = skillTask.Result,
                Switches = switchTask.Result
            };
        }

        private async Task<SessionCollectionsData> ReloadCurrentTabAsync(SessionTab tab)
        {
            var session = GameSessionInfo;
            if (session is null)
            {
                return SessionCollections;
            }

            return tab switch
            {
                SessionTab.Currency => SessionCollections with
                {
                    Currencies = await LoadCollectionAsync(session, Service.GetListCurrencyDisplayAsync).ConfigureAwait(false)
                },
                SessionTab.Inventory => SessionCollections with
                {
                    Inventories = await LoadCollectionAsync(session, Service.GetListInventoryDisplayAsync).ConfigureAwait(false)
                },
                SessionTab.Character => SessionCollections with
                {
                    Characters = await LoadCollectionAsync(session, Service.GetListCharacterDisplayAsync).ConfigureAwait(false)
                },
                SessionTab.Monster => SessionCollections with
                {
                    Monsters = await LoadCollectionAsync(session, Service.GetListMonsterDisplayAsync).ConfigureAwait(false)
                },
                SessionTab.Skill => SessionCollections with
                {
                    Skills = await LoadCollectionAsync(session, Service.GetListSkillDisplayAsync).ConfigureAwait(false)
                },
                SessionTab.Switch => SessionCollections with
                {
                    Switches = await LoadCollectionAsync(session, Service.GetListSwitchDisplayAsync).ConfigureAwait(false)
                },
                _ => SessionCollections
            };
        }

        private static async Task<TDisplay[]> LoadCollectionAsync<TDisplay>(GameSessionInfoDTO session, Func<GameSessionInfoDTO, Task<MonoResultDTO<TDisplay[]>>> loader) where TDisplay : class
        {
            var result = await loader(session).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return data;
            }

            return [];
        }
    }
}
