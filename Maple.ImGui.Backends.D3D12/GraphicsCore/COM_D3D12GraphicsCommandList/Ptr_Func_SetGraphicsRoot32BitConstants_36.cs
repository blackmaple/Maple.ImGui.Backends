using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D;
using Windows.Win32.Graphics.Direct3D12;
using Windows.Win32.Graphics.Dxgi.Common;
namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12GraphicsCommandList
{

    /// <summary>
    /// ID3D12GraphicsCommandList::SetGraphicsRoot32BitConstants
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal unsafe readonly struct Ptr_Func_SetGraphicsRoot32BitConstants_36(nint ptr) : IHookMethod
    {
        public const string Name = "SetGraphicsRoot32BitConstants";
        /// <summary>
        /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, void*, uint, void> SetGraphicsRoot32BitConstants_36;
        /// </summary>
        private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, void*, uint, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, void*, uint, void>)ptr;
        public nint PtrMethod => (nint)_proc;
        public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint rootParameterIndex, uint num32BitValuesToSet, void* pSrcData, uint destOffsetIn32BitValues) => _proc(pThis, rootParameterIndex, num32BitValuesToSet, pSrcData, destOffsetIn32BitValues);
        public override string ToString() => PtrMethod.ToString("X8");
    }
}
