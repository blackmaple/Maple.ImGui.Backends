using System.Runtime.InteropServices;

namespace Maple.ImGui.Backends
{
    public interface IImGuiCustomRender
    {
        void RaiseRender();
    }

    public class DefaultImGuiCustomRender : IImGuiCustomRender
    {
        public void RaiseRender()
        {
        //    Hexa.NET.ImGui.ImGui.Begin("Demo");
            Hexa.NET.ImGui.ImGui.ShowDemoWindow();
         //   Hexa.NET.ImGui.ImGui.End();
        }
    }
}
