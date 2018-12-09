using UnityEngine;

namespace DIWidget
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Dialog : Widget<Dialog>
    {
        [SerializeField] private string identify;
        public override object Identify => identify;

        private CanvasGroup _canvasGroup;
        protected CanvasGroup CanvasGroup =>
            _canvasGroup != null ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();

        protected override void OnOpen()
        {
            CanvasGroup.alpha = 1;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }

        protected override void OnClose()
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }
    }
}
