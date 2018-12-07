using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class WindowCloseButton : MonoBehaviour
    {

        [SerializeField] private Window _window;
        [Inject] private WindowManager _manager;

        [ContextMenu("Remove")]
        public void Remove()
        {
            _manager.Remove(_window);
        }
    }
}