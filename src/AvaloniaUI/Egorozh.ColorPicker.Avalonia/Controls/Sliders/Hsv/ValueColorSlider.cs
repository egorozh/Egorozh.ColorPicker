using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker.Avalonia
{
    public class ValueColorSlider : ColorSlider
    {
        #region Constructor

        public ValueColorSlider()
        {
            Minimum = 0;
            Maximum = 100;
        }

        #endregion

        #region Protected Methods

        protected override void UpdateColor(in Color color)
        {
            base.UpdateColor(in color);

            var hsv = new HsvColor(color);

            Value = hsv.V * 100;
        }

        protected override void ValueChanged()
        {
            base.ValueChanged();

            var hsv = new HsvColor(ColorManager.CurrentColor)
            {
                V = Value / 100.0
            };

            ColorManager.CurrentColor = hsv.ToRgbColor();
        }

        protected override List<Color> CreateBackgroundColors(in Color color)
        {
            var colors = new List<Color>();

            var hsv = new HsvColor(color)
            {
                V = 0
            };
            colors.Add(hsv.ToRgbColor());

            hsv.V = 1;

            colors.Add(hsv.ToRgbColor());

            return colors;
        }

        #endregion
    }
}