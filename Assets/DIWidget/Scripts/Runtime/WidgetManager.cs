using System;
using Zenject;

namespace DIWidget
{
    public class WidgetManager<TWidget> : IWidgetManager<TWidget> where TWidget : Widget<TWidget>
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
            if (widget == null)
                throw new NullReferenceException($"Instance of widget to remove is null. : {nameof(TWidget)}");
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

        IWidget IWidgetManager.Open(object identify, params object[] parameters)
        {
            return Open(identify, parameters);
        }

        IWidget IWidgetManager.Remove(IWidget widget)
        {
            var item = widget as TWidget;
            if (item == null)
                throw new Exception(
                    $"instance of widget to remove is type of different. {widget.GetType()}\n{typeof(TWidget)}");
            return Remove(item);
        }

        IWidget IWidgetManager.Remove()
        {
            return Remove();
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
            widget.ResetManager();
        }

        private IMemoryPool<TWidget> GetPool(object identify)
        {
            return WidgetSet.GetPool(identify);
        }

        private IMemoryPool<TWidget> GetPool(IWidget widget)
        {
            return WidgetSet.GetPool(widget.Identify);
        }
    }

    public interface IWidgetManager
    {
        IWidget Open(object identify, params object[] parameters);
        IWidget Remove(IWidget widget);
        IWidget Remove();
    }

    public interface IWidgetManager<TWidget> : IWidgetManager where TWidget : IWidget<TWidget>
    {
        new TWidget Open(object identify, params object[] parameters);
        TWidget Remove(TWidget widget);
        new TWidget Remove();
    }
}