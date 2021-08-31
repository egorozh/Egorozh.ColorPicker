using System;
using System.Drawing;
using System.Text;

namespace Egorozh.ColorPicker
{
    public struct HslColor
    {
        #region Constants

        public static readonly HslColor Empty;

        #endregion

        #region Fields

        private int _alpha;

        private double _hue;

        private double _lightness;

        private double _saturation;

        #endregion

        #region Static Constructors

        static HslColor()
        {
            Empty = new HslColor
            {
                IsEmpty = true
            };
        }

        #endregion

        #region Constructors

        public HslColor(double hue, double saturation, double lightness)
            : this(255, hue, saturation, lightness)
        {
        }

        public HslColor(byte alpha, double hue, double saturation, double lightness)
        {
            _hue = Math.Min(359, hue);
            _saturation = Math.Min(1, saturation);
            _lightness = Math.Min(1, lightness);
            _alpha = alpha;
            IsEmpty = false;
        }

        public HslColor(Color color)
        {
            _alpha = color.A;
            _hue = color.GetHue();
            _saturation = color.GetSaturation();
            _lightness = color.GetBrightness();
            IsEmpty = false;
        }

        #endregion

        #region Operators

        public static bool operator ==(HslColor a, HslColor b)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            return a.H == b.H && a.L == b.L && a.S == b.S && a.A == b.A;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }

        public static implicit operator HslColor(Color color) => new(color);

        public static implicit operator Color(HslColor color) => color.ToRgbColor();

        public static bool operator !=(HslColor a, HslColor b) => !(a == b);

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

                if (_hue > 359) _hue = 0;

                if (_hue < 0) _hue = 359;
            }
        }

        public double L
        {
            get => _lightness;
            set => _lightness = Math.Min(1, Math.Max(0, value));
        }

        public double S
        {
            get => _saturation;
            set => _saturation = Math.Min(1, Math.Max(0, value));
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            var result = obj is HslColor hslColor && this == hslColor;

            return result;
        }

        public override int GetHashCode() => base.GetHashCode();

        public Color ToRgbColor() => ToRgbColor(A);

        public Color ToRgbColor(int alpha)
        {
            var q = L switch
            {
                < 0.5 => L * (1 + S),
                _ => L + S - L * S
            };

            var p = 2 * L - q;

            var hk = H / 360;

            var r = GetColor(hk + 1d / 3d, p, q);
            var g = GetColor(hk, p, q);
            var b = GetColor(hk - 1d / 3d, p, q);

            return Color.FromArgb(alpha, (int)r, (int)g, (int)b);
        }

        public override string ToString()
        {
            StringBuilder builder = new();

            builder.Append(GetType().Name);
            builder.Append(" [");
            builder.Append("H=");
            builder.Append(H);
            builder.Append(", S=");
            builder.Append(S);
            builder.Append(", L=");
            builder.Append(L);
            builder.Append("]");

            return builder.ToString();
        }

        #endregion

        #region Private Methods

        private static double GetColor(double tc, double p, double q)
        {
            if (tc < 0)
                tc += 1;

            if (tc > 1)
                tc -= 1;

            var color = tc switch
            {
                < 1d / 6d => p + (q - p) * 6 * tc,
                >= 1d / 6d and < 1d / 2d => q,
                >= 1d / 2d and < 2d / 3d => p + (q - p) * 6 * (2d / 3d - tc),
                _ => p
            };

            color *= 255;

            return color;
        }

        #endregion
    }
}