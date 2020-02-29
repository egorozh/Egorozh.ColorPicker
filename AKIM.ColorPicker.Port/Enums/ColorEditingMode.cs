using AKIM.ColorPicker.Forms;

namespace AKIM.ColorPicker.Port
{
    /// <summary>
    /// Specifies the edit mode of a <see cref="ColorGrid" />.
    /// </summary>
    public enum ColorEditingMode
    {
        /// <summary>
        /// None. No editing is allowed.
        /// </summary>
        None,

        /// <summary>
        /// Only custom colors can be edited.
        /// </summary>
        CustomOnly,

        /// <summary>
        /// Custom or standard colors can be edited.
        /// </summary>
        Both
    }
}