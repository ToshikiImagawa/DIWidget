using UnityEngine;
using System;
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
        [SerializeField] private Transform blindWindowViewPoint;
        [SerializeField] private BlindWidget blindWidgetPrefab;

        public override void InstallBindings()
        {
            // WidgetSet
            Container.BindWidgetSetSingle<Window, SampleWindowSet>()
                .WhenInjectedIntoTargetOnly<WindowManager>();
            Container.BindWidgetSetSingle<Window, SampleBlindWindowSet>()
                .WhenInjectedIntoTargetOnly<BlindWindowManager>();

            // ViewPoint
            Container.Bind<Transform>().FromInstance(windowViewPoint)
                .WhenInjectedInto<SampleWindowSet>();
            Container.Bind<Transform>().FromInstance(blindWindowViewPoint)
                .WhenInjectedInto(typeof(SampleBlindWindowSet), typeof(BlindWidget));

            // BlindWidget OnClick Event
            Container.Bind<Action>().FromResolveGetter<BlindWindowManager>(manager => manager.RemoveOpenWindow)
                .WhenInjectedInto<BlindWidget>();

            // BlindWidget Factory
            Container.BindFactory<BlindWidget, BlindWidget.Factory>().FromComponentInNewPrefab(blindWidgetPrefab)
                .AsSingle();

            // Pool
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Window, Window.Pool>(prefab1);
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Window, Window.Pool>(prefab2);
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Window, Window.Pool>(prefab3);
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Window, Window.Pool>(prefab4);

            // Manager
            Container.Bind<BlindWindowManager>().AsSingle();
            Container.Bind<WindowManager>().AsSingle();
        }
    }
}