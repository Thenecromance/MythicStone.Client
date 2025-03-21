namespace Client.UI.Helpers;

public class IndexToClassNameConverter : IValueConverter
{
    private static List<string> _classNames = new()
    {
        "",
        "战士",
        "圣骑士",
        "猎人",
        "盗贼",
        "牧师",
        "死亡骑士",
        "萨满",
        "法师",
        "术士",
        "武僧",
        "德鲁伊",
        "死亡猎手",
        "唤魔师"
    };

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is int idx && idx >= 0 && idx < _classNames.Count)
        {
            return _classNames[idx];
        }

        return "未知";
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}