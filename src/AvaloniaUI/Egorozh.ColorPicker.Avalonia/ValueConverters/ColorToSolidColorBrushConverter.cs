namespace Egorozh.ColorPicker; 

using Avalonia.Media;

public class ColorToSolidColorBrushConverter : BaseValueConverter
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is System.Drawing.Color color)
            return new SolidColorBrush(color.ToColor());

        return Brushes.White;
    }
}