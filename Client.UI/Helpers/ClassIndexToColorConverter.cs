namespace Client.UI.Helpers;

public class ClassIndexToColorConverter : IValueConverter
{
    private readonly List<Brush> _shortCuts =
    [
        new SolidColorBrush(Color.FromRgb(255, 255, 255)),
        new SolidColorBrush(Color.FromRgb(199, 156, 110)),
        new SolidColorBrush(Color.FromRgb(245, 140, 186)),
        new SolidColorBrush(Color.FromRgb(170, 211, 114)),
        new SolidColorBrush(Color.FromRgb(255, 244, 104)), // "Rogue",
        new SolidColorBrush(Color.FromRgb(240, 235, 224)), //"Priest",
        new SolidColorBrush(Color.FromRgb(196, 30, 58)), // "DeathKnight",
        new SolidColorBrush(Color.FromRgb(0, 112, 222)), // "Shaman",
        new SolidColorBrush(Color.FromRgb(105, 204, 240)), // "Mage",
        new SolidColorBrush(Color.FromRgb(135, 135, 237)), // "Warlock",
        new SolidColorBrush(Color.FromRgb(0, 255, 150)), // "Monk",
        new SolidColorBrush(Color.FromRgb(255, 125, 10)), // "Druid",
        new SolidColorBrush(Color.FromRgb(163, 48, 201)), // "DemonHunter",
        new SolidColorBrush(Color.FromRgb(0, 255, 150)) // "Evoker"
    ];


    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int index && index >= 0 && index < _shortCuts.Count)
        {
            return _shortCuts[index];
        }

        return _shortCuts[0];
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}