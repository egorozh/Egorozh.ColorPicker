namespace Egorozh.ColorPicker;

public class DivideDoubleToDoubleConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var divider = 2.0;

        if (parameter != null && double.TryParse(parameter.ToString(), out var div))
            divider = div;

        if (value is double dividend)
            return (dividend / divider);

        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}