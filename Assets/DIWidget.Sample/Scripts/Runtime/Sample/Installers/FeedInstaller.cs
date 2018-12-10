using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class FeedInstaller : MonoInstaller
    {
        [SerializeField] private Feed prefab1;
        [SerializeField] private Feed prefab2;
        [SerializeField] private Transform dialogViewPoint;

        public override void InstallBindings()
        {
            // WidgetSet
            Container.BindWidgetSetSingle<Feed, SampleFeedSet>().WhenInjectedInto<FeedManager>();

            // ViewPoint
            Container.Bind<Transform>().FromInstance(dialogViewPoint).WhenInjectedInto(typeof(SampleFeedSet));

            // Pool
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Feed, Feed.Pool>(prefab1);
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Feed, Feed.Pool>(prefab2);

            // Manager
            Container.Bind<FeedManager>().AsSingle();
        }
    }
}