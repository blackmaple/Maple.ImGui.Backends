using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;


namespace Maple.ImGui.Backends.Windows.GraphicsCore.Native
{
    public unsafe sealed class Win32WindowFactory : IDisposable
    {
        nint _ClassNamePointer;
        WNDCLASSEXW _WNDCLASSEX;
        public Win32WindowFactory()
        {
            var className = Guid.NewGuid().ToString("N");
            _ClassNamePointer = Marshal.StringToHGlobalUni(className);
            _WNDCLASSEX = new WNDCLASSEXW
            {
                cbSize = (uint)Unsafe.SizeOf<WNDCLASSEXW>(),
                style = WNDCLASS_STYLES.CS_OWNDC,
                lpfnWndProc = &DefWindowProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = PInvoke.GetModuleHandle(default(char*)),
                hIcon = HICON.Null,
                hCursor = HCURSOR.Null,
                hbrBackground = HBRUSH.Null,
                lpszMenuName = default,
                lpszClassName = new PCWSTR((char*)_ClassNamePointer.ToPointer()),
                hIconSm = HICON.Null,
            };
            var status = PInvoke.RegisterClassEx(in _WNDCLASSEX);
            Debug.Assert(status != 0);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        private static LRESULT DefWindowProc(HWND hWnd, uint uMsg, WPARAM wParam, LPARAM lParam)
        {
            return PInvoke.DefWindowProc(hWnd, uMsg, wParam, lParam);
        }

        public Win32MainWindow Create()
        {
            var hwnd = PInvoke.CreateWindowEx(
                WINDOW_EX_STYLE.WS_EX_LEFT,
                  _WNDCLASSEX.lpszClassName, default,
                  WINDOW_STYLE.WS_OVERLAPPEDWINDOW,
                  0, 0, 1, 1,
                  HWND.Null,
                  HMENU.Null,
                  _WNDCLASSEX.hInstance,
                  default);
            return new Win32MainWindow(hwnd);
        }

        public void Dispose()
        {
            var pointer = this._ClassNamePointer;
            this._ClassNamePointer = nint.Zero;
            if (pointer != IntPtr.Zero)
            {
                PInvoke.UnregisterClass(new PCWSTR((char*)_ClassNamePointer.ToPointer()), this._WNDCLASSEX.hInstance);
                Marshal.FreeHGlobal(pointer);
            }
        }
    }
}
