using System;
using UnityEngine;
using UnityEngine.UI;

namespace DIWidget.Sample
{
    public class FeedCloseButton : MonoBehaviour
    {
        [SerializeField] private Feed _feed;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Remove);
        }

        [ContextMenu("Remove")]
        private void Remove()
        {
            try
            {
                _feed.Manager.Remove(_feed);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}