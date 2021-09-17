using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public abstract class ColorSlider : Slider, IColorClient
    {
        //private Thumb _thumb;

        #region Protected Properties

        protected bool UpdateBackgroundWhenColorUpdated = true;
        //private bool _isDownOnSlider;

        protected IColorManager? ColorManager { get; private set; }

        #endregion

        #region Static Constructor

        static ColorSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorSlider),
                new FrameworkPropertyMetadata(typeof(ColorSlider)));
        }

        #endregion

        #region Constructor

        protected ColorSlider()
        {
            TickFrequency = 1;

            ValueChanged += ColorSlider_ValueChanged;
        }

        #endregion

        #region Public Methods

        public virtual void ColorUpdated(Color color, IColorClient client)
        {
            ValueChanged -= ColorSlider_ValueChanged;

            UpdateColor(color);

            if (UpdateBackgroundWhenColorUpdated)
                Background = CreateBackgroundBrush(color);

            ValueChanged += ColorSlider_ValueChanged;
        }

        public void Init(IColorManager colorManager)
        {
            ColorManager = colorManager;
        }

        #endregion

        #region Protected Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            if (ColorManager != null)
            {
                ColorUpdated(ColorManager.CurrentColor, this);
                Background = CreateBackgroundBrush(ColorManager.CurrentColor);
            }

            //if (_thumb != null)
            //    _thumb.LostMouseCapture -= thumb_LostMouseCapture;

            //if (GetTemplateChild("PART_Track") is Track track)
            //{
            //    _thumb = track.Thumb;

            //    if (_thumb != null)
            //        _thumb.LostMouseCapture += thumb_LostMouseCapture;
            //}
        }
        
        protected virtual void OnValueChanged()
        {
        }

        protected virtual void UpdateColor(in Color color)
        {
        }

        protected virtual List<Color> CreateBackgroundColors(in Color color) =>
            new()
            {
                Color.Transparent
            };

        protected virtual Brush CreateBackgroundBrush(in Color color)
        {
            var colors = CreateBackgroundColors(color);

            Brush brush;

            var count = colors.Count;

            if (count > 1)
            {
                double angle = Orientation == Orientation.Horizontal ? 0 : 90;

                GradientStopCollection gradStops = new()
                {
                    new(colors[0].ToColor(), 0)
                };

                for (var i = 1; i < count - 1; i++)
                {
                    var offset = 1.0D / count * i;

                    gradStops.Add(new(colors[i].ToColor(), offset));
                }

                gradStops.Add(new(colors[colors.Count-1].ToColor(), 1));

                var start = Orientation == Orientation.Vertical
                    ? new Point(0, 1)
                    : new Point(0, 0);

                var end = Orientation == Orientation.Vertical
                    ? new Point(0, 0)
                    : new Point(1, 0);

                brush = new LinearGradientBrush
                {
                    StartPoint = start,
                    EndPoint = end,
                    GradientStops = gradStops
                };
            }
            else if (count == 1)
                brush = new SolidColorBrush(colors[0].ToColor());
            else
                brush = Brushes.Transparent;

            return brush;
        }

        #endregion

        #region Private Methods

        private void ColorSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OnValueChanged();
        }

        //protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    base.OnPreviewMouseLeftButtonDown(e);

        //    _isDownOnSlider = true;
        //    Mouse.Capture(_thumb);
        //    _thumb.PreviewMouseMove += _thumb_PreviewMouseMove;
        //}

        //private void _thumb_PreviewMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (_isDownOnSlider && !_thumb.IsDragging && e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        var args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left)
        //        {
        //            RoutedEvent = MouseLeftButtonDownEvent
        //        };

        //        Mouse.Capture(null);
        //        _thumb.PreviewMouseMove -= _thumb_PreviewMouseMove;
        //        _isDownOnSlider = false;

        //        _thumb.RaiseEvent(args);
        //    }
        //}

        //private void thumb_LostMouseCapture(object sender, EventArgs e)
        //{
        //    _isDownOnSlider = false;
        //}

        #endregion
    }
}