using AKIM.ColorPicker.Forms;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Determines how the selected cell in a <see cref="ColorGrid" /> control is rendered.
    /// </summary>
    public enum ColorGridSelectedCellStyle
    {
        /// <summary>
        /// The selected cell is drawn no differently to any other cell.
        /// </summary>
        None,

        /// <summary>
        /// The selected cell displays a basic outline and focus rectangle.
        /// </summary>
        Standard,

        /// <summary>
        /// The selected cell is displayed larger than other cells
        /// </summary>
        Zoomed
    }
}