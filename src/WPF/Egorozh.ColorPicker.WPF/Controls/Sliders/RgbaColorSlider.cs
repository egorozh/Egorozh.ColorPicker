using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public class RgbaColorSlider : ColorSlider
    {
        #region Dependency Properties

        public static readonly DependencyProperty ChannelProperty = DependencyProperty.Register(
            nameof(Channel), typeof(RgbaChannel), typeof(RgbaColorSlider),
            new PropertyMetadata(default(RgbaChannel), ChannelChanged));

        private static void ChannelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RgbaColorSlider colorSlider)
                colorSlider.ChannelChanged();
        }

        #endregion

        #region Public Properties

        public RgbaChannel Channel
        {
            get => (RgbaChannel) GetValue(ChannelProperty);
            set => SetValue(ChannelProperty, value);
        }

        #endregion

        #region Constructor

        public RgbaColorSlider()
        {
            Minimum = 0;
            Maximum = 255;
        }

        #endregion

        #region Protected Methods

        protected override List<Color> CreateBackgroundColors(in Color color)
        {
            var colors = new List<Color>();

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

                colors.Add(Color.FromArgb(a, r, g, b));
            }

            return colors;
        }

        protected override Brush CreateBackgroundBrush(in Color color)
        {
            GradientStopCollection gradStops = new();

            const int count = 255;
            const int step = 3;

            for (uint i = 0; i < count; i += step)
            {
                var a = color.A;
                var r = color.R;
                var g = color.G;
                var b = color.B;

                switch (Channel)
                {
                    case RgbaChannel.Red:
                        r = (byte) i;
                        break;
                    case RgbaChannel.Green:
                        g = (byte) i;
                        break;
                    case RgbaChannel.Blue:
                        b = (byte) i;
                        break;
                    case RgbaChannel.Alpha:
                        a = (byte) i;
                        break;
                }

                var offset = i switch
                {
                    0 => 0,
                    count - 1 => 1,
                    _ => 1.0D / count * i
                };

                gradStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(a, r, g, b), offset));
            }

            double angle = Orientation == Orientation.Horizontal ? 0 : 90;
            var start = Orientation == Orientation.Vertical
                ? new Point(0, 1)
                : new Point(0, 0);

            var end = Orientation == Orientation.Vertical
                ? new Point(0, 0)
                : new Point(1, 0);

            return new LinearGradientBrush
            {
                StartPoint = start,
                EndPoint = end,
                GradientStops = gradStops
            };
        }

        protected override void UpdateColor(in Color color)
        {
            base.UpdateColor(in color);

            Value = Channel switch
            {
                RgbaChannel.Alpha => color.A,
                RgbaChannel.Red => color.R,
                RgbaChannel.Green => color.G,
                RgbaChannel.Blue => color.B,
                _ => 0
            };
        }

        protected override void OnValueChanged()
        {
            base.OnValueChanged();

            var color = ColorManager.CurrentColor;

            var r = color.R;
            var g = color.G;
            var b = color.B;
            var a = color.A;

            switch (Channel)
            {
                case RgbaChannel.Red:
                    r = (byte) Value;
                    break;
                case RgbaChannel.Green:
                    g = (byte) Value;
                    break;
                case RgbaChannel.Blue:
                    b = (byte) Value;
                    break;
                case RgbaChannel.Alpha:
                    a = (byte) Value;
                    break;
            }

            ColorManager.CurrentColor = Color.FromArgb(a, r, g, b);
        }

        #endregion

        #region Private Methods

        private void ChannelChanged()
        {
            if (ColorManager != null)
                UpdateColor(ColorManager.CurrentColor);
        }

        #endregion
    }
}