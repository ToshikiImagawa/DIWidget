using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class SampleBlindWindowSet : WidgetSet<Window>
    {
        [Inject]
        public SampleBlindWindowSet(
            [Inject(Id = "Window1")] Window.Pool pool1,
            [Inject(Id = "Window2")] Window.Pool pool2,
            [Inject(Id = "Window3")] Window.Pool pool3,
            [Inject(Id = "Window4")] Window.Pool pool4,
            Transform viewPoint)
        {
            SetPool("Window1", pool1);
            SetPool("Window2", pool2);
            SetPool("Window3", pool3);
            SetPool("Window4", pool4);
            ViewPoint = viewPoint;
        }
    }
}