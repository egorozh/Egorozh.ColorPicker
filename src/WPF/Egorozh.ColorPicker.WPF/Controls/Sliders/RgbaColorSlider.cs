using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class RgbaColorSlider : ColorSlider, IColorClient
    {
        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
        }
    }
}