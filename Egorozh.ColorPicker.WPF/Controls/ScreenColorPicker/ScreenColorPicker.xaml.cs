using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;

namespace Egorozh.ColorPicker
{
    public partial class ScreenColorPicker : IColorEditor
    {
        #region Private Fields

        private Bitmap _bitmap;
        private Color _color;
        private bool _isCapturing;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty EyedropperCursorProperty = DependencyProperty.Register(
            nameof(EyedropperCursor), typeof(Cursor), typeof(ScreenColorPicker), new PropertyMetadata(Cursors.Arrow));

        public static readonly DependencyProperty InitImageProperty = DependencyProperty.Register(
            nameof(InitImage), typeof(FrameworkElement), typeof(ScreenColorPicker),
            new PropertyMetadata(default(FrameworkElement), InitImageChanged));

        private static void InitImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScreenColorPicker screenColorPicker)
                screenColorPicker.InitImageContentControl.Content = e.NewValue;
        }

        #endregion

        #region Public Properties

        public int Zoom { get; set; } = 7;

        public Color Color
        {
            get => _color;
            set => SetColor(value);
        }

        public FrameworkElement InitImage
        {
            get => (FrameworkElement) GetValue(InitImageProperty);
            set => SetValue(InitImageProperty, value);
        }

        public Cursor EyedropperCursor
        {
            get => (Cursor) GetValue(EyedropperCursorProperty);
            set => SetValue(EyedropperCursorProperty, value);
        }

        #endregion

        #region Events

        public event EventHandler ColorChanged;

        #endregion

        #region Constructor

        public ScreenColorPicker()
        {
            InitializeComponent();

            RenderOptions.SetBitmapScalingMode(SnapshotImage, BitmapScalingMode.NearestNeighbor);

            Color = System.Drawing.Color.Black;
            Zoom = 6;

            MouseLeftButtonDown += UIElement_OnMouseLeftButtonDown;
            MouseMove += UIElement_OnMouseMove;
            MouseLeftButtonUp += UIElement_OnMouseLeftButtonUp;
        }

        #endregion

        #region Private Methods

        private void OnColorChanged(EventArgs e)
        {
            ColorChanged?.Invoke(this, e);
        }

        private void SetColor(Color color)
        {
            if (_color != color)
            {
                _color = color;

                this.OnColorChanged(EventArgs.Empty);
            }
        }

        private void UpdateSnapshot()
        {
            TargetPixelRect.Visibility = Visibility.Visible;
            InitImageContentControl.Content = null;

            CreateSnapshotBitmap();

            using var graphics = Graphics.FromImage(_bitmap);

            graphics.Clear(Color.Empty);

            var cursor = GetMousePosition();
            cursor.X -= _bitmap.Width / 2;
            cursor.Y -= _bitmap.Height / 2;

            graphics.CopyFromScreen(cursor, Point.Empty, _bitmap.Size);

            Color = _bitmap.GetPixel(_bitmap.Width / 2, _bitmap.Height / 2);

            SnapshotImage.Source = PortExtensions.ToWpfBitmap(_bitmap);
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

        public static Point GetMousePosition()
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

        #endregion
    }
}