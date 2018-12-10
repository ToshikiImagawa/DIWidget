using System;
using Zenject;

namespace DIWidget
{
    public class WidgetManager<TWidget> : IWidgetManager where TWidget : Widget<TWidget>
    {
        [Inject]
        protected WidgetSet<TWidget> WidgetSet { get; private set; }

        protected TWidget Current { get; private set; }

        /// <summary>
        /// Open
        /// </summary>
        /// <param name="identify"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TWidget Open(object identify, params object[] parameters)
        {
            OnBeforeOpen();
            var widget = GetPool(identify).Spawn();
            widget.transform.SetParent(WidgetSet.ViewPoint, false);
            if (parameters != null) widget.UpdateWidget(parameters);
            Current = SetCurrentWidgetOnOpen(widget);
            OnAfterOpen();
            return Current;
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="widget"></param>
        /// <returns></returns>
        public TWidget Remove(TWidget widget)
        {
            if (widget == null) throw new NullReferenceException($"Instance of widget to remove is null. : {nameof(TWidget)}");
            OnBeforeRemove();
            Current = SetCurrentWidgetOnRemove(widget);
            OnAfterRemove();
            return Current;
        }

        /// <summary>
        /// Remove current widget
        /// </summary>
        /// <returns></returns>
        public TWidget Remove()
        {
            return Current != null ? Remove(Current) : null;
        }

        protected virtual TWidget SetCurrentWidgetOnOpen(TWidget openWidget)
        {
            if (Current != null)
            {
                Finalize(Current);
                Despawn(Current);
            }

            Initialize(openWidget);
            return openWidget;
        }

        protected virtual TWidget SetCurrentWidgetOnRemove(TWidget removeWidget)
        {
            if (Current != removeWidget) return Current;
            Finalize(Current);
            Despawn(Current);
            return null;
        }

        protected virtual void OnBeforeOpen()
        {
        }

        protected virtual void OnAfterOpen()
        {
        }

        protected virtual void OnBeforeRemove()
        {
        }

        protected virtual void OnAfterRemove()
        {
        }

        protected void Despawn(TWidget widget)
        {
            GetPool(widget).Despawn(widget);
        }

        protected void Initialize(TWidget widget)
        {
            widget.Open();
            widget.SetManager(this);
        }

        protected static void Finalize(TWidget widget)
        {
            widget.Close();
            widget.SetManager(null);
        }

        private Widget<TWidget>.Pool GetPool(object identify)
        {
            return WidgetSet.GetPool(identify);
        }

        private Widget<TWidget>.Pool GetPool(IWidget widget)
        {
            return WidgetSet.GetPool(widget.Identify);
        }
    }

    public interface IWidget : IDisposable
    {
        object Identify { get; }
    }

    public interface IWidgetManager
    {
    }
}