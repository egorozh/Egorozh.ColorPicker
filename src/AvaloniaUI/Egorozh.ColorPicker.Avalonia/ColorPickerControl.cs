using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using System;

namespace Egorozh.ColorPicker
{
    public class ColorPickerControl : TemplatedControl, IStyleable
    {
        #region Private Fields

        private readonly IColorManager _manager = new ColorManager();

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPickerControl);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<LoadPaletteHandlerAsync?> LoadPaletteHandlerProperty =
            AvaloniaProperty.Register<ColorPickerControl, LoadPaletteHandlerAsync?>(nameof(LoadPaletteHandler));

        public static readonly StyledProperty<SavePaletteHandler?> SavePaletteHandlerProperty =
            AvaloniaProperty.Register<ColorPickerControl, SavePaletteHandler?>(nameof(SavePaletteHandler));

        public static readonly StyledProperty<GetColorHandler?> GetColorHandlerProperty =
            AvaloniaProperty.Register<ColorPickerControl, GetColorHandler?>(nameof(GetColorHandler));

        public static readonly StyledProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerControl, Color>(nameof(Color), notifying: ColorChanged);

        private static void ColorChanged(IAvaloniaObject obj, bool isAfter)
        {
            if (obj is ColorPickerControl colorPickerControl)
                colorPickerControl.ColorChanged(colorPickerControl.Color);
        }

        #endregion

        #region Public Properties

        public LoadPaletteHandlerAsync? LoadPaletteHandler
        {
            get => GetValue(LoadPaletteHandlerProperty);
            set => SetValue(LoadPaletteHandlerProperty, value);
        }

        public SavePaletteHandler? SavePaletteHandler
        {
            get => GetValue(SavePaletteHandlerProperty);
            set => SetValue(SavePaletteHandlerProperty, value);
        }

        public GetColorHandler? GetColorHandler
        {
            get => GetValue(GetColorHandlerProperty);
            set => SetValue(GetColorHandlerProperty, value);
        }

        public Color Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _manager.ColorChanged += Manager_ColorChanged;

            var colorWheel = e.NameScope.Find<ColorWheel>("PART_ColorWheel");
            var colorPreview = e.NameScope.Find<ColorPreview>("PART_ColorPreview");
            var alphaSlider = e.NameScope.Find<RgbaColorSlider>("PART_AlphaSlider");
            var valuesSlider = e.NameScope.Find<ValueColorSlider>("PART_ValueSlider");
            var colorEditor = e.NameScope.Find<ColorEditor>("PART_ColorEditor");
            var colorPalette = e.NameScope.Find<ColorPalette>("PART_ColorPalette");

            _manager.AddClient(colorWheel, colorPreview, alphaSlider,
                valuesSlider, colorEditor, colorPalette);
        }

        #endregion

        #region Private Methods

        private void Manager_ColorChanged(System.Drawing.Color color)
        {
            _manager.ColorChanged -= Manager_ColorChanged;

            Color = color.ToColor();

            _manager.ColorChanged += Manager_ColorChanged;
        }

        private void ColorChanged(Color color)
        {
            _manager.CurrentColor = color.ToColor();
        }

        #endregion
    }
}