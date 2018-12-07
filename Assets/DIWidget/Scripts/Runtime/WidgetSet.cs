using System;
using System.Collections.Generic;
using UnityEngine;

namespace DIWidget
{
    public abstract class WidgetSet<TWidget> where TWidget : Widget<TWidget>
    {
        private readonly Dictionary<object, Widget<TWidget>.Pool> _poolSet =
            new Dictionary<object, Widget<TWidget>.Pool>();
        
        public Transform ViewPoint { get; protected set; }

        protected void SetPool(object identify, Widget<TWidget>.Pool pool)
        {
            _poolSet[identify] = pool;
        }

        public Widget<TWidget>.Pool GetPool(object identify)
        {
            if (!_poolSet.ContainsKey(identify))
                throw new Exception($"Specified prefab is not registered. : {identify}");
            return _poolSet[identify];
        }
    }
}