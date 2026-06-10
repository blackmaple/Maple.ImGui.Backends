using Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12Fence;
using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Direct3D12;

namespace Maple.ImGui.Backends.D3D12.GraphicsCore.COM_D3D12CommandQueue
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D12CommandQueueImp
    {

        public static readonly Guid GUID = new("0EC870A6-5D7E-4C22-8CFC-5BAAE07616ED");

        internal readonly Ptr_Func_GetPrivateData_3 GetPrivateData_3;
        internal readonly Ptr_Func_SetPrivateData_4 SetPrivateData_4;
        internal readonly Ptr_Func_SetPrivateDataInterface_5 SetPrivateDataInterface_5;
        internal readonly Ptr_Func_SetName_6 SetName_6;
        internal readonly Ptr_Func_GetDevice_7 GetDevice_7;
        internal readonly Ptr_Func_UpdateTileMappings_8 UpdateTileMappings_8;
        internal readonly Ptr_Func_CopyTileMappings_9 CopyTileMappings_9;
        internal readonly Ptr_Func_ExecuteCommandLists_10 ExecuteCommandLists_10;
        internal readonly Ptr_Func_SetMarker_11 SetMarker_11;
        internal readonly Ptr_Func_BeginEvent_12 BeginEvent_12;
        internal readonly Ptr_Func_EndEvent_13 EndEvent_13;
        internal readonly Ptr_Func_Signal_14 Signal_14;
        internal readonly Ptr_Func_Wait_15 Wait_15;
        internal readonly Ptr_Func_GetTimestampFrequency_16 GetTimestampFrequency_16;
        internal readonly Ptr_Func_GetClockCalibration_17 GetClockCalibration_17;
        internal readonly Ptr_Func_GetDesc_18 GetDesc_18;




    }

    public static class ID3D12CommandQueueImpExtension
    {
        extension(COM_PTR_IUNKNOWN<ID3D12CommandQueueImp> @this)
        {
            public void ExecuteCommandLists(params ReadOnlySpan<COM_PTR_IUNKNOWN> commandLists)
            {
                @this.Interface_VTable.ExecuteCommandLists_10.Invoke(@this, commandLists);
            }

            internal void Signal(COM_PTR_IUNKNOWN<ID3D12FenceImp> fence, ulong value)
            {
                @this.Interface_VTable.Signal_14.Invoke(@this, fence, value);
            }
            internal D3D12_COMMAND_QUEUE_DESC GetDesc()
            {
                @this.Interface_VTable.GetDesc_18.Invoke(@this, out var desc);
                return desc;
            }
        }
    }

}
