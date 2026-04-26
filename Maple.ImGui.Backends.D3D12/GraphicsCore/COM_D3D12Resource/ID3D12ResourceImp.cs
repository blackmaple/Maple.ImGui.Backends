using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D12;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Resource
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
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_RANGE*, void**, int> Map_8;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_RANGE*, void> Unmap_9;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC> GetDesc_10;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong> GetGPUVirtualAddress_11;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_BOX*, void*, uint, uint, int> WriteToSubresource_12;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, uint, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_BOX*, int> ReadFromSubresource_13;
    public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_PROPERTIES*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_FLAGS*, int> GetHeapProperties_14;

     
     */
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12ResourceImp
    {
        
        public static readonly Guid GUID = new("696442BE-A72E-4059-BC79-5B5C98040FAD");

        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetDevice_7 GetDevice_7;
        internal readonly Ptr_Func_Map_8 Map_8;
        internal readonly Ptr_Func_Unmap_9 Unmap_9;
        internal readonly Ptr_Func_GetDesc_10 GetDesc_10;
        internal readonly Ptr_Func_GetGPUVirtualAddress_11 GetGPUVirtualAddress_11;
        internal readonly Ptr_Func_WriteToSubresource_12 WriteToSubresource_12;
        internal readonly Ptr_Func_ReadFromSubresource_13 ReadFromSubresource_13;
        internal readonly Ptr_Func_GetHeapProperties_14 GetHeapProperties_14;
    }


    public static class ID3D12ResourceImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D12ResourceImp> @this)
        {
            internal D3D12_RESOURCE_DESC GetDesc() => @this.Interface_VTable.GetDesc_10.Invoke(@this);
        }

    }



 

    /// <summary>
    /// ID3D12Resource::GetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetPrivateData_3(nint ptr) : IHookMethod
    {
        public const string Name = "GetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint*, void*, int> GetPrivateData_3;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, uint*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, uint*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, Guid* guid, uint* pDataSize, void* pData) => _proc(pThis, guid, pDataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::SetPrivateData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateData_4(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, uint, void*, int> SetPrivateData_4;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, uint, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, Guid* guid, uint dataSize, void* pData) => _proc(pThis, guid, dataSize, pData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::SetPrivateDataInterface
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetPrivateDataInterface_5(nint ptr) : IHookMethod
    {
        public const string Name = "SetPrivateDataInterface";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void*, int> SetPrivateDataInterface_5;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, Guid* guid, void* pUnkData) => _proc(pThis, guid, pUnkData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::SetName
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetName_6(nint ptr) : IHookMethod
    {
        public const string Name = "SetName";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Foundation.PCWSTR, int> SetName_6;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, PCWSTR, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, PCWSTR, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, PCWSTR name) => _proc(pThis, name);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::GetDevice
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDevice_7(nint ptr) : IHookMethod
    {
        public const string Name = "GetDevice";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::System.Guid*, void**, int> GetDevice_7;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, Guid*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, Guid* riid, void** ppvDevice) => _proc(pThis, riid, ppvDevice);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::Map
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Map_8(nint ptr) : IHookMethod
    {
        public const string Name = "Map";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_RANGE*, void**, int> Map_8;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, uint, D3D12_RANGE*, void**, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, uint, D3D12_RANGE*, void**, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, uint subresource, D3D12_RANGE* pReadRange, void** ppData) => _proc(pThis, subresource, pReadRange, ppData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::Unmap
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_Unmap_9(nint ptr) : IHookMethod
    {
        public const string Name = "Unmap";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_RANGE*, void> Unmap_9;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, uint, D3D12_RANGE*, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, uint, D3D12_RANGE*, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, uint subresource, D3D12_RANGE* pWrittenRange) => _proc(pThis, subresource, pWrittenRange);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::GetDesc
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetDesc_10(nint ptr) : IHookMethod
    {
        public const string Name = "GetDesc";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_RESOURCE_DESC> GetDesc_10;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, D3D12_RESOURCE_DESC> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, D3D12_RESOURCE_DESC>)ptr;
        public nint PtrMethod => (nint)_proc;
        public D3D12_RESOURCE_DESC Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::GetGPUVirtualAddress
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetGPUVirtualAddress_11(nint ptr) : IHookMethod
    {
        public const string Name = "GetGPUVirtualAddress";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, ulong> GetGPUVirtualAddress_11;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, ulong> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, ulong>)ptr;
        public nint PtrMethod => (nint)_proc;
        public ulong Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis) => _proc(pThis);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::WriteToSubresource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_WriteToSubresource_12(nint ptr) : IHookMethod
    {
        public const string Name = "WriteToSubresource";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_BOX*, void*, uint, uint, int> WriteToSubresource_12;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, uint, D3D12_BOX*, void*, uint, uint, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, uint, D3D12_BOX*, void*, uint, uint, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, uint dstSubresource, D3D12_BOX* pDstBox, void* pSrcData, uint srcRowPitch, uint srcDepthPitch) => _proc(pThis, dstSubresource, pDstBox, pSrcData, srcRowPitch, srcDepthPitch);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::ReadFromSubresource
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_ReadFromSubresource_13(nint ptr) : IHookMethod
    {
        public const string Name = "ReadFromSubresource";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, void*, uint, uint, uint, global::Windows.Win32.Graphics.Direct3D12.D3D12_BOX*, int> ReadFromSubresource_13;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, void*, uint, uint, uint, D3D12_BOX*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, void*, uint, uint, uint, D3D12_BOX*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, void* pDstData, uint dstRowPitch, uint dstDepthPitch, uint srcSubresource, D3D12_BOX* pSrcBox) => _proc(pThis, pDstData, dstRowPitch, dstDepthPitch, srcSubresource, pSrcBox);
        public override string ToString() => PtrMethod.ToString("X8");
    }

    /// <summary>
    /// ID3D12Resource::GetHeapProperties
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_GetHeapProperties_14(nint ptr) : IHookMethod
    {
        public const string Name = "GetHeapProperties";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_PROPERTIES*, global::Windows.Win32.Graphics.Direct3D12.D3D12_HEAP_FLAGS*, int> GetHeapProperties_14;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, D3D12_HEAP_PROPERTIES*, D3D12_HEAP_FLAGS*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12ResourceImp>, D3D12_HEAP_PROPERTIES*, D3D12_HEAP_FLAGS*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<ID3D12ResourceImp> pThis, D3D12_HEAP_PROPERTIES* pHeapProperties, D3D12_HEAP_FLAGS* pHeapFlags) => _proc(pThis, pHeapProperties, pHeapFlags);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
