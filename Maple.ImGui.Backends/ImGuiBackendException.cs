using System.Diagnostics.CodeAnalysis;

namespace Maple.ImGui.Backends
{
    public class ImGuiBackendException(string? msg) : Exception(msg)
    {

        [DoesNotReturn]
        public static void Throw(string? msg)=>throw new ImGuiBackendException(msg);

        [DoesNotReturn]
        public static T Throw<T>(string? msg) => throw new ImGuiBackendException(msg);

    }
}
