using System.Windows;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    internal class RgbaColorSliderNew : ColorSliderNew
    {
        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(RgbaColorSliderNew),
            new PropertyMetadata(default(Color), OnColorChanged));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RgbaColorSliderNew slider)
                slider.OnColorChanged();
        }

        public static readonly DependencyProperty ChannelProperty = DependencyProperty.Register(
            nameof(Channel), typeof(RgbaChannel), typeof(RgbaColorSliderNew),
            new PropertyMetadata(default(RgbaChannel), OnChannelChanged));

        private static void OnChannelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RgbaColorSliderNew slider)
                slider.CreateBarColors();
        }

        #endregion

        #region Public Properties

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public RgbaChannel Channel
        {
            get => (RgbaChannel) GetValue(ChannelProperty);
            set => SetValue(ChannelProperty, value);
        }

        #endregion

        #region Constructor

        public RgbaColorSliderNew()
        {
            Maximum = 255;
            BarStyle = ColorBarStyle.Custom;
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
            var custom = new ColorCollection();
            var color = Color;

            for (byte i = 0; i < 254; i++)
            {
                var a = color.A;
                var r = color.R;
                var g = color.G;
                var b = color.B;

                switch (Channel)
                {
                    case RgbaChannel.Red:
                        r = i;
                        break;
                    case RgbaChannel.Green:
                        g = i;
                        break;
                    case RgbaChannel.Blue:
                        b = i;
                        break;
                    case RgbaChannel.Alpha:
                        a = i;
                        break;
                }

                custom.Add(Color.FromArgb(a, r, g, b));
            }

            CustomColors = custom;
        }

        #endregion
    }
}