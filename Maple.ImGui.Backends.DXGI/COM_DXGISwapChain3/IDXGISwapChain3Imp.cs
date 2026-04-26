using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain2;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain1;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain4;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain3
{
    /*
         public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetParent_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT, int> Present_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.Guid*, void**, int> GetBuffer_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int, void*, int> SetFullscreenState_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.BOOL*, global::Windows.Win32.Graphics.Dxgi.IDXGIOutput_unmanaged**, int> GetFullscreenState_11;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC*, int> GetDesc_12;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, int> ResizeBuffers_13;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_DESC*, int> ResizeTarget_14;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetContainingOutput_15;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_FRAME_STATISTICS*, int> GetFrameStatistics_16;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, int> GetLastPresentCount_17;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC1*, int> GetDesc1_18;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int> GetFullscreenDesc_19;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.HWND*, int> GetHwnd_20;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetCoreWindow_21;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT_PARAMETERS*, int> Present1_22;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> IsTemporaryMonoSupported_23;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetRestrictToOutput_24;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_RGBA*, int> SetBackgroundColor_25;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_RGBA*, int> GetBackgroundColor_26;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_ROTATION, int> SetRotation_27;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_ROTATION*, int> GetRotation_28;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, int> SetSourceSize_29;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, uint*, int> GetSourceSize_30;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, int> SetMaximumFrameLatency_31;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, int> GetMaximumFrameLatency_32;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*> GetFrameLatencyWaitableObject_33;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_MATRIX_3X2_F*, int> SetMatrixTransform_34;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_MATRIX_3X2_F*, int> GetMatrixTransform_35;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetCurrentBackBufferIndex_36;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_COLOR_SPACE_TYPE, uint*, int> CheckColorSpaceSupport_37;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_COLOR_SPACE_TYPE, int> SetColorSpace1_38;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, uint*, global::System.IntPtr*, int> ResizeBuffers1_39;

     */

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGISwapChain3Imp
    {
        
        public readonly static Guid GUID = new("94D99BDB-F1F8-4AB0-B236-7DA0170EDAB1");

        public readonly IDXGISwapChain2Imp IDXGISwapChain2Imp;

        internal readonly Ptr_Func_GetCurrentBackBufferIndex_36 GetCurrentBackBufferIndex_36;
        internal readonly Ptr_Func_CheckColorSpaceSupport_37 CheckColorSpaceSupport_37;
        internal readonly Ptr_Func_SetColorSpace1_38 SetColorSpace1_38;
        internal readonly Ptr_Func_ResizeBuffers1_39 ResizeBuffers1_39;
    }


    public static class IDXGISwapChain3ImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> @this)
        {
           public uint GetCurrentBackBufferIndex()=> @this.Interface_VTable.GetCurrentBackBufferIndex_36.Invoke(@this);

            public COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> BaseClass => Unsafe.As<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>>(ref @this);


        }
    }


    ///// <summary>
    ///// IDXGISwapChain::SetPrivateData
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_SetPrivateData_3(nint ptr) : IHookMethod
    //{
    //    public const string Name = "SetPrivateData";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_3;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, uint, void*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, Guid* guid, uint dataSize, void* pData) => _proc(pThis, guid, dataSize, pData);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::SetPrivateDataInterface
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_SetPrivateDataInterface_4(nint ptr) : IHookMethod
    //{
    //    public const string Name = "SetPrivateDataInterface";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_4;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, Guid* guid, void* pUnkData) => _proc(pThis, guid, pUnkData);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetPrivateData
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetPrivateData_5(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetPrivateData";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_5;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, uint*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, uint*, void*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, Guid* guid, uint* pDataSize, void* pData) => _proc(pThis, guid, pDataSize, pData);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetParent
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetParent_6(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetParent";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetParent_6;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, Guid* riid, void** ppvObject) => _proc(pThis, riid, ppvObject);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetDevice
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetDevice_7(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetDevice";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, Guid* riid, void** ppvDevice) => _proc(pThis, riid, ppvDevice);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::Present
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_Present_8(nint ptr) : IHookMethod
    //{
    //    public const string Name = "Present";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT, int> Present_8;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, DXGI_PRESENT, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, DXGI_PRESENT, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, uint syncInterval, DXGI_PRESENT flags) => _proc(pThis, syncInterval, flags);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetBuffer
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetBuffer_9(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetBuffer";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::System.Guid*, void**, int> GetBuffer_9;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, Guid*, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, uint bufferIndex, Guid* riid, void** ppvObject) => _proc(pThis, bufferIndex, riid, ppvObject);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::SetFullscreenState
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_SetFullscreenState_10(nint ptr) : IHookMethod
    //{
    //    public const string Name = "SetFullscreenState";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int, void*, int> SetFullscreenState_10;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, int, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, int, void*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, int fullscreen, void* pTarget) => _proc(pThis, fullscreen, pTarget);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetFullscreenState
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetFullscreenState_11(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetFullscreenState";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.BOOL*, global::Windows.Win32.Graphics.Dxgi.IDXGIOutput_unmanaged**, int> GetFullscreenState_11;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, int*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, int*, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, int* pFullscreen, void** ppTarget) => _proc(pThis, pFullscreen, ppTarget);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetDesc
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetDesc_12(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetDesc";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC*, int> GetDesc_12;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_SWAP_CHAIN_DESC*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_SWAP_CHAIN_DESC*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_SWAP_CHAIN_DESC* pDesc) => _proc(pThis, pDesc);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::ResizeBuffers
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_ResizeBuffers_13(nint ptr) : IHookMethod
    //{
    //    public const string Name = "ResizeBuffers";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, int> ResizeBuffers_13;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, uint, uint, DXGI_FORMAT, uint, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, uint, uint, DXGI_FORMAT, uint, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, uint bufferCount, uint width, uint height, DXGI_FORMAT newFormat, uint swapChainFlags) => _proc(pThis, bufferCount, width, height, newFormat, swapChainFlags);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::ResizeTarget
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_ResizeTarget_14(nint ptr) : IHookMethod
    //{
    //    public const string Name = "ResizeTarget";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_DESC*, int> ResizeTarget_14;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_MODE_DESC*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_MODE_DESC*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_MODE_DESC* pNewTargetParameters) => _proc(pThis, pNewTargetParameters);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetContainingOutput
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetContainingOutput_15(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetContainingOutput";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetContainingOutput_15;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, void** ppOutput) => _proc(pThis, ppOutput);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetFrameStatistics
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetFrameStatistics_16(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetFrameStatistics";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_FRAME_STATISTICS*, int> GetFrameStatistics_16;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_FRAME_STATISTICS*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_FRAME_STATISTICS*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_FRAME_STATISTICS* pStats) => _proc(pThis, pStats);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetLastPresentCount
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetLastPresentCount_17(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetLastPresentCount";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, int> GetLastPresentCount_17;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, uint* pLastPresentCount) => _proc(pThis, pLastPresentCount);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetDesc1
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetDesc1_18(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetDesc1";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC1*, int> GetDesc1_18;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_SWAP_CHAIN_DESC1*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_SWAP_CHAIN_DESC1*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_SWAP_CHAIN_DESC1* pDesc) => _proc(pThis, pDesc);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetFullscreenDesc
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetFullscreenDesc_19(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetFullscreenDesc";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int> GetFullscreenDesc_19;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc) => _proc(pThis, pDesc);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetHwnd
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetHwnd_20(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetHwnd";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.HWND*, int> GetHwnd_20;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, nint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, nint*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, nint* pHwnd) => _proc(pThis, pHwnd);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetCoreWindow
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetCoreWindow_21(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetCoreWindow";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetCoreWindow_21;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, Guid*, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, Guid* riid, void** ppvObject) => _proc(pThis, riid, ppvObject);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::Present1
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_Present1_22(nint ptr) : IHookMethod
    //{
    //    public const string Name = "Present1";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT_PARAMETERS*, int> Present1_22;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, DXGI_PRESENT, DXGI_PRESENT_PARAMETERS*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, DXGI_PRESENT, DXGI_PRESENT_PARAMETERS*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, uint syncInterval, DXGI_PRESENT presentFlags, DXGI_PRESENT_PARAMETERS* pPresentParameters) => _proc(pThis, syncInterval, presentFlags, pPresentParameters);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::IsTemporaryMonoSupported
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_IsTemporaryMonoSupported_23(nint ptr) : IHookMethod
    //{
    //    public const string Name = "IsTemporaryMonoSupported";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> IsTemporaryMonoSupported_23;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, int>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public int Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis) => _proc(pThis);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetRestrictToOutput
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetRestrictToOutput_24(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetRestrictToOutput";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetRestrictToOutput_24;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, void**, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, void** ppRestrictToOutput) => _proc(pThis, ppRestrictToOutput);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::SetBackgroundColor
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_SetBackgroundColor_25(nint ptr) : IHookMethod
    //{
    //    public const string Name = "SetBackgroundColor";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_RGBA*, int> SetBackgroundColor_25;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_RGBA*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_RGBA*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_RGBA* pColor) => _proc(pThis, pColor);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetBackgroundColor
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetBackgroundColor_26(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetBackgroundColor";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_RGBA*, int> GetBackgroundColor_26;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_RGBA*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_RGBA*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_RGBA* pColor) => _proc(pThis, pColor);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::SetRotation
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_SetRotation_27(nint ptr) : IHookMethod
    //{
    //    public const string Name = "SetRotation";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_ROTATION, int> SetRotation_27;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_MODE_ROTATION, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_MODE_ROTATION, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_MODE_ROTATION rotation) => _proc(pThis, rotation);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}

    ///// <summary>
    ///// IDXGISwapChain::GetRotation
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //internal unsafe readonly struct Ptr_Func_GetRotation_28(nint ptr) : IHookMethod
    //{
    //    public const string Name = "GetRotation";
    //    /// <summary>
    //    /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_ROTATION*, int> GetRotation_28;
    //    /// </summary>
    //    private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_MODE_ROTATION*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_MODE_ROTATION*, COM_HRESULT>)ptr;
    //    public nint PtrMethod => (nint)_proc;
    //    public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_MODE_ROTATION* pRotation) => _proc(pThis, pRotation);
    //    public override string ToString() => PtrMethod.ToString("X8");
    //}


    /// <summary>
    /// IDXGISwapChain::GetCurrentBackBufferIndex
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetCurrentBackBufferIndex_36(nint ptr) : IHookMethod
    {
        public const string Name = "GetCurrentBackBufferIndex";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint> GetCurrentBackBufferIndex_36;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint>)ptr;
        public nint PtrMethod => (nint)_proc;
        public uint Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::CheckColorSpaceSupport
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_CheckColorSpaceSupport_37(nint ptr) : IHookMethod
    {
        public const string Name = "CheckColorSpaceSupport";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_COLOR_SPACE_TYPE, uint*, int> CheckColorSpaceSupport_37;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_COLOR_SPACE_TYPE, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_COLOR_SPACE_TYPE, uint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_COLOR_SPACE_TYPE colorSpace, uint* pColorSpaceSupport) => _proc(pThis, colorSpace, pColorSpaceSupport);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::SetColorSpace1
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetColorSpace1_38(nint ptr) : IHookMethod
    {
        public const string Name = "SetColorSpace1";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_COLOR_SPACE_TYPE, int> SetColorSpace1_38;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_COLOR_SPACE_TYPE, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, DXGI_COLOR_SPACE_TYPE, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, DXGI_COLOR_SPACE_TYPE colorSpace) => _proc(pThis, colorSpace);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::ResizeBuffers1
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ResizeBuffers1_39(nint ptr) : IHookMethod
    {
        public const string Name = "ResizeBuffers1";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_FORMAT, uint, uint*, global::System.IntPtr*, int> ResizeBuffers1_39;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, uint, uint, DXGI_FORMAT, uint, uint*, nint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>, uint, uint, uint, DXGI_FORMAT, uint, uint*, nint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> pThis, uint bufferCount, uint width, uint height, DXGI_FORMAT format, uint swapChainFlags, uint* pCreationNodeMask, nint* ppPresentQueue) => _proc(pThis, bufferCount, width, height, format, swapChainFlags, pCreationNodeMask, ppPresentQueue);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
