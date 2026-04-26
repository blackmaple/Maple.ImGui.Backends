using Maple.ImGui.Backends.Windows.GraphicsCore.COM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Maple.ImGui.Backends.Windows.GraphicsCore
{
    public readonly struct ComponentTransactionScope() : IDisposable
    {
        private List<COM_PTR_IUNKNOWN> Components { get; } = new(32);

        public void AddComponent(COM_PTR_IUNKNOWN component)
        {
            Components.Add(component);
        }

        public void Reset() => this.Components.Clear();

        public void Commit()
        {
            Components.Clear();
        }

        public void Rollback()
        {
            for (int i = Components.Count - 1; i >= 0; i--)
            {
                var item = Components[i];
                item.Release();
            }
            this.Components.Clear();
        }

        public void Dispose()
        {
            this.Rollback();
        }
    }
}
