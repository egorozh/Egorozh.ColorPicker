using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    public class ColorHexComboBox : ComboBox, IColorClient
    {
        #region Static Constructor

        static ColorHexComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorHexComboBox),
                new FrameworkPropertyMetadata(typeof(ColorHexComboBox)));
        }

        #endregion

        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
        }
    }
}