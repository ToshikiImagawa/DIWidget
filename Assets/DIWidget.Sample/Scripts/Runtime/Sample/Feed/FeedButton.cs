using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class FeedButton : MonoBehaviour
    {
        [SerializeField] private string identify;

        [Inject] private FeedManager _manager;

        [ContextMenu("Open")]
        public void Open()
        {
            _manager.Open(identify);
        }
    }
}