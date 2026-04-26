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
    /// ID3D12GraphicsCommandList::DrawInstanced
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
        internal unsafe readonly struct Ptr_Func_DrawInstanced_12(nint ptr) : IHookMethod
        {
            public const string Name = "DrawInstanced";
            /// <summary>
            /// public delegate* unmanaged[MemberFunction]<global::System.Runtime.InteropServices.ComWrappers.ComInterfaceDispatch*, uint, uint, uint, uint, void> DrawInstanced_12;
            /// </summary>
            private readonly unsafe delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, uint, uint, void> _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp>, uint, uint, uint, uint, void>)ptr;
            public nint PtrMethod => (nint)_proc;
            public unsafe void Invoke(COM_PTR_IUNKNOWN<ID3D12GraphicsCommandListImp> pThis, uint vertexCountPerInstance, uint instanceCount, uint startVertexLocation, uint startInstanceLocation) => _proc(pThis, vertexCountPerInstance, instanceCount, startVertexLocation, startInstanceLocation);
            public override string ToString() => PtrMethod.ToString("X8");
        }
    }
