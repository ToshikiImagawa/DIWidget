using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class DialogInstaller : MonoInstaller
    {
        [SerializeField] private Dialog prefab1;
        [SerializeField] private Dialog prefab2;
        [SerializeField] private Transform dialogViewPoint;

        public override void InstallBindings()
        {
            // WidgetSet
            Container.BindWidgetSetSingle<Dialog, SampleDialogSet>().WhenInjectedInto<DialogManager>();

            // ViewPoint
            Container.Bind<Transform>().FromInstance(dialogViewPoint).WhenInjectedInto(typeof(SampleDialogSet));

            // Pool
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Dialog, Dialog.Pool>(prefab1);
            Container.BindWidgetMemoryPoolFromComponentInNewPrefab<Dialog, Dialog.Pool>(prefab2);

            // Manager
            Container.Bind<DialogManager>().AsSingle();
        }
    }
}