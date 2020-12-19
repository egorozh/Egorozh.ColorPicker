using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Image = System.Windows.Controls.Image;
using Point = System.Drawing.Point;

namespace Egorozh.ColorPicker
{
    [TemplatePart(Name = PART_SnapshotImage, Type = typeof(Image))]
    public class ScreenColorPicker : Control, IColorClient
    {
        #region Private Fields

        private const string PART_SnapshotImage = "PART_SnapshotImage";

        private IColorManager? _colorManager;

        private Bitmap _bitmap;
        private bool _isCapturing;
        private Image? _snapshotImage;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsStartSelectColorProperty = DependencyProperty.Register(
            "IsStartSelectColor", typeof(bool), typeof(ScreenColorPicker), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty EyedropperCursorProperty = DependencyProperty.Register(
            "EyedropperCursor", typeof(Cursor), typeof(ScreenColorPicker), new PropertyMetadata(Cursors.Arrow));

        public static readonly DependencyProperty InitImageProperty = DependencyProperty.Register(
            "InitImage", typeof(UIElement), typeof(ScreenColorPicker), new PropertyMetadata(default(UIElement)));

        #endregion

        #region Public Properties

        public int Zoom { get; set; } = 12;

        public Cursor EyedropperCursor
        {
            get => (Cursor) GetValue(EyedropperCursorProperty);
            set => SetValue(EyedropperCursorProperty, value);
        }

        public UIElement InitImage
        {
            get => (UIElement) GetValue(InitImageProperty);
            set => SetValue(InitImageProperty, value);
        }

        public bool IsStartSelectColor
        {
            get => (bool) GetValue(IsStartSelectColorProperty);
            set => SetValue(IsStartSelectColorProperty, value);
        }

        #endregion

        #region Static Constructor

        static ScreenColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenColorPicker),
                new FrameworkPropertyMetadata(typeof(ScreenColorPicker)));
        }

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _snapshotImage = GetTemplateChild(PART_SnapshotImage) as Image;

            RenderOptions.SetBitmapScalingMode(_snapshotImage, BitmapScalingMode.NearestNeighbor);

            MouseLeftButtonDown += UIElement_OnMouseLeftButtonDown;
            MouseMove += UIElement_OnMouseMove;
            MouseLeftButtonUp += UIElement_OnMouseLeftButtonUp;
        }

        #endregion

        #region Private Methods

        private void UpdateSnapshot()
        {
            IsStartSelectColor = true;
            //TargetPixelRect.Visibility = Visibility.Visible;
            //InitImageContentControl.Content = null;

            CreateSnapshotBitmap();

            using var graphics = Graphics.FromImage(_bitmap);

            graphics.Clear(Color.Empty);

            var cursor = GetMousePosition();
            cursor.X -= _bitmap.Width / 2;
            cursor.Y -= _bitmap.Height / 2;

            graphics.CopyFromScreen(cursor, Point.Empty, _bitmap.Size);

            var color = _bitmap.GetPixel(_bitmap.Width / 2, _bitmap.Height / 2);

            _colorManager.CurrentColor = color;

            _snapshotImage.Source = ToWpfBitmap(_bitmap);
        }

        private void CreateSnapshotBitmap()
        {
            if (_bitmap == null)
            {
                var snapshotWidth = (int) Math.Ceiling((double) (ActualWidth / Zoom));
                var snapshotHeight = (int) Math.Ceiling((double) (ActualHeight / Zoom));

                _bitmap = new Bitmap(snapshotWidth, snapshotHeight,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
        }

        private static Point GetMousePosition()
        {
            var w32Mouse = new NativeMethods.Win32Point();
            NativeMethods.GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        #region Mouse Events

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isCapturing)
            {
                Cursor = EyedropperCursor;
                _isCapturing = true;
                Mouse.Capture(this);
            }
        }

        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isCapturing)
                UpdateSnapshot();
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isCapturing)
            {
                Cursor = Cursors.Arrow;
                _isCapturing = false;
                Mouse.Capture(null);
            }
        }

        #endregion

        private static BitmapSource ToWpfBitmap(Bitmap bitmap)
        {
            var stream = new MemoryStream();
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

        #endregion
    }
}