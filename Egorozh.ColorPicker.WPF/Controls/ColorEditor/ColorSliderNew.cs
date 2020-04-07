using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
            new PropertyMetadata(default(ColorBarStyle)));

        public static readonly DependencyProperty CustomColorsProperty = DependencyProperty.Register(
            nameof(CustomColors), typeof(ColorCollectionNew), typeof(ColorSliderNew),
            new PropertyMetadata(default(ColorCollectionNew)));

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
            {
                _thumb.MouseEnter -= thumb_MouseEnter;
                _thumb.LostMouseCapture -= thumb_LostMouseCapture;
            }

            if (GetTemplateChild("PART_Track") is Track track)
            {
                _thumb = track.Thumb;

                if (_thumb != null)
                {
                    _thumb.MouseEnter += thumb_MouseEnter;
                    _thumb.LostMouseCapture += thumb_LostMouseCapture;
                }
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            _isDownOnSlider = true;
        }

        #endregion

        #region Private Methods

        private void thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _isDownOnSlider)
            {
                // the left button is pressed on mouse enter 
                // so the thumb must have been moved under the mouse 
                // in response to a click on the track. 
                // Generate a MouseLeftButtonDown event. 
                MouseButtonEventArgs args = new MouseButtonEventArgs(
                    e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
        }

        private void thumb_LostMouseCapture(object sender, EventArgs e)
        {
            _isDownOnSlider = false;
        }

        #endregion
    }
}