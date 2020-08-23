using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    public static class ColorsExtensions
    {
        public static Color ToColor(this System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static System.Drawing.Color ToColor(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static int ToArgb(this Color color)
        {
            // To integer
            int iCol = (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;

            return iCol;
        }

        public static Color FromArgb(int iCol)
        {
            Color color = Color.FromArgb((byte) (iCol >> 24),
                (byte) (iCol >> 16),
                (byte) (iCol >> 8),
                (byte) (iCol));

            return color;
        }

        public static bool IsNamedColor(this Color c) => System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).IsNamedColor;

        public static string Name(this Color c) => System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).Name;
    }
}