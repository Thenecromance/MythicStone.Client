using System.Windows.Controls;

namespace Client.UI.Views.Components.PlayerComp;

/// <summary>
/// DataComp.xaml 的交互逻辑
/// </summary>
public partial class DataComp : UserControl
{
    #region Key

    public static readonly DependencyProperty KeyNameProperty = DependencyProperty.Register(
        nameof(KeyName),
        typeof(string),
        typeof(DataComp),
        new PropertyMetadata(null)
    );

    public string? KeyName
    {
        get => (string?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    #endregion

    #region Value

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
        nameof(Value),
        typeof(string),
        typeof(DataComp),
        new PropertyMetadata(null)
    );

    public string? Value
    {
        get => (string?)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    #endregion

    #region Color

    public static readonly DependencyProperty ValueColorProperty = DependencyProperty.Register(
        nameof(ValueColor),
        typeof(Brush),
        typeof(DataComp),
        new PropertyMetadata(
            null)
    );

    public Brush? ValueColor
    {
        get => (Brush?)GetValue(ValueColorProperty);
        set => SetValue(ValueColorProperty, value);
    }

    #endregion

    public DataComp()
    {
        InitializeComponent();
        //Loaded += (s, e) => DataContext = this;
    }
}