using Maple.UnmanagedExtensions;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.Ime;
using static System.Net.Mime.MediaTypeNames;
using ImGuiApi = Hexa.NET.ImGui.ImGui;

namespace Maple.ImGui.Backends.Windows
{
    public partial class ImGuiWin32InputBridge : IImGuiWin32InputBridge
    {
        [ThreadStatic]
        private static char[]? _chars;

        public static char[] RentChars(int minChars)
        {
            var buf = _chars;
            if (buf == null || buf.Length < minChars)
            {
                buf = new char[Math.Max(minChars, 256)];
                _chars = buf;
                return buf;
            }
            Array.Clear(buf, 0 , buf.Length);
            return buf;
        }
        [ThreadStatic]
        private static byte[]? _bytes;

        public static byte[] RentBytes(int minBytes)
        {
            var buf = _bytes;
            if (buf == null || buf.Length < minBytes)
            {
                buf = new byte[Math.Max(minBytes, 256)];
                _bytes = buf;
                return buf;
            }
            Array.Clear(buf, 0, buf.Length);

            return buf;
        }

        //private const uint WM_IME_COMPOSITION = 0x010F;
        //private const int GCS_RESULTSTR = 0x0800;
        //private const int VK_LBUTTON = 0x01;
        //private const int VK_RBUTTON = 0x02;
        //private const int VK_MBUTTON = 0x04;
        //private const int VK_XBUTTON1 = 0x05;
        //private const int VK_XBUTTON2 = 0x06;
        //public unsafe static void SetImm(nint windowHandle, int x, int y)
        //{
        //    var compositionForm = new COMPOSITIONFORM
        //    {
        //        dwStyle = PInvoke.CFS_FORCE_POSITION,
        //        ptCurrentPos = new System.Drawing.Point { X = x, Y = y }
        //    };
        //    var hWnd = new HWND(windowHandle);
        //    var himc = PInvoke.ImmGetContext(hWnd);
        //    nint ptr_compositionForm = UnsafeIn<COMPOSITIONFORM>.FromIn(compositionForm).Pointer;
        //    PInvoke.ImmSetCompositionWindow(himc, (COMPOSITIONFORM*)ptr_compositionForm.ToPointer());
        //    PInvoke.ImmReleaseContext(hWnd, himc);
        //}

        //private static nint CurrentWindowHandle { get; set; }

        //public static void SetWindowHandle(nint hWnd)
        //{
        //    CurrentWindowHandle = hWnd;
        //}

        //public static nint GetWindowHandle()
        //{
        //    return CurrentWindowHandle;
        //}

        //public static string? Debug(uint uMsg, nint wParam, nint lParam)
        //{


        //    if (uMsg == PInvoke.WM_SETFOCUS
        //        || uMsg == PInvoke.WM_KILLFOCUS
        //        || uMsg == PInvoke.WM_IME_SETCONTEXT
        //        || uMsg == PInvoke.WM_IME_STARTCOMPOSITION
        //        || uMsg == PInvoke.WM_IME_COMPOSITION
        //        || uMsg == PInvoke.WM_IME_ENDCOMPOSITION
        //        || uMsg == PInvoke.WM_CHAR)
        //    {
        //        if (uMsg == PInvoke.WM_IME_COMPOSITION && ((uint)lParam & GCS_RESULTSTR) != 0u)
        //        {

        //        }

        //        var fMain = PInvoke.GetForegroundWindow();
        //        var fFocus = PInvoke.GetFocus();

        //        uint fgThread = PInvoke.GetWindowThreadProcessId(fMain, out _);
        //        uint curThread = PInvoke.GetCurrentThreadId();
        //        return $"uMsg:{uMsg:X8}/fMain:{fMain:X8}/fFocus:{fFocus:X8}/fgThread:{fgThread}/curThread:{curThread}";
        //    }
        //    return default;
        //}

        //public static bool TryGetClientSize(out Vector2 clientSize)
        //{
        //    var hWnd = CurrentWindowHandle;
        //    if (hWnd == nint.Zero || !GetClientRect(hWnd, out var rect))
        //    {
        //        clientSize = default;
        //        return false;
        //    }

        //    clientSize = new Vector2(
        //        Math.Max(1, rect.Right - rect.Left),
        //        Math.Max(1, rect.Bottom - rect.Top));
        //    return true;
        //}

        //public static float GetWindowDpiScale(nint hWnd)
        //{
        //    if (hWnd == nint.Zero)
        //    {
        //        return 1.0f;
        //    }

        //    var dpi = GetDpiForWindow(hWnd);
        //    if (dpi == 0)
        //    {
        //        return 1.0f;
        //    }

        //    return dpi / 96.0f;
        //}

        //public static bool TryGetMousePosition(out Vector2 mousePosition)
        //{
        //    var hWnd = CurrentWindowHandle;
        //    if (hWnd == nint.Zero || !GetCursorPos(out var point) || !ScreenToClient(hWnd, ref point))
        //    {
        //        mousePosition = default;
        //        return false;
        //    }

        //    mousePosition = new Vector2(point.X, point.Y);
        //    return true;
        //}

