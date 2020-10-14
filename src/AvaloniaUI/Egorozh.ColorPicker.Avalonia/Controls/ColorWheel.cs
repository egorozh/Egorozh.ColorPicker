using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using System;
using System.Runtime.InteropServices;
using Color = System.Drawing.Color;
using Point = Avalonia.Point;

namespace Egorozh.ColorPicker.Avalonia
{
    public class ColorWheel : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Methods

        private Ellipse _cursorEllipse;
        private Ellipse _spectrumEllipse;
        private bool _isDragging;
        private IColorManager _colorManager;

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorWheel);

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
            SetCursor(color);
        }

        public void Init(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _cursorEllipse = e.NameScope.Find<Ellipse>("PART_CursorEllipse");
            _spectrumEllipse = e.NameScope.Find<Ellipse>("PART_SpectrumEllipse");

            PointerPressed += ColorWheel_PointerPressed;
            PointerMoved += ColorWheel_PointerMoved;
            PointerReleased += ColorWheel_PointerReleased;

            SetCursor(_colorManager.CurrentColor);
        }

        #endregion

        #region Private Methods

        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = base.ArrangeOverride(finalSize);
            FillHsvSpectrum();
            return size;
        }

        private void FillHsvSpectrum()
        {
            int width = (int) _spectrumEllipse.Bounds.Width;
            int height = (int) _spectrumEllipse.Bounds.Height;

            var bitmap = new WriteableBitmap(new PixelSize(width, height),
                new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Premul);

            var bgraMinPixelData = ColorWheelHelpers.GetHsvData(width, height);

            using (var fb = bitmap.Lock())
                Marshal.Copy(bgraMinPixelData.ToArray(), 0, fb.Address, bgraMinPixelData.Count);

            _spectrumEllipse.Fill = new ImageBrush(bitmap);
        }

        private void SetCursor(Color color)
        {
            var location = GetColorLocation(color);

            Canvas.SetLeft(_cursorEllipse, location.X - _cursorEllipse.Width / 2);
            Canvas.SetTop(_cursorEllipse, location.Y - _cursorEllipse.Height / 2);
        }

        private void SetColor(Point mousePosition)
        {
            var centerPoint = new Point(Bounds.Width / 2, Bounds.Height / 2);

            var radius = (Bounds.Height - _cursorEllipse.Height) / 2;

            var dx = Math.Abs(mousePosition.X - centerPoint.X);
            var dy = Math.Abs(mousePosition.Y - centerPoint.Y);

            var angle = Math.Atan(dy / dx) / Math.PI * 180;
            var distance = Math.Pow(Math.Pow(dx, 2) + Math.Pow(dy, 2), 0.5);
            var saturation = distance / radius;

            if (mousePosition.X < centerPoint.X)
                angle = 180 - angle;

            if (mousePosition.Y > centerPoint.Y)
                angle = 360 - angle;

            _colorManager.SetColorFromHsl(angle, saturation, 0.5);
        }

        private Point GetColorLocation(in Color color)
        {
            var angle = color.GetHue() * Math.PI / 180;
            var radius = (Height - _cursorEllipse.Height) / 2 * color.GetSaturation();

            var centerPoint = new Point(Width / 2, Height / 2);

            var x = centerPoint.X + Math.Cos(angle) * radius;
            var y = centerPoint.Y - Math.Sin(angle) * radius;

            return new Point(x, y);
        }

        #region Pointer Events

        private void ColorWheel_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (!_isDragging)
            {
                _isDragging = true;

                SetColor(e.GetPosition(this));
                e.Pointer.Capture(this);
            }
        }

        private void ColorWheel_PointerMoved(object sender, PointerEventArgs e)
        {
            if (_isDragging)
                SetColor(e.GetPosition(this));
        }

        private void ColorWheel_PointerReleased(object sender, PointerEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;

                e.Pointer.Capture(null);
            }
        }

        #endregion

        #endregion
    }
}