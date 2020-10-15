﻿using Avalonia;
using System.Drawing;

namespace Egorozh.ColorPicker.Avalonia
{
    public class RgbaColorSlider : ColorSlider
    {
        #region Dependency Properties

        public static readonly StyledProperty<RgbaChannel> ChannelProperty =
            AvaloniaProperty.Register<RgbaColorSlider, RgbaChannel>(nameof(RgbaChannel), notifying: ChannelChanged);

        private static void ChannelChanged(IAvaloniaObject obj, bool isAfter)
        {
            if (obj is RgbaColorSlider rgbaSlider)
                rgbaSlider.ChannelChanged();
        }

        #endregion

        #region Public Properties

        public RgbaChannel Channel
        {
            get => GetValue(ChannelProperty);
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

        protected override void ValueChanged()
        {
            base.ValueChanged();

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