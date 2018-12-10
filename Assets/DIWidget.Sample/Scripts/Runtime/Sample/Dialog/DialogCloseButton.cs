using UnityEngine;

namespace DIWidget.Sample
{
    public class DialogCloseButton : MonoBehaviour
    {
        [SerializeField] private Dialog _dialog;

        [ContextMenu("Remove")]
        public void Remove()
        {
            _dialog.Manager.Remove(_dialog);
        }
    }
}