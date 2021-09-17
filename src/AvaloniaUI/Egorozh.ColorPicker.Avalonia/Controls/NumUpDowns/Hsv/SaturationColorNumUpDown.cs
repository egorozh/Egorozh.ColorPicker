using System.Drawing;

namespace Egorozh.ColorPicker;

public class SaturationColorNumUpDown : LabelNumericUpDown
{
    #region Constructor

    public SaturationColorNumUpDown()
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

        Value = hsv.S * 100;
    }

    protected override void OnValueChanged()
    {
        base.OnValueChanged();

        var hsv = new HsvColor(ColorManager.CurrentColor)
        {
            S = Value / 100.0
        };

        ColorManager.CurrentColor = hsv.ToRgbColor();
    }

    #endregion
}