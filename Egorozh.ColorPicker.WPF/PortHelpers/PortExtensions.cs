using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Drawing.Brush;

namespace Egorozh.ColorPicker
{
    internal static class PortExtensions
    {   
        public static Brush CreateTransparencyBrush()
        {
            var dg = ColorPickerControl.TransparentTile;

            var drawingImage = new System.Windows.Controls.Image {Source = new DrawingImage(dg)};

            const double scale = 0.1;

            var width = dg.Bounds.Width * scale;
            var height = dg.Bounds.Height * scale;
            drawingImage.Arrange(new Rect(0, 0, width, height));

            var bitmap = new RenderTargetBitmap((int) width, (int) height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(drawingImage);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using var stream = new MemoryStream();
            encoder.Save(stream);

            return new TextureBrush(Image.FromStream(stream), WrapMode.Tile);
        }
        
        public static BitmapSource ToWpfBitmap(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;

            var result = new BitmapImage();

            result.BeginInit();
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();

            return result;
        }
    }
}