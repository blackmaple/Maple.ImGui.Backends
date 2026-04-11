using Hexa.NET.ImGui;
using Microsoft.Extensions.Hosting;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Maple.ImGui.Backends
{
    public abstract class ImGuiBackendImpBase : IDisposable
    {
        protected Dictionary<IntPtr, ImTextureID> TextureCache { get; } = [];

        protected ImGuiBackendBridgeCollection BridgeCollection { get; }

        protected IImGuiUIView View { get; }
        public ImGuiBackendImpBase(ImGuiBackendBridgeCollection bridgeCollection, IImGuiUIView view)
        {
            this.BridgeCollection = bridgeCollection;
            this.View = view;
            this.View.TryDrawImage = TryDrawImage;
        }


        protected abstract void Starting(nint context);
        protected abstract void Start(nint context);
        protected abstract void Started(nint context);
        protected abstract void Build(nint context);

        public virtual void Run(nint context)
        {
            this.Starting(context);
            this.Start(context);
            this.Started(context);
            this.Build(context);
        }



        protected abstract void Shutdown();


        public virtual void Resetting(nint context) { }
        public virtual void Reset(nint context) { }
        public virtual void Resetted(nint context) { }



        public void Dispose()
        {
            this.Shutdown();
            GC.SuppressFinalize(this);
        }

        protected unsafe bool TryDrawImage(string? category, string objectId)
        {
            var uintyInputBridge = this.BridgeCollection.UnityInputBridge;
            if (uintyInputBridge is null)
            {
                return false;
            }
            if (false ==  uintyInputBridge.TryGetImageInfo(category, objectId, out var nativePtr, out var u0, out var v0, out var u1, out var v1))
            {
                return false;
            }
            var textureId = GetOrCreateTextureId(nativePtr);
            if (textureId.IsNull)
            {
                return false;
            }
            Hexa.NET.ImGui.ImGui.Image(new ImTextureRef(default, textureId), new Vector2(48,48), new Vector2(u0, v0), new Vector2(u1, v1));
            return true;
        }
        protected ImTextureID GetOrCreateTextureId(nint nativePtr)
        {
            if (TextureCache.TryGetValue(nativePtr, out ImTextureID cachedId))
            {
                return cachedId;
            }
            ImTextureID newId = CreateImTextureID(nativePtr);
            TextureCache.Add(nativePtr, newId);
            return newId;
        }

        protected virtual ImTextureID CreateImTextureID(nint textureNativePtr) => default;



    }





}
