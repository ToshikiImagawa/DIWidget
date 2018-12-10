using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DIWidget
{
    public abstract class Widget<TWidget> : WidgetBase where TWidget : Widget<TWidget>
    {
        private EStep Step { get; set; } = EStep.None;

        public WidgetManager<TWidget> Manager { get; private set; }

        public override void Dispose()
        {
        }

        private bool Display
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        /// <summary>
        /// 開く
        /// ※Managerからのみ実行
        /// </summary>
        internal void Open()
        {
            if (Step != EStep.None && Step != EStep.FadeOut) return;
            if (Step == EStep.FadeOut)
            {
                OnClose();
                Display = false;
                Step = EStep.None;
            }

            if (!Display)
            {
                Display = true;
                Step = EStep.FadeIn;
                OnFadeIn(() =>
                {
                    if (Step != EStep.FadeIn) return;
                    OnOpen();
                    Step = EStep.Idle;
                });
            }
            else
            {
                Step = EStep.Idle;
            }
        }

        /// <summary>
        /// 閉じる
        /// ※Managerからのみ実行
        /// </summary>
        internal void Close()
        {
            if (Step != EStep.Idle && Step != EStep.FadeIn) return;
            if (Step == EStep.FadeIn)
            {
                OnOpen();
                Step = EStep.Idle;
            }

            if (Display)
            {
                Step = EStep.FadeOut;
                OnFadeOut(() =>
                {
                    if (Step != EStep.FadeOut) return;
                    // 閉じる処理
                    OnClose();

                    // アクティブ状態の設定
                    Display = false;
                    Step = EStep.None;
                });
            }
            else
            {
                Step = EStep.None;
            }
        }

        /// <summary>
        /// Managerを設定する
        /// </summary>
        /// <param name="widgetManager"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void SetManager(IWidgetManager widgetManager)
        {
            Manager = widgetManager as WidgetManager<TWidget>;
        }

        /// <summary>
        /// Fade-in 終了後に呼ばれる
        /// </summary>
        protected abstract void OnOpen();

        /// <summary>
        /// Fade-out 終了後に呼ばれる
        /// </summary>
        protected abstract void OnClose();

        /// <summary>
        /// Updates the widget.
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        protected virtual void OnUpdateWidget(params object[] parameters)
        {
        }

        /// <summary>
        /// FadeIn時に実行する
        /// ※FadeIn終了時に必ずfadeInEndActionを実行する.
        /// </summary>
        /// <param name="fadeInEndAction"></param>
        protected virtual void OnFadeIn(Action fadeInEndAction)
        {
            fadeInEndAction();
        }

        /// <summary>
        /// FadeOut時に実行する
        /// ※FadeOut終了時に必ずfadeOutEndActionを実行する.
        /// </summary>
        /// <param name="fadeOutEndAction"></param>
        protected virtual void OnFadeOut(Action fadeOutEndAction)
        {
            fadeOutEndAction();
        }

        internal void UpdateWidget(params object[] parameters)
        {
            OnUpdateWidget(parameters);
        }

        public class Pool : MemoryPool<TWidget>
        {
        }

        private enum EStep
        {
            None,
            Idle,
            FadeIn,
            FadeOut,
        }
    }
}