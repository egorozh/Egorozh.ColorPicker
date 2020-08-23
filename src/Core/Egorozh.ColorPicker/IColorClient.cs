using System;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public interface IColorClient
    {
        event Action<Color> ColorChanged;

        void ColorUpdated(Color color, IColorClient client);
    }
}