namespace DIWidget
{
    public class DialogManager : WidgetManager<Dialog>
    {
        protected override void OnOpen()
        {
            ClosedListStack.Clear();
        }
    }
}