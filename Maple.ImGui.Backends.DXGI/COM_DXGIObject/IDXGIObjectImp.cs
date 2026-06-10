using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Maple.ImGui.Backends.DXGI.COM_DXGIObject
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGIObjectImp
    {
        public static readonly Guid GUID = new("AE02EED5-5380-4216-9CA2-812AFAEBF6AD");

        // IUnknown + IDXGIObject 方法 (槽位 0-7)
        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetDevice_7 GetDevice_7;

    }


    /// <summary>
    /// IDXGIObject::GetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetPrivateData_3(nint ptr) : IHookMethod
    {
        public const string Name = "GetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_3;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, uint*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, uint*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIObjectImp> pThis, Guid* guid, uint* pDataSize, void* pData) => _proc(pThis, guid, pDataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGIObject::SetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateData_4(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, uint, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIObjectImp> pThis, Guid* guid, uint dataSize, void* pData) => _proc(pThis, guid, dataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGIObject::SetPrivateDataInterface
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateDataInterface_5(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateDataInterface";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_5;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIObjectImp> pThis, Guid* guid, void* pUnkData) => _proc(pThis, guid, pUnkData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGIObject::SetName
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetName_6(nint ptr) : IHookMethod
    {
        public const string Name = "SetName";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, int> SetName_6;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, PCWSTR, COM_HRESULT> _proc 
            = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, PCWSTR, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIObjectImp> pThis, PCWSTR name) => _proc(pThis, name);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// IDXGIObject::GetDevice
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDevice_7(nint ptr) : IHookMethod
    {
        public const string Name = "GetDevice";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGIObjectImp>, Guid*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGIObjectImp> pThis, Guid* riid, void** ppvDevice) => _proc(pThis, riid, ppvDevice);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
