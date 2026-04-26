using Maple.Hook.Abstractions;

namespace Maple.ImGui.Backends.GraphicsCore
{
    public interface ISupperHookFactory
    {
        public IHookFactory HookFactory { get; }

        THookItem Create<THookItem>(nint pTarget, nint pDetour) where THookItem : HookItem, new();
    }

    public class JmpChainSupperHookFactory(IHookFactory hookFactory) : ISupperHookFactory
    {
        public IHookFactory HookFactory { get; } = hookFactory;
        public THookItem Create<THookItem>(nint pTarget, nint pDetour) where THookItem : HookItem, new()
            => HookFactory.Create<THookItem>(typeof(THookItem).Name, pTarget, pDetour, true);
    }

    public class DefaultSupperHookFactory(IHookFactory hookFactory) : ISupperHookFactory
    {
        public IHookFactory HookFactory { get; } = hookFactory;
        public THookItem Create<THookItem>(nint pTarget, nint pDetour) where THookItem : HookItem, new()
            => HookFactory.Create<THookItem>(pTarget, pDetour);
    }
}
