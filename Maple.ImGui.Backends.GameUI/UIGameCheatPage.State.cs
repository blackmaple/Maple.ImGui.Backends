using Maple.MonoGameAssistant.GameDTO;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 定义页面运行所需的状态对象、结果对象与内部模型。
    /// </summary>
    public partial class UIGameCheatPage
    {
        /// <summary>
        /// 表示货币编辑弹窗当前绑定的数据上下文。
        /// </summary>
        private sealed record CurrencyEditState(GameCurrencyInfoDTO Info, string? Category, string DisplayName, string DisplayDesc);

        /// <summary>
        /// 表示背包编辑弹窗当前绑定的数据上下文。
        /// </summary>
        private sealed record InventoryEditState(GameInventoryInfoDTO Info, string? Category, string DisplayName, string DisplayDesc);

        /// <summary>
        /// 表示角色属性对话框当前绑定的数据上下文。
        /// </summary>
        private sealed record CharacterStatusDialogState(GameCharacterStatusDTO Data, GameCharacterDisplayDTO Character, string DisplayName, string DisplayDesc);

        /// <summary>
        /// 表示角色技能对话框当前绑定的数据上下文。
        /// </summary>
        private sealed record CharacterSkillDialogState(GameCharacterSkillDTO Data, GameCharacterDisplayDTO Character, string DisplayName, string DisplayDesc);

        /// <summary>
        /// 表示角色技能加减操作的待确认上下文。
        /// </summary>
        private sealed record CharacterSkillActionConfirmState(string? ModifyCategory, string OldSkill, string NewSkill, string DisplayName, bool IsAdd);

        /// <summary>
        /// 表示怪物详情窗口当前绑定的数据上下文。
        /// </summary>
        private sealed record MonsterInfoDialogState(GameMonsterDisplayDTO Data, string DisplayName, string DisplayDesc);

        /// <summary>
        /// 表示怪物加入成员操作的待确认上下文。
        /// </summary>
        private sealed record MonsterAddConfirmState(GameMonsterDisplayDTO Monster, string DisplayName, string DisplayDesc);

        /// <summary>
        /// 表示角色技能选择窗口中的统一展示项。
        /// </summary>
        private sealed record CharacterSkillSelectorItem(string? ObjectId, string? DisplayCategory, string? DisplayName, string? DisplayDesc);

        /// <summary>
        /// 表示主界面 Switch 展示项更新后的结果上下文。
        /// </summary>
        private sealed record SwitchDisplayUpdateState(GameSwitchDisplayDTO Display, string Message);

        /// <summary>
        /// 表示一次 Switch 展示项编辑前的原始值快照，用于失败回滚。
        /// </summary>
        private sealed record SwitchDisplayOriginalValueState(GameSwitchDisplayDTO Target, string? ContentValue);

        /// <summary>
        /// 封装一次 ImGui 页面内的异步请求生命周期。
        /// </summary>
        private sealed class ImGuiAsyncRequest<TResult>
        {
            private Task<TResult>? _task;
            private bool _consumed = true;

            public bool IsRunning => _task is { IsCompleted: false };

            public bool TryStart(Func<Task<TResult>> factory)
            {
                if (IsRunning)
                {
                    return false;
                }

                _task = factory();
                _consumed = false;
                return true;
            }

            public void Update(Action<TResult> onSuccess, Action<Exception> onError)
            {
                if (_consumed || _task is null || !_task.IsCompleted)
                {
                    return;
                }

                _consumed = true;
                if (_task.IsCanceled)
                {
                    onError(new TaskCanceledException());
                    return;
                }

                if (_task.IsFaulted)
                {
                    onError(_task.Exception?.GetBaseException() ?? new InvalidOperationException("Unknown async request error."));
                    return;
                }

                onSuccess(_task.Result);
            }
        }

        /// <summary>
        /// 保存搜索输入框文本与已应用搜索文本。
        /// </summary>
        private sealed class SearchState
        {
            public string InputText = string.Empty;
            public string AppliedText = string.Empty;
        }

        /// <summary>
        /// 保存当前会话中各类展示列表的数据集合。
        /// </summary>
        private sealed record SessionCollectionsData
        {
            public static SessionCollectionsData Empty { get; } = new();

            public GameCurrencyDisplayDTO[] Currencies { get; init; } = [];
            public GameInventoryDisplayDTO[] Inventories { get; init; } = [];
            public GameCharacterDisplayDTO[] Characters { get; init; } = [];
            public GameMonsterDisplayDTO[] Monsters { get; init; } = [];
            public GameSkillDisplayDTO[] Skills { get; init; } = [];
            public GameSwitchDisplayDTO[] Switches { get; init; } = [];

            public bool IsEmpty => Currencies.Length == 0
                && Inventories.Length == 0
                && Characters.Length == 0
                && Monsters.Length == 0
                && Skills.Length == 0
                && Switches.Length == 0;
        }

        /// <summary>
        /// 表示一次异步业务请求的统一返回结果。
        /// </summary>
        private sealed record AsyncFetchResult<T>(bool IsSuccess, T? Data, string? Message)
        {
            public static AsyncFetchResult<T> Success(T? data, string? message = null) => new(true, data, message);

            public static AsyncFetchResult<T> Failure(string message) => new(false, default, message);
        }

        private enum UiToastKind
        {
            Success,
            Error
        }

        private enum SessionTab
        {
            Currency,
            Inventory,
            Character,
            Monster,
            Skill,
            Switch
        }

        /// <summary>
        /// 表示一个待显示的页面提示消息。
        /// </summary>
        private sealed record UiToast(int Id, UiToastKind Kind, string Message, DateTime ExpireAt);
    }
}
