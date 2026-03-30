using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.DXGI;
using Vortice.Mathematics;
using static Vortice.Direct3D11.D3D11;
namespace ImGui.App.D3D11
{
    public class D3D11Window: ITestWindow
    {
        // Win32 constants
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
        private const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        private const int SW_SHOW = 5;
        private const uint PM_REMOVE = 0x0001;

        private const uint WM_DESTROY = 0x0002;
        private const uint WM_SIZE = 0x0005;
        private const uint WM_CLOSE = 0x0010;

        // D3D objects
        private static ID3D11Device _device;
        private static ID3D11DeviceContext _context;
        private static IDXGISwapChain _swapChain;
        private static ID3D11RenderTargetView _rtv;

        // Keep delegate alive for the lifetime of window
        private static WndProcDelegate _wndProcDelegate;

        public static void Run()
        {
            // 注册窗口类并创建窗口
            var className = "VorticeD3D11Win32WindowClass";
            _wndProcDelegate = WndProc;

            var hInstance = Native.GetModuleHandle(IntPtr.Zero);
            var wndClassEx = new Native.WNDCLASSEX()
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

            var atom = Native.RegisterClassEx(ref wndClassEx);
            if (atom == 0)
            {
                Console.WriteLine("RegisterClassEx failed");
                return;
            }

            var hwnd = Native.CreateWindowEx(
                0,
                className,
                "Vortice D3D11 Win32 Window",
                WS_OVERLAPPEDWINDOW,
                CW_USEDEFAULT, CW_USEDEFAULT, 1280, 720,
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

            // 初始化 D3D11
            InitD3D(hwnd);

            // 主循环：处理消息并在空闲时渲染
            var msg = new Native.MSG();
            bool quit = false;
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

            // 清理
            Cleanup();
        }

        private static void InitD3D(IntPtr hwnd)
        {
            // 创建设备与上下文
            var featureLevels = new[] {
            FeatureLevel.Level_11_0,
            FeatureLevel.Level_10_1,
            FeatureLevel.Level_10_0
        };
            DeviceCreationFlags creationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            creationFlags |= DeviceCreationFlags.Debug;
#endif
            FeatureLevel featureLevel;
            var hr = D3D11CreateDevice(null, DriverType.Hardware, creationFlags, featureLevels, out _device, out featureLevel, out _context);
            if (hr.Failure)
                throw new Exception("D3D11CreateDevice failed: " + hr.Code);

            // 交换链描述
            var swapDesc = new SwapChainDescription()
            {
                BufferCount = 1,
                BufferDescription = new ModeDescription(1280, 720, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                BufferUsage = Usage.RenderTargetOutput,
                OutputWindow = hwnd,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Windowed = true
            };

            // 通过设备获取 DXGI Factory 创建 SwapChain
            using (var dxgiDevice = _device.QueryInterface<IDXGIDevice>())
            using (var adapter = dxgiDevice.GetAdapter())
            using (var factory = adapter.GetParent<IDXGIFactory>())
            {
                _swapChain = factory.CreateSwapChain(_device, swapDesc);
            }

            CreateRenderTargetAndViewport(hwnd);
        }

        private static void CreateRenderTargetAndViewport(IntPtr hwnd)
        {
            _rtv?.Dispose();

            using (var backBuffer = _swapChain.GetBuffer<ID3D11Texture2D>(0))
            {
                _rtv = _device.CreateRenderTargetView(backBuffer);
            }

            _context.OMSetRenderTargets(_rtv);
            var rect = new Native.RECT();
            Native.GetClientRect(hwnd, out rect);
            int width = Math.Max(1, rect.right - rect.left);
            int height = Math.Max(1, rect.bottom - rect.top);
            _context.RSSetViewports([new Viewport(0, 0, width, height)]);
        }

        private static void Render()
        {
            // 清屏颜色
            _context.ClearRenderTargetView(_rtv, new Color4(0.1f, 0.48f, 0.75f, 1.0f));
            _swapChain.Present(1, PresentFlags.None);
        }

        private static void Resize(int width, int height)
        {
            if (_swapChain == null)
                return;

            _rtv?.Dispose();
            _context.OMSetRenderTargets(null as ID3D11RenderTargetView);

            _swapChain.ResizeBuffers(1, (uint)Math.Max(1, width), (uint)Math.Max(1, height), Format.R8G8B8A8_UNorm, SwapChainFlags.None);

            using (var backBuffer = _swapChain.GetBuffer<ID3D11Texture2D>(0))
            {
                _rtv = _device.CreateRenderTargetView(backBuffer);
            }

            _context.OMSetRenderTargets(_rtv);
            _context.RSSetViewports([new Viewport(0, 0, Math.Max(1, width), Math.Max(1, height))]);
        }

        private static void Cleanup()
        {
            _rtv?.Dispose();
            _swapChain?.Dispose();

            if (_context != null)
            {
                _context.ClearState();
                _context.Flush();
                _context.Dispose();
                _context = null;
            }

            _device?.Dispose();
            _device = null;
        }

        // WndProc 处理消息（包括 WM_SIZE）
        private static IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case Native.WM_SIZE:
                    {
                        int width = lParam.ToInt32() & 0xFFFF;
                        int height = (lParam.ToInt32() >> 16) & 0xFFFF;
                        // 当窗口最小化时 width/height 可能为 0，确保至少 1
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

        // Native interop and structures
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
                [MarshalAs(UnmanagedType.LPWStr)] public string lpszMenuName;
                [MarshalAs(UnmanagedType.LPWStr)] public string lpszClassName;
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
                int x, int y, int nWidth, int nHeight,
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
            public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin,
               uint wMsgFilterMax, uint wRemoveMsg);

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
            public static extern int GetClientRect(IntPtr hWnd, out RECT lpRect);

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left, top, right, bottom;
            }
        }
    }


}