using System.Windows;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    internal class ColorSliderNew : Slider
    {
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
    }
}