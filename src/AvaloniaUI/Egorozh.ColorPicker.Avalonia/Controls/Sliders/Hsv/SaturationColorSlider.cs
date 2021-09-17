using System.Drawing;

namespace Egorozh.ColorPicker;

public class SaturationColorSlider : ColorSlider
{
    #region Constructor

    public SaturationColorSlider()
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

    protected override List<Color> CreateBackgroundColors(in Color color)
    {
        var colors = new List<Color>();

        var hsv = new HsvColor(color)
        {
            S = 0
        };
        colors.Add(hsv.ToRgbColor());

        hsv.S = 1;

        colors.Add(hsv.ToRgbColor());

        return colors;
    }

    #endregion
}