//using System;
//using System.Runtime.InteropServices;
//using SharpDX;
//using SharpDX.Direct3D10;
//using SharpDX.DXGI;
//using SharpDX.Mathematics.Interop;
//using Device = SharpDX.Direct3D10.Device;
//using Resource = SharpDX.Direct3D10.Resource;

//namespace ImGui.App.D3D11
//{
//    public class D3D10Window : ITestWindow
//    {
//        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
//        private const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
//        private const int SW_SHOW = 5;
//        private const uint PM_REMOVE = 0x0001;

//        private static Device? _device;
//        private static SwapChain? _swapChain;
//        private static RenderTargetView? _renderTargetView;
//        private static WndProcDelegate? _wndProcDelegate;

//        public static void Run()
//        {
//            const string className = "SharpDXD3D10Win32WindowClass";
//            _wndProcDelegate = WndProc;

//            var hInstance = Native.GetModuleHandle(IntPtr.Zero);
//            var wndClassEx = new Native.WNDCLASSEX
//            {
//                cbSize = Marshal.SizeOf<Native.WNDCLASSEX>(),
//                style = 0,
//                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(_wndProcDelegate),
//                cbClsExtra = 0,
//                cbWndExtra = 0,
//                hInstance = hInstance,
//                hIcon = IntPtr.Zero,
//                hCursor = Native.LoadCursor(IntPtr.Zero, Native.IDC_ARROW),
//                hbrBackground = IntPtr.Zero,
//                lpszMenuName = null,
//                lpszClassName = className,
//                hIconSm = IntPtr.Zero
//            };

//            if (Native.RegisterClassEx(ref wndClassEx) == 0)
//            {
//                Console.WriteLine("RegisterClassEx failed");
//                return;
//            }

//            var hwnd = Native.CreateWindowEx(
//                0,
//                className,
//                "SharpDX D3D10 Win32 Window",
//                WS_OVERLAPPEDWINDOW,
//                CW_USEDEFAULT,
//                CW_USEDEFAULT,
//                1280,
//                720,
//                IntPtr.Zero,
//                IntPtr.Zero,
//                hInstance,
//                IntPtr.Zero);

//            if (hwnd == IntPtr.Zero)
//            {
//                Console.WriteLine("CreateWindowEx failed");
//                return;
//            }

//            Native.ShowWindow(hwnd, SW_SHOW);
//            InitD3D(hwnd);

//            var msg = new Native.MSG();
//            var quit = false;
//            while (!quit)
//            {
//                while (Native.PeekMessage(out msg, IntPtr.Zero, 0, 0, PM_REMOVE))
//                {
//                    if (msg.message == Native.WM_QUIT)
//                    {
//                        quit = true;
//                        break;
//                    }

//                    Native.TranslateMessage(ref msg);
//                    Native.DispatchMessage(ref msg);
//                }

//                if (!quit)
//                {
//                    Render();
//                }
//            }

//            Cleanup();
//        }

//        private static void InitD3D(IntPtr hwnd)
//        {
//            var swapChainDescription = new SwapChainDescription
//            {
//                BufferCount = 1,
//                ModeDescription = new ModeDescription(1280, 720, new Rational(60, 1), Format.R8G8B8A8_UNorm),
//                IsWindowed = true,
//                OutputHandle = hwnd,
//                SampleDescription = new SampleDescription(1, 0),
//                Usage = Usage.RenderTargetOutput,
//                SwapEffect = SwapEffect.Discard,
//                Flags = SwapChainFlags.None
//            };

//            var creationFlags = DeviceCreationFlags.BgraSupport;
//#if DEBUG
//            creationFlags |= DeviceCreationFlags.Debug;
//#endif

//            Device.CreateWithSwapChain(SharpDX.Direct3D10.DriverType.Hardware, creationFlags, swapChainDescription, out _device, out _swapChain);

//            using (var factory = _swapChain!.GetParent<Factory>())
//            {
//                factory.MakeWindowAssociation(hwnd, WindowAssociationFlags.IgnoreAll);
//            }

//            CreateRenderTargetAndViewport(hwnd);
//        }

//        private static void CreateRenderTargetAndViewport(IntPtr hwnd)
//        {
//            if (_device is null || _swapChain is null)
//            {
//                return;
//            }

//            Utilities.Dispose(ref _renderTargetView);
//            using (var backBuffer = Resource.FromSwapChain<Texture2D>(_swapChain, 0))
//            {
//                _renderTargetView = new RenderTargetView(_device, backBuffer);
//            }

//            _device.OutputMerger.SetTargets(_renderTargetView);
//            Native.GetClientRect(hwnd, out var rect);
//            var width = Math.Max(1, rect.right - rect.left);
//            var height = Math.Max(1, rect.bottom - rect.top);
//            _device.Rasterizer.SetViewports(new RawViewport
//            {
//                X = 0,
//                Y = 0,
//                Width = width,
//                Height = height,
//                MinDepth = 0.0f,
//                MaxDepth = 1.0f
//            });
//        }

