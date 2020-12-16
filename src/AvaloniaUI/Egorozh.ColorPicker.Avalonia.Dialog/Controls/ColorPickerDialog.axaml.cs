using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MessageBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Egorozh.ColorPicker.Dialog
{
    public class ColorPickerDialog : Window
    {
        #region Private Fields

        private ColorPickerControl? _colorPickerControl;

        #endregion

        #region Dependency Properties

        public static readonly AvaloniaProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerDialog, Color>(nameof(Color));

        public static readonly StyledProperty<IEnumerable<Color>> ColorsProperty =
            AvaloniaProperty.Register<ColorPickerDialog, IEnumerable<Color>>(
                nameof(Colors), ColorPalettes.PaintPalette.Select(c => c.ToColor()));

        #endregion

        #region Public Properties

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public IEnumerable<Color> Colors
        {
            get => GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        #endregion

        #region Constructor

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _colorPickerControl = this.Find<ColorPickerControl>("PartColorPickerControl");
            _colorPickerControl.LoadPaletteHandler = LoadPaletteAsync;
            _colorPickerControl.SavePaletteHandler = SavePaletteAsync;
            _colorPickerControl.GetColorHandler = GetColor;
        }

        private async Task<(bool, Color)> GetColor(Color color)
        {
            ColorPickerDialog colorPickerDialog = new ()
            {
                Color = color
            };

            var res = await colorPickerDialog.ShowDialog<bool>(this);

            return res ? (true, colorPickerDialog.Color) : (false, color);
        }

        private async Task<(bool, IEnumerable<Color>)> LoadPaletteAsync()
        {
            OpenFileDialog openFileDialog = new()
            {
                Title = "Open Palette File",
                AllowMultiple = false,
                Filters = CreateFilters(PaletteSerializer.DefaultOpenFilterForAvalonia)
            };

            var res = await openFileDialog.ShowAsync(this);

            if (res.Length > 0)
            {
                try
                {
                    var serializer = PaletteSerializer.GetSerializer(res[0]);

                    if (serializer != null)
                    {
                        if (!serializer.CanRead)
                            throw new InvalidOperationException("Serializer does not support reading palettes.");

                        List<System.Drawing.Color>? palette;

                        await using (var file = File.OpenRead(res[0]))
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
                    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow("Load Palette",
                            $"Sorry, unable to open palette. {ex.GetBaseException().Message}",
                            ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error);
                    messageBoxStandardWindow.Show();
                }
            }

            return (false, new Color[0]);
        }

        private async void SavePaletteAsync(IEnumerable<Color> colors)
        {
            SaveFileDialog dialog = new()
            {
                Filters = CreateFilters(PaletteSerializer.DefaultSaveFilterForAvalonia),
                Title = "Save Palette File As"
            };

            var res = await dialog.ShowAsync(this);

            if (res != null)
            {
                var fileExt = new FileInfo(res).Extension.Substring(1);

                var serializer = PaletteSerializer.AllSerializers.Where(s => s.CanWrite)
                    .FirstOrDefault(s => s.DefaultExtensions.Contains(fileExt));

                if (serializer != null)
                {
                    try
                    {
                        await using var file = File.OpenWrite(res);
                        serializer.Serialize(file, (colors.Select(c => c.ToColor())));
                    }
                    catch (Exception ex)
                    {
                        var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                            .GetMessageBoxStandardWindow("Save Palette",
                                $"Sorry, unable to save palette. {ex.GetBaseException().Message}",
                                ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Error);
                        messageBoxStandardWindow.Show();
                    }
                }
                else
                {
                    var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow("Save Palette",
                            "Sorry, unable to save palette, the file format is not supported or is not recognized.",
                            ButtonEnum.Ok, MessageBox.Avalonia.Enums.Icon.Warning);
                    messageBoxStandardWindow.Show();
                }
            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            Close(true);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e) => Close(false);

        private static List<FileDialogFilter> CreateFilters(List<(string, List<string>)> initFilters)
        {
            List<FileDialogFilter> filters = new();

            foreach (var (name, extensions) in initFilters)
            {
                filters.Add(new FileDialogFilter
                {
                    Extensions = extensions,
                    Name = name
                });
            }

            return filters;
        }

        #endregion
    }
}