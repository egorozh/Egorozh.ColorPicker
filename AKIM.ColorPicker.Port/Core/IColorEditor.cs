using System;
using System.Drawing;

namespace AKIM.ColorPicker.Port
{
    /// <summary>
    /// Provides functionality required by color editors that are bindable
    /// </summary>
    public interface IColorEditor
    {
        #region Events

        /// <summary>
        /// Occurs when the <see cref="Color"/> property is changed.
        /// </summary>
        event EventHandler ColorChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the component color.
        /// </summary>
        /// <value>The component color.</value>
        Color Color { get; set; }

        #endregion
    }
}