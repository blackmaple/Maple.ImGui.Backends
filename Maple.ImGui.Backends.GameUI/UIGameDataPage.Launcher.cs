using Hexa.NET.ImGui;
using System.Numerics;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.GameUI
{
    /// <summary>
    /// 负责启动入口、拖拽行为与提示消息的绘制。
    /// </summary>
    public partial class UIGameDataPage
    {
        private void RenderLauncherWindow()
        {
            if (!LauncherVisible)
            {
                return;
            }

            const float buttonSizeValue = 48.0f;
            var buttonSize = new Vector2(buttonSizeValue, buttonSizeValue);
            var menuFlags = ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoSavedSettings
                | ImGuiWindowFlags.NoFocusOnAppearing
                | ImGuiWindowFlags.NoNav;

            ImGuiApi.SetNextWindowPos(LauncherPosition, ImGuiCond.Always);
            ImGuiApi.SetNextWindowSize(buttonSize, ImGuiCond.Always);
            ImGuiApi.SetNextWindowBgAlpha(0.0f);

            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, 12.0f);
            ImGuiApi.PushStyleVar(ImGuiStyleVar.FrameRounding, 12.0f);
            ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
            ImGuiApi.PushStyleColor(ImGuiCol.Border, new Vector4(1f, 1f, 1f, 0.00f));
            if (!ImGuiApi.Begin("##SessionLauncher", menuFlags))
            {
                ImGuiApi.End();
                ImGuiApi.PopStyleColor(2);
                ImGuiApi.PopStyleVar(3);
                return;
            }

            HandleLauncherDrag();

            if (ImGuiApi.InvisibleButton("##SessionLauncherButton", buttonSize))
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
                    LauncherVisible = false;
                    if (!_sessionCollectionsRequest.IsRunning && SessionCollections.IsEmpty)
                    {
                        _sessionCollectionsRequest.TryStart(LoadSessionCollectionsAsync);
                    }
                }
            }

            var buttonMin = ImGuiApi.GetItemRectMin();
            RenderLauncherButtonImage(buttonMin, buttonSize, ImGuiApi.IsItemHovered(), ImGuiApi.IsItemActive());

            ImGuiApi.End();
            ImGuiApi.PopStyleColor(2);
            ImGuiApi.PopStyleVar(3);
        }

        private static void RenderLauncherButtonBackground(Vector2 buttonMin, Vector2 buttonSize, bool hovered, bool active)
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var buttonMax = buttonMin + buttonSize;
            const float rounding = 12.0f;
            var bgColor = active
                ? new Vector4(0.16f, 0.17f, 0.20f, 1.0f)
                : hovered
                    ? new Vector4(0.26f, 0.27f, 0.31f, 1.0f)
                    : new Vector4(0.21f, 0.22f, 0.26f, 1.0f);

            drawList.AddRectFilled(buttonMin, buttonMax, ImGuiApi.ColorConvertFloat4ToU32(bgColor), rounding);
            drawList.AddRect(
                buttonMin,
                buttonMax,
                ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1f, 1f, 1f, 0.08f)),
                rounding,
                ImDrawFlags.None,
                1.0f);
        }

        private void RenderLauncherButtonImage(Vector2 buttonMin, Vector2 buttonSize, bool hovered, bool active)
        {
            ImGuiApi.SetCursorScreenPos(buttonMin);
            if (TryDrawLauncher?.Invoke() != true)
            {
                RenderLauncherButtonBackground(buttonMin, buttonSize, hovered, active);
                DrawLauncherFallbackIcon(buttonMin, buttonSize);
            }
        }

        private static void DrawLauncherFallbackIcon(Vector2 buttonMin, Vector2 buttonSize)
        {
            var drawList = ImGuiApi.GetWindowDrawList();
            var center = buttonMin + (buttonSize * 0.5f);
            var featureColor = ImGuiApi.ColorConvertFloat4ToU32(new Vector4(1.0f, 1.0f, 1.0f, 0.95f));

            drawList.AddLine(center + new Vector2(-8.0f, -2.0f), center + new Vector2(-5.0f, -5.5f), featureColor, 2.0f);
            drawList.AddLine(center + new Vector2(-5.0f, -5.5f), center + new Vector2(-2.0f, -2.0f), featureColor, 2.0f);

            drawList.AddLine(center + new Vector2(2.0f, -2.0f), center + new Vector2(5.0f, -5.5f), featureColor, 2.0f);
            drawList.AddLine(center + new Vector2(5.0f, -5.5f), center + new Vector2(8.0f, -2.0f), featureColor, 2.0f);

            drawList.AddLine(center + new Vector2(-4.0f, 4.0f), center + new Vector2(4.0f, 4.0f), featureColor, 2.2f);
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
                var toastAnchor = MainWindowSize == Vector2.Zero
                    ? new Vector2(320, 36)
                    : MainWindowPosition + new Vector2(MainWindowSize.X * 0.5f, 14.0f);

                ImGuiApi.SetNextWindowPos(toastAnchor + new Vector2(0, i * 58), ImGuiCond.Always, new Vector2(0.5f, 0.0f));
                ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowRounding, 12.0f);
                ImGuiApi.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(18.0f, 14.0f));
                ImGuiApi.PushStyleColor(ImGuiCol.WindowBg, windowBg);
                ImGuiApi.PushStyleColor(ImGuiCol.Text, new Vector4(1, 1, 1, 1));
                if (ImGuiApi.Begin($"##Toast_{toast.Id}", toastFlags))
                {
                    ImGuiApi.TextUnformatted(toast.Message);
                }

                ImGuiApi.End();
                ImGuiApi.PopStyleVar(2);
                ImGuiApi.PopStyleColor(2);
            }
        }
    }
}
