using System;
using System.Threading.Tasks;
using UnityEngine;

namespace DIWidget
{
    [RequireComponent(typeof(Animator))]
    public class AnimationWindow : Window
    {
        private Animator _animator;

        private int _lastStateHash = 0;
        private int _layerNo = 0;

        private Animator Animator =>
            _animator != null ? _animator : _animator = GetComponent<Animator>();

        private bool KeepWaiting
        {
            get
            {
                var currentAnimatorState = Animator.GetCurrentAnimatorStateInfo(_layerNo);
                return currentAnimatorState.fullPathHash == _lastStateHash &&
                       (currentAnimatorState.normalizedTime < 1);
            }
        }

        protected override void OnFadeIn(Action fadeInEndAction)
        {
            Animator.Play("FadeIn");
            WaitAnimation(0, fadeInEndAction);
        }

        protected override void OnFadeOut(Action fadeOutEndAction)
        {
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
            Animator.Play("FadeOut");
            WaitAnimation(0, fadeOutEndAction);
        }

        protected override void OnOpen()
        {
            Animator.Play("Show", 0);
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }

        protected override void OnClose()
        {
            Animator.Play("Hide", 0);
        }

        private async void WaitAnimation(int layerNo, Action endAction)
        {
            await Task.Delay(15);
            _layerNo = layerNo;
            _lastStateHash = Animator.GetCurrentAnimatorStateInfo(layerNo).fullPathHash;
            while (KeepWaiting)
            {
                await Task.Delay(1);
            }

            endAction.Invoke();
        }
    }
}