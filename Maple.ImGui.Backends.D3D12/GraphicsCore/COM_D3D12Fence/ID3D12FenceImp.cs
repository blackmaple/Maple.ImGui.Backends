using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Security;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence
{
    /*
         public delegate* unmanaged[MemberFunction]<void*, global::System.Guid*, void**, int> QueryInterface_0;
    public delegate* unmanaged[MemberFunction]<void*, uint> AddRef_1;
    public delegate* unmanaged[MemberFunction]<void*, uint> Release_2;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_3;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_5;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, int> SetName_6;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong> GetCompletedValue_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong, void*, int> SetEventOnCompletion_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong, int> Signal_10;
     */
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12FenceImp
    {
        public static readonly Guid GUID = new("0A753DCF-C4D8-4B91-ADF6-BE5A60D95A76");

        // IUnknown + IDXGIObject 方法 (槽位 0-7)
        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetDevice_7 GetDevice_7;

        // ID3D12Fence 特有方法 (槽位 8-10)
        internal readonly Ptr_Func_GetCompletedValue_8 GetCompletedValue_8;
        internal readonly Ptr_Func_SetEventOnCompletion_9 SetEventOnCompletion_9;
        internal readonly Ptr_Func_Signal_10 Signal_10;

    }

    public static class ID3D12FenceImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D12FenceImp> @this)
        {
            internal ulong GetCompletedValue() => @this.Interface_VTable.GetCompletedValue_8.Invoke(@this);
            internal COM_HRESULT SetEventOnCompletion(ulong value, HANDLE hEvent) => @this.Interface_VTable.SetEventOnCompletion_9.Invoke(@this, value, hEvent);
            internal COM_HRESULT Signal(ulong value) => @this.Interface_VTable.Signal_10.Invoke(@this, value);

            internal static unsafe HANDLE CreateEvent() => PInvoke.CreateEvent(default(SECURITY_ATTRIBUTES*), false, false, default(PCWSTR));
        }
    }




    /// <summary>
    /// ID3D12Fence::GetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetPrivateData_3(nint ptr) : IHookMethod
    {
        public const string Name = "GetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_3;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, uint*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, uint*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, Guid* guid, uint* pDataSize, void* pData) => _proc(pThis, guid, pDataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::SetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateData_4(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, uint, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, Guid* guid, uint dataSize, void* pData) => _proc(pThis, guid, dataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::SetPrivateDataInterface
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateDataInterface_5(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateDataInterface";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_5;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, Guid* guid, void* pUnkData) => _proc(pThis, guid, pUnkData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::SetName
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetName_6(nint ptr) : IHookMethod
    {
        public const string Name = "SetName";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, int> SetName_6;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, PCWSTR, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, PCWSTR, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, PCWSTR name) => _proc(pThis, name);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::GetDevice
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDevice_7(nint ptr) : IHookMethod
    {
        public const string Name = "GetDevice";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, Guid*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, Guid* riid, void** ppvDevice) => _proc(pThis, riid, ppvDevice);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::GetCompletedValue
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetCompletedValue_8(nint ptr) : IHookMethod
    {
        public const string Name = "GetCompletedValue";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong> GetCompletedValue_8;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, ulong> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, ulong>)ptr;
        public nint PtrMethod => (nint)_proc;
        public ulong Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::SetEventOnCompletion
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetEventOnCompletion_9(nint ptr) : IHookMethod
    {
        public const string Name = "SetEventOnCompletion";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong, void*, int> SetEventOnCompletion_9;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, ulong, HANDLE, COM_HRESULT> _proc =
            (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, ulong, HANDLE, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, ulong value, HANDLE hEvent) => _proc(pThis, value, hEvent);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Fence::Signal
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Signal_10(nint ptr) : IHookMethod
    {
        public const string Name = "Signal";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong, int> Signal_10;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, ulong, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12FenceImp>, ulong, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12FenceImp> pThis, ulong value) => _proc(pThis, value);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
