using System;
using UnityEngine;
using UnityEngine.UI;

namespace DIWidget.Sample
{
    public class WindowCloseButton : MonoBehaviour
    {
        [SerializeField] private Window _window;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Remove);
        }

        [ContextMenu("Remove")]
        public void Remove()
        {
            try
            {
                _window.Manager.Remove(_window);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}