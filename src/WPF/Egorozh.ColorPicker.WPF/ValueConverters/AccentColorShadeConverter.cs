using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    public class AccentColorShadeConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int shade;
            HsvColor hsvColor;

            try
            {
                shade = System.Convert.ToInt32(parameter?.ToString());
            }
            catch
            {
                throw new ArgumentException("Invalid parameter provided, unable to convert to double");
            }

            try
            {
                hsvColor = new HsvColor(((Color) value).ToColor());
            }
            catch
            {
                throw new ArgumentException("Invalid color value provided, unable to convert to HsvColor");
            }

            switch (shade)
            {
                case -3:
                {
                    hsvColor.H = hsvColor.H * 1.0;
                    hsvColor.S = hsvColor.S * 1.10;
                    hsvColor.V = hsvColor.V * 0.40;
                    break;
                }
                case -2:
                {
                    hsvColor.H = hsvColor.H * 1.0;
                    hsvColor.S = hsvColor.S * 1.05;
                    hsvColor.V = hsvColor.V * 0.50;
                    break;
                }
                case -1:
                {
                    hsvColor.H = hsvColor.H * 1.0;
                    hsvColor.S = hsvColor.S * 1.0;
                    hsvColor.V = hsvColor.V * 0.75;
                    break;
                }
                case 0:
                {
                    // No change
                    break;
                }
                case 1:
                {
                    hsvColor.H = hsvColor.H * 1.00;
                    hsvColor.S = hsvColor.S * 1.00;
                    hsvColor.V = hsvColor.V * 1.05;
                    break;
                }
                case 2:
                {
                    hsvColor.H = hsvColor.H * 1.00;
                    hsvColor.S = hsvColor.S * 0.75;
                    hsvColor.V = hsvColor.V * 1.05;
                    break;
                }
                case 3:
                {
                    hsvColor.H = hsvColor.H * 1.00;
                    hsvColor.S = hsvColor.S * 0.65;
                    hsvColor.V = hsvColor.V * 1.05;
                    break;
                }
            }

            return hsvColor.ToRgbColor().ToColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}