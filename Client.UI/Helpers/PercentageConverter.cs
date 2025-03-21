namespace Client.UI.Helpers;

public class PercentageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        double parentHeight = (double)value;
        double percentage = System.Convert.ToDouble(parameter);
        return parentHeight * percentage;
        
        
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}