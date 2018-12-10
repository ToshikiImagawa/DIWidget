using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class SampleFeedSet : WidgetSet<Feed>
    {
        [Inject]
        public SampleFeedSet(
            [Inject(Id = "Feed1")] Feed.Pool pool1,
            [Inject(Id = "Feed2")] Feed.Pool pool2,
            Transform viewPoint)
        {
            SetPool("Feed1", pool1);
            SetPool("Feed2", pool2);
            ViewPoint = viewPoint;
        }
    }
}