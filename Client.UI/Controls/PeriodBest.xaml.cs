using System.Windows.Controls;

namespace Client.UI.Controls;

public class PeriodBest : UserControl
{
    public static readonly DependencyProperty PeriodSourceProperty = DependencyProperty.Register(
        nameof(PeriodSource),
        typeof(object),
        typeof(PeriodBest),
        new PropertyMetadata(null)
    );


    public object? PeriodSource
    {
        get => GetValue(PeriodSourceProperty);
        set => SetValue(PeriodSourceProperty, value);
    }
}