using System;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public interface IColorManager
    {
        event Action<Color> ColorChanged;

        Color CurrentColor { get; set; } 
        
        void AddClient(params IColorClient[] clients);

        void SetColorFromHsl(double hue, double saturation, double lightness);
    }
}