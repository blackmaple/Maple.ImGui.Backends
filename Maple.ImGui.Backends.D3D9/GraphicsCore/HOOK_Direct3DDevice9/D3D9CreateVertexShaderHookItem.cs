using Maple.Hook.Abstractions;
using Maple.ImGui.Backends.D3D9.GraphicsCore.COM_Direct3DDevice9;
using Maple.ImGui.Backends.GraphicsCore;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Maple.UnmanagedExtensions;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct3D9;

namespace Maple.ImGui.Backends.D3D9.GraphicsCore.HOOK_Direct3DDevice9
{
    internal class D3D9CreateVertexShaderHookItem : HookItem<D3D9CreateVertexShaderHookItem, Ptr_Func_CreateVertexShader_91, Ptr_Func_CreateVertexShader_91>, IGraphicsHookItem<D3D9CreateVertexShaderHookItem>
    {
        public const string MethodName = Ptr_Func_CreateVertexShader_91.Name;

        public Func<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnmanagedExtensions.UnsafeRef<int>, UnmanagedExtensions.UnsafeOut<nint>, D3D9CreateVertexShaderHookItem, COM_HRESULT>? SyncCallback { get; set; }

        public static D3D9CreateVertexShaderHookItem Create(ISupperHookFactory hookFactory, GraphicsFunctionsProvider functionsProvider)
        {
            if (!functionsProvider.TryGetGraphicsFunctions(MethodName, out var functionPtr))
            {
                return GraphicsException.Throw<D3D9CreateVertexShaderHookItem>($"NOT FOUND {MethodName}");
            }
            var hookItemImp = hookFactory.Create<D3D9CreateVertexShaderHookItem>(
                functionPtr,
                GetHookMethodPointer());
            return hookItemImp;
        }

        private static unsafe nint GetHookMethodPointer()
        {
            delegate* unmanaged[Stdcall, SuppressGCTransition]<Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp>, UnmanagedExtensions.UnsafeRef<int>, UnmanagedExtensions.UnsafeOut<nint>, COM_HRESULT>
                _proc = &Hook_CreateVertexShader;
            return new(_proc);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall),typeof(CallConvSuppressGCTransition)])]
        private static COM_HRESULT Hook_CreateVertexShader(Windows.GraphicsCore.COM.COM_PTR_IUNKNOWN<IDirect3DDevice9Imp> @this, UnmanagedExtensions.UnsafeRef<int> pFunction, UnmanagedExtensions.UnsafeOut<nint> ppShader)
        {
            if (D3D9CreateVertexShaderHookItem.TryGet(out var hookItem))
            {
                if (hookItem.SyncCallback is not null)
                {
                    return hookItem.SyncCallback.Invoke(@this, pFunction, ppShader, hookItem);
                }
                return hookItem.OriginalMethod.Invoke(@this, pFunction, ppShader);
            }
            return 0;
        }
    }
}