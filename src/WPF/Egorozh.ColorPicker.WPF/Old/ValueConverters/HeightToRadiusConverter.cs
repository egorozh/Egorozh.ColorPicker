using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Egorozh.ColorPicker
{
    internal class HeightToRadiusConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height)
                return height / 2;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}