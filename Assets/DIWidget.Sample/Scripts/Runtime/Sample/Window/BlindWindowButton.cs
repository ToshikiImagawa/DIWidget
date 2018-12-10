using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class BlindWindowButton: MonoBehaviour
    {
        [SerializeField] private string identify;

        [Inject] private BlindWindowManager _blindWindowManager;

        [ContextMenu("Open")]
        public void Open()
        {
            _blindWindowManager.Open(identify);
        }
    }
}