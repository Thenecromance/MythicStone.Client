namespace Client.UI.Helpers.ValueToColorConverter;

public class EquipmentToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int equipment)
        {
            return equipment switch
            {
                < 600 => Brushes.White,
                < 620 => new SolidColorBrush(Color.FromRgb(30, 255, 0)),
                < 640 => new SolidColorBrush(Color.FromRgb(0, 112, 255)),
                < 650 => new SolidColorBrush(Color.FromRgb(163, 53, 238)),
                < 680 => new SolidColorBrush(Color.FromRgb(255, 128, 0)),
                _ => Brushes.Black
            };
        }

        return Brushes.White;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DungeonScoreToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double score)
        {
            return score switch
            {
                < 1000 => Brushes.White,
                < 1500 => new SolidColorBrush(Color.FromRgb(30, 255, 0)),
                < 2000 => new SolidColorBrush(Color.FromRgb(0, 112, 255)),
                < 2800 => new SolidColorBrush(Color.FromRgb(163, 53, 238)),
                < 4000 => new SolidColorBrush(Color.FromRgb(255, 128, 0)),
                _ => Brushes.Black
            };
        }
        else if (value is float scoreF)
        {
            return scoreF switch
            {
                < 1000 => Brushes.White,
                < 1500 => new SolidColorBrush(Color.FromRgb(30, 255, 0)),
                < 2000 => new SolidColorBrush(Color.FromRgb(0, 112, 255)),
                < 2800 => new SolidColorBrush(Color.FromRgb(163, 53, 238)),
                < 4000 => new SolidColorBrush(Color.FromRgb(255, 128, 0)),
                _ => Brushes.Black
            };
        }

        return Brushes.White;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}