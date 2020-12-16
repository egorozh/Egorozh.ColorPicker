using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace Egorozh.ColorPicker
{
    [TemplatePart(Name = PART_ColorWheel, Type = typeof(ColorWheel))]
    [TemplatePart(Name = PART_ColorPreview, Type = typeof(ColorPreview))]
    [TemplatePart(Name = PART_AlphaSlider, Type = typeof(RgbaColorSlider))]
    [TemplatePart(Name = PART_ValueSlider, Type = typeof(ValueColorSlider))]
    [TemplatePart(Name = PART_ColorEditor, Type = typeof(ColorEditor))]
    [TemplatePart(Name = PART_ColorPalette, Type = typeof(ColorPalette))]
    public class ColorPickerControl : Control
    {
        #region Private Fields

        private const string PART_ColorWheel = "PART_ColorWheel";
        private const string PART_ColorPreview = "PART_ColorPreview";
        private const string PART_AlphaSlider = "PART_AlphaSlider";
        private const string PART_ValueSlider = "PART_ValueSlider";
        private const string PART_ColorEditor = "PART_ColorEditor";
        private const string PART_ColorPalette = "PART_ColorPalette";

        private readonly IColorManager _manager = new ColorManager();

        #endregion

        #region Static Constructor

        static ColorPickerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl),
                new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty LoadPaletteHandlerProperty = DependencyProperty.Register(
            nameof(LoadPaletteHandler), typeof(LoadPaletteHandlerAsync), typeof(ColorPickerControl),
            new PropertyMetadata(new LoadPaletteHandlerAsync(LoadPaletteAsync)));

        public static readonly DependencyProperty SavePaletteHandlerProperty = DependencyProperty.Register(
            nameof(SavePaletteHandler), typeof(SavePaletteHandler), typeof(ColorPickerControl),
            new PropertyMetadata(new SavePaletteHandler(SavePaletteAsync)));

        public static readonly DependencyProperty GetColorHandlerProperty = DependencyProperty.Register(
            nameof(GetColorHandler), typeof(GetColorHandler), typeof(ColorPickerControl),
            new PropertyMetadata(default(GetColorHandler)));

        public static readonly DependencyProperty ColorsProperty = DependencyProperty.Register(
            nameof(Colors), typeof(IEnumerable<Color>), typeof(ColorPickerControl),
            new PropertyMetadata(default(IEnumerable<Color>)));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(ColorPickerControl),
            new PropertyMetadata(default(Color), ColorChanged));

        private static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPickerControl colorPickerControl)
                colorPickerControl.ColorChanged(colorPickerControl.Color);
        }

        #endregion

        #region Public Properties

        public LoadPaletteHandlerAsync? LoadPaletteHandler
        {
            get => (LoadPaletteHandlerAsync?) GetValue(LoadPaletteHandlerProperty);
            set => SetValue(LoadPaletteHandlerProperty, value);
        }

        public SavePaletteHandler? SavePaletteHandler
        {
            get => (SavePaletteHandler?) GetValue(SavePaletteHandlerProperty);
            set => SetValue(SavePaletteHandlerProperty, value);
        }

        public GetColorHandler? GetColorHandler
        {
            get => (GetColorHandler?) GetValue(GetColorHandlerProperty);
            set => SetValue(GetColorHandlerProperty, value);
        }

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public IEnumerable<Color> Colors
        {
            get => (IEnumerable<Color>) GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _manager.ColorChanged += Manager_ColorChanged;

            var colorWheel = GetTemplateChild(PART_ColorWheel) as ColorWheel;
            var colorPreview = GetTemplateChild(PART_ColorPreview) as ColorPreview;
            var alphaSlider = GetTemplateChild(PART_AlphaSlider) as RgbaColorSlider;
            var valuesSlider = GetTemplateChild(PART_ValueSlider) as ValueColorSlider;
            var colorEditor = GetTemplateChild(PART_ColorEditor) as ColorEditor;
            var colorPalette = GetTemplateChild(PART_ColorPalette) as ColorPalette;

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

        private static async Task<(bool, IEnumerable<Color>)> LoadPaletteAsync()
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Open Palette File",
                Filter = PaletteSerializer.DefaultOpenFilterForWpf,
                Multiselect = false,
            };

            var res = openFileDialog.ShowDialog();

            if (res.HasValue && res.Value)
            {
                try
                {
                    var fileName = openFileDialog.FileName;

                    var serializer = PaletteSerializer.GetSerializer(fileName);

                    if (serializer != null)
                    {
                        if (!serializer.CanRead)
                            throw new InvalidOperationException("Serializer does not support reading palettes.");

                        List<System.Drawing.Color>? palette;

                        await using (var file = File.OpenRead(fileName))
                        {
                            palette = serializer.DeserializeNew(file);
                        }

                        if (palette != null)
                        {
                            return (true, palette.Select(c => c.ToColor()));
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Sorry, unable to open palette. {ex.GetBaseException().Message}",
                        "Load Palette",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }

            return (false, new Color[0]);
        }

        private static async void SavePaletteAsync(IEnumerable<Color> colors)
        {
            SaveFileDialog dialog = new()
            {
                Filter = PaletteSerializer.DefaultSaveFilterForWpf,
                Title = "Save Palette File As",
            };

            var res = dialog.ShowDialog();

            if (res.HasValue && res.Value)
            {
                var fileName = dialog.FileName;

                var fileExt = new FileInfo(fileName).Extension.Substring(1);

                var serializer = PaletteSerializer.AllSerializers.Where(s => s.CanWrite)
                    .FirstOrDefault(s => s.DefaultExtensions.Contains(fileExt));

                if (serializer != null)
                {
                    try
                    {
                        await using var file = File.OpenWrite(fileName);
                        serializer.Serialize(file, (colors.Select(c => c.ToColor())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sorry, unable to save palette. {ex.GetBaseException().Message}",
                            "Save Palette", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Sorry, unable to save palette, the file format is not supported or is not recognized.",
                        "Save Palette", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        #endregion
    }

    public delegate Task<(bool, IEnumerable<Color>)> LoadPaletteHandlerAsync();

    public delegate void SavePaletteHandler(IEnumerable<Color> colors);

    public delegate Task<(bool, Color)> GetColorHandler(Color oldColor);
}