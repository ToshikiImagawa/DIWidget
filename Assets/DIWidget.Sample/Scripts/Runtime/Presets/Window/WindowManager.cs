namespace DIWidget
{
    public class WindowManager : StackWidgetManager<Window>
    {
        public void RemoveOpenWindow()
        {
            Remove(Current);
        }
    }
}