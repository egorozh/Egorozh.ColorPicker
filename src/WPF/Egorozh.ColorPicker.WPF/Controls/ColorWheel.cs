using System.Drawing;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    public class ColorWheel : Control, IColorClient
    {
        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
        }
    }
}