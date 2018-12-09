using System;
using Zenject;

namespace DIWidget
{
    public abstract class WidgetManager<TWidget> : IWidgetManager where TWidget : Widget<TWidget>
    {
        private TWidget _openWidget;
        private readonly ListStack<TWidget> _closedListStack = new ListStack<TWidget>();
        private readonly object _closedListLock = new object();

        [Inject]
        protected WidgetSet<TWidget> WidgetSet { get; private set; }

        protected TWidget OpenWidget
        {
            get
            {
                lock (_closedListLock)
                {
                    return _openWidget;
                }
            }
        }

        protected ListStack<TWidget> ClosedListStack => _closedListStack;

        private Widget<TWidget>.Pool GetPool(object identify)
        {
            return WidgetSet.GetPool(identify);
        }

        private Widget<TWidget>.Pool GetPool(IWidget widget)
        {
            return WidgetSet.GetPool(widget.Identify);
        }

        public TWidget Open(object identify, params object[] parameters)
        {
            lock (_closedListLock)
            {
                if (_openWidget != null)
                {
                    _closedListStack.Push(_openWidget);
                    Finalize(_openWidget);
                    _openWidget = null;
                }
            }
            var widget = GetPool(identify).Spawn();
            widget.transform.SetParent(WidgetSet.ViewPoint, false);
            Initialize(widget);

            if (parameters != null) widget.UpdateWidget(parameters);

            lock (_closedListLock)
            {
                _openWidget = widget;
                OnOpen();
            }
            return widget;
        }

        public void Remove(TWidget widget)
        {
            lock (_closedListLock)
            {
                if (_openWidget == widget)
                {
                    Finalize(_openWidget);
                    _openWidget = null;

                    if (_closedListStack.Length > 0)
                    {
                        _openWidget = _closedListStack.Pop();
                        Initialize(_openWidget);
                    }
                }
                else
                {
                    _closedListStack.Remove(widget);
                }
                OnRemove();
            }
            GetPool(widget).Despawn(widget);
        }

        protected virtual void OnOpen() { }
        protected virtual void OnRemove() { }

        private static void Initialize(IWidget widget)
        {
            widget.Open();
        }

        private static void Finalize(IWidget widget)
        {
            widget.Close();
        }
    }

    internal interface IWidget : IDisposable
    {
        object Identify { get; }
        void Open();
        void Close();
    }

    public interface IWidgetManager
    {
    }
}