using System.Windows.Media.Imaging;

namespace Client.UI.Helpers;

public class LazyImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string url)
        {
            return new BitmapImage(new Uri(url))
            {
                CreateOptions = BitmapCreateOptions.DelayCreation
            };
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}