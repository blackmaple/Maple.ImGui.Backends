using System;
using System.Runtime.InteropServices;

namespace ImGui.App.D3D11
{
    public class OpenGLWindow : ITestWindow
    {
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
        private const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        private const int SW_SHOW = 5;
        private const uint PM_REMOVE = 0x0001;
        private const uint PFD_DRAW_TO_WINDOW = 0x00000004;
        private const uint PFD_SUPPORT_OPENGL = 0x00000020;
        private const uint PFD_DOUBLEBUFFER = 0x00000001;
        private const byte PFD_TYPE_RGBA = 0;
        private const byte PFD_MAIN_PLANE = 0;
        private const uint GL_COLOR_BUFFER_BIT = 0x00004000;

        private static IntPtr _hwnd;
        private static IntPtr _hdc;
        private static IntPtr _hglrc;
        private static WndProcDelegate? _wndProcDelegate;

        public static void Run()
        {
            const string className = "OpenGLWin32WindowClass";
            _wndProcDelegate = WndProc;

            var hInstance = Native.GetModuleHandle(IntPtr.Zero);
            var wndClassEx = new Native.WNDCLASSEX
            {
                cbSize = Marshal.SizeOf<Native.WNDCLASSEX>(),
                style = 0,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_wndProcDelegate),
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = hInstance,
                hIcon = IntPtr.Zero,
                hCursor = Native.LoadCursor(IntPtr.Zero, Native.IDC_ARROW),
                hbrBackground = IntPtr.Zero,
                lpszMenuName = null,
                lpszClassName = className,
                hIconSm = IntPtr.Zero
            };

            if (Native.RegisterClassEx(ref wndClassEx) == 0)
            {
                Console.WriteLine("RegisterClassEx failed");
                return;
            }

            _hwnd = Native.CreateWindowEx(
                0,
                className,
                "OpenGL Win32 Window",
                WS_OVERLAPPEDWINDOW,
                CW_USEDEFAULT,
                CW_USEDEFAULT,
                1280,
                720,
                IntPtr.Zero,
                IntPtr.Zero,
                hInstance,
                IntPtr.Zero);

            if (_hwnd == IntPtr.Zero)
            {
                Console.WriteLine("CreateWindowEx failed");
                return;
            }

            Native.ShowWindow(_hwnd, SW_SHOW);
            InitOpenGL(_hwnd);

            var msg = new Native.MSG();
            var quit = false;
            while (!quit)
            {
                while (Native.PeekMessage(out msg, IntPtr.Zero, 0, 0, PM_REMOVE))
                {
                    if (msg.message == Native.WM_QUIT)
                    {
                        quit = true;
                        break;
                    }

                    Native.TranslateMessage(ref msg);
                    Native.DispatchMessage(ref msg);
                }

                if (!quit)
                {
                    Render();
                }
            }

            Cleanup();
        }

        private static void InitOpenGL(IntPtr hwnd)
        {
            _hdc = Native.GetDC(hwnd);
            if (_hdc == IntPtr.Zero)
            {
                throw new Exception("GetDC failed");
            }

            var pixelFormatDescriptor = new Native.PIXELFORMATDESCRIPTOR
            {
                nSize = (ushort)Marshal.SizeOf<Native.PIXELFORMATDESCRIPTOR>(),
                nVersion = 1,
                dwFlags = PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER,
                iPixelType = PFD_TYPE_RGBA,
                cColorBits = 32,
                cDepthBits = 24,
                cStencilBits = 8,
                iLayerType = PFD_MAIN_PLANE
            };

            var pixelFormat = Native.ChoosePixelFormat(_hdc, ref pixelFormatDescriptor);
            if (pixelFormat == 0 || !Native.SetPixelFormat(_hdc, pixelFormat, ref pixelFormatDescriptor))
            {
                throw new Exception("SetPixelFormat failed");
            }

            _hglrc = Native.wglCreateContext(_hdc);
            if (_hglrc == IntPtr.Zero || !Native.wglMakeCurrent(_hdc, _hglrc))
            {
                throw new Exception("wglCreateContext failed");
            }

            Resize(1280, 720);
        }

        private static void Render()
        {
            if (_hdc == IntPtr.Zero)
            {
                return;
            }

            Native.glClearColor(0.10f, 0.35f, 0.18f, 1.0f);
            Native.glClear(GL_COLOR_BUFFER_BIT);
            Native.SwapBuffers(_hdc);
        }

        private static void Resize(int width, int height)
        {
            Native.glViewport(0, 0, Math.Max(1, width), Math.Max(1, height));
        }

