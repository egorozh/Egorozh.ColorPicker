namespace AKIM.ColorPicker.Port
{
    /// <summary>
    /// Specifies the color space of an Adobe Photoshop color swatch file
    /// </summary>
    public enum AdobePhotoshopColorSwatchColorSpace
    {
        /// <summary>
        /// RGB
        /// </summary>
        Rgb = 0,

        /// <summary>
        /// HSB
        /// </summary>
        Hsb = 1,

        /// <summary>
        /// CMYK
        /// </summary>
        Cmyk = 2,

        /// <summary>
        /// Lab
        /// </summary>
        Lab = 7,

        /// <summary>
        /// Grayscale
        /// </summary>
        Grayscale = 8
    }
}