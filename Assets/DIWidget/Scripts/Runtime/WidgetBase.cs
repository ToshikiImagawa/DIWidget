using UnityEngine.EventSystems;

namespace DIWidget
{
    public abstract class WidgetBase : UIBehaviour, IWidget
    {
        public abstract void Dispose();
        public abstract object Identify { get; }
    }
}