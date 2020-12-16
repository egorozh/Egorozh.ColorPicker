using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    internal class ColorSliderOld : Slider
    {
        #region Private Fields

        private Thumb _thumb = null;
        private bool _isDownOnSlider = false;

        #endregion  

        #region Dependency Properties

        public static readonly DependencyProperty BarStyleProperty = DependencyProperty.Register(
            nameof(BarStyle), typeof(ColorBarStyle), typeof(ColorSlider),
            new PropertyMetadata(default(ColorBarStyle), BarStyleChanged));

        private static void BarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSlider slider)
                slider.PaintBar();
        }

        public static readonly DependencyProperty CustomColorsProperty = DependencyProperty.Register(
            nameof(CustomColors), typeof(ColorCollection), typeof(ColorSlider),
            new PropertyMetadata(default(ColorCollection), CustomColorsChanged));

        private static void CustomColorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSlider slider)
                slider.PaintBar();
        }

        public static readonly DependencyProperty BarBrushProperty = DependencyProperty.Register(
            nameof(BarBrush), typeof(Brush), typeof(ColorSlider), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty Color1Property = DependencyProperty.Register(
            nameof(Color1), typeof(Color), typeof(ColorSlider), new PropertyMetadata(default(Color), Color1Changed));

        private static void Color1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSlider slider)
                slider.PaintBar();
        }

        public static readonly DependencyProperty Color2Property = DependencyProperty.Register(
            nameof(Color2), typeof(Color), typeof(ColorSlider), new PropertyMetadata(default(Color), Color2Changed));

        private static void Color2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSlider slider)
                slider.PaintBar();
        }

        public static readonly DependencyProperty Color3Property = DependencyProperty.Register(
            nameof(Color3), typeof(Color), typeof(ColorSlider), new PropertyMetadata(default(Color), Color3Changed));

        private static void Color3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSlider slider)
                slider.PaintBar();
        }

        #endregion

        #region Public Properties

        public Color Color1
        {
            get => (Color) GetValue(Color1Property);
            set => SetValue(Color1Property, value);
        }

        public Color Color2
        {
            get => (Color) GetValue(Color2Property);
            set => SetValue(Color2Property, value);
        }

        public Color Color3
        {
            get => (Color) GetValue(Color3Property);
            set => SetValue(Color3Property, value);
        }

        public ColorBarStyle BarStyle
        {
            get => (ColorBarStyle) GetValue(BarStyleProperty);
            set => SetValue(BarStyleProperty, value);
        }

        public ColorCollection CustomColors
        {
            get => (ColorCollection) GetValue(CustomColorsProperty);
            set => SetValue(CustomColorsProperty, value);
        }

        public Brush BarBrush
        {
            get => (Brush) GetValue(BarBrushProperty);
            set => SetValue(BarBrushProperty, value);
        }

        #endregion

        #region Static Constructor

        static ColorSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSlider),
                new FrameworkPropertyMetadata(typeof(ColorSlider)));
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_thumb != null)
                _thumb.LostMouseCapture -= thumb_LostMouseCapture;

            if (GetTemplateChild("PART_Track") is Track track)
            {
                _thumb = track.Thumb;

                if (_thumb != null)
                    _thumb.LostMouseCapture += thumb_LostMouseCapture;
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            _isDownOnSlider = true;
            Mouse.Capture(_thumb);
            _thumb.PreviewMouseMove += _thumb_PreviewMouseMove;
        }

        private void _thumb_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDownOnSlider && !_thumb.IsDragging && e.LeftButton == MouseButtonState.Pressed)
            {
                var args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left)
                {
                    RoutedEvent = MouseLeftButtonDownEvent
                };

                Mouse.Capture(null);
                _thumb.PreviewMouseMove -= _thumb_PreviewMouseMove;
                _isDownOnSlider = false;

                _thumb.RaiseEvent(args);
            }
        }

        #endregion

        #region Private Methods

        private void thumb_LostMouseCapture(object sender, EventArgs e)
        {
            _isDownOnSlider = false;
        }

        private void PaintBar()
        {
            double angle = Orientation == Orientation.Horizontal ? 0 : 90;

            Brush brush;

            switch (BarStyle)
            {
                case ColorBarStyle.TwoColor:

                    brush = new LinearGradientBrush(Color1, Color2, angle);

                    break;
                case ColorBarStyle.ThreeColor:

                    var grStops = new[]
                    {
                        new GradientStop(Color1, 0),
                        new GradientStop(Color2, 0.5),
                        new GradientStop(Color3, 1),
                    };

                    brush = new LinearGradientBrush(new GradientStopCollection(grStops), angle);

                    break;
                case ColorBarStyle.Custom:

                    var custom = CustomColors;
                    var count = custom?.Count ?? 0;

                    if (custom != null && count > 0)
                    {
                        var grs = custom.Select((color, i) =>
                            new GradientStop(color, i == 0 ? 0 : i == count - 1 ? 1 : 1.0D / count * i));

                        brush = new LinearGradientBrush(new GradientStopCollection(grs), angle);
                    }
                    else
                        brush = Brushes.Transparent;

                    break;
                default:
                    brush = new LinearGradientBrush();
                    break;
            }

            BarBrush = brush;
        }

        #endregion
    }
}