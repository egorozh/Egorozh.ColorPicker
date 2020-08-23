using System.Linq;

namespace Egorozh.ColorPicker
{
    internal class HueColorSlider : ColorSlider
    {
        public HueColorSlider()
        {
            BarStyle = ColorBarStyle.Custom;
            Maximum = 359;
            CustomColors =
                new ColorCollection(Enumerable.Range(0, 359).Select(h => new HslColor(h, 1, 0.5).ToRgbColorNew()));
        }
    }
}   