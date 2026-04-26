using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{
    public static class ComExtensions
    {
        extension(COM_PTR_IUNKNOWN @this)
        {
            public ref readonly COM_IUNKNOWN IUNKNOWN => ref @this.PTR_IUNKNOWN.Raw;
            public ref readonly COM_IUNKNOWN_VTABLE IUnknown_VTable => ref @this.IUNKNOWN.VTable;

            public COM_HRESULT QueryInterface<TSub>(in Guid riid, out COM_PTR_IUNKNOWN<TSub> ppvObject)
                where TSub : unmanaged
                => @this.IUnknown_VTable.QueryInterface_0.Invoke(@this, in riid, out ppvObject);

            public COM_HRESULT Release()
                => @this.IUnknown_VTable.Release_2.Invoke(@this);

            public COM_HRESULT AddRef()
                => @this.IUnknown_VTable.AddRef_1.Invoke(@this);

            public unsafe TAs* AsPointer<TAs>() where TAs : unmanaged
            {
                return (TAs*)@this.PTR_IUNKNOWN.Pointer;
            }
        }

        extension<T>(COM_PTR_IUNKNOWN<T> @this)
            where T : unmanaged
        {
            public ref readonly COM_IUNKNOWN<T> IUNKNOWN => ref @this.PTR_IUNKNOWN.Raw;
            public ref readonly COM_IUNKNOWN_VTABLE<T> VTable => ref @this.IUNKNOWN.VTable;
            public ref readonly COM_IUNKNOWN_VTABLE IUnknown_VTable => ref @this.VTable.IUnknown_VTable;
            public ref readonly T Interface_VTable => ref @this.VTable.Interface_VTable;

            public COM_HRESULT QueryInterface<TSub>(in Guid riid, out COM_PTR_IUNKNOWN<TSub> ppvObject)
                where TSub : unmanaged
                => @this.IUnknown_VTable.QueryInterface_0.Invoke(@this, in riid, out ppvObject);

            public COM_HRESULT Release()
                => @this.IUnknown_VTable.Release_2.Invoke(@this);

            public COM_HRESULT AddRef()
                => @this.IUnknown_VTable.AddRef_1.Invoke(@this);

            public unsafe TAs* AsPointer<TAs>() where TAs : unmanaged
            {
                return (TAs*)@this.PTR_IUNKNOWN.Pointer;
            }
        }

    }
}