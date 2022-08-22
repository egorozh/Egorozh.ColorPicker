using System.Drawing;

namespace Egorozh.ColorPicker;

public class NamedColor
{
    public string Name { get; }
    public Color Color { get; }

    public NamedColor(string name, Color color)
    {
        Name = name;
        Color = color;
    }
}