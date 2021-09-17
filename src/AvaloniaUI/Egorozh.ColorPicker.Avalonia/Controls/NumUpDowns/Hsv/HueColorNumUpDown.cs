using System.Drawing;

namespace Egorozh.ColorPicker;

public class HueColorNumUpDown : LabelNumericUpDown
{
    #region Constructor

    public HueColorNumUpDown()
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

    protected override void OnValueChanged()
    {
        base.OnValueChanged();

        var hsv = new HsvColor(ColorManager.CurrentColor)
        {
            H = Value
        };

        ColorManager.CurrentColor = hsv.ToRgbColor();
    }

    #endregion
}