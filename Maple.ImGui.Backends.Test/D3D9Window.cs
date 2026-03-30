using System;
using System.Runtime.InteropServices;
using Vortice.Direct3D9;
using Vortice.Mathematics;
using static Vortice.Direct3D9.D3D9;

namespace ImGui.App.D3D11
{
    public class D3D9Window : ITestWindow
    {
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
        private const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        private const int SW_SHOW = 5;
        private const uint PM_REMOVE = 0x0001;

        private static IDirect3D9? _direct3D;
        private static IDirect3DDevice9? _device;
        private static PresentParameters _presentParameters;
        private static WndProcDelegate? _wndProcDelegate;

        public static void Run()
        {
            const string className = "VorticeD3D9Win32WindowClass";
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

            var hwnd = Native.CreateWindowEx(
                0,
                className,
                "Vortice D3D9 Win32 Window",
                WS_OVERLAPPEDWINDOW,
                CW_USEDEFAULT,
                CW_USEDEFAULT,
                1280,
                720,
                IntPtr.Zero,
                IntPtr.Zero,
                hInstance,
                IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
            {
                Console.WriteLine("CreateWindowEx failed");
                return;
            }

            Native.ShowWindow(hwnd, SW_SHOW);
            InitD3D(hwnd);

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

        private static void InitD3D(IntPtr hwnd)
        {
            _direct3D = Direct3DCreate9();
            if (_direct3D is null)
            {
                throw new Exception("Direct3DCreate9 failed");
            }

            _presentParameters = new PresentParameters
            {
                Windowed = true,
                SwapEffect = SwapEffect.Discard,
                DeviceWindowHandle = hwnd,
                PresentationInterval = PresentInterval.One,
                BackBufferWidth = 1280,
                BackBufferHeight = 720,
                BackBufferFormat = Format.A8R8G8B8
            };

            var createFlags = CreateFlags.HardwareVertexProcessing | CreateFlags.Multithreaded;
            _device = _direct3D.CreateDevice(0, DeviceType.Hardware, hwnd, createFlags, _presentParameters);
        }

        private static void Render()
        {
            if (_device is null)
            {
                return;
            }

            _device.Clear(ClearFlags.Target, new Color(26, 46, 90, 255), 1.0f, 0);
            _device.BeginScene();
            _device.EndScene();
            _device.Present();
        }

        private static void Resize(int width, int height)
        {
            if (_device is null)
            {
                return;
            }

            _presentParameters.BackBufferWidth = (uint)Math.Max(1, width);
            _presentParameters.BackBufferHeight = (uint)Math.Max(1, height);
            _device.Reset(ref _presentParameters);
        }

        private static void Cleanup()
        {
            _device?.Dispose();
            _direct3D?.Dispose();

            _device = null;
            _direct3D = null;
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
        }
    }
}
