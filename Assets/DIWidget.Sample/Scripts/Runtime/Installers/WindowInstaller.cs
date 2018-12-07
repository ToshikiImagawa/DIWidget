using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class WindowInstaller : MonoInstaller
    {
        [SerializeField] private Window prefab1;
        [SerializeField] private Window prefab2;
        [SerializeField] private Window prefab3;
        [SerializeField] private Window prefab4;
        [SerializeField] private Transform windowViewPoint;

        public override void InstallBindings()
        {
            Container.Bind<WidgetSet<Window>>().To<SampleWindowSet>().FromNew().AsSingle()
                .WhenInjectedInto<WindowManager>();
            Container.Bind<Transform>().WithId("WindowViewPoint").FromInstance(windowViewPoint);
            Container.BindMemoryPool<Window, Window.Pool>().WithId(prefab1.Identify).FromComponentInNewPrefab(prefab1);
            Container.BindMemoryPool<Window, Window.Pool>().WithId(prefab2.Identify).FromComponentInNewPrefab(prefab2);
            Container.BindMemoryPool<Window, Window.Pool>().WithId(prefab3.Identify).FromComponentInNewPrefab(prefab3);
            Container.BindMemoryPool<Window, Window.Pool>().WithId(prefab4.Identify).FromComponentInNewPrefab(prefab4);
            Container.Bind<WindowManager>().FromNew().AsSingle();
        }
    }
}