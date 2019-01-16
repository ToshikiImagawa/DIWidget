using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DIWidget
{
    public abstract class WidgetSet<TWidget> where TWidget : Widget<TWidget>
    {
        private readonly Dictionary<object, IMemoryPool<TWidget>> _poolSet =
            new Dictionary<object, IMemoryPool<TWidget>>();
        
        public Transform ViewPoint { get; protected set; }

        protected void SetPool(object identify, IMemoryPool<TWidget> pool)
        {
            _poolSet[identify] = pool;
        }

        public IMemoryPool<TWidget> GetPool(object identify)
        {
            if (!_poolSet.ContainsKey(identify))
                throw new Exception($"Specified prefab is not registered. : {identify}");
            return _poolSet[identify];
        }
    }
}