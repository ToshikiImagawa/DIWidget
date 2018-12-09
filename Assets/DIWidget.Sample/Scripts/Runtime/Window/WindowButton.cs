using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class WindowButton : MonoBehaviour
    {
        [SerializeField] private string identify;

        [Inject] private WindowManager _manager;

        [ContextMenu("Open")]
        public void Open()
        {
            _manager.Open(identify);
        }
    }
}