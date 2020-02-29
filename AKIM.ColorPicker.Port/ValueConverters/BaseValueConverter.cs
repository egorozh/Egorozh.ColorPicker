using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AKIM.ColorPicker.Port
{
    /// <summary>   
    /// Базовый класс, реализующий <see cref="IValueConverter"/>
    /// </summary>
    /// <typeparam name="T">Value Converter</typeparam>
    internal abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}