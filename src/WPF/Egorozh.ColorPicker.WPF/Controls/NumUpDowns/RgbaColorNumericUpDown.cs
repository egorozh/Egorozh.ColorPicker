using System.Drawing;
using System.Windows;

namespace Egorozh.ColorPicker
{
    public class RgbaColorNumericUpDown : LabelNumericUpDown
    {
        #region Dependency Properties

        public static readonly DependencyProperty ChannelProperty = DependencyProperty.Register(
            nameof(Channel), typeof(RgbaChannel), typeof(RgbaColorNumericUpDown),
            new PropertyMetadata(default(RgbaChannel), ChannelChanged));

        private static void ChannelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RgbaColorNumericUpDown rgbaColorNumericUpDown)
                rgbaColorNumericUpDown.ChannelChanged();
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

        public RgbaColorNumericUpDown()
        {
            Minimum = 0;
            Maximum = 255;
        }

        #endregion

        #region Protected Methods

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