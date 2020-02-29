using System;
using System.Globalization;
using System.Windows.Media;


namespace AKIM.ColorPicker.Port
{
    internal class ColorToFormsColorConverter : BaseValueConverter<ColorToFormsColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                color.ToColor();
            }

            return new System.Drawing.Color();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color color)
            {
                color.ToColor();
            }

            return new Color();
        }

    }
}