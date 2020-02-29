using System;
using System.Globalization;
using System.Windows;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Конвертирует <see cref="bool"/> в <see cref="Visibility"/>
    /// </summary>
    internal class BoolToVisibilityConverter : BaseValueConverter<BoolToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isVisible)) return Visibility.Collapsed;

            if (parameter != null)
                return isVisible ? Visibility.Collapsed : Visibility.Visible;

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}