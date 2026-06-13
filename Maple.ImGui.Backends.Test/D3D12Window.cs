using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Vortice.Direct3D;
using Vortice.Direct3D12;
using Vortice.DXGI;
using Vortice.Mathematics;
using static Vortice.Direct3D12.D3D12;
using static Vortice.DXGI.DXGI;

namespace ImGui.App.D3D11
{
    public class D3D12Window : ITestWindow
    {
        private const int CW_USEDEFAULT = unchecked((int)0x80000000);
        private const uint WS_OVERLAPPEDWINDOW = 0x00CF0000;
        private const int SW_SHOW = 5;
        private const uint PM_REMOVE = 0x0001;
        private const int FrameCount = 2;

        private static readonly ID3D12Resource?[] s_renderTargets = new ID3D12Resource[FrameCount];
        private static readonly ID3D12CommandAllocator?[] s_commandAllocators = new ID3D12CommandAllocator[FrameCount];
        private static readonly ulong[] s_fenceValues = new ulong[FrameCount];

        private static IDXGIFactory4? s_factory;
        private static ID3D12Device? s_device;
        private static ID3D12CommandQueue? s_commandQueue;
        private static IDXGISwapChain3? s_swapChain;
        private static ID3D12DescriptorHeap? s_rtvHeap;
        private static ID3D12GraphicsCommandList? s_commandList;
        private static ID3D12Fence? s_fence;
        private static WndProcDelegate? s_wndProcDelegate;
        private static int s_rtvDescriptorSize;
        private static uint s_frameIndex;
        private static bool s_isMinimized;
        private static bool s_hasPendingResize;
        private static int s_pendingWidth = 1280;
        private static int s_pendingHeight = 720;

        public static void Run()
        {
            const string className = "VorticeD3D12Win32WindowClass";
            s_wndProcDelegate = WndProc;

            var hInstance = Native.GetModuleHandle(IntPtr.Zero);
            var wndClassEx = new Native.WNDCLASSEX()
            {
                cbSize = Marshal.SizeOf<Native.WNDCLASSEX>(),
                style = 0,
                lpfnWndProc = Marshal.GetFunctionPointerForDelegate(s_wndProcDelegate),
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
                "Vortice D3D12 Win32 Window",
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

                if (quit)
                {
                    continue;
                }

                if (s_hasPendingResize && !s_isMinimized)
                {
                    Resize(s_pendingWidth, s_pendingHeight);
                    s_hasPendingResize = false;
                }

                if (s_isMinimized)
                {
                    Thread.Sleep(10);
                    continue;
                }

                Render();
            }

            Cleanup();
        }

        private static void InitD3D(IntPtr hwnd)
        {
            s_factory = CreateDXGIFactory2<IDXGIFactory4>(false);

            var hr = D3D12CreateDevice(null, FeatureLevel.Level_11_0, out s_device);
            if (hr.Failure || s_device is null)
            {
                throw new InvalidOperationException($"D3D12CreateDevice failed: {hr.Code}");
            }

            s_commandQueue = s_device.CreateCommandQueue(new CommandQueueDescription(CommandListType.Direct));

            var swapChainDescription = new SwapChainDescription1()
            {
                Width = 1280,
                Height = 720,
                Format = Format.R8G8B8A8_UNorm,
                Stereo = false,
                SampleDescription = new SampleDescription(1, 0),
                BufferUsage = Vortice.DXGI.Usage.RenderTargetOutput,
                BufferCount = FrameCount,
                Scaling = Scaling.Stretch,
                SwapEffect = SwapEffect.FlipDiscard,
                AlphaMode = AlphaMode.Ignore,
                Flags = SwapChainFlags.None
            };

            using (var swapChain = s_factory.CreateSwapChainForHwnd(s_commandQueue, hwnd, swapChainDescription))
            {
                s_swapChain = swapChain.QueryInterface<IDXGISwapChain3>();
            }

            s_frameIndex = s_swapChain.CurrentBackBufferIndex;
            s_factory.MakeWindowAssociation(hwnd, WindowAssociationFlags.IgnoreAltEnter);

            s_rtvHeap = s_device.CreateDescriptorHeap(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, FrameCount));
            s_rtvDescriptorSize = (int)s_device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);

