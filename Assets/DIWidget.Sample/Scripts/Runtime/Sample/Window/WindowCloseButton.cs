using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class WindowCloseButton : MonoBehaviour
    {

        [SerializeField] private Window _window;

        [ContextMenu("Remove")]
        public void Remove()
        {
            _window.Manager.Remove(_window);
        }
    }
}