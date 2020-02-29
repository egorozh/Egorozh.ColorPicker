namespace AKIM.ColorPicker.Port
{
    /// <summary>
    /// Specifies the style of a color cell border.
    /// </summary>
    public enum ColorCellBorderStyle
    {
        /// <summary>
        /// No border.
        /// </summary>
        None,

        /// <summary>
        /// A single line border.
        /// </summary>
        FixedSingle,

        /// <summary>
        /// A contrasting double border with a soft inner outline using the color of the cell.
        /// </summary>
        DoubleSoft
    }
}