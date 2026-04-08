using Hexa.NET.ImGui;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends
{
    public sealed partial class ImGuiWin32InputModule
    {
        private const uint WM_MOUSEWHEEL = 0x020A;
        private const uint WM_MOUSEHWHEEL = 0x020E;
        private const int WheelDelta = 120;

        private static readonly (int VirtualKey, ImGuiKey ImGuiKey)[] KeyMap =
        [
            (0x09, ImGuiKey.Tab),
            (0x25, ImGuiKey.LeftArrow),
            (0x27, ImGuiKey.RightArrow),
            (0x26, ImGuiKey.UpArrow),
            (0x28, ImGuiKey.DownArrow),
            (0x21, ImGuiKey.PageUp),
            (0x22, ImGuiKey.PageDown),
            (0x24, ImGuiKey.Home),
            (0x23, ImGuiKey.End),
            (0x2D, ImGuiKey.Insert),
            (0x2E, ImGuiKey.Delete),
            (0x08, ImGuiKey.Backspace),
            (0x20, ImGuiKey.Space),
            (0x0D, ImGuiKey.Enter),
            (0x1B, ImGuiKey.Escape),
            (0x11, ImGuiKey.LeftCtrl),
            (0xA2, ImGuiKey.LeftCtrl),
            (0xA3, ImGuiKey.RightCtrl),
            (0x10, ImGuiKey.LeftShift),
            (0xA0, ImGuiKey.LeftShift),
            (0xA1, ImGuiKey.RightShift),
            (0x12, ImGuiKey.LeftAlt),
            (0xA4, ImGuiKey.LeftAlt),
            (0xA5, ImGuiKey.RightAlt),
            (0x5B, ImGuiKey.LeftSuper),
            (0x5C, ImGuiKey.RightSuper),
            (0x5D, ImGuiKey.Menu),
            (0x30, ImGuiKey.Key0),
            (0x31, ImGuiKey.Key1),
            (0x32, ImGuiKey.Key2),
            (0x33, ImGuiKey.Key3),
            (0x34, ImGuiKey.Key4),
            (0x35, ImGuiKey.Key5),
            (0x36, ImGuiKey.Key6),
            (0x37, ImGuiKey.Key7),
            (0x38, ImGuiKey.Key8),
            (0x39, ImGuiKey.Key9),
            (0x41, ImGuiKey.A),
            (0x42, ImGuiKey.B),
            (0x43, ImGuiKey.C),
            (0x44, ImGuiKey.D),
            (0x45, ImGuiKey.E),
            (0x46, ImGuiKey.F),
            (0x47, ImGuiKey.G),
            (0x48, ImGuiKey.H),
            (0x49, ImGuiKey.I),
            (0x4A, ImGuiKey.J),
            (0x4B, ImGuiKey.K),
            (0x4C, ImGuiKey.L),
            (0x4D, ImGuiKey.M),
            (0x4E, ImGuiKey.N),
            (0x4F, ImGuiKey.O),
            (0x50, ImGuiKey.P),
            (0x51, ImGuiKey.Q),
            (0x52, ImGuiKey.R),
            (0x53, ImGuiKey.S),
            (0x54, ImGuiKey.T),
            (0x55, ImGuiKey.U),
            (0x56, ImGuiKey.V),
            (0x57, ImGuiKey.W),
            (0x58, ImGuiKey.X),
            (0x59, ImGuiKey.Y),
            (0x5A, ImGuiKey.Z),
            (0x70, ImGuiKey.F1),
            (0x71, ImGuiKey.F2),
            (0x72, ImGuiKey.F3),
            (0x73, ImGuiKey.F4),
            (0x74, ImGuiKey.F5),
            (0x75, ImGuiKey.F6),
            (0x76, ImGuiKey.F7),
            (0x77, ImGuiKey.F8),
            (0x78, ImGuiKey.F9),
            (0x79, ImGuiKey.F10),
            (0x7A, ImGuiKey.F11),
            (0x7B, ImGuiKey.F12)
        ];

        private readonly Dictionary<ImGuiKey, bool> _keyStates = [];
        private readonly object _syncRoot = new();
        private float _pendingWheelX;
        private float _pendingWheelY;

        public void HandleWindowMessage(uint uMsg, nuint wParam, nint lParam)
        {
            var wheel = uMsg switch
            {
                WM_MOUSEWHEEL => new Vector2(0.0f, GetWheelDelta(wParam) / (float)WheelDelta),
                WM_MOUSEHWHEEL => new Vector2(GetWheelDelta(wParam) / (float)WheelDelta, 0.0f),
                _ => Vector2.Zero,
            };

            if (wheel == Vector2.Zero)
            {
                return;
            }

            lock (_syncRoot)
            {
                _pendingWheelX += wheel.X;
                _pendingWheelY += wheel.Y;
            }
        }

        public void Update(ImGuiIOPtr io)
        {
            foreach (var (virtualKey, imGuiKey) in KeyMap)
            {
                var isDown = IsKeyDown(virtualKey);
                var hadPreviousState = _keyStates.TryGetValue(imGuiKey, out var previousState);
                if (hadPreviousState && previousState == isDown)
                {
                    continue;
                }

                _keyStates[imGuiKey] = isDown;
                io.AddKeyEvent(imGuiKey, isDown);
                if (isDown && (!hadPreviousState || !previousState))
                {
                    TryAddInputCharacter(io, virtualKey);
                }
            }

            float wheelX;
            float wheelY;
            lock (_syncRoot)
            {
                wheelX = _pendingWheelX;
                wheelY = _pendingWheelY;
                _pendingWheelX = 0.0f;
                _pendingWheelY = 0.0f;
            }

            if (wheelX != 0.0f || wheelY != 0.0f)
            {
                io.AddMouseWheelEvent(wheelX, wheelY);
            }
        }

        private static bool IsKeyDown(int virtualKey)
        {
            return (GetAsyncKeyState(virtualKey) & 0x8000) != 0;
        }

        private static short GetWheelDelta(nuint wParam)
        {
            return unchecked((short)(((ulong)wParam >> 16) & 0xFFFF));
        }

        private static unsafe void TryAddInputCharacter(ImGuiIOPtr io, int virtualKey)
        {
            Span<byte> keyboardState = stackalloc byte[256];
            fixed (byte* pKeyboardState = keyboardState)
            {
                if (!GetKeyboardState(pKeyboardState))
                {
                    return;
                }

                Span<char> chars = stackalloc char[4];
                fixed (char* pChars = chars)
                {
                    var scanCode = MapVirtualKey((uint)virtualKey, 0);
                    var count = ToUnicode((uint)virtualKey, scanCode, pKeyboardState, pChars, chars.Length, 0);
                    if (count <= 0)
                    {
                        return;
                    }

                    io.AddInputCharactersUTF8(new string(pChars, 0, count));
                }
            }
        }

        [LibraryImport("user32.dll")]
        private static partial short GetAsyncKeyState(int vKey);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static unsafe partial bool GetKeyboardState(byte* lpKeyState);

        [LibraryImport("user32.dll", EntryPoint = "MapVirtualKeyW")]
        private static partial uint MapVirtualKey(uint uCode, uint uMapType);

        [LibraryImport("user32.dll")]
        private static unsafe partial int ToUnicode(uint wVirtKey, uint wScanCode, byte* lpKeyState, char* pwszBuff, int cchBuff, uint wFlags);
    }
}
