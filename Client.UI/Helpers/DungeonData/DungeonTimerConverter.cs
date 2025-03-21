using Client.UI.Services;

namespace Client.UI.Helpers;

public class DungeonTimerConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int index || index < 0) return null;
        //format to 00:00:00
        return TimeSpan.FromMilliseconds(index).ToString(@"hh\:mm\:ss");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DungeonFinishStatus : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool status)
            return "未知";
        ;

        return status ? "已完成" : "未完成";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}