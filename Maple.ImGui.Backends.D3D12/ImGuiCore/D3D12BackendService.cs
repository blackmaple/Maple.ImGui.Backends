using Maple.Hook.WinMsg;
using Maple.ImGui.Backends.D3D12.GraphicsCore;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.HOOK_DXGISwapChain;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.ImGui.Backends.Windows.ImGuiCore;
using Maple.UnmanagedExtensions;
using Microsoft.Extensions.Logging;

namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{
    public class D3D12BackendService : Win32ImGuiBackendService
    {
        public DXGIPresentHookItem PresentHookItem { get; }
        public D3D12ExecuteCommandListsHookItem ExecuteCommandListsHookItem { get; }
        public DXGIResizeBuffersHookItem ResizeBuffersHookItem { get; }
        private bool _initialized = false;

        public D3D12BackendService(ILogger<D3D12BackendService> logger, IGraphicsHookFactory hookFactory, WinMsgHookFactory winMsgHookFactory, ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view)
            : base(logger, hookFactory, winMsgHookFactory, bridgeCollection, view)
        {
            this.PresentHookItem = hookFactory.Create<DXGIPresentHookItem>(EnumGraphicsType.D3D12);
            this.PresentHookItem.SyncCallback = HookPresent;
            this.ExecuteCommandListsHookItem = hookFactory.Create<D3D12ExecuteCommandListsHookItem>(EnumGraphicsType.D3D12);
            this.ExecuteCommandListsHookItem.SyncCallback = HookExecuteCommandLists;
            this.ResizeBuffersHookItem = hookFactory.Create<DXGIResizeBuffersHookItem>(EnumGraphicsType.D3D12);
            this.ResizeBuffersHookItem.SyncCallback = HookResizeBuffers;
        }

        private COM_HRESULT HookPresent(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint SyncInterval, uint Flags, DXGIPresentHookItem hookItem)
        {
            if (BackendImp is not null)
            {
                BackendImp.Run(@this);
            }
            else if (D3D12BackendImp.TryCreateImp(@this, this, out var backendImp))
            {
                BackendImp = backendImp;
            }
            var hr = hookItem.OriginalMethod.Invoke(@this, SyncInterval, Flags);
            return hr;
        }

        private void HookExecuteCommandLists(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> @this, uint NumCommandLists, UnsafeRef<COM_PTR_IUNKNOWN> ppCommandLists, D3D12ExecuteCommandListsHookItem hookItem)
        {
            if (_initialized == false && BackendImp is not null)
            {
                _initialized = BackendImp.Initialize(@this);
            }

            hookItem.OriginalMethod.Invoke(@this, NumCommandLists, ppCommandLists);

        }

        private COM_HRESULT HookResizeBuffers(COM_PTR_IUNKNOWN<IDXGISwapChainImp> @this, uint BufferCount, uint Width, uint Height, uint NewFormat, uint SwapChainFlags, DXGIResizeBuffersHookItem hookItem)
        {
            if (_initialized && BackendImp is not null)
            {
                BackendImp.Resetting(@this);
                BackendImp.Reset(@this);
                var h = hookItem.InvokeOriginal(@this, BufferCount, Width, Height, NewFormat, SwapChainFlags);
                if (h)
                {
                    BackendImp.Resetted(@this);
                }
                return h;
            }
            return hookItem.InvokeOriginal(@this, BufferCount, Width, Height, NewFormat, SwapChainFlags);
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.PresentHookItem.Enable();
            this.ExecuteCommandListsHookItem.Enable();
            this.ResizeBuffersHookItem.Enable();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.ExecuteCommandListsHookItem.Dispose();
            this.PresentHookItem.Dispose();
            this.ResizeBuffersHookItem.Dispose();

            this.BackendImp?.Dispose();
            // this.BackendImp = default;

            return Task.CompletedTask;

        }
    }
}
