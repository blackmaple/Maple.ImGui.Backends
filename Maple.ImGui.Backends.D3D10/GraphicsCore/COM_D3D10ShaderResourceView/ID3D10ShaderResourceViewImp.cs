using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32.Graphics.Direct3D10;

namespace Maple.ImGui.Backends.D3D10.GraphicsCore.COM_D3D10ShaderResourceView
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct ID3D10ShaderResourceViewImp
    {
         
        public static readonly Guid GUID = new("9B7E4C07-342C-4106-A19F-4F2704F689F0");
    }
}
