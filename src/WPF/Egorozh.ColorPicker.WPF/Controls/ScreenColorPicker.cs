using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    public class ScreenColorPicker : Control, IColorClient
    {
        #region Static Constructor

        static ScreenColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenColorPicker),
                new FrameworkPropertyMetadata(typeof(ScreenColorPicker)));
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