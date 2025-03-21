using Client.UI.Model.Dungeon;
using Client.UI.Services;

namespace Client.UI.Helpers;

public class DungeonConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int index || index < 0) return null;

        // return DungeonList[index].Name;
        return (from dungeon in App.GetRequiredService<DungeonService>().GetDungeons()
            where dungeon.Id == index
            select dungeon.Name).FirstOrDefault();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}