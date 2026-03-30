using Hexa.NET.ImGui;
using Maple.ImGui.Backends;
using Maple.MonoGameAssistant.GameCore;
using Maple.MonoGameAssistant.GameDTO;
using Maple.MonoGameAssistant.Model;
using System.Numerics;
using System.Text;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.Test
{
    internal class UIPageManager(GameHttpClientService service) : IImGuiCustomRender
    {
        private readonly ImGuiAsyncRequest<AsyncFetchResult<GameSessionInfoDTO?>> _gameSessionInfoRequest = new();
        private readonly ImGuiAsyncRequest<SessionCollectionsData> _sessionCollectionsRequest = new();
        private const string GameSessionHelpPopupName = "Game Session Help";
        private const float SessionTitleBarHeight = 44.0f;
        private const float SessionWindowViewportRatio = 0.9f;
        private const float SessionContentLeftMargin = 16.0f;
        private const float SessionContentTopMargin = 12.0f;
        private const float SessionContentRightMargin = 24.0f;
        private const float SessionContentBottomMargin = 24.0f;
        private const float DisplayCardHeight = 88.0f;
        private const float DisplayCardSpacing = 12.0f;
        private const float DisplayCardIconSize = 28.0f;
        private const float DisplayCardActionButtonWidth = 36.0f;
        private const int SearchInputBufferSize = 1024;

        GameHttpClientService Service { get; } = service;
        List<UiToast> Toasts { get; } = [];

        bool LauncherVisible { get; set; } = true;
        bool ShowSessionWindow { get; set; }
        bool PendingInitializeSessionWindow { get; set; }
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

        SearchState CurrencySearch { get; } = new();
        SearchState InventorySearch { get; } = new();
        SearchState CharacterSearch { get; } = new();
        SearchState MonsterSearch { get; } = new();
        SearchState SkillSearch { get; } = new();
        SearchState SwitchSearch { get; } = new();

        public void RaiseRender()
        {
            UpdateAsyncRequests();
            RenderSessionWindow();
            RenderLauncherWindow();
            RenderToasts();
        }

        private void UpdateAsyncRequests()
        {
            _gameSessionInfoRequest.Update(
                onSuccess: result =>
                {
                    if (result.IsSuccess && result.Data is not null)
                    {
                        GameSessionInfo = result.Data;
                        ShowSessionWindow = true;
                        PendingInitializeSessionWindow = true;
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
                    AddToast("Session display lists loaded.", UiToastKind.Success);
                },
                onError: exception =>
                {
                    SessionCollections = SessionCollectionsData.Empty;
                    AddToast(exception.Message, UiToastKind.Error);
                });
        }

        private void RenderSessionWindow()
        {
            if (!ShowSessionWindow)
            {
                return;
            }

            if (PendingInitializeSessionWindow)
            {
                var hostWindowSize = GetHostWindowSize(ImGuiApi.GetIO());
                var sessionWindowSize = GetSessionWindowSize(hostWindowSize);
                var sessionWindowPos = GetSessionWindowPosition(hostWindowSize, sessionWindowSize);
                ImGuiApi.SetNextWindowPos(sessionWindowPos, ImGuiCond.Appearing);
                ImGuiApi.SetNextWindowSize(sessionWindowSize, ImGuiCond.Appearing);
                PendingInitializeSessionWindow = false;
            }

            var isOpen = true;
            var windowFlags = ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoTitleBar;

            const string sessionWindowTitle = "###GameSessionWindow";

            if (!ImGuiApi.Begin(sessionWindowTitle, ref isOpen, windowFlags))
            {
                ImGuiApi.End();
                if (!isOpen)
                {
                    ShowSessionWindow = false;
                    LauncherVisible = true;
                }
                return;
            }

            MainWindowPosition = ImGuiApi.GetWindowPos();
            MainWindowSize = ImGuiApi.GetWindowSize();

            RenderSessionTitleBar(ref isOpen);
            var contentPosition = new Vector2(SessionContentLeftMargin, SessionTitleBarHeight + SessionContentTopMargin);
            var contentSize = new Vector2(
                MathF.Max(1.0f, MainWindowSize.X - SessionContentLeftMargin - SessionContentRightMargin),
                MathF.Max(1.0f, MainWindowSize.Y - SessionTitleBarHeight - SessionContentTopMargin - SessionContentBottomMargin));
            ImGuiApi.SetCursorPos(contentPosition);

            if (_sessionCollectionsRequest.IsRunning)
            {
                ImGuiApi.TextUnformatted("Loading display lists...");
            }

            if (PendingOpenGameSessionHelpPopup)
            {
                ImGuiApi.OpenPopup(GameSessionHelpPopupName);
                PendingOpenGameSessionHelpPopup = false;
            }

            if (ImGuiApi.BeginChild("##SessionWindowContent", contentSize, ImGuiChildFlags.AlwaysUseWindowPadding))
            {
                if (_sessionCollectionsRequest.IsRunning)
                {
                    ImGuiApi.Separator();
                }

                RenderGameSessionTabs();
            }

            ImGuiApi.EndChild();
            RenderGameSessionInfoHelpDialog();

            ImGuiApi.End();

            if (!isOpen)
            {
                ShowSessionWindow = false;
                LauncherVisible = true;
            }
        }

        private void RenderSessionTitleBar(ref bool isOpen)
        {
            var title = string.IsNullOrWhiteSpace(GameSessionInfo?.DisplayName)
                ? "Game Session"
                : GameSessionInfo.DisplayName;

            var drawList = ImGuiApi.GetWindowDrawList();
            var titleBarMin = MainWindowPosition;
            var titleBarMax = MainWindowPosition + new Vector2(MainWindowSize.X, SessionTitleBarHeight);
            drawList.AddRectFilled(titleBarMin, titleBarMax, ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.10f, 0.12f, 0.16f, 0.98f)));
            drawList.AddLine(
                new Vector2(titleBarMin.X, titleBarMax.Y),
                new Vector2(titleBarMax.X, titleBarMax.Y),
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.12f)));

            var buttonSize = new Vector2(28, 28);
            const float helpX = 16.0f;
            var closeX = helpX + buttonSize.X + 8.0f;

            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(1f, 1f, 1f, 1f));
            ImGuiApi.SetCursorPos(new Vector2(closeX + buttonSize.X + 16.0f, 10));
            ImGuiApi.TextUnformatted(title);
            ImGuiApi.PopStyleColor();

            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 999.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, new Vector4(0.22f, 0.26f, 0.34f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.30f, 0.35f, 0.45f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.18f, 0.22f, 0.30f, 1.0f));
            ImGuiApi.SetCursorPos(new Vector2(helpX, 8));
            if (ImGuiApi.Button("H", buttonSize))
            {
                PendingOpenGameSessionHelpPopup = true;
            }
            ImGuiApi.PopStyleColor(3);
            ImGuiApi.PopStyleVar();

            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 999.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, new Vector4(0.74f, 0.20f, 0.20f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.86f, 0.28f, 0.28f, 1.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.62f, 0.16f, 0.16f, 1.0f));
            ImGuiApi.SetCursorPos(new Vector2(closeX, 8));
            if (ImGuiApi.Button("X", buttonSize))
            {
                isOpen = false;
            }
            ImGuiApi.PopStyleColor(3);
            ImGuiApi.PopStyleVar();
        }

        private void RenderGameSessionTabs()
        {
            if (!ImGuiApi.BeginTabBar("GameSessionTabs"))
            {
                return;
            }

            RenderDisplayTab("Currency", SessionCollections.Currencies, CurrencySearch);
            RenderDisplayTab("Inventory", SessionCollections.Inventories, InventorySearch);
            RenderDisplayTab("Character", SessionCollections.Characters, CharacterSearch);
            RenderDisplayTab("Monster", SessionCollections.Monsters, MonsterSearch);
            RenderDisplayTab("Skill", SessionCollections.Skills, SkillSearch);
            RenderDisplayTab("Switch", SessionCollections.Switches, SwitchSearch);

            ImGuiApi.EndTabBar();
        }

        private void RenderDisplayTab<TDisplay>(string tabName, TDisplay[] items, SearchState searchState) where TDisplay : GameDisplayDTO
        {
            if (!ImGuiApi.BeginTabItem(tabName))
            {
                return;
            }

            RenderTabToolbar(tabName, searchState);
            var filteredItems = FilterDisplays(items, searchState.AppliedText);
            RenderDisplayCards(tabName, filteredItems);
            ImGuiApi.EndTabItem();
        }

        private void RenderTabToolbar(string tabName, SearchState searchState)
        {
            ImGuiApi.TextUnformatted("Search:");
            ImGuiApi.SameLine();
            var inputWidth = MathF.Max(180.0f, ImGuiApi.GetContentRegionAvail().X - 152.0f);
            RenderSearchInput($"##{tabName}_SearchInput", searchState, inputWidth);
            ImGuiApi.SameLine();
            if (ImGuiApi.Button($"Search##{tabName}"))
            {
                searchState.AppliedText = searchState.InputText;
            }

            ImGuiApi.SameLine();
            if (ImGuiApi.Button($"Reload##{tabName}") && GameSessionInfo is not null && !_sessionCollectionsRequest.IsRunning)
            {
                _sessionCollectionsRequest.TryStart(LoadSessionCollectionsAsync);
            }

            ImGuiApi.Separator();
        }

        private void RenderDisplayCards<TDisplay>(string tabName, TDisplay[] items) where TDisplay : GameDisplayDTO
        {
            var childSize = ImGuiApi.GetContentRegionAvail();
            if (!ImGuiApi.BeginChild($"##{tabName}Cards", childSize, ImGuiChildFlags.Borders))
            {
                ImGuiApi.EndChild();
                return;
            }

            var clipper = new ImGuiListClipper();
            clipper.Begin(items.Length, DisplayCardHeight + DisplayCardSpacing);
            while (clipper.Step())
            {
                for (var i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
                {
                    RenderDisplayCard(tabName, items[i], i, childSize.X);
                    ImGuiApi.Dummy(new Vector2(0, DisplayCardSpacing));
                }
            }
            clipper.End();

            ImGuiApi.EndChild();
        }

        private void RenderDisplayCard<TDisplay>(string tabName, TDisplay item, int index, float availableWidth) where TDisplay : GameDisplayDTO
        {
            var cardWidth = MathF.Max(1.0f, availableWidth - 4.0f);
            if (!ImGuiApi.BeginChild($"##{tabName}_Card_{item.ObjectId}_{index}", new Vector2(cardWidth, DisplayCardHeight), ImGuiChildFlags.Borders))
            {
                ImGuiApi.EndChild();
                return;
            }

            var drawList = ImGuiApi.GetWindowDrawList();
            var windowPos = ImGuiApi.GetWindowPos();
            var iconOffset = new Vector2(14.0f, 18.0f);
            var iconMin = windowPos + iconOffset;
            var iconMax = iconMin + new Vector2(DisplayCardIconSize, DisplayCardIconSize);
            var iconColor = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(0.88f, 0.88f, 0.88f, 1.0f));
            drawList.AddRect(iconMin, iconMax, iconColor, 0.0f, ImDrawFlags.None, 1.5f);
            drawList.AddRect(iconMin + new Vector2(4.0f, 4.0f), iconMax - new Vector2(4.0f, 4.0f), iconColor, 0.0f, ImDrawFlags.None, 1.0f);

            const float actionButtonSize = 28.0f;
            var actionButtonPosition = new Vector2(cardWidth - DisplayCardActionButtonWidth - 10.0f, 22.0f);
            var textStartX = iconOffset.X + DisplayCardIconSize + 16.0f;
            var textWidth = MathF.Max(120.0f, actionButtonPosition.X - textStartX - 12.0f);

            ImGuiApi.SetCursorPos(new Vector2(textStartX, 12.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.96f, 0.96f, 0.96f, 1.0f));
            ImGuiApi.PushTextWrapPos(textStartX + textWidth);
            ImGuiApi.TextUnformatted(item.DisplayName ?? string.Empty);
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();

            ImGuiApi.SetCursorPos(new Vector2(textStartX, 38.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(0.65f, 0.65f, 0.65f, 1.0f));
            ImGuiApi.PushTextWrapPos(textStartX + textWidth);
            ImGuiApi.TextUnformatted(item.DisplayDesc ?? item.ObjectId ?? string.Empty);
            ImGuiApi.PopTextWrapPos();
            ImGuiApi.PopStyleColor();

            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 6.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.Button, new Vector4(0.92f, 0.40f, 0.02f, 0.18f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.92f, 0.40f, 0.02f, 0.35f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.92f, 0.40f, 0.02f, 0.50f));
            ImGuiApi.SetCursorPos(actionButtonPosition);
            if (ImGuiApi.Button($">##{tabName}_{item.ObjectId}_{index}", new Vector2(actionButtonSize, actionButtonSize)))
            {
                AddToast($"Edit {item.DisplayName ?? item.ObjectId ?? tabName} not implemented.", UiToastKind.Success);
            }
            ImGuiApi.PopStyleColor(3);
            ImGuiApi.PopStyleVar();

            ImGuiApi.EndChild();
        }

        private static Vector2 GetHostWindowSize(ImGuiIOPtr io)
        {
            return ImGuiWin32InputBridge.TryGetClientSize(out var clientSize)
                ? clientSize
                : io.DisplaySize;
        }

        private static Vector2 GetSessionWindowSize(Vector2 hostWindowSize)
        {
            return new Vector2(
                MathF.Max(1.0f, hostWindowSize.X * SessionWindowViewportRatio),
                MathF.Max(1.0f, hostWindowSize.Y * SessionWindowViewportRatio));
        }

        private static Vector2 GetSessionWindowPosition(Vector2 hostWindowSize, Vector2 sessionWindowSize)
        {
          //  return Vector2.Zero; ;
            return new Vector2(
                MathF.Max(0.0f, (hostWindowSize.X - sessionWindowSize.X) * 0.5f),
                MathF.Max(0.0f, (hostWindowSize.Y - sessionWindowSize.Y) * 0.5f));
        }

        private static unsafe void RenderSearchInput(string label, SearchState searchState, float width)
        {
            ImGuiApi.SetNextItemWidth(width);
            fixed (byte* inputBuffer = searchState.InputBuffer)
            {
                if (ImGuiApi.InputText(label, inputBuffer, (nuint)searchState.InputBuffer.Length))
                {
                    searchState.UpdateInputText();
                }
            }
        }

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

        private static async Task<TDisplay[]> LoadCollectionAsync<TDisplay>(GameSessionInfoDTO session, Func<GameSessionInfoDTO, Task<MonoResultDTO<TDisplay[]>>> loader) where TDisplay : class
        {
            var result = await loader(session).ConfigureAwait(false);
            if (result.TryGet(out var data) && data is not null)
            {
                return data;
            }

            return [];
        }

        private void RenderGameSessionInfoHelpDialog()
        {
            if (!ImGuiApi.BeginPopupModal(GameSessionHelpPopupName, ImGuiWindowFlags.AlwaysAutoResize))
            {
                return;
            }

            ImGuiApi.TextUnformatted($"DisplayName: {GameSessionInfo?.DisplayName ?? string.Empty}");
            ImGuiApi.TextUnformatted($"DisplayDesc: {GameSessionInfo?.DisplayDesc ?? string.Empty}");
            ImGuiApi.TextUnformatted($"ApiVer: {GameSessionInfo?.ApiVer ?? string.Empty}");
            ImGuiApi.TextUnformatted($"QQ: {GameSessionInfo?.QQ ?? string.Empty}");

            if (ImGuiApi.Button("Close"))
            {
                ImGuiApi.CloseCurrentPopup();
            }

            ImGuiApi.EndPopup();
        }

        private void RenderLauncherWindow()
        {
            if (!LauncherVisible)
            {
                return;
            }

            const float buttonSizeValue = 60.0f;
            var buttonSize = new Vector2(buttonSizeValue, buttonSizeValue);
            var menuFlags = ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoSavedSettings
                | ImGuiWindowFlags.NoFocusOnAppearing
                | ImGuiWindowFlags.NoNav;

            ImGuiApi.SetNextWindowPos(LauncherPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(buttonSize, ImGuiCond.Always);

            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, 10.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 10.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(1f, 1f, 1f, 0.00f));
            ImGuiApi.PushStyleColor(ImGuiCol.Button, new Vector4(0.20f, 0.55f, 0.95f, 0.22f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.30f, 0.65f, 1.00f, 0.48f));
            ImGuiApi.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.18f, 0.48f, 0.88f, 0.58f));
            if (!ImGuiApi.Begin("##SessionLauncher", menuFlags))
            {
                ImGuiApi.End();
                ImGuiApi.PopStyleColor(5);
                ImGuiApi.PopStyleVar(3);
                return;
            }

            HandleLauncherDrag();

            var buttonText = _gameSessionInfoRequest.IsRunning ? GetLoadingText() : "GO";
            if (ImGuiApi.Button(buttonText, buttonSize))
            {
                if (GameSessionInfo is null)
                {
                    if (!_gameSessionInfoRequest.IsRunning)
                    {
                        _gameSessionInfoRequest.TryStart(GetGameSessionInfoAsync);
                    }
                }
                else
                {
                    ShowSessionWindow = true;
                    PendingInitializeSessionWindow = false;
                    LauncherVisible = false;
                    if (!_sessionCollectionsRequest.IsRunning && SessionCollections.IsEmpty)
                    {
                        _sessionCollectionsRequest.TryStart(LoadSessionCollectionsAsync);
                    }
                }
            }

            ImGuiApi.End();
            ImGuiApi.PopStyleColor(5);
            ImGuiApi.PopStyleVar(3);
        }

        private void HandleLauncherDrag()
        {
            var io = ImGuiApi.GetIO();
            var mousePosition = io.MousePos;

            if (!IsDraggingLauncher && ImGuiApi.IsWindowHovered() && ImGuiApi.IsMouseClicked(ImGuiMouseButton.Right))
            {
                IsDraggingLauncher = true;
                LauncherDragOffset = mousePosition - LauncherPosition;
            }

            if (IsDraggingLauncher)
            {
                if (ImGuiApi.IsMouseDown(ImGuiMouseButton.Right))
                {
                    LauncherPosition = mousePosition - LauncherDragOffset;
                }
                else
                {
                    IsDraggingLauncher = false;
                }
            }
        }

        private string GetLoadingText()
        {
            LoadingFrame = (LoadingFrame + 1) % 36;
            var dotCount = 1 + (LoadingFrame / 12);
            return dotCount switch
            {
                1 => ".",
                2 => "..",
                _ => "..."
            };
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

        private void RenderToasts()
        {
            Toasts.RemoveAll(static x => x.ExpireAt <= DateTime.UtcNow);
            if (Toasts.Count == 0)
            {
                return;
            }

            var toastFlags = ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoSavedSettings
                | ImGuiWindowFlags.NoFocusOnAppearing
                | ImGuiWindowFlags.NoNav
                | ImGuiWindowFlags.AlwaysAutoResize;

            for (var i = 0; i < Toasts.Count; i++)
            {
                var toast = Toasts[i];
                var windowBg = toast.Kind == UiToastKind.Success
                    ? new Vector4(0.10f, 0.35f, 0.16f, 0.95f)
                    : new Vector4(0.45f, 0.12f, 0.12f, 0.95f);

                var toastCenter = MainWindowSize == Vector2.Zero
                    ? new Vector2(320, 180)
                    : MainWindowPosition + MainWindowSize * 0.5f;

                ImGuiApi.SetNextWindowPos(toastCenter + new Vector2(0, i * 64), ImGuiCond.Always, new Vector2(0.5f, 0.5f));
                ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, windowBg);
                ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(1, 1, 1, 1));
                if (ImGuiApi.Begin($"##Toast_{toast.Id}", toastFlags))
                {
                    ImGuiApi.TextUnformatted(toast.Message);
                }

                ImGuiApi.End();
                ImGuiApi.PopStyleColor(2);
            }
        }

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

        private sealed class SearchState
        {
            public byte[] InputBuffer = new byte[SearchInputBufferSize];
            public string InputText = string.Empty;
            public string AppliedText = string.Empty;

            public void UpdateInputText()
            {
                var nullIndex = Array.IndexOf(InputBuffer, (byte)0);
                var byteCount = nullIndex >= 0 ? nullIndex : InputBuffer.Length;
                InputText = Encoding.UTF8.GetString(InputBuffer, 0, byteCount);
            }
        }

        private sealed class SessionCollectionsData
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

        private sealed record UiToast(int Id, UiToastKind Kind, string Message, DateTime ExpireAt);
    }
}
