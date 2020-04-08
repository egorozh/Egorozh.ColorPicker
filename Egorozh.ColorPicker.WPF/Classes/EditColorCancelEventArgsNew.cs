using System.ComponentModel;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    public class EditColorCancelEventArgsNew : CancelEventArgs
    {
        #region Constructors

        public EditColorCancelEventArgsNew(Color color, int colorIndex)
        {
            this.Color = color;
            this.ColorIndex = colorIndex;
        }

        protected EditColorCancelEventArgsNew()
        {
        }

        #endregion

        #region Properties

        public Color Color { get; protected set; }

        public int ColorIndex { get; protected set; }

        #endregion
    }
}