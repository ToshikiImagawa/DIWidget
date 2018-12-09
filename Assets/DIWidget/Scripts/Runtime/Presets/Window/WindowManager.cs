using Zenject;

namespace DIWidget
{
    public class WindowManager : WidgetManager<Window>
    {
        [Inject] private BlindWidget.Factory blindFactory;

        private BlindWidget _blindWidget;
        private BlindWidget BlindWidget
        {
            get
            {
                if (_blindWidget == null)
                {
                    _blindWidget = blindFactory.Create();
                }
                return _blindWidget;
            }
        }

        protected override void OnOpen()
        {
            if (ClosedListStack.Length != 0) return;
            ShowBlind();
        }

        protected override void OnRemove()
        {
            if (OpenWidget != null) return;
            HideBlind();
        }

        private void ShowBlind()
        {
            BlindWidget.Show();
        }

        private void HideBlind()
        {
            BlindWidget.Hide();
        }

        public void RemoveOpenWindow()
        {
            Remove(OpenWidget);
        }
    }
}