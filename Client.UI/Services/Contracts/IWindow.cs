namespace Client.UI.Services.Contracts;

public interface IWindow
{
    event RoutedEventHandler Loaded;
    void Show();
}
