using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class ColorHitTestInfo
    {
        #region Properties

        public Color Color { get; set; }

        public int Index { get; set; }

        public ColorSource Source { get; set; }

        #endregion
    }
}