        private static void Cleanup()
        {
            if (_hglrc != IntPtr.Zero)
            {
                Native.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                Native.wglDeleteContext(_hglrc);
                _hglrc = IntPtr.Zero;
            }

            if (_hdc != IntPtr.Zero)
            {
                Native.ReleaseDC(_hwnd, _hdc);
                _hdc = IntPtr.Zero;
            }

            _hwnd = IntPtr.Zero;
        }

        private static IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case Native.WM_SIZE:
                    {
                        var width = lParam.ToInt32() & 0xFFFF;
                        var height = (lParam.ToInt32() >> 16) & 0xFFFF;
                        if (width == 0) width = 1;
                        if (height == 0) height = 1;
                        Resize(width, height);
                        break;
                    }
                case Native.WM_DESTROY:
                    Native.PostQuitMessage(0);
                    return IntPtr.Zero;
                case Native.WM_CLOSE:
                    Native.DestroyWindow(hwnd);
                    return IntPtr.Zero;
            }

            return Native.DefWindowProc(hwnd, msg, wParam, lParam);
        }

        private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private static class Native
        {
            public const int WM_QUIT = 0x0012;
            public const int IDC_ARROW = 32512;
            public const uint WM_SIZE = 0x0005;
            public const uint WM_DESTROY = 0x0002;
            public const uint WM_CLOSE = 0x0010;

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            public static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WNDCLASSEX
            {
                public int cbSize;
                public uint style;
                public IntPtr lpfnWndProc;
                public int cbClsExtra;
                public int cbWndExtra;
                public IntPtr hInstance;
                public IntPtr hIcon;
                public IntPtr hCursor;
                public IntPtr hbrBackground;
                [MarshalAs(UnmanagedType.LPWStr)] public string? lpszMenuName;
                [MarshalAs(UnmanagedType.LPWStr)] public string? lpszClassName;
                public IntPtr hIconSm;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MSG
            {
                public IntPtr hwnd;
                public uint message;
                public UIntPtr wParam;
                public IntPtr lParam;
                public uint time;
                public System.Drawing.Point pt;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct PIXELFORMATDESCRIPTOR
            {
                public ushort nSize;
                public ushort nVersion;
                public uint dwFlags;
                public byte iPixelType;
                public byte cColorBits;
                public byte cRedBits;
                public byte cRedShift;
                public byte cGreenBits;
                public byte cGreenShift;
                public byte cBlueBits;
                public byte cBlueShift;
                public byte cAlphaBits;
                public byte cAlphaShift;
                public byte cAccumBits;
                public byte cAccumRedBits;
                public byte cAccumGreenBits;
                public byte cAccumBlueBits;
                public byte cAccumAlphaBits;
                public byte cDepthBits;
                public byte cStencilBits;
                public byte cAuxBuffers;
                public byte iLayerType;
                public byte bReserved;
                public uint dwLayerMask;
                public uint dwVisibleMask;
                public uint dwDamageMask;
            }

            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern ushort RegisterClassEx([In] ref WNDCLASSEX lpwcx);

            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern IntPtr CreateWindowEx(
                uint dwExStyle,
                [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
                [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
                uint dwStyle,
                int x,
                int y,
                int nWidth,
                int nHeight,
                IntPtr hWndParent,
                IntPtr hMenu,
                IntPtr hInstance,
                IntPtr lpParam);

            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32.dll")]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

            [DllImport("user32.dll")]
            public static extern bool TranslateMessage([In] ref MSG lpMsg);

            [DllImport("user32.dll")]
            public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

            [DllImport("user32.dll")]
            public static extern void PostQuitMessage(int nExitCode);

            [DllImport("user32.dll")]
            public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool DestroyWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr GetDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

            [DllImport("gdi32.dll")]
            public static extern int ChoosePixelFormat(IntPtr hdc, [In] ref PIXELFORMATDESCRIPTOR ppfd);

            [DllImport("gdi32.dll")]
            public static extern bool SetPixelFormat(IntPtr hdc, int format, [In] ref PIXELFORMATDESCRIPTOR ppfd);

            [DllImport("gdi32.dll")]
            public static extern bool SwapBuffers(IntPtr hdc);

            [DllImport("opengl32.dll")]
            public static extern IntPtr wglCreateContext(IntPtr hdc);

            [DllImport("opengl32.dll")]
            public static extern bool wglDeleteContext(IntPtr hglrc);

            [DllImport("opengl32.dll")]
            public static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hglrc);

            [DllImport("opengl32.dll")]
            public static extern void glClear(uint mask);

            [DllImport("opengl32.dll")]
            public static extern void glClearColor(float red, float green, float blue, float alpha);

            [DllImport("opengl32.dll")]
            public static extern void glViewport(int x, int y, int width, int height);
        }
    }
}
