using Hexa.NET.ImGui;
using Hexa.NET.ImGui.Backends.D3D12;
using Hexa.NET.ImGui.Backends.Vulkan;
using Hexa.NET.ImGui.Backends.Win32;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandAllocator;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12DescriptorHeap;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Device;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain3;
using Maple.ImGui.Backends.ImGuiCore;
using Maple.ImGui.Backends.Windows.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{

    internal class D3D12BackendImp(
        ImGuiContextPtr imguiContext,
       D3D12ComponentContext componentContext,
        D3D12SyncContextManager syncContextManager,

        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view) : ImGuiBackendImpBase(bridgeCollection, view)
    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = imguiContext;
        D3D12ComponentContext ComponentContext { get; } = componentContext;
        D3D12SyncContextManager SyncContextManager { get; } = syncContextManager;

        private static DXGI_SWAP_CHAIN_DESC GetDesc(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            var hr = pSwapChain.GetDesc<DXGI_SWAP_CHAIN_DESC>(out var pDesc);
            if (!hr)
            {
                return ImGuiBackendException.Throw<DXGI_SWAP_CHAIN_DESC>($"{nameof(IDXGISwapChainImpExtension.GetDesc)}:{hr}");
            }
            return pDesc;
        }
        private static COM_PTR_IUNKNOWN<ID3D12DeviceImp> GetDevice(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain)
        {
            var hr = pSwapChain.GetDevice(ID3D12DeviceImp.GUID, out COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice);
            if (!hr)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D12DeviceImp>>($"{nameof(IDXGISwapChainImpExtension.GetDevice)}:{hr}");
            }
            return pDevice;
        }
        private static COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> GetSrvHeap(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var hr = pDevice.CreateDescriptorHeapForSRV(D3D12ComponentContext.SRV_HEAP_CAPACITY, out var srvHeap);
            if (!hr)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>>($"{nameof(ID3D12DeviceImpExtension.CreateDescriptorHeapForSRV)}:{hr}");
            }
            return srvHeap;
        }
        private static COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> GetRtvHeap(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var hr = pDevice.CreateDescriptorHeapForRTV(D3D12ComponentContext.RTV_HEAP_CAPACITY, out var rtvHeap);
            if (!hr)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp>>($"{nameof(ID3D12DeviceImpExtension.CreateDescriptorHeapForRTV)}:{hr}");
            }
            return rtvHeap;
        }
        private static COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> CreateDirectCommandAllocator(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var hr = pDevice.CreateDirectCommandAllocator(out var pCommandAllocator);
            if (!hr)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp>>($"{nameof(ID3D12DeviceImpExtension.CreateDirectCommandAllocator)}:{hr}");
            }
            return pCommandAllocator;
        }
        private static COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> CreateGraphicsCommandList(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pCommandAllocatorout)
        {
            var hr = pDevice.CreateGraphicsCommandList(0U, pCommandAllocatorout, out var pCommandList);
            if (!hr)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>>($"{nameof(ID3D12DeviceImpExtension.CreateGraphicsCommandList)}:{hr}");
            }
            pCommandList.Close();
            return pCommandList;
        }
        private static COM_PTR_IUNKNOWN<ID3D12FenceImp> CreateFence(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var hr = pDevice.CreateFence(out var pFence);
            if (!hr)
            {
                return ImGuiBackendException.Throw<COM_PTR_IUNKNOWN<ID3D12FenceImp>>($"{nameof(ID3D12DeviceImpExtension.CreateFence)}:{hr}");
            }
            return pFence;
        }
        public unsafe static bool TryCreateImp(COM_PTR_IUNKNOWN<IDXGISwapChainImp> pSwapChain, D3D12BackendService backendService, out D3D12BackendImp backendImp)
        {
            Unsafe.SkipInit(out backendImp);
            COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice = default;
            COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> srvHeap = default;
            COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> rtvHeap = default;
            COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pCommandAllocator = default;
            COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pCommandList = default;
            COM_PTR_IUNKNOWN<ID3D12FenceImp> pFence = default;
            D3D12BackBuffer[]? backBuffers = default;
            D3D12FrameContext[]? frameContexts = default;
            var win32Init = false;
            var d3d12Init = false;
            ImGuiContextPtr imguiContext = default;
            try
            {
                var pDesc = GetDesc(pSwapChain);
                pDevice = GetDevice(pSwapChain);
                srvHeap = GetSrvHeap(pDevice);
                rtvHeap = GetRtvHeap(pDevice);
                pCommandAllocator = CreateDirectCommandAllocator(pDevice);
                pCommandList = CreateGraphicsCommandList(pDevice, pCommandAllocator);
                pFence = CreateFence(pDevice);
                backBuffers = [.. D3D12SyncContextManager.CreateBackBuffers(pSwapChain, pDevice, rtvHeap, pDesc.BufferCount)];
                frameContexts = [.. D3D12SyncContextManager.CreateFrameContexts(pDevice)];
                var syncContextManager = new D3D12SyncContextManager()
                {
                    BackBuffers = backBuffers,
                    FrameContexts = frameContexts,
                };


                imguiContext = ImGuiApi.CreateContext();
                ImGuiApi.SetCurrentContext(imguiContext);
                ImGuiImplWin32.SetCurrentContext(imguiContext);
                win32Init = backendService.InitPlatform(imguiContext, pDesc.OutputWindow);
                if (!win32Init)
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(D3D12BackendService.InitPlatform)} IS ERROR");
                }
                var srvCPU = srvHeap.GetCPUDescriptorHandleForHeapStart();
                var srvGPU = srvHeap.GetGPUDescriptorHandleForHeapStart();
                ImGuiImplDX12InitInfo initInfo = new()
                {
                    Device = pDevice.AsPointer<ID3D12DeviceImp, Hexa.NET.ImGui.Backends.D3D12.ID3D12Device>(),
                    NumFramesInFlight = D3D12SyncContextManager.NUM_FRAMES_IN_FLIGHT,
                    RTVFormat = (int)pDesc.BufferDesc.Format,
                    DSVFormat = (int)DXGI_FORMAT.DXGI_FORMAT_UNKNOWN,
                    SrvDescriptorHeap = srvHeap.AsPointer<ID3D12DescriptorHeapImp, Hexa.NET.ImGui.Backends.D3D12.ID3D12DescriptorHeap>(),
                    LegacySingleSrvCpuDescriptor = new(srvCPU.ptr),
                    LegacySingleSrvGpuDescriptor = new(srvGPU.ptr),

                };
                ImGuiImplD3D12.SetCurrentContext(imguiContext);
                d3d12Init = ImGuiImplD3D12.Init(ref initInfo);
                if (!d3d12Init)
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(ImGuiImplD3D12.Init)} IS ERROR");
                }
                if (!ImGuiImplD3D12.CreateDeviceObjects())
                {
                    return ImGuiBackendException.Throw<bool>($"{nameof(ImGuiImplD3D12.CreateDeviceObjects)} IS ERROR");
                }

                var componentContext = new D3D12ComponentContext()
                {

                    ID3D12DevicePtr = pDevice,
                    SRVHeapPtr = srvHeap,
                    RTVHeapPtr = rtvHeap,
                    ID3D12FencePtr = pFence,
                    ID3D12CommandListPtr = pCommandList,
                    ID3D12CommandAllocatorPtr = pCommandAllocator,
                    TextureSlots = D3D12ComponentContext.CreateTextureSlot(pDevice, srvHeap, srvCPU, srvGPU),
                };

                backendImp = new D3D12BackendImp(
                    imguiContext, componentContext,
                    syncContextManager, backendService.BridgeCollection, backendService.View);
                return true;

            }
            catch (Exception ex)
            {
                backendService.Logger.LogError("{NAME} EXCEPTION: {EX}", nameof(TryCreateImp), ex);

                D3D12SyncContextManager.DestroyBackBuffer(backBuffers);
                D3D12SyncContextManager.DestroyFrameContexts(frameContexts);
                if (pFence) pFence.Release();
                if (pCommandList) pCommandList.Release();
                if (pCommandAllocator) pCommandAllocator.Release();
                if (srvHeap) srvHeap.Release();
                if (rtvHeap) rtvHeap.Release();
                if (pDevice) pDevice.Release();


                if (d3d12Init) ImGuiImplD3D12.Shutdown();
                if (win32Init) ImGuiImplWin32.Shutdown();
                if (!imguiContext.IsNull) ImGuiApi.DestroyContext(imguiContext);
            }

            return false;


        }





        #region Reset

        public override void Resetting(nint context)
        {
            // 1. 等待 GPU 完成所有工作
            this.ComponentContext.WaitForGPU();


        }

        public override void Reset(nint context)
        {
            //2. 通知 ImGui 后端清理其设备资源（字体纹理等）
            ImGuiImplD3D12.InvalidateDeviceObjects();

            // 3. 释放你的 BackBuffer 资源
            this.SyncContextManager.DestroyBackBuffer();
        }

        public override void Resetted(nint context)
        {
            var pSwapChain = new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context);

            //5. 重新创建 BackBuffer 数组和 RTV
            this.SyncContextManager.ResetBackBuffers(pSwapChain, this.ComponentContext.ID3D12DevicePtr, this.ComponentContext.RTVHeapPtr);

            //6. 刷新 FrameContext 的 FenceValue（重置为 0）
            this.SyncContextManager.ResetFrameContexts();

            // 7. 通知 ImGui 后端重新创建设备资源
            ImGuiImplD3D12.CreateDeviceObjects();
        }
        #endregion

        #region Start


        protected override void Starting(nint context)
        {
            //3. 开始 ImGui 新帧
            ImGuiImplWin32.NewFrame();
            ImGuiImplD3D12.NewFrame();
            ImGuiApi.NewFrame();
        }

        protected override void Start(nint context)
        {
            //UI 绘制代码
            this.View.RaiseRender();
        }

        protected override void Started(nint context)
        {

            // ImGuiApi.EndFrame();
            //4. 结束 ImGui 帧并生成绘制数据（Render 内部调用 EndFrame）
            ImGuiApi.Render();

        }

        protected unsafe override void Build(nint context)
        {
            //执行 ImGui 的绘制命令
            var commandListPtr = this.ComponentContext.ID3D12CommandListPtr.AsPointer<ID3D12GraphicsCommandListImp, Hexa.NET.ImGui.Backends.D3D12.ID3D12GraphicsCommandList>();
            ImGuiImplD3D12.RenderDrawData(ImGuiApi.GetDrawData(), new ID3D12GraphicsCommandListPtr(commandListPtr));


        }
        #endregion

        public override bool Initialize(nint context)
        {
            this.ComponentContext.SetCommandQueue(new COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>(context));
            return true;
        }

        public unsafe override void Run(nint context)
        {
            if (this.ComponentContext.ID3D12CommandQueuePtr == nint.Zero)
            {
                return;
            }

            var pSwapChain = new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context);
            pSwapChain.As3(out var pSwapChain3);

            //1. 等待上一帧 GPU 完成（如果尚未完成）
            ref var currentFrameContext = ref this.SyncContextManager.GetCurrentFrameContext(this.ComponentContext.GetCurrSignaledValue());
            this.ComponentContext.WaitForFence(currentFrameContext.FenceValue);
            var pCommandAllocator = currentFrameContext.CommandAllocator;
            pCommandAllocator.Reset();


            this.Starting(context);
            this.Start(context);
            this.Started(context);

            //获取当前后台缓冲区索引和 RTV 句柄
            ref var currbackBuffer = ref this.SyncContextManager.GetCurrentBackBuffer(pSwapChain3);

            //重置命令列表（使用当前帧的 CommandAllocator）
            this.ComponentContext.ID3D12CommandListPtr.Reset(pCommandAllocator, default);


            var barrier = new D3D12_RESOURCE_BARRIER()
            {
                Type = D3D12_RESOURCE_BARRIER_TYPE.D3D12_RESOURCE_BARRIER_TYPE_TRANSITION,
                Flags = D3D12_RESOURCE_BARRIER_FLAGS.D3D12_RESOURCE_BARRIER_FLAG_NONE,
            };
            barrier.Anonymous.Transition = new D3D12_RESOURCE_TRANSITION_BARRIER_unmanaged()
            {
                pResource = ComExtensions.AsPointer<ID3D12Resource_unmanaged>(currbackBuffer.Resource),
                Subresource = PInvoke.D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES,
                StateBefore = D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PRESENT,
                StateAfter = D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_RENDER_TARGET
            };
            //第一个 ResourceBarrier: PRESENT -> RENDER_TARGET
            this.ComponentContext.ID3D12CommandListPtr.ResourceBarrier(barrier);
            //设置渲染目标
            this.ComponentContext.ID3D12CommandListPtr.OMSetRenderTargets(currbackBuffer.RTV);
            //设置描述符堆（SRV 堆，用于字体纹理等）
            this.ComponentContext.ID3D12CommandListPtr.SetDescriptorHeaps(this.ComponentContext.SRVHeapPtr);


            this.Build(context);

            //第二个 ResourceBarrier: RENDER_TARGET -> PRESENT
            barrier.Anonymous.Transition.StateBefore = D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PRESENT;
            barrier.Anonymous.Transition.StateAfter = D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_RENDER_TARGET;
            this.ComponentContext.ID3D12CommandListPtr.ResourceBarrier(barrier);

            //关闭命令列表并执行
            this.ComponentContext.ID3D12CommandListPtr.Close();
            this.ComponentContext.ID3D12CommandQueuePtr.ExecuteCommandLists(this.ComponentContext.ID3D12CommandListPtr);

            // 5. 提交信号并更新 Fence 值
            currentFrameContext.FenceValue = this.ComponentContext.SignalNextFrame();
        }

        protected override void Shutdown()
        {

            var imguiContext = this.ImGuiContextPtr;
            this.ImGuiContextPtr = default;
            if (!imguiContext.IsNull)
            {
                this.ComponentContext.WaitForGPU();

                this.ComponentContext.Dispose();
                this.SyncContextManager.Dispose();

                foreach(var texture  in this.TextureCache.Keys)
                {
                    var com = new COM_PTR_IUNKNOWN(texture);
                    com.Release();
                }
                this.TextureCache.Clear();

                ImGuiImplD3D12.Shutdown();
                ImGuiImplWin32.Shutdown();
                ImGuiApi.DestroyContext(imguiContext);
            }
        }

        protected override ImTextureID CreateImTextureID(nint textureNativePtr)
        {
            var pResource = new COM_PTR_IUNKNOWN<ID3D12ResourceImp>(textureNativePtr);
            if (this.ComponentContext.TryCreateShaderResourceView(pResource, out var pSRV))
            {
                return new ImTextureID(pSRV.ptr);
            }
            return default;
        }

      
    }
}