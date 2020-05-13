using System.Windows;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    internal class LightnessColorSlider : ColorSlider
    {
        #region Private Fields

        private Color _color;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(LightnessColorSlider),
            new PropertyMetadata(default(Color), OnColorChanged));


        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LightnessColorSlider slider)
                slider.OnColorChanged();
        }

        #endregion

        #region Public Properties

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region Constructor

        public LightnessColorSlider()
        {
            Maximum = 100;
            BarStyle = ColorBarStyle.TwoColor;
            Color = Colors.Black;
        }

        #endregion

        #region Private Methods

        private void OnColorChanged()
        {
            //Value = (float)new HslColor(value).L * 100;

            var color = new HslColor(Color.ToColor())
            {
                L = Value / 100D
            };

            _color = color.ToRgbColorNew();

            CreateBarColors();
        }

        private void CreateBarColors()
        {
            var color = new HslColor(_color.ToColor())
            {
                L = 0
            };

            Color1 = color.ToRgbColorNew();

            color.L = 1;
            Color2 = color.ToRgbColorNew();
        }

        #endregion
    }
}