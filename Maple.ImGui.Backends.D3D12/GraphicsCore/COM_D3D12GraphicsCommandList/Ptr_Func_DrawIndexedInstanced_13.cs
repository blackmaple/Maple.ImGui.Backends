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
    /// ID3D12GraphicsCommandList::DrawIndexedInstanced
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
        internal unsafe readonly struct Ptr_Func_DrawIndexedInstanced_13(nint ptr) : IHookMethod
        {
            public const string Name = "DrawIndexedInstanced";
            /// <summary>
            /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, int, uint, void> DrawIndexedInstanced_13;
            /// </summary>
            private readonly unsafe delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, uint, int, uint, void> _proc = (delegate* unmanaged[Stdcall]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, uint, int, uint, void>)ptr;
            public nint PtrMethod => (nint)_proc;
            public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint indexCountPerInstance, uint instanceCount, uint startIndexLocation, int baseVertexLocation, uint startInstanceLocation) => _proc(pThis, indexCountPerInstance, instanceCount, startIndexLocation, baseVertexLocation, startInstanceLocation);
            public override string ToString() => PtrMethod.ToString("X8");
        }
}
