using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain2;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain4;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;
using Windows.Win32.Graphics.Dxgi.Common;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain1
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
     
     */
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGISwapChain1Imp
    {
        
        public static readonly Guid GUID = new("790A45F7-0D42-4876-983A-0A55CFE6F4AA");

        internal readonly IDXGISwapChainImp DXGISwapChainImp;

        // IDXGISwapChain1 新增方法 (槽位 18-22)
        internal readonly Ptr_Func_GetDesc1_18 GetDesc1_18;
        internal readonly Ptr_Func_GetFullscreenDesc_19 GetFullscreenDesc_19;
        internal readonly Ptr_Func_GetHwnd_20 GetHwnd_20;
        internal readonly Ptr_Func_GetCoreWindow_21 GetCoreWindow_21;
        internal readonly Ptr_Func_Present1_22 Present1_22;

        // IDXGISwapChain1 继承自父接口的方法 (槽位 23-28)
        internal readonly Ptr_Func_IsTemporaryMonoSupported_23 IsTemporaryMonoSupported_23;
        internal readonly Ptr_Func_GetRestrictToOutput_24 GetRestrictToOutput_24;
        internal readonly Ptr_Func_SetBackgroundColor_25 SetBackgroundColor_25;
        internal readonly Ptr_Func_GetBackgroundColor_26 GetBackgroundColor_26;
        internal readonly Ptr_Func_SetRotation_27 SetRotation_27;
        internal readonly Ptr_Func_GetRotation_28 GetRotation_28;
    }

    public static class IDXGISwapChain1ImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> @this)
        {
            public COM_PTR_IUNKNOWN<IDXGISwapChainImp> BaseClass => Unsafe.As<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, COM_PTR_IUNKNOWN<IDXGISwapChainImp>>(ref @this);

        }
    }

    /// <summary>
    /// IDXGISwapChain1::GetDesc1
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDesc1_18(nint ptr) : IHookMethod
    {
        public const string Name = "GetDesc1";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_DESC1*, int> GetDesc1_18;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, DXGI_SWAP_CHAIN_DESC1*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, DXGI_SWAP_CHAIN_DESC1*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> pThis, DXGI_SWAP_CHAIN_DESC1* pDesc) => _proc(pThis, pDesc);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain1::GetFullscreenDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetFullscreenDesc_19(nint ptr) : IHookMethod
    {
        public const string Name = "GetFullscreenDesc";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, int> GetFullscreenDesc_19;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, DXGI_SWAP_CHAIN_FULLSCREEN_DESC*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> pThis, DXGI_SWAP_CHAIN_FULLSCREEN_DESC* pDesc) => _proc(pThis, pDesc);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain1::GetHwnd
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetHwnd_20(nint ptr) : IHookMethod
    {
        public const string Name = "GetHwnd";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.HWND*, int> GetHwnd_20;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, nint*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, nint*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> pThis, nint* pHwnd) => _proc(pThis, pHwnd);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain1::GetCoreWindow
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetCoreWindow_21(nint ptr) : IHookMethod
    {
        public const string Name = "GetCoreWindow";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetCoreWindow_21;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, Guid*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> pThis, Guid* riid, void** ppvObject) => _proc(pThis, riid, ppvObject);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain1::Present1
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Present1_22(nint ptr) : IHookMethod
    {
        public const string Name = "Present1";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT, global::Windows.Win32.Graphics.Dxgi.DXGI_PRESENT_PARAMETERS*, int> Present1_22;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, uint, DXGI_PRESENT, DXGI_PRESENT_PARAMETERS*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain1Imp>, uint, DXGI_PRESENT, DXGI_PRESENT_PARAMETERS*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain1Imp> pThis, uint syncInterval, DXGI_PRESENT flags, DXGI_PRESENT_PARAMETERS* pPresentParameters) => _proc(pThis, syncInterval, flags, pPresentParameters);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain2::IsTemporaryMonoSupported
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_IsTemporaryMonoSupported_23(nint ptr) : IHookMethod
    {
        public const string Name = "IsTemporaryMonoSupported";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, int> IsTemporaryMonoSupported_23;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, int> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, int>)ptr;
        public nint PtrMethod => (nint)_proc;
        public int Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain2::GetRestrictToOutput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetRestrictToOutput_24(nint ptr) : IHookMethod
    {
        public const string Name = "GetRestrictToOutput";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void**, int> GetRestrictToOutput_24;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, void** ppRestrictToOutput) => _proc(pThis, ppRestrictToOutput);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain2::SetBackgroundColor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetBackgroundColor_25(nint ptr) : IHookMethod
    {
        public const string Name = "SetBackgroundColor";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_RGBA*, int> SetBackgroundColor_25;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_RGBA*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_RGBA*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, DXGI_RGBA* pColor) => _proc(pThis, pColor);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain2::GetBackgroundColor
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetBackgroundColor_26(nint ptr) : IHookMethod
    {
        public const string Name = "GetBackgroundColor";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_RGBA*, int> GetBackgroundColor_26;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_RGBA*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_RGBA*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, DXGI_RGBA* pColor) => _proc(pThis, pColor);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain2::SetRotation
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetRotation_27(nint ptr) : IHookMethod
    {
        public const string Name = "SetRotation";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_ROTATION, int> SetRotation_27;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MODE_ROTATION, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MODE_ROTATION, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, DXGI_MODE_ROTATION rotation) => _proc(pThis, rotation);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGISwapChain2::GetRotation
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetRotation_28(nint ptr) : IHookMethod
    {
        public const string Name = "GetRotation";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.Common.DXGI_MODE_ROTATION*, int> GetRotation_28;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MODE_ROTATION*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain2Imp>, DXGI_MODE_ROTATION*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain2Imp> pThis, DXGI_MODE_ROTATION* pRotation) => _proc(pThis, pRotation);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
