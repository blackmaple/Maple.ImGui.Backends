using System.Diagnostics.CodeAnalysis;

namespace Maple.ImGui.Backends.GraphicsCore
{
    public class GraphicsException(string? msg) : Exception(msg)
    {
        [DoesNotReturn]
        public static void Throw(string? msg = default) => throw new GraphicsException(msg);

        [DoesNotReturn]
        public static T Throw<T>(string? msg = default) => throw new GraphicsException(msg);
    }



}
