using UnityEngine;
using Zenject;

namespace DIWidget.Sample
{
    public class SampleDialogSet : WidgetSet<Dialog>
    {
        [Inject]
        public SampleDialogSet(
            [Inject(Id = "Dialog1")] Dialog.Pool pool1,
            [Inject(Id = "Dialog2")] Dialog.Pool pool2,
            Transform viewPoint)
        {
            SetPool("Dialog1", pool1);
            SetPool("Dialog2", pool2);
            ViewPoint = viewPoint;
        }
    }
}