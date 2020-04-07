using System.Linq;

namespace Egorozh.ColorPicker
{
    internal class HueColorSliderNew : ColorSliderNew
    {
        public HueColorSliderNew()
        {
            BarStyle = ColorBarStyle.Custom;
            Maximum = 359;
            CustomColors =
                new ColorCollectionNew(Enumerable.Range(0, 359).Select(h => new HslColor(h, 1, 0.5).ToRgbColorNew()));
        }
    }
}