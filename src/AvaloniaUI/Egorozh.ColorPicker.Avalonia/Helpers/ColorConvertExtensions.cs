using Avalonia.Media;

namespace Egorozh.ColorPicker
{
    internal static class ColorConvertExtensions
    {
        public static System.Drawing.Color ToColor(this Color color)
            => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

        public static Color ToColor(this System.Drawing.Color color)
            => Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}