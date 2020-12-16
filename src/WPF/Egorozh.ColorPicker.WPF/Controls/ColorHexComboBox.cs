using System.Drawing;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    public class ColorHexComboBox : ComboBox, IColorClient
    {
        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
        }
    }
}