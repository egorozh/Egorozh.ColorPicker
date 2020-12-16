using System.Windows;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    internal class SaturationColorSliderOld : ColorSlider
    {
        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(SaturationColorSlider),
            new PropertyMetadata(default(Color), OnColorChanged));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SaturationColorSlider slider)
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

        public SaturationColorSlider()
        {
            Maximum = 100;
            BarStyle = ColorBarStyle.TwoColor;
            Color = Colors.Black;
        }

        #endregion

        #region Private Methods

        private void OnColorChanged()
        {
            CreateBarColors();
        }

        private void CreateBarColors()
        {
            var color = new HslColor(Color.ToColor())
            {
                S = 0
            };

            Color1 = color.ToRgbColorNew();

            color.S = 1;
            Color2 = color.ToRgbColorNew();
        }

        #endregion
    }
}