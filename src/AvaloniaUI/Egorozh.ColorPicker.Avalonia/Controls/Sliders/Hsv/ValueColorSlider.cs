using System.Drawing;

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

        #endregion
    }
}