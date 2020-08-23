using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace Egorozh.ColorPicker.Avalonia
{
    public class ColorPickerControl : UserControl, IStyleable
    {
        #region Private Fields

        private readonly ColorManager _manager = new ColorManager();

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPickerControl);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerControl, Color>(nameof(Color), notifying: ColorChanged);

        private static void ColorChanged(IAvaloniaObject obj, bool isAfter)
        {
            if (obj is ColorPickerControl colorPickerControl)
                colorPickerControl.ColorChanged(colorPickerControl.Color);
        }

        #endregion

        #region Public Properties

        public Color Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion
        
        #region Private Methods

        private void ColorChanged(Color color)
        {
            _manager.CurrentColor = color.ToColor();
        }

        #endregion
    }
}