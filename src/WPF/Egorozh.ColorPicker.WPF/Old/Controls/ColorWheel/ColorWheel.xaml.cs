using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public partial class ColorWheelOld 
    {   
        /*
        #region Private Fields

        private Color _color;
        private HslColor _hslColor;
        private bool _lockUpdates;
        private bool _isDragging;

        #endregion

        #region Public Properties

        public HslColor HslColor
        {
            get => _hslColor;
            set => SetHslColor(value);
        }

        public Color Color
        {
            get => _color;
            set => SetColor(value);
        }

        #endregion

        #region Events

        public event EventHandler ColorChanged;

        #endregion

        #region Constructor

        public ColorWheel()
        {
            InitializeComponent();

            _color = Color.Black;
            _hslColor = new HslColor(_color);

            MouseLeftButtonDown += ColorWheelPort_MouseLeftButtonDown;
            MouseMove += ColorWheelPort_MouseMove;
            MouseLeftButtonUp += ColorWheelPort_MouseLeftButtonUp;

            SizeChanged += (s, e) => UpdateCursor();
        }

        #endregion

        #region Private Methods

        private Point GetColorLocation(HslColor color)
        {
            var angle = color.H * Math.PI / 180;
            var radius = (ActualHeight - CursorEllipse.Height) / 2 * color.S;

            return GetColorLocation(angle, radius);
        }

        private Point GetColorLocation(double angleR, double radius)
        {
            var centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);

            var x = centerPoint.X + Math.Cos(angleR) * radius;
            var y = centerPoint.Y - Math.Sin(angleR) * radius;

            return new Point(x, y);
        }

        private void SetColor(Point mousePosition)
        {
            var centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);
         
            var radius = (ActualHeight - CursorEllipse.Height) / 2;

            var dx = Math.Abs(mousePosition.X - centerPoint.X);
            var dy = Math.Abs(mousePosition.Y - centerPoint.Y);

            var angle = Math.Atan(dy / dx) / Math.PI * 180;
            var distance = Math.Pow(Math.Pow(dx, 2) + Math.Pow(dy, 2), 0.5);
            var saturation = distance / radius;

            if (mousePosition.X < centerPoint.X)
            {
                angle = 180 - angle;
            }

            if (mousePosition.Y > centerPoint.Y)
            {
                angle = 360 - angle;
            }

            var newColor = new HslColor(angle, saturation, 0.5);

            if (_hslColor != newColor)
            {
                _lockUpdates = true;

                HslColor = newColor;
                Color = _hslColor.ToRgbColor();

                _lockUpdates = false;
            }
        }

        private void UpdateCursor()
        {
            var location = this.GetColorLocation(_hslColor);

            Canvas.SetLeft(CursorEllipse, location.X - CursorEllipse.ActualWidth / 2);
            Canvas.SetTop(CursorEllipse, location.Y - CursorEllipse.ActualHeight / 2);
        }

        private void SetHslColor(HslColor value)
        {
            if (_hslColor != value)
            {
                _hslColor = value;

                if (!_lockUpdates)
                    Color = _hslColor.ToRgbColor();
            }
        }

        private void SetColor(Color value)
        {
            if (_color != value)
            {
                _color = value;

                if (!_lockUpdates)
                    HslColor = new HslColor(_color);

                UpdateCursor();

                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Mouse Events

        private void ColorWheelPort_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isDragging)
            {
                _isDragging = true;
                SetColor(e.GetPosition(this));
                Mouse.Capture(this);
            }
        }

        private void ColorWheelPort_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
                SetColor(e.GetPosition(this));
        }

        private void ColorWheelPort_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                Mouse.Capture(null);
            }
        }

        #endregion
        */
    }
}