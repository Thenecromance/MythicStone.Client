using System.Windows.Controls;

namespace Client.UI.Controls;

public class DungeonListShow : UserControl
{
    public static readonly DependencyProperty DungeonSourceProperty = DependencyProperty.Register(
        nameof(DungeonSource),
        typeof(object),
        typeof(DungeonListShow),
        new PropertyMetadata(null)
    );


    public object? DungeonSource
    {
        get => GetValue(DungeonSourceProperty);
        set => SetValue(DungeonSourceProperty, value);
    }
}