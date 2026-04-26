using Maple.ImGui.Backends.GraphicsCore;
using System.Runtime.InteropServices;
using Windows.Win32;

namespace Maple.ImGui.Backends.OPENGL.GraphicsCore
{
    internal class OPENGLFunctionsProvider : GraphicsFunctionsProvider, IGraphicsFunctions<OPENGLFunctionsProvider>
    {
       // public Dictionary<string, nint> Functions { get; } = [];


        public static OPENGLFunctionsProvider Create(IServiceProvider _)
        {
            var proc = GetAddress();
            var functionsProvider = new OPENGLFunctionsProvider();
            functionsProvider.TryAddGraphicsFunctions(Ptr_Func_wglSwapBuffers.Name, proc);
            return functionsProvider;
        }

        const string LibraryName = "opengl32.dll";
        const string EntryPoint = "wglSwapBuffers";
        private static nint GetAddress()
        {
            var proc = PInvoke.wglGetProcAddress(EntryPoint);
            if (proc != IntPtr.Zero)
            {
                return proc;
            }
            if (NativeLibrary.TryLoad(LibraryName, out var handle) && NativeLibrary.TryGetExport(handle, EntryPoint, out var address))
            {
                return address;
            }
            return GraphicsException.Throw<nint>($"{nameof(GetAddress)} ERROR");
        }




    }
}
