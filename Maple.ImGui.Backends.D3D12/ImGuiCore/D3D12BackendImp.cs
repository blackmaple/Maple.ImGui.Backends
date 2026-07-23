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
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;
using SharpGen.Runtime;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;
using ImGuiApi = Hexa.NET.ImGui.ImGui;
using VorticeD3D12 = Vortice.Direct3D12;
namespace Maple.ImGui.Backends.D3D12.ImGuiCore
{

    internal class D3D12BackendImp(
        ImGuiContextPtr imguiContext,
       D3D12ComponentContext componentContext,
        D3D12SyncContextManager syncContextManager,

        ImGuiBackendBridgeCollection bridgeCollection,
        IImGuiUIView view,
        DXGI_FORMAT rtvFormat,
        Queue<D3D12TextureSlot> imguiSrvSlots) : ImGuiBackendImpBase(bridgeCollection, view)
    {
        ImGuiContextPtr ImGuiContextPtr { get; set; } = imguiContext;
        D3D12ComponentContext ComponentContext { get; } = componentContext;
        D3D12SyncContextManager SyncContextManager { get; } = syncContextManager;
        DXGI_FORMAT RTVFormat { get; } = rtvFormat;
        bool D3D12Initialized { get; set; }
        Queue<D3D12TextureSlot> ImGuiSrvSlots { get; } = imguiSrvSlots;
        object ImGuiSrvSlotsLock { get; } = new();
        nint DescriptorAllocatorUserData { get; set; }

        // Diagnostic bridge wrappers. COM_PTR_IUNKNOWN remains the owner of these native pointers.
        VorticeD3D12.ID3D12GraphicsCommandList VorticeCommandList { get; } = new((nint)componentContext.ID3D12CommandListPtr);
        VorticeD3D12.ID3D12DescriptorHeap VorticeSrvHeap { get; } = new((nint)componentContext.SRVHeapPtr);
        VorticeD3D12.ID3D12Fence VorticeFence { get; } = new((nint)componentContext.ID3D12FencePtr);
        VorticeD3D12.ID3D12CommandQueue? VorticeCommandQueue { get; set; }

        private static VorticeD3D12.CpuDescriptorHandle ToVortice(D3D12_CPU_DESCRIPTOR_HANDLE handle)
            => new() { Ptr = (nuint)handle.ptr };

        private static VorticeD3D12.ResourceBarrier ToTransitionBarrier(
            VorticeD3D12.ID3D12Resource resource,
            D3D12_RESOURCE_STATES stateBefore,
            D3D12_RESOURCE_STATES stateAfter)
            => VorticeD3D12.ResourceBarrier.BarrierTransition(
                resource,
                (VorticeD3D12.ResourceStates)stateBefore,
                (VorticeD3D12.ResourceStates)stateAfter,
                PInvoke.D3D12_RESOURCE_BARRIER_ALL_SUBRESOURCES,
                default);

        private static unsafe T* ToHexaPtr<T>(nint ptr) where T : unmanaged
            => (T*)ptr;

        private static COM_PTR_IUNKNOWN<TImp> DetachToComPtr<TImp>(CppObject obj)
            where TImp : unmanaged
        {
            var nativePointer = obj.NativePointer;
            obj.NativePointer = IntPtr.Zero;
            return new COM_PTR_IUNKNOWN<TImp>(nativePointer);
        }

        private static D3D12_CPU_DESCRIPTOR_HANDLE ToWindows(VorticeD3D12.CpuDescriptorHandle handle)
            => new() { ptr = (nuint)handle.Ptr };

        private static D3D12_GPU_DESCRIPTOR_HANDLE ToWindows(VorticeD3D12.GpuDescriptorHandle handle)
            => new() { ptr = handle.Ptr };

        private static unsafe D3D12BackendImp GetBackendFromUserData(void* userData)
        {
            return (D3D12BackendImp)GCHandle.FromIntPtr((nint)userData).Target!;
        }

        private bool TryRentImGuiSrvSlot([MaybeNullWhen(false)] out D3D12TextureSlot slot)
        {
            lock (this.ImGuiSrvSlotsLock)
            {
                return this.ImGuiSrvSlots.TryDequeue(out slot);
            }
        }

        private void ReturnImGuiSrvSlot(D3D12TextureSlot slot)
        {
            lock (this.ImGuiSrvSlotsLock)
            {
                this.ImGuiSrvSlots.Enqueue(slot);
            }
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe void SrvDescriptorAllocCallback(ImGuiImplDX12InitInfo* info, D3D12CpuDescriptorHandle* outCpuDescHandle, D3D12GpuDescriptorHandle* outGpuDescHandle)
        {
            if (info is null || outCpuDescHandle is null || outGpuDescHandle is null)
            {
                return;
            }

            var backend = GetBackendFromUserData(info->UserData);
            if (!backend.TryRentImGuiSrvSlot(out var slot))
            {
                *outCpuDescHandle = default;
                *outGpuDescHandle = default;
                return;
            }

            *outCpuDescHandle = new(slot.CPU.ptr);
            *outGpuDescHandle = new(slot.GPU.ptr);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe void SrvDescriptorFreeCallback(ImGuiImplDX12InitInfo* info, D3D12CpuDescriptorHandle cpuDescHandle, D3D12GpuDescriptorHandle gpuDescHandle)
        {
            if (info is null)
            {
                return;
            }

            var backend = GetBackendFromUserData(info->UserData);
            backend.ReturnImGuiSrvSlot(new D3D12TextureSlot()
            {
                CPU = new() { ptr = cpuDescHandle.Ptr },
                GPU = new() { ptr = gpuDescHandle.Ptr },
            });
        }

        private unsafe bool TryInitializeD3D12Backend()
        {
            if (this.D3D12Initialized)
            {
                return true;
            }

            var srvCPU = ToWindows(this.ComponentContext.VorticeSRVHeap.GetCPUDescriptorHandleForHeapStart());
            var srvGPU = ToWindows(this.ComponentContext.VorticeSRVHeap.GetGPUDescriptorHandleForHeapStart());
            if (this.DescriptorAllocatorUserData == nint.Zero)
            {
                this.DescriptorAllocatorUserData = GCHandle.ToIntPtr(GCHandle.Alloc(this));
            }

            ImGuiImplDX12InitInfo initInfo = new()
            {
                Device = ToHexaPtr<Hexa.NET.ImGui.Backends.D3D12.ID3D12Device>((nint)this.ComponentContext.ID3D12DevicePtr),
                CommandQueue = ToHexaPtr<Hexa.NET.ImGui.Backends.D3D12.ID3D12CommandQueue>((nint)this.ComponentContext.ID3D12CommandQueuePtr),
                NumFramesInFlight = D3D12SyncContextManager.NUM_FRAMES_IN_FLIGHT,
                RTVFormat = (int)this.RTVFormat,
                DSVFormat = (int)DXGI_FORMAT.DXGI_FORMAT_UNKNOWN,
                UserData = (void*)this.DescriptorAllocatorUserData,
                SrvDescriptorHeap = ToHexaPtr<Hexa.NET.ImGui.Backends.D3D12.ID3D12DescriptorHeap>((nint)this.ComponentContext.SRVHeapPtr),
                SrvDescriptorAllocFn = (void*)(delegate* unmanaged[Cdecl]<ImGuiImplDX12InitInfo*, D3D12CpuDescriptorHandle*, D3D12GpuDescriptorHandle*, void>)&SrvDescriptorAllocCallback,
                SrvDescriptorFreeFn = (void*)(delegate* unmanaged[Cdecl]<ImGuiImplDX12InitInfo*, D3D12CpuDescriptorHandle, D3D12GpuDescriptorHandle, void>)&SrvDescriptorFreeCallback,
                LegacySingleSrvCpuDescriptor = new(srvCPU.ptr),
                LegacySingleSrvGpuDescriptor = new(srvGPU.ptr),
            };

            ImGuiImplD3D12.SetCurrentContext(this.ImGuiContextPtr);
            if (!ImGuiImplD3D12.Init(ref initInfo))
            {
                return false;
            }

            if (!ImGuiImplD3D12.CreateDeviceObjects())
            {
                ImGuiImplD3D12.Shutdown();
                return false;
            }

            this.D3D12Initialized = true;
            return true;
        }

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
            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            try
            {
                var desc = new VorticeD3D12.DescriptorHeapDescription(
                    VorticeD3D12.DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView,
                    D3D12ComponentContext.SRV_HEAP_CAPACITY,
                    VorticeD3D12.DescriptorHeapFlags.ShaderVisible,
                    0);
                var srvHeap = vorticeDevice.CreateDescriptorHeap(desc);
                return DetachToComPtr<ID3D12DescriptorHeapImp>(srvHeap);
            }
            finally
            {
                vorticeDevice.NativePointer = IntPtr.Zero;
            }
        }
        private static COM_PTR_IUNKNOWN<ID3D12DescriptorHeapImp> GetRtvHeap(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            try
            {
                var desc = new VorticeD3D12.DescriptorHeapDescription(
                    VorticeD3D12.DescriptorHeapType.RenderTargetView,
                    D3D12ComponentContext.RTV_HEAP_CAPACITY,
                    VorticeD3D12.DescriptorHeapFlags.None,
                    1);
                var rtvHeap = vorticeDevice.CreateDescriptorHeap(desc);
                return DetachToComPtr<ID3D12DescriptorHeapImp>(rtvHeap);
            }
            finally
            {
                vorticeDevice.NativePointer = IntPtr.Zero;
            }
        }
        private static COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> CreateDirectCommandAllocator(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            try
            {
                var commandAllocator = vorticeDevice.CreateCommandAllocator(VorticeD3D12.CommandListType.Direct);
                return DetachToComPtr<ID3D12CommandAllocatorImp>(commandAllocator);
            }
            finally
            {
                vorticeDevice.NativePointer = IntPtr.Zero;
            }
        }
        private static COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> CreateGraphicsCommandList(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice, COM_PTR_IUNKNOWN<ID3D12CommandAllocatorImp> pCommandAllocatorout)
        {
            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            var vorticeCommandAllocator = new VorticeD3D12.ID3D12CommandAllocator((nint)pCommandAllocatorout);
            try
            {
                var commandList = vorticeDevice.CreateCommandList<VorticeD3D12.ID3D12GraphicsCommandList>(
                    0U,
                    VorticeD3D12.CommandListType.Direct,
                    vorticeCommandAllocator,
                    null);
                commandList.Close();
                return DetachToComPtr<ID3D12GraphicsCommandListImp>(commandList);
            }
            finally
            {
                vorticeCommandAllocator.NativePointer = IntPtr.Zero;
                vorticeDevice.NativePointer = IntPtr.Zero;
            }
        }
        private static COM_PTR_IUNKNOWN<ID3D12FenceImp> CreateFence(COM_PTR_IUNKNOWN<ID3D12DeviceImp> pDevice)
        {
            var vorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice);
            try
            {
                var fence = vorticeDevice.CreateFence(0, VorticeD3D12.FenceFlags.None);
                return DetachToComPtr<ID3D12FenceImp>(fence);
            }
            finally
            {
                vorticeDevice.NativePointer = IntPtr.Zero;
            }
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
                frameContexts = [.. D3D12SyncContextManager.CreateFrameContexts(pDevice)];
                backBuffers = [.. D3D12SyncContextManager.CreateBackBuffers(pSwapChain, pDevice, rtvHeap, pDesc.BufferCount)];

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

                var vorticeSrvHeap = new VorticeD3D12.ID3D12DescriptorHeap((nint)srvHeap);
                var srvCPU = ToWindows(vorticeSrvHeap.GetCPUDescriptorHandleForHeapStart());
                var srvGPU = ToWindows(vorticeSrvHeap.GetGPUDescriptorHandleForHeapStart());
                vorticeSrvHeap.NativePointer = IntPtr.Zero;
                var imguiSrvSlots = D3D12ComponentContext.CreateDescriptorSlots(pDevice, srvCPU, srvGPU, D3D12ComponentContext.IMGUI_SRV_START_SLOT, D3D12ComponentContext.IMGUI_SRV_MAX_SLOT);

                var componentContext = new D3D12ComponentContext()
                {

                    ID3D12DevicePtr = pDevice,
                    VorticeDevice = new VorticeD3D12.ID3D12Device((nint)pDevice),
                    SRVHeapPtr = srvHeap,
                    VorticeSRVHeap = new VorticeD3D12.ID3D12DescriptorHeap((nint)srvHeap),
                    RTVHeapPtr = rtvHeap,
                    VorticeRTVHeap = new VorticeD3D12.ID3D12DescriptorHeap((nint)rtvHeap),
                    ID3D12FencePtr = pFence,
                    VorticeFence = new VorticeD3D12.ID3D12Fence((nint)pFence),
                    ID3D12CommandListPtr = pCommandList,
                    VorticeCommandList = new VorticeD3D12.ID3D12GraphicsCommandList((nint)pCommandList),
                    ID3D12CommandAllocatorPtr = pCommandAllocator,
                    TextureSlots = D3D12ComponentContext.CreateTextureSlot(pDevice, srvHeap, srvCPU, srvGPU),
                };

                backendImp = new D3D12BackendImp(
                    imguiContext, componentContext,
                    syncContextManager, backendService.BridgeCollection, backendService.View, pDesc.BufferDesc.Format, imguiSrvSlots);
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
            if (this.D3D12Initialized)
            {
                ImGuiImplD3D12.InvalidateDeviceObjects();
            }

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
            if (this.D3D12Initialized)
            {
                ImGuiImplD3D12.CreateDeviceObjects();
            }
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
            var drawData = ImGuiApi.GetDrawData();
            ref var commandList = ref Unsafe.AsRef<Hexa.NET.ImGui.Backends.D3D12.ID3D12GraphicsCommandList>(
                (void*)(nint)this.ComponentContext.ID3D12CommandListPtr);
            ImGuiImplD3D12.RenderDrawData(drawData, ref commandList);


        }
        #endregion

        public override bool Initialize(nint context)
        {
            var initialized = this.ComponentContext.SetCommandQueue(new COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>(context));
            if (!initialized)
            {
                return false;
            }

            if (this.VorticeCommandQueue is null)
            {
                this.VorticeCommandQueue = new((nint)this.ComponentContext.ID3D12CommandQueuePtr);
            }

            return this.TryInitializeD3D12Backend();
        }

        public unsafe override void Run(nint context)
        {
            if (!this.D3D12Initialized || this.ComponentContext.ID3D12CommandQueuePtr == nint.Zero || this.VorticeCommandQueue is null)
            {
                return;
            }

            var pSwapChain = new COM_PTR_IUNKNOWN<IDXGISwapChainImp>(context);
            pSwapChain.As3(out var pSwapChain3);

            //1. 等待上一帧 GPU 完成（如果尚未完成）
            ref var currentFrameContext = ref this.SyncContextManager.GetCurrentFrameContext(this.ComponentContext.GetCurrSignaledValue());
            if (currentFrameContext.FenceValue != 0 && this.VorticeFence.CompletedValue < currentFrameContext.FenceValue)
            {
                var fenceEvent = ID3D12FenceImpExtension.CreateEvent();
                this.VorticeFence.SetEventOnCompletion(currentFrameContext.FenceValue, fenceEvent);
                PInvoke.WaitForSingleObject(fenceEvent, PInvoke.INFINITE);
                PInvoke.CloseHandle(fenceEvent);
            }

            var vorticeCommandAllocator = currentFrameContext.VorticeCommandAllocator!;
            vorticeCommandAllocator.Reset();


            this.Starting(context);
            this.Start(context);
            this.Started(context);

            //获取当前后台缓冲区索引和 RTV 句柄
            ref var currbackBuffer = ref this.SyncContextManager.GetCurrentBackBuffer(pSwapChain3);
            var toRenderTargetBarrier = ToTransitionBarrier(
                currbackBuffer.VorticeResource!,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PRESENT,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_RENDER_TARGET);
            var toPresentBarrier = ToTransitionBarrier(
                currbackBuffer.VorticeResource!,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_RENDER_TARGET,
                D3D12_RESOURCE_STATES.D3D12_RESOURCE_STATE_PRESENT);

            //重置命令列表（使用当前帧的 CommandAllocator）
            this.VorticeCommandList.Reset(vorticeCommandAllocator, null);
            //第一个 ResourceBarrier: PRESENT -> RENDER_TARGET
            this.VorticeCommandList.ResourceBarrier([toRenderTargetBarrier]);
            //设置渲染目标
            this.VorticeCommandList.OMSetRenderTargets(ToVortice(currbackBuffer.RTV), null);
            //设置描述符堆（SRV 堆，用于字体纹理等）
            this.VorticeCommandList.SetDescriptorHeaps(this.VorticeSrvHeap);


            this.Build(context);

            //第二个 ResourceBarrier: RENDER_TARGET -> PRESENT
            this.VorticeCommandList.ResourceBarrier([toPresentBarrier]);

            //关闭命令列表并执行
            this.VorticeCommandList.Close();
            this.VorticeCommandQueue!.ExecuteCommandLists([this.VorticeCommandList]);

            // 5. 提交信号并更新 Fence 值
            currentFrameContext.FenceValue = this.ComponentContext.GetNextSignaledValue();
            this.VorticeCommandQueue.Signal(this.VorticeFence, currentFrameContext.FenceValue);
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

                foreach (var texture in this.TextureCache.Keys)
                {
                    var com = new COM_PTR_IUNKNOWN(texture);
                    com.Release();
                }
                this.TextureCache.Clear();

                if (this.D3D12Initialized)
                {
                    ImGuiImplD3D12.Shutdown();
                    this.D3D12Initialized = false;
                }
                if (this.DescriptorAllocatorUserData != nint.Zero)
                {
                    GCHandle.FromIntPtr(this.DescriptorAllocatorUserData).Free();
                    this.DescriptorAllocatorUserData = nint.Zero;
                }
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