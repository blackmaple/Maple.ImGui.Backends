namespace Maple.ImGui.Backends
{
    /// <summary>
    /// unity 输入接口放这里，主要是为了处理输入法相关的接口，目前仅有一个接口，后续如果有其他输入相关的接口也可以放在这里
    /// </summary>
    public interface IImGuiUnityInputBridge
    {
        void PlatformSetImeDataFn(bool on);

        bool TryGetImageInfo(string? category, string objectId, out nint nativePtr, out float u0, out float v0, out float u1, out float v1);

        public void BlockInput(IImGuiUIView view) { }
    }
}
