using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3D9
{

    /// <summary>
    /// 封装 IDirect3D9::CreateDevice 函数指针 (VTable 索引 16)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal readonly unsafe struct Ptr_Func_CreateDevice_16(nint ptr)
    {
        private readonly delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, uint, D3DDEVTYPE, HWND, uint, UnsafeIn<D3DPRESENT_PARAMETERS>, UnsafeOut<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>>, COM_HRESULT> 
            _proc = (delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN, uint, D3DDEVTYPE, HWND, uint, UnsafeIn<D3DPRESENT_PARAMETERS>, UnsafeOut<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>>, COM_HRESULT>)ptr;

        public COM_HRESULT Invoke(COM_PTR_IUNKNOWN pThis, uint Adapter, D3DDEVTYPE DeviceType, HWND hFocusWindow, uint BehaviorFlags, in D3DPRESENT_PARAMETERS pPresentationParameters, out Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> ppReturnedDeviceInterface) 
            => _proc(pThis, Adapter, DeviceType, hFocusWindow, BehaviorFlags, UnsafeIn<D3DPRESENT_PARAMETERS>.FromIn(in pPresentationParameters), UnsafeOut<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>>.FromOut(out ppReturnedDeviceInterface));

        public override string ToString()
        {
            return (new nint(_proc)).ToString("X8");
        }
    }
}