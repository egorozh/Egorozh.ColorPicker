using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Egorozh.ColorPicker
{
    public static class HexComboBoxHelpers
    {
        public static List<NamedColor> GetNamedColors()
        {
            var colors = new List<NamedColor>();

            var type = typeof(Color);

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (property.PropertyType == type)
                {
                    var color = (Color) property.GetValue(type, null);

                    if (!color.IsEmpty)
                        colors.Add(new NamedColor(color.Name, color));
                }
            }

            return colors;
        }
    }
}