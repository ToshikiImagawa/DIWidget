using System;
using System.Linq;
using Zenject;

namespace DIWidget
{
    public static class BindExtension
    {
        public static FromBinderGeneric<TWidgetSet> BindWidgetSet<TWidget, TWidgetSet>(this DiContainer self)
            where TWidgetSet : WidgetSet<TWidget> where TWidget : Widget<TWidget>
        {
            return self.Bind<WidgetSet<TWidget>>().To<TWidgetSet>();
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder BindWidgetSetSingle<TWidget, TWidgetSet>(
            this DiContainer self)
            where TWidgetSet : WidgetSet<TWidget> where TWidget : Widget<TWidget>
        {
            return self.BindWidgetSet<TWidget, TWidgetSet>().AsSingle();
        }

        public static NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder
            BindWidgetMemoryPoolFromComponentInNewPrefab<TWidget, TWidgetPool>(this DiContainer self, TWidget prefab)
            where TWidgetPool : MemoryPool<TWidget> where TWidget : Widget<TWidget>
        {
            return self.BindMemoryPool<TWidget, TWidgetPool>().WithId(prefab.Identify).FromComponentInNewPrefab(prefab);
        }

        public static NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder
            BindWidgetMemoryPoolFromNewComponentOnNewPrefab<TWidget, TWidgetPool>(this DiContainer self, TWidget prefab)
            where TWidgetPool : MemoryPool<TWidget> where TWidget : Widget<TWidget>
        {
            return self.BindMemoryPool<TWidget, TWidgetPool>().WithId(prefab.Identify)
                .FromNewComponentOnNewPrefab(prefab);
        }

        public static NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder
            BindWidgetMemoryPoolFromNewComponentOnNewGameObject<TWidget, TWidgetPool>(this DiContainer self,
                object identify)
            where TWidgetPool : MemoryPool<TWidget> where TWidget : Widget<TWidget>
        {
            return self.BindMemoryPool<TWidget, TWidgetPool>().WithId(identify)
                .FromNewComponentOnNewGameObject();
        }

        public static NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder
            BindWidgetMemoryPoolFromComponentInNewPrefabResource<TWidget, TWidgetPool>(this DiContainer self,
                object identify, string resourcePath)
            where TWidgetPool : MemoryPool<TWidget> where TWidget : Widget<TWidget>
        {
            return self.BindMemoryPool<TWidget, TWidgetPool>().WithId(identify)
                .FromComponentInNewPrefabResource(resourcePath);
        }

        public static NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder
            BindWidgetMemoryPoolFromNewComponentOnNewPrefabResource<TWidget, TWidgetPool>(this DiContainer self,
                object identify, string resourcePath)
            where TWidgetPool : MemoryPool<TWidget> where TWidget : Widget<TWidget>
        {
            return self.BindMemoryPool<TWidget, TWidgetPool>().WithId(identify)
                .FromNewComponentOnNewPrefabResource(resourcePath);
        }

        public static CopyNonLazyBinder WhenInjectedIntoTargetOnly(this ConditionCopyNonLazyBinder self,
            params Type[] targets)
        {
            return self.When(r => targets.Any(x => r.ObjectType != null && r.ObjectType == x));
        }

        public static CopyNonLazyBinder WhenInjectedIntoTargetOnly<T>(this ConditionCopyNonLazyBinder self)
        {
            return self.When(r => r.ObjectType != null && r.ObjectType == typeof(T));
        }
    }
}