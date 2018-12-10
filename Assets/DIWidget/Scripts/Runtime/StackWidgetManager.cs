using System.Linq;

namespace DIWidget
{
    public class StackWidgetManager<TWidget> : WidgetManager<TWidget> where TWidget : Widget<TWidget>
    {
        private readonly ListStack<TWidget> _closedListStack = new ListStack<TWidget>();
        private readonly object _closedListLock = new object();

        protected TWidget[] ClosedList
        {
            get
            {
                lock (_closedListLock)
                {
                    return _closedListStack.ToArray();
                }
            }
        }
        
        public void RemoveAll()
        {
            lock (_closedListLock)
            {
                foreach (var widget in _closedListStack)
                {
                    Finalize(widget);
                    Despawn(widget);
                }

                _closedListStack.Clear();
            }

            if (Current != null) Remove(Current);
        }

        protected override TWidget SetCurrentWidgetOnOpen(TWidget openWidget)
        {
            lock (_closedListLock)
            {
                if (Current != null)
                {
                    _closedListStack.Push(Current);
                    Finalize(Current);
                }
                Initialize(openWidget);
                return openWidget;
            }
        }

        protected override TWidget SetCurrentWidgetOnRemove(TWidget removeWidget)
        {
            lock (_closedListLock)
            {
                if (Current == removeWidget)
                {
                    Finalize(Current);
                    Despawn(Current);

                    if (_closedListStack.Length <= 0) return null;
                    var currentWidget = _closedListStack.Pop();
                    Initialize(currentWidget);
                    return currentWidget;
                }

                _closedListStack.Remove(removeWidget);
                return Current;
            }
        }
    }
}