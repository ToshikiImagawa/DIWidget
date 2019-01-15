using System;
using UnityEngine;
using UnityEngine.UI;

namespace DIWidget.Sample
{
    public class DialogCloseButton : MonoBehaviour
    {
        [SerializeField] private Dialog _dialog;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Remove);
        }

        [ContextMenu("Remove")]
        private void Remove()
        {
            try
            {
                _dialog.Manager.Remove(_dialog);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}