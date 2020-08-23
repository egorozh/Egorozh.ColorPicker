using System.Drawing;

namespace Egorozh.ColorPicker
{
    internal class NamedColor
    {
        public string Name { get; }
        public Color Color { get; }

        public NamedColor(string name, Color color)
        {
            Name = name;
            Color = color;
        }
    }
}