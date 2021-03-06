﻿using System;

namespace DIWidget
{
    public abstract class Widget<TWidget> : WidgetBase, IWidget<TWidget> where TWidget : Widget<TWidget>
    {
        public IWidgetManager<TWidget> Manager { get; private set; }

        public abstract object Identify { get; }

        IWidgetManager IWidget.Manager => Manager;

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Set up manager
        /// </summary>
        /// <param name="widgetManager"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void SetManager(IWidgetManager widgetManager)
        {
            var manager = widgetManager as WidgetManager<TWidget>;
            Manager = manager ?? throw new Exception(
                          $"type of WidgetManager does not match. {typeof(TWidget)}\n{widgetManager?.GetType()}");
        }

        /// <summary>
        /// Reset manager
        /// </summary>
        internal void ResetManager()
        {
            Manager = null;
        }

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        protected virtual void OnUpdateWidget(params object[] parameters)
        {
        }


        internal void UpdateWidget(params object[] parameters)
        {
            OnUpdateWidget(parameters);
        }
    }

    public interface IWidget : IDisposable
    {
        object Identify { get; }
        IWidgetManager Manager { get; }
    }

    public interface IWidget<TWidget> : IWidget where TWidget : IWidget<TWidget>
    {
        new IWidgetManager<TWidget> Manager { get; }
    }
}