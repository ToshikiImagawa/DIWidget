using System;
using UnityEngine.EventSystems;
using Zenject;

namespace DIWidget
{
    public abstract class Widget<TWidget> : UIBehaviour, IWidget where TWidget : Widget<TWidget>
    {
        private EStep Step { get; set; } = EStep.None;
        private event Action<TWidget> CallBackWidgetEnd;

        public virtual void Dispose()
        {
        }

        private bool Display
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public abstract object Identify { get; }

        /// <summary>
        /// 開く
        /// ※Managerからのみ実行
        /// </summary>
        void IWidget.Open()
        {
            if (Display && Step != EStep.None) return;
            Display = true;
            Step = EStep.FadeIn;
            OnFadeIn(() =>
            {
                OnOpen();
                Step = EStep.Idle;
            });
        }

        /// <summary>
        /// 閉じる
        /// ※Managerからのみ実行
        /// </summary>
        void IWidget.Close()
        {
            if (Step != EStep.Idle && Step != EStep.FadeIn) return;
            if (Display)
            {
                Step = EStep.FadeOut;
                OnFadeOut(() =>
                {
                    // 閉じる処理
                    OnClose();

                    // アクティブ状態の設定
                    Display = false;
                    Step = EStep.None;

                    // 終了コールバック
                    CallBackWidgetEnd?.Invoke(this as TWidget);
                });
            }
            else
            {
                Step = EStep.None;
            }
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