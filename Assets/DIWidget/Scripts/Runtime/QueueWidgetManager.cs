namespace DIWidget
{
    public class QueueWidgetManager<TWidget> : WidgetManager<TWidget> where TWidget : Widget<TWidget>
    {
        private readonly ListQueue<TWidget> _openWaitQueue = new ListQueue<TWidget>();
        private readonly object _openWaitQueueLock = new object();

        protected override TWidget SetCurrentWidgetOnOpen(TWidget openWidget)
        {
            lock (_openWaitQueueLock)
            {
                if (Current == null)
                {
                    Initialize(openWidget);
                    return openWidget;
                }

                _openWaitQueue.Enqueue(openWidget);
                return Current;
            }
        }

        protected override TWidget SetCurrentWidgetOnRemove(TWidget removeWidget)
        {
            lock (_openWaitQueueLock)
            {
                if (Current != removeWidget)
                {
                    _openWaitQueue.Remove(removeWidget);
                    return removeWidget;
                }

                Finalize(removeWidget);
                Despawn(removeWidget);

                if (_openWaitQueue.Length <= 0) return null;
                var openWidget = _openWaitQueue.Dequeue();
                Initialize(openWidget);
                return openWidget;
            }
        }
    }
}