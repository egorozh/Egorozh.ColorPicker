using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace Egorozh.ColorPicker.Avalonia
{
    public class ColorPickerControl : UserControl, IStyleable
    {
        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPickerControl);

        #endregion

        #region Dependency Properties

        public static readonly AvaloniaProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerControl, Color>(nameof(Color));

        //public static readonly DependencyProperty GetColorForPaletteActionProperty = DependencyProperty.Register(
        //    nameof(GetColorForPaletteAction), typeof(GetColorHandler), typeof(ColorPickerDialog),
        //    new PropertyMetadata(new GetColorHandler(GetColorForPalette)));

        #endregion

        #region Public Properties

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        //public GetColorHandler GetColorForPaletteAction
        //{
        //    get => (GetColorHandler) GetValue(GetColorForPaletteActionProperty);
        //    set => SetValue(GetColorForPaletteActionProperty, value);
        //}

        #endregion
    }
}