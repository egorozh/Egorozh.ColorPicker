using System.Collections.Generic;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class ColorCollection : List<Color>
    {
        public ColorCollection(IEnumerable<Color> init)
        {
            AddRange(init);
        }

        public ColorCollection()
        {
            
        }   
    }
}