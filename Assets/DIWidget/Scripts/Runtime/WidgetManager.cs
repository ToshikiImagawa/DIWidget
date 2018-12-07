using System;
using DIWidget.Presets;
using UnityEngine;
using Zenject;

namespace DIWidget
{
    public abstract class WidgetManager<TWidget> : IWidgetManager where TWidget : Widget<TWidget>
    {
        [Inject]
        private WidgetSet<TWidget> WidgetSet { get; set; }

        private Widget<TWidget>.Pool GetPool(object identify)
        {
            return WidgetSet.GetPool(identify);
        }

        private Widget<TWidget>.Pool GetPool(IWidget widget)
        {
            return WidgetSet.GetPool(widget.Identify);
        }

        private readonly ListStack<TWidget> _closeList = new ListStack<TWidget>();
        private TWidget _openWidget;

        public TWidget Open(object identify)
        {
            if (_openWidget != null)
            {
                _closeList.Push(_openWidget);
                Finalize(_openWidget);
                _openWidget = null;
            }

            var widget = GetPool(identify).Spawn();
            widget.transform.SetParent(WidgetSet.ViewPoint, false);
            Initialize(widget);
            _openWidget = widget;
            return widget;
        }

        public void Remove(TWidget widget)
        {
            if (_openWidget == widget)
            {
                Finalize(_openWidget);
                _openWidget = null;

                if (_closeList.Length > 0)
                {
                    _openWidget = _closeList.Pop();
                    Initialize(_openWidget);
                }
            }
            else
            {
                _closeList.Remove(widget);
            }

            GetPool(widget).Despawn(widget);
        }

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