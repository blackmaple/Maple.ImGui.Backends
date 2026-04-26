using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain1;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain3;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain4;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain2
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
     
     
     */
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGISwapChain2Imp
    {
      
        public static readonly Guid GUID = new("A8BE2AC4-199F-4946-B331-79599FB98DE7");

        public readonly IDXGISwapChain1Imp IDXGISwapChain1;

        internal readonly Ptr_Func_SetSourceSize_29 SetSourceSize_29;
        internal readonly Ptr_Func_GetSourceSize_30 GetSourceSize_30;
        internal readonly Ptr_Func_SetMaximumFrameLatency_31 SetMaximumFrameLatency_31;
        internal readonly Ptr_Func_GetMaximumFrameLatency_32 GetMaximumFrameLatency_32;
        internal readonly Ptr_Func_GetFrameLatencyWaitableObject_33 GetFrameLatencyWaitableObject_33;
        internal readonly Ptr_Func_SetMatrixTransform_34 SetMatrixTransform_34;
        internal readonly Ptr_Func_GetMatrixTransform_35 GetMatrixTransform_35;

        
    }

    public static class IDXGISwapChain2ImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> @this)
        {
            public COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> BaseClass => Unsafe.As<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>>(ref @this);
        }
    }


    /// <summary>
    /// IDXGISwapChain::SetSourceSize
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetSourceSize_29(nint ptr) : IHookMethod
    {
        public const string Name = "SetSourceSize";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, int> SetSourceSize_29;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint, uint, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint, uint, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, uint width, uint height) => _proc(pThis, width, height);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::GetSourceSize
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetSourceSize_30(nint ptr) : IHookMethod
    {
        public const string Name = "GetSourceSize";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, uint*, int> GetSourceSize_30;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint*, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint*, uint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, uint* pWidth, uint* pHeight) => _proc(pThis, pWidth, pHeight);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::SetMaximumFrameLatency
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetMaximumFrameLatency_31(nint ptr) : IHookMethod
    {
        public const string Name = "SetMaximumFrameLatency";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, int> SetMaximumFrameLatency_31;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, uint maxLatency) => _proc(pThis, maxLatency);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::GetMaximumFrameLatency
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetMaximumFrameLatency_32(nint ptr) : IHookMethod
    {
        public const string Name = "GetMaximumFrameLatency";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint*, int> GetMaximumFrameLatency_32;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, uint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, uint* pMaxLatency) => _proc(pThis, pMaxLatency);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::GetFrameLatencyWaitableObject
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetFrameLatencyWaitableObject_33(nint ptr) : IHookMethod
    {
        public const string Name = "GetFrameLatencyWaitableObject";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*> GetFrameLatencyWaitableObject_33;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, void*> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, void*>)ptr;
        public nint PtrMethod => (nint)_proc;
        public void* Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::SetMatrixTransform
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetMatrixTransform_34(nint ptr) : IHookMethod
    {
        public const string Name = "SetMatrixTransform";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_MATRIX_3X2_F*, int> SetMatrixTransform_34;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MATRIX_3X2_F*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MATRIX_3X2_F*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, DXGI_MATRIX_3X2_F* pMatrix) => _proc(pThis, pMatrix);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain::GetMatrixTransform
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetMatrixTransform_35(nint ptr) : IHookMethod
    {
        public const string Name = "GetMatrixTransform";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_MATRIX_3X2_F*, int> GetMatrixTransform_35;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MATRIX_3X2_F*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MATRIX_3X2_F*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, DXGI_MATRIX_3X2_F* pMatrix) => _proc(pThis, pMatrix);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
