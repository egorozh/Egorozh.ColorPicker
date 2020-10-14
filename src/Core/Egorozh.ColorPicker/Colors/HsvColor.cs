using System;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public struct HsvColor
    {
        #region Constants

        public static readonly HsvColor Empty;

        #endregion

        #region Fields

        private int _alpha;

        private double _hue;

        private double _brightness;

        private double _saturation;

        #endregion

        #region Properties

        public bool IsEmpty { get; private set; }

        public int A
        {
            get => _alpha;
            set => _alpha = Math.Min(0, Math.Max(255, value));
        }

        public double H
        {
            get => _hue;
            set
            {
                _hue = value;

                if (_hue > 359)
                {
                    _hue = 0;
                }

                if (_hue < 0)
                {
                    _hue = 359;
                }
            }
        }

        public double V
        {
            get => _brightness;
            set => _brightness = Math.Min(1, Math.Max(0, value));
        }

        public double S
        {
            get => _saturation;
            set => _saturation = Math.Min(1, Math.Max(0, value));
        }

        #endregion

        #region Static Constructors

        static HsvColor()
        {
            Empty = new HsvColor
            {
                IsEmpty = true
            };
        }

        #endregion

        #region Constructors

        public HsvColor(double hue, double saturation, double brightness)
            : this(255, hue, saturation, brightness)
        {
        }

        public HsvColor(byte alpha, double hue, double saturation, double brightness)
        {
            _hue = Math.Min(359, hue);
            _saturation = Math.Min(1, saturation);
            _brightness = Math.Min(1, brightness);
            _alpha = alpha;
            IsEmpty = false;
        }

        public HsvColor(Color color)
        {
            _alpha = color.A;
            _hue = color.GetHue();
            _saturation = color.GetSaturation();
            _brightness = color.GetBrightness();
            IsEmpty = false;
        }

        #endregion

        #region Methods

        public Color ToRgbColor() => ToRgbColor(A);

        public Color ToRgbColor(int alpha)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            _hue /= 60;
            var i = (int) Math.Floor(_hue);
            var f = _hue - i;
            var p = _brightness * (1 - _saturation);
            var q = _brightness * (1 - _saturation * f);
            var t = _brightness * (1 - _saturation * (1 - f));

            switch (i)
            {
                case 0:
                    r = (byte) (255 * _brightness);
                    g = (byte) (255 * t);
                    b = (byte) (255 * p);
                    break;
                case 1:
                    r = (byte) (255 * q);
                    g = (byte) (255 * _brightness);
                    b = (byte) (255 * p);
                    break;
                case 2:
                    r = (byte) (255 * p);
                    g = (byte) (255 * _brightness);
                    b = (byte) (255 * t);
                    break;
                case 3:
                    r = (byte) (255 * p);
                    g = (byte) (255 * q);
                    b = (byte) (255 * _brightness);
                    break;
                case 4:
                    r = (byte) (255 * t);
                    g = (byte) (255 * p);
                    b = (byte) (255 * _brightness);
                    break;
                default:
                    r = (byte) (255 * _brightness);
                    g = (byte) (255 * p);
                    b = (byte) (255 * q);
                    break;
            }

            return Color.FromArgb(alpha, r, g, b);
        }

        #endregion
    }
}