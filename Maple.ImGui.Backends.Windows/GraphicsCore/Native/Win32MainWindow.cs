using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace Maple.ImGui.Backends.Windows.GraphicsCore.Native
{
    public unsafe struct Win32MainWindow : IDisposable
    {
        private HWND _WindowHandle;
        internal Win32MainWindow(HWND hwnd) => _WindowHandle = hwnd;

        public void Dispose()
        {
            var hWnd = this._WindowHandle;
            this._WindowHandle = HWND.Null;
            if (!hWnd.IsNull)
            {
                PInvoke.DestroyWindow(hWnd);
            }
        }

        public readonly override string ToString()
        {
            return (new nint(this._WindowHandle.Value)).ToString("X8");
        }

        public static implicit operator nint(Win32MainWindow x) => new(x._WindowHandle.Value);
        public static implicit operator void*(Win32MainWindow x) => x._WindowHandle;
        public static implicit operator bool(Win32MainWindow x) => !x._WindowHandle.IsNull;
        public readonly nint Handle => _WindowHandle;
    }
}
