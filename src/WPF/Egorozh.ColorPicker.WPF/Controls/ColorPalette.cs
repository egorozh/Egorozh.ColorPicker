using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    public class ColorPalette : ListBox, IColorClient
    {
        #region Static Constructor

        static ColorPalette()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPalette),
                new FrameworkPropertyMetadata(typeof(ColorPalette)));
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