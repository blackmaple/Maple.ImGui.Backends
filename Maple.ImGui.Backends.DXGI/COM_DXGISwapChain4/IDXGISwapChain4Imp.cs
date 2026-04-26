using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain3;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain1;
using Maple.ImGui.Backends.DXGI.COM_DXGISwapChain2;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Dxgi;

namespace Maple.ImGui.Backends.DXGI.COM_DXGISwapChain4
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct IDXGISwapChain4Imp
    {
        public static readonly Guid GUID = new("3D585D5A-BD4A-489E-B1F4-3DBCB6452FFB");


        public readonly IDXGISwapChain3Imp IDXGISwapChain3Imp;

        internal readonly Ptr_Func_SetHDRMetaData_40 SetHDRMetaData_40;



        public static implicit operator IDXGISwapChain3Imp(IDXGISwapChain4Imp value) => value;
    }

    public static class IDXGISwapChain4ImpExtension
    {
        extension(COM_PTR_IUNKNOWN<IDXGISwapChain4Imp> @this)
        {
            public COM_PTR_IUNKNOWN<IDXGISwapChain3Imp> BaseClass => Unsafe.As<COM_PTR_IUNKNOWN<IDXGISwapChain4Imp>, COM_PTR_IUNKNOWN<IDXGISwapChain3Imp>>(ref @this);
        }
    }

    /// <summary>
    /// IDXGISwapChain4::SetHDRMetaData
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetHDRMetaData_40(nint ptr) : IHookMethod
    {
        public const string Name = "SetHDRMetaData";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, global::Windows.Win32.Graphics.Dxgi.DXGI_HDR_METADATA_TYPE, uint, void*, int> SetHDRMetaData_40;
        /// </summary>
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain4Imp>, DXGI_HDR_METADATA_TYPE, uint, void*, COM_HRESULT> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<IDXGISwapChain4Imp>, DXGI_HDR_METADATA_TYPE, uint, void*, COM_HRESULT>)ptr;
        public nint PtrMethod => (nint)_proc;
        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN<IDXGISwapChain4Imp> pThis, DXGI_HDR_METADATA_TYPE type, uint size, void* pMetaData) => _proc(pThis, type, size, pMetaData);
        public override string ToString() => PtrMethod.ToString("X8");
    }

}