        //public static void UpdateMouseButtons()
        //{
        //    var io = ImGuiApi.GetIO();
        //    io.MouseDown[0] = IsKeyDown(VK_LBUTTON);
        //    io.MouseDown[1] = IsKeyDown(VK_RBUTTON);
        //    io.MouseDown[2] = IsKeyDown(VK_MBUTTON);
        //    io.MouseDown[3] = IsKeyDown(VK_XBUTTON1);
        //    io.MouseDown[4] = IsKeyDown(VK_XBUTTON2);
        //}

        //public static nint MatchThreadDpiAwarenessContext(nint hWnd)
        //{
        //    if (hWnd == nint.Zero)
        //    {
        //        return nint.Zero;
        //    }

        //    var dpiContext = GetWindowDpiAwarenessContext(hWnd);
        //    if (dpiContext == nint.Zero)
        //    {
        //        return nint.Zero;
        //    }

        //    return SetThreadDpiAwarenessContext(dpiContext);
        //}

        //public static void RestoreThreadDpiAwarenessContext(nint previousContext)
        //{
        //    if (previousContext != nint.Zero)
        //    {
        //        _ = SetThreadDpiAwarenessContext(previousContext);
        //    }
        //}

        public unsafe bool TryHandleImeComposition(nint handle, uint uMsg, nuint wParam, nint lParam)
        {

            if (uMsg != PInvoke.WM_IME_COMPOSITION || ((uint)lParam & (uint)IME_COMPOSITION_STRING.GCS_RESULTSTR) == 0)
            {
                return false;
            }
            var hWnd = new HWND(handle);
            var hImc = PInvoke.ImmGetContext(hWnd);
            if (hImc == nint.Zero)
            {
                return false;
            }

            try
            {
                var byteCount = PInvoke.ImmGetCompositionString(hImc, IME_COMPOSITION_STRING.GCS_RESULTSTR, default, default);
                if (byteCount <= 0)
                {
                    return false;
                }


                char[] charBuffer = RentChars(byteCount + 2);
                fixed (char* p = charBuffer)
                {
                    int copied = PInvoke.ImmGetCompositionString(hImc, IME_COMPOSITION_STRING.GCS_RESULTSTR, p, (uint)byteCount);
                    if (copied <= 0)
                    {
                        return false;
                    }

                    var realCopied = copied / 2;
                    byteCount = Encoding.UTF8.GetByteCount(charBuffer, 0, realCopied);
                    byte[] byteBuffer = RentBytes(byteCount + 2);
                    int written = Encoding.UTF8.GetBytes(charBuffer, 0, realCopied, byteBuffer, 0);
                    Debug.WriteLine(Encoding.UTF8.GetString(byteBuffer.AsSpan(0, written)));
                    ImGuiApi.GetIO().AddInputCharactersUTF8(byteBuffer.AsSpan(0, written));

                    return true;    
                }
            }
            finally
            {
                PInvoke.ImmReleaseContext(hWnd, hImc);
            }


            //try
            //{
            //    var byteCount = ImmGetCompositionStringW(hImc, GCS_RESULTSTR, nint.Zero, 0);
            //    if (byteCount <= 0)
            //    {
            //        return false;
            //    }

            //    var buffer = Marshal.AllocHGlobal(byteCount + 2);
            //    try
            //    {
            //        _ = ImmGetCompositionStringW(hImc, GCS_RESULTSTR, buffer, byteCount);
            //        var text = Marshal.PtrToStringUni(buffer, byteCount / 2);
            //        if (string.IsNullOrEmpty(text))
            //        {
            //            return false;
            //        }

            //        ImGuiApi.GetIO().AddInputCharactersUTF8(text);
            //        return true;
            //    }
            //    finally
            //    {
            //        Marshal.FreeHGlobal(buffer);
            //    }
            //}
            //finally
            //{
            //    _ = ImmReleaseContext(hWnd, hImc);
            //}
        }

        //private static bool IsKeyDown(int virtualKey)
        //{
        //    return (GetAsyncKeyState(virtualKey) & 0x8000) != 0;
        //}

        //[LibraryImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static partial bool GetClientRect(nint hWnd, out RECT lpRect);

        //[LibraryImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static partial bool GetCursorPos(out POINT lpPoint);

        //[LibraryImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static partial bool ScreenToClient(nint hWnd, ref POINT lpPoint);

        //[LibraryImport("user32.dll")]
        //private static partial short GetAsyncKeyState(int vKey);

        //[LibraryImport("user32.dll")]
        //private static partial uint GetDpiForWindow(nint hWnd);

        //[LibraryImport("user32.dll")]
        //private static partial nint GetWindowDpiAwarenessContext(nint hWnd);

        //[LibraryImport("user32.dll")]
        //private static partial nint SetThreadDpiAwarenessContext(nint dpiContext);

        //[LibraryImport("imm32.dll")]
        //private static partial nint ImmGetContext(nint hWnd);

        //[LibraryImport("imm32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static partial bool ImmReleaseContext(nint hWnd, nint hImc);

        //[LibraryImport("imm32.dll", EntryPoint = "ImmGetCompositionStringW")]
        //private static partial int ImmGetCompositionStringW(nint hImc, int dwIndex, nint lpBuf, int dwBufLen);

        //[StructLayout(LayoutKind.Sequential)]
        //private struct RECT
        //{
        //    public int Left;
        //    public int Top;
        //    public int Right;
        //    public int Bottom;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //private struct POINT
        //{
        //    public int X;
        //    public int Y;
        //}
    }
}
