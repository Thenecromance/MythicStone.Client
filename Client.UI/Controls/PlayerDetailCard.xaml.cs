using System.Windows.Controls;

namespace Client.UI.Controls
{
    [ContentProperty(nameof(CardContent))]
    public class PlayerDetailCard : Control
    {
        #region CardContent
        public static readonly DependencyProperty CardContentProperty = DependencyProperty.Register(
            nameof(CardContent),
            typeof(object),
            typeof(PlayerDetailCard),
            new PropertyMetadata(null)
        );

        public object? CardContent
        {
            get => GetValue(CardContentProperty);
            set => SetValue(CardContentProperty, value);
        }
        #endregion

        #region CardName
        public static readonly DependencyProperty CardNameProperty = DependencyProperty.Register(
            nameof(CardName),
            typeof(string),
            typeof(PlayerDetailCard),
            new PropertyMetadata(null)
        );
        public string? CardName
        {
            get => (string?)GetValue(CardNameProperty);
            set => SetValue(CardNameProperty, value);
        }
        #endregion
    }
}
