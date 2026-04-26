using Maple.Hook.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Maple.ImGui.Backends.GraphicsCore
{

    public abstract class GraphicsFunctionsProvider
    {
        protected Dictionary<string, nint> Functions { get; } = [];
        public bool TryGetGraphicsFunctions(string functionName, out nint functionPtr)
        {
            return Functions.TryGetValue(functionName, out functionPtr);
        }
        public bool TryAddGraphicsFunctions(string functionName, nint functionPtr)
        {
            Functions.Remove(functionName, out _);
            return Functions.TryAdd(functionName, functionPtr);
        }

       // public abstract static IGraphicsFunctionsProvider Create(TempWindow.D3DTempWindowFactory windowFactory) => default;
    
    }




  
}
