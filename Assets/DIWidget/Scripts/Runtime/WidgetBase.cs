using System;
using UnityEngine.EventSystems;

namespace DIWidget
{
    public abstract class WidgetBase : UIBehaviour
    {
        private bool Display
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        private EStep Step { get; set; } = EStep.None;

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
                    OnClose();
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
        /// Open
        /// Called after Fade-in is over
        /// </summary>
        protected abstract void OnOpen();

        /// <summary>
        /// Close
        /// Called after Fade-out is over
        /// </summary>
        protected abstract void OnClose();
        
        /// <summary>
        /// Fade-in
        /// </summary>
        /// <param name="fadeInEndAction"></param>
        protected virtual void OnFadeIn(Action fadeInEndAction)
        {
            fadeInEndAction();
        }

        /// <summary>
        /// Fade-out
        /// </summary>
        /// <param name="fadeOutEndAction"></param>
        protected virtual void OnFadeOut(Action fadeOutEndAction)
        {
            fadeOutEndAction();
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