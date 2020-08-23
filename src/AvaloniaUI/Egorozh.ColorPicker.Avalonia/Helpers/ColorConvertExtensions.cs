using Avalonia.Media;

namespace Egorozh.ColorPicker.Avalonia
{
    internal static class ColorConvertExtensions
    {
        public static System.Drawing.Color ToColor(this Color color)
            => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}