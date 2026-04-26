using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Windows.Win32.Foundation;
namespace Maple.ImGui.Backends.Windows.GraphicsCore.COM
{

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct COM_HRESULT(int v)
    {
        //public const uint S_OK = 0U;
        public const int S_FALSE = int.MinValue;

        internal readonly HRESULT Value = new(v);

        //[MarshalAs(UnmanagedType.U4)]
        //public readonly uint Value = v;

        public static implicit operator int(COM_HRESULT v) => v.Value;
        public static implicit operator COM_HRESULT(int v) => new(v);
        public static implicit operator bool(COM_HRESULT v) => v.Value.Succeeded;

        public override string ToString()
        {
            return Value.Value.ToString("X8");
        }
    }
}