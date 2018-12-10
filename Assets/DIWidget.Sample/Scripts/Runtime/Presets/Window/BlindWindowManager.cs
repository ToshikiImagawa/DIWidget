using Zenject;

namespace DIWidget
{
    public class BlindWindowManager : WindowManager
    {
        [Inject] private BlindWidget.Factory _blindFactory;

        private BlindWidget _blindWidget;

        private BlindWidget BlindWidget
        {
            get
            {
                if (_blindWidget == null)
                {
                    _blindWidget = _blindFactory.Create();
                }

                return _blindWidget;
            }
        }

        protected override void OnAfterOpen()
        {
            if (ClosedList.Length != 0) return;
            ShowBlind();
        }

        protected override void OnAfterRemove()
        {
            if (Current != null) return;
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
    }
}