using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Egorozh.ColorPicker
{
    public class HueColorSlider : ColorSlider
    {
        #region Constructor

        public HueColorSlider()
        {
            Minimum = 0;
            Maximum = 359;
        }

        #endregion

        #region Protected Methods

        protected override void UpdateColor(in Color color)
        {
            base.UpdateColor(in color);

            var hsv = new HsvColor(color);

            Value = hsv.H;
        }

        protected override void ValueChanged()
        {
            base.ValueChanged();

            var hsv = new HsvColor(ColorManager.CurrentColor)
            {
                H = Value
            };

            ColorManager.CurrentColor = hsv.ToRgbColor();
        }

        public override void ColorUpdated(Color color, IColorClient client)
        {
            PropertyChanged -= ColorSlider_PropertyChanged;

            UpdateColor(color);

            PropertyChanged += ColorSlider_PropertyChanged;
        }

        protected override List<Color> CreateBackgroundColors(in Color color) =>
            Enumerable.Range(0, 359)
                .Select(h => new HslColor(h, 1, 0.5).ToRgbColor())
                .ToList();

        #endregion
    }
}