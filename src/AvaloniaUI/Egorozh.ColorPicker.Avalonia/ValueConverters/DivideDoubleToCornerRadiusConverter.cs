namespace Egorozh.ColorPicker;

public class DivideDoubleToCornerRadiusConverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var divider = 2.0;

        if (parameter != null && double.TryParse(parameter.ToString(), out var div))
        {
            divider = div;
        }

        if (value is double dividend)
            return new CornerRadius(dividend / divider);

        return 0;
    }
}