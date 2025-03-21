using System.Windows.Media.Imaging;

namespace Client.UI.Helpers;

public class ShortCutConverter : IValueConverter
{
    private readonly List<string> _shortCuts =
    [
        "Rogue",
        "Warrior",
        "Paladin",
        "Hunter",
        "Rogue",
        "Priest",
        "DeathKnight",
        "Shaman",
        "Mage",
        "Warlock",
        "Monk",
        "Druid",
        "DemonHunter",
        "Evoker"
    ];

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int index && index >= 0 && index < _shortCuts.Count)
        {
            string uri = $"pack://application:,,,/Assets/Class/{_shortCuts[index]}.png";
            return new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        if (value is string path && !string.IsNullOrEmpty(path))
        {
            return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        }

        return "";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}