using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using Maple.UnmanagedExtensions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore
{
    public class D3D12ExecuteCommandListsHookItem : HookItem<D3D12ExecuteCommandListsHookItem, Ptr_Func_ExecuteCommandLists_10, Ptr_Func_ExecuteCommandLists_10>, IGraphicsHookItem<D3D12ExecuteCommandListsHookItem>
    {
        public const string MethodName = Ptr_Func_ExecuteCommandLists_10.Name;

        public Action<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, D3D12ExecuteCommandListsHookItem>? SyncCallback { get; set; }
         

        public static D3D12ExecuteCommandListsHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D12ExecuteCommandListsHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D12ExecuteCommandListsHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<COM_PTR_IUNKNOWN<ID3D12CommandQueueImp>, uint, UnsafeRef<COM_PTR_IUNKNOWN>, void>
                _proc = &Hook_Present;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall), typeof(CallConvSuppressGCTransition)])]
        private static void Hook_Present(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> @this, uint NumCommandLists, UnsafeRef<COM_PTR_IUNKNOWN> ppCommandLists)
        {
            if (D3D12ExecuteCommandListsHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    hookItem.SyncCallback.Invoke(@this, NumCommandLists, ppCommandLists, hookItem);
                }
                else
                {
                    hookItem.OriginalMethod.Invoke(@this, NumCommandLists, ppCommandLists);
                }
            }
            
        }
    }
}
