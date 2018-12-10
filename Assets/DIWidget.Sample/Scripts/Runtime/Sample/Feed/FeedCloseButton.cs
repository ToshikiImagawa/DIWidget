using UnityEngine;

namespace DIWidget.Sample
{
    public class FeedCloseButton : MonoBehaviour
    {
        [SerializeField] private Feed _feed;

        [ContextMenu("Remove")]
        public void Remove()
        {
            _feed.Manager.Remove(_feed);
        }
    }
}
