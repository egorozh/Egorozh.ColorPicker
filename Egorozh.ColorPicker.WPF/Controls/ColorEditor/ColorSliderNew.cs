using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Egorozh.ColorPicker
{
    internal class ColorSliderNew : Slider
    {
        #region Private Fields

        private Thumb _thumb = null;
        private bool _isDownOnSlider = false;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty BarStyleProperty = DependencyProperty.Register(
            nameof(BarStyle), typeof(ColorBarStyle), typeof(ColorSliderNew),
            new PropertyMetadata(default(ColorBarStyle), BarStyleChanged));

        private static void BarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderNew slider)
                slider.PaintBar();
        }

        public static readonly DependencyProperty CustomColorsProperty = DependencyProperty.Register(
            nameof(CustomColors), typeof(ColorCollectionNew), typeof(ColorSliderNew),
            new PropertyMetadata(default(ColorCollectionNew), CustomColorsChanged));

        private static void CustomColorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorSliderNew slider)
                slider.PaintBar();
        }

        public static readonly DependencyProperty BarBrushProperty = DependencyProperty.Register(
            nameof(BarBrush), typeof(Brush), typeof(ColorSliderNew), new PropertyMetadata(default(Brush)));

        #endregion

        #region Public Properties

        public ColorBarStyle BarStyle
        {
            get => (ColorBarStyle) GetValue(BarStyleProperty);
            set => SetValue(BarStyleProperty, value);
        }

        public ColorCollectionNew CustomColors
        {
            get => (ColorCollectionNew) GetValue(CustomColorsProperty);
            set => SetValue(CustomColorsProperty, value);
        }

        public Brush BarBrush
        {
            get => (Brush) GetValue(BarBrushProperty);
            set => SetValue(BarBrushProperty, value);
        }

        #endregion

        #region Static Constructor

        static ColorSliderNew()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSliderNew),
                new FrameworkPropertyMetadata(typeof(ColorSliderNew)));
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

            var brush = new LinearGradientBrush();
            
            switch (BarStyle)
            {
                case ColorBarStyle.TwoColor:
                    break;
                case ColorBarStyle.ThreeColor:
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
                    {
                        //blend.Colors = new[]
                        //{
                        //                    this.Color1,
                        //                    this.Color2
                        //                };
                        //blend.Positions = new[]
                        //{
                        //                    0F,
                        //                    1F
                        //                };
                    }

                    break;
            }
            
            BarBrush = brush;

            //float angle;

            //angle = this.Orientation == Orientation.Horizontal ? 0 : 90;

            //if (this.BarBounds.Height > 0 && this.BarBounds.Width > 0)
            //{
            //    ColorBlend blend;

            //    // HACK: Inflating the brush rectangle by 1 seems to get rid of a odd issue where the last color is drawn on the first pixel

            //    blend = new ColorBlend();
            //    using (LinearGradientBrush brush = new LinearGradientBrush(Rectangle.Inflate(this.BarBounds, 1, 1),
            //        Color.Empty, Color.Empty, angle, false))
            //    {
            //        switch (this.BarStyle)
            //        {
            //            case ColorBarStyle.TwoColor:
            //                blend.Colors = new[]
            //                {
            //                    this.Color1,
            //                    this.Color2
            //                };
            //                blend.Positions = new[]
            //                {
            //                    0F,
            //                    1F
            //                };
            //                break;
            //            case ColorBarStyle.ThreeColor:
            //                blend.Colors = new[]
            //                {
            //                    this.Color1,
            //                    this.Color2,
            //                    this.Color3
            //                };
            //                blend.Positions = new[]
            //                {
            //                    0,
            //                    0.5F,
            //                    1
            //                };
            //                break;
            //    }

            //        brush.InterpolationColors = blend;
            //        e.Graphics.FillRectangle(brush, this.BarBounds);
            //    }
            //}
        }

        #endregion
    }
}