            for (var i = 0; i < FrameCount; i++)
            {
                s_commandAllocators[i] = s_device.CreateCommandAllocator(CommandListType.Direct);
            }
            var device = new COM_PTR_IUNKNOWN<ID3D12DeviceImp>(s_device.NativePointer);
            device.CreateGraphicsCommandList(0 , new (s_commandAllocators[0]!.NativePointer),   out  var commandList);
            s_commandList = new ID3D12GraphicsCommandList(commandList);
            //s_commandList = s_device.CreateCommandList<ID3D12GraphicsCommandList>(CommandListType.Direct, s_commandAllocators[0], null);
            s_commandList.Close();

            s_fence = s_device.CreateFence(0);

            CreateRenderTargets();
        }

        private static void CreateRenderTargets()
        {
            if (s_device is null || s_swapChain is null || s_rtvHeap is null)
            {
                return;
            }
            //   COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> rtvHeap = new(s_rtvHeap.NativePointer);
            var rtvHandle = s_rtvHeap.GetCPUDescriptorHandleForHeapStart();
            //var rtvHandle2 = s_rtvHeap.GetCPUDescriptorHandleForHeapStart();
            //CpuDescriptorHandle rtvHandle3  =    new CpuDescriptorHandle() { Ptr = rtvHeap.GetCPUDescriptorHandleForHeapStart_Test() };
            //CpuDescriptorHandle rtvHandle4= new CpuDescriptorHandle() { Ptr = rtvHeap.GetCPUDescriptorHandleForHeapStart_Test() };

            //var t = rtvHeap.GetGPUDescriptorHandleForHeapStart_Test();

            for (var i = 0; i < FrameCount; i++)
            {
                s_renderTargets[i]?.Dispose();
                s_renderTargets[i] = s_swapChain.GetBuffer<ID3D12Resource>((uint)i);
                s_device.CreateRenderTargetView(s_renderTargets[i], null, rtvHandle);

                //COM_PTR_IUNKNOWN<Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device.ID3D12DeviceImp> pDevice
                //    = new(s_device.NativePointer);
                //pDevice.CreateRenderTargetView(new (s_renderTargets[i]!.NativePointer), rtvHandle1.Ptr);

                rtvHandle += s_rtvDescriptorSize;
            }
        }

        private static void Render()
        {
            if (s_device is null || s_swapChain is null || s_commandQueue is null || s_commandList is null || s_fence is null)
            {
                return;
            }

            var frameIndex = (int)s_frameIndex;
            var renderTarget = s_renderTargets[frameIndex];
           // var commandAllocator 
            COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> commandAllocator =new(  s_commandAllocators[frameIndex]!.NativePointer);
            if (renderTarget is null || !commandAllocator || s_rtvHeap is null)
            {
                return;
            }
         
            commandAllocator.Reset();
            s_commandList.Reset(new ID3D12CommandAllocator(commandAllocator));

            var rtvHandle = s_rtvHeap.GetCPUDescriptorHandleForHeapStart();
            rtvHandle += frameIndex * s_rtvDescriptorSize;

            COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> commandList = new(s_commandList.NativePointer);
     
            s_commandList.ResourceBarrierTransition(renderTarget, ResourceStates.Present, ResourceStates.RenderTarget);
             s_commandList.OMSetRenderTargets(rtvHandle);
            //   commandList.ResourceBarrier_Test(ref resourceBarrier);
            //    commandList.OMSetRenderTargets_Test(rtvHandle.Ptr);
           s_commandList.ClearRenderTargetView(rtvHandle, new Color4(0.18f, 0.35f, 0.10f, 1.0f));

            s_commandList.ResourceBarrierTransition(renderTarget, ResourceStates.RenderTarget, ResourceStates.Present);

            s_commandList.Close();
            //  commandList.Close();
            s_commandQueue.ExecuteCommandList(s_commandList);
            //    COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> commandQueueImp = new(s_commandQueue.NativePointer);
            //    commandQueueImp.ExecuteCommandLists(new COM_PTR_IUNKNOWN(s_commandList.NativePointer));

            s_swapChain.Present(1, PresentFlags.None);
            

         //   COM_PTR_IUNKNOWN<IDXGISwapChainImp> swapChain = new(s_swapChain.NativePointer);
         //   swapChain.Present(1, (uint)PresentFlags.None);

            //  s_swapChain.Present(1, PresentFlags.None);
            MoveToNextFrame();


            unsafe static nint Get(nint _nativePointer, int index)
            {
                return new nint(*(void**)((nint)(*(IntPtr*)_nativePointer) + (nint)index * (nint)sizeof(void*)));
            }
        }

        private static void Resize(int width, int height)
        {
            if (s_swapChain is null)
            {
                return;
            }

            width = Math.Max(1, width);
            height = Math.Max(1, height);

            WaitForGpu();

            for (var i = 0; i < FrameCount; i++)
            {
                s_renderTargets[i]?.Dispose();
                s_renderTargets[i] = null;
            }

            s_swapChain.ResizeBuffers(FrameCount, (uint)Math.Max(1, width), (uint)Math.Max(1, height), Format.R8G8B8A8_UNorm, SwapChainFlags.None);
            s_frameIndex = s_swapChain.CurrentBackBufferIndex;
            CreateRenderTargets();
        }

        private static void MoveToNextFrame()
        {
            if (s_swapChain is null || s_commandQueue is null || s_fence is null)
            {
                return;
            }

            var currentFrame = s_frameIndex;
            var nextFenceValue = ++s_fenceValues[currentFrame];
            s_commandQueue.Signal(s_fence, nextFenceValue);

            s_frameIndex = s_swapChain.CurrentBackBufferIndex;

            var frameFenceValue = s_fenceValues[s_frameIndex];
            while (frameFenceValue != 0 && s_fence.CompletedValue < frameFenceValue)
            {
                Thread.Sleep(1);
            }
        }

        private static void WaitForGpu()
        {
            if (s_commandQueue is null || s_fence is null)
            {
                return;
            }

            var fenceValue = ++s_fenceValues[s_frameIndex];
            s_commandQueue.Signal(s_fence, fenceValue);
            while (s_fence.CompletedValue < fenceValue)
            {
                Thread.Sleep(1);
            }
        }

        private static void Cleanup()
        {
            WaitForGpu();

            for (var i = 0; i < FrameCount; i++)
            {
                s_renderTargets[i]?.Dispose();
                s_renderTargets[i] = null;

                s_commandAllocators[i]?.Dispose();
                s_commandAllocators[i] = null;
            }

            s_commandList?.Dispose();
            s_commandList = null;

            s_rtvHeap?.Dispose();
            s_rtvHeap = null;

            s_swapChain?.Dispose();
            s_swapChain = null;

            s_commandQueue?.Dispose();
            s_commandQueue = null;

            s_fence?.Dispose();
            s_fence = null;

            s_device?.Dispose();
            s_device = null;

            s_factory?.Dispose();
            s_factory = null;
        }

        private static IntPtr WndProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case Native.WM_SIZE:
                    {
                        var width = lParam.ToInt32() & 0xFFFF;
                        var height = (lParam.ToInt32() >> 16) & 0xFFFF;
                        s_isMinimized = width == 0 || height == 0;
                        if (!s_isMinimized)
                        {
                            s_pendingWidth = Math.Max(1, width);
                            s_pendingHeight = Math.Max(1, height);
                            s_hasPendingResize = true;
                        }

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