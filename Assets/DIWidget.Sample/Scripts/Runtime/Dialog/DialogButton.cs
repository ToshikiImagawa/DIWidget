using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class DialogButton : MonoBehaviour
    {
        [SerializeField] private string identify;

        [Inject] private DialogManager _manager;

        [ContextMenu("Open")]
        public void Open()
        {
            _manager.Open(identify);
        }
    }
}