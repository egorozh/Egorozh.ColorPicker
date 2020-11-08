using System.Collections.Generic;
using System.Linq;

namespace Egorozh.ColorPicker
{
    public static class HexComboBoxHelpers
    {
        public static List<NamedColor> GetNamedColors() 
            => ColorPalettes.NamedColors.Select(c => new NamedColor(c.Name, c)).ToList();
    }
}