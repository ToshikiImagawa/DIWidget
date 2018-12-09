using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace DIWidget
{
    [RequireComponent(typeof(CanvasGroup), typeof(Button))]
    public class BlindWidget : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private Button _button;

        protected CanvasGroup CanvasGroup =>
            _canvasGroup != null ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();

        private Button Button =>
            _button != null ? _button : _button = GetComponent<Button>();

        [Inject]
        private void Construct(Transform viewPoint, Action onClick)
        {
            transform.SetParent(viewPoint, false);
            transform.SetAsFirstSibling();
            Button.onClick.AddListener(onClick.Invoke);
        }

        public void Show() {
            CanvasGroup.alpha = 1f;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            CanvasGroup.alpha = 0f;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }

        public class Factory : PlaceholderFactory<BlindWidget>
        {
        }
    }
}
