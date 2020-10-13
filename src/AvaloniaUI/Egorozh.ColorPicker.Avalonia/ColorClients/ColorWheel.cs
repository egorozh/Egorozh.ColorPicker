using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Color = System.Drawing.Color;
using Point = Avalonia.Point;

namespace Egorozh.ColorPicker.Avalonia
{
    public class ColorWheel : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Methods

        private Ellipse _cursorEllipse;
        private Canvas _hsvCanvas;
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
            _hsvCanvas = e.NameScope.Find<Canvas>("PART_HsvCanvas");

            PointerPressed += ColorWheel_PointerPressed;
            PointerMoved += ColorWheel_PointerMoved;
            PointerReleased += ColorWheel_PointerReleased;

            SetCursor(_colorManager.CurrentColor);
            FillHsvCanvas();
        }

        #endregion

        #region Private Methods

        private void FillHsvCanvas()
        {
            for (var i = 0; i < 360; i++)
            {
                var brush = new LinearGradientBrush
                {
                    StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                    EndPoint = new RelativePoint(1, 0, RelativeUnit.Relative),
                };

                brush.GradientStops.Add(new GradientStop
                {
                    Color = Colors.White,
                });
                brush.GradientStops.Add(new GradientStop
                {
                    Color = ConvertHsv2Rgb(i, 1, 1).ToColor(),
                    Offset = 1
                });

                var line = new Line
                {
                    StartPoint = new Point(_hsvCanvas.Width / 2, _hsvCanvas.Height / 2),
                    EndPoint = new Point(_hsvCanvas.Width, _hsvCanvas.Height / 2),
                    StrokeThickness = 2,
                    RenderTransform = new RotateTransform(-i),
                    RenderTransformOrigin =
                        new RelativePoint(_hsvCanvas.Width / 2, _hsvCanvas.Height / 2, RelativeUnit.Absolute),
                    Stroke = brush
                };

                _hsvCanvas.Children.Add(line);
            }
        }

        private static Color ConvertHsv2Rgb(float h, float s, float v)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            h /= 60;
            int i = (int) Math.Floor(h);
            float f = h - i;
            float p = v * (1 - s);
            float q = v * (1 - s * f);
            float t = v * (1 - s * (1 - f));

            switch (i)
            {
                case 0:
                    r = (byte) (255 * v);
                    g = (byte) (255 * t);
                    b = (byte) (255 * p);
                    break;
                case 1:
                    r = (byte) (255 * q);
                    g = (byte) (255 * v);
                    b = (byte) (255 * p);
                    break;
                case 2:
                    r = (byte) (255 * p);
                    g = (byte) (255 * v);
                    b = (byte) (255 * t);
                    break;
                case 3:
                    r = (byte) (255 * p);
                    g = (byte) (255 * q);
                    b = (byte) (255 * v);
                    break;
                case 4:
                    r = (byte) (255 * t);
                    g = (byte) (255 * p);
                    b = (byte) (255 * v);
                    break;
                default:
                    r = (byte) (255 * v);
                    g = (byte) (255 * p);
                    b = (byte) (255 * q);
                    break;
            }

            return Color.FromArgb(255, r, g, b);
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