//        private static void Render()
//        {
//            if (_device is null || _swapChain is null || _renderTargetView is null)
//            {
//                return;
//            }

//            _device.ClearRenderTargetView(_renderTargetView, new RawColor4(0.35f, 0.18f, 0.10f, 1.0f));
//            _swapChain.Present(1, PresentFlags.None);
//        }

//        private static void Resize(int width, int height)
//        {
//            if (_device is null || _swapChain is null)
//            {
//                return;
//            }

//            Utilities.Dispose(ref _renderTargetView);
//            _swapChain.ResizeBuffers(1, Math.Max(1, width), Math.Max(1, height), Format.R8G8B8A8_UNorm, SwapChainFlags.None);

//            using (var backBuffer = Resource.FromSwapChain<Texture2D>(_swapChain, 0))
//            {
//                _renderTargetView = new RenderTargetView(_device, backBuffer);
//            }

//            _device.OutputMerger.SetTargets(_renderTargetView);
//            _device.Rasterizer.SetViewports(new RawViewport
//            {
//                X = 0,
//                Y = 0,
//                Width = Math.Max(1, width),
//                Height = Math.Max(1, height),
//                MinDepth = 0.0f,
//                MaxDepth = 1.0f
//            });
//        }

//        private static void Cleanup()
//        {
//            Utilities.Dispose(ref _renderTargetView);
//            Utilities.Dispose(ref _swapChain);
//            Utilities.Dispose(ref _device);
//        }

//        private static IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
//        {
//            switch (msg)
//            {
//                case Native.WM_SIZE:
//                    {
//                        var width = lParam.ToInt32() & 0xFFFF;
//                        var height = (lParam.ToInt32() >> 16) & 0xFFFF;
//                        if (width == 0) width = 1;
//                        if (height == 0) height = 1;
//                        Resize(width, height);
//                        break;
//                    }
//                case Native.WM_DESTROY:
//                    Native.PostQuitMessage(0);
//                    return IntPtr.Zero;
//                case Native.WM_CLOSE:
//                    Native.DestroyWindow(hwnd);
//                    return IntPtr.Zero;
//            }

//            return Native.DefWindowProc(hwnd, msg, wParam, lParam);
//        }

//        private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

//        private static class Native
//        {
//            public const int WM_QUIT = 0x0012;
//            public const int IDC_ARROW = 32512;
//            public const uint WM_SIZE = 0x0005;
//            public const uint WM_DESTROY = 0x0002;
//            public const uint WM_CLOSE = 0x0010;

//            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
//            public static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

//            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
//            public struct WNDCLASSEX
//            {
//                public int cbSize;
//                public uint style;
//                public IntPtr lpfnWndProc;
//                public int cbClsExtra;
//                public int cbWndExtra;
//                public IntPtr hInstance;
//                public IntPtr hIcon;
//                public IntPtr hCursor;
//                public IntPtr hbrBackground;
//                [MarshalAs(UnmanagedType.LPWStr)] public string? lpszMenuName;
//                [MarshalAs(UnmanagedType.LPWStr)] public string? lpszClassName;
//                public IntPtr hIconSm;
//            }

//            [StructLayout(LayoutKind.Sequential)]
//            public struct MSG
//            {
//                public IntPtr hwnd;
//                public uint message;
//                public UIntPtr wParam;
//                public IntPtr lParam;
//                public uint time;
//                public System.Drawing.Point pt;
//            }

//            [StructLayout(LayoutKind.Sequential)]
//            public struct RECT
//            {
//                public int left;
//                public int top;
//                public int right;
//                public int bottom;
//            }

//            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
//            public static extern ushort RegisterClassEx([In] ref WNDCLASSEX lpwcx);

//            [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
//            public static extern IntPtr CreateWindowEx(
//                uint dwExStyle,
//                [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
//                [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
//                uint dwStyle,
//                int x,
//                int y,
//                int nWidth,
//                int nHeight,
//                IntPtr hWndParent,
//                IntPtr hMenu,
//                IntPtr hInstance,
//                IntPtr lpParam);

//            [DllImport("user32.dll")]
//            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

//            [DllImport("user32.dll")]
//            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

//            [DllImport("user32.dll")]
//            public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

//            [DllImport("user32.dll")]
//            public static extern bool TranslateMessage([In] ref MSG lpMsg);

//            [DllImport("user32.dll")]
//            public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

//            [DllImport("user32.dll")]
//            public static extern void PostQuitMessage(int nExitCode);

//            [DllImport("user32.dll")]
//            public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

//            [DllImport("user32.dll", SetLastError = true)]
//            public static extern bool DestroyWindow(IntPtr hWnd);

//            [DllImport("user32.dll")]
//            public static extern int GetClientRect(IntPtr hWnd, out RECT lpRect);
//        }
//    }
//}
