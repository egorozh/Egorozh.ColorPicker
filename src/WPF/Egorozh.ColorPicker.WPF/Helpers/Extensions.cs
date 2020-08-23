using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Egorozh.ColorPicker
{
    internal static class Extensions
    {
        public static List<NamedColor> GetNamedColors()
        {
            var colors = new List<NamedColor>();

            var type = typeof(SystemColors);
            var colorType = typeof(Color);

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (property.PropertyType == colorType)
                {
                    var color = (Color) property.GetValue(type, null);

                    if (!color.IsEmpty)
                        colors.Add(new NamedColor(color.Name, color));
                }
            }

            type = typeof(Color);

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (property.PropertyType == colorType)
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