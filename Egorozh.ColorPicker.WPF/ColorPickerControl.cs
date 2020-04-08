using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using Color = System.Windows.Media.Color;
using Control = System.Windows.Controls.Control;
using Cursor = System.Windows.Input.Cursor;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Egorozh.ColorPicker
{
    [TemplatePart(Name = PART_ColorGrid, Type = typeof(ColorGrid))]
    [TemplatePart(Name = PART_ColorEditor, Type = typeof(ColorEditor))]
    [TemplatePart(Name = PART_ScreenColorPicker, Type = typeof(ScreenColorPicker))]
    [TemplatePart(Name = PART_ColorWheel, Type = typeof(ColorWheel))]
    [TemplatePart(Name = PART_PreviewPanel, Type = typeof(Border))]
    [TemplatePart(Name = PART_LoadPaletteButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_SavePaletteButton, Type = typeof(Button))]
    public class ColorPickerControl : Control
    {
        #region Private Fields

        private const string PART_ColorGrid = "PART_ColorGrid";
        private const string PART_ColorEditor = "PART_ColorEditor";
        private const string PART_ScreenColorPicker = "PART_ScreenColorPicker";
        private const string PART_ColorWheel = "PART_ColorWheel";
        private const string PART_PreviewPanel = "PART_PreviewPanel";
        private const string PART_LoadPaletteButton = "PART_LoadPaletteButton";
        private const string PART_SavePaletteButton = "PART_SavePaletteButton";

        private ColorEditorManager _colorEditorManager;
        private Border _previewPanel;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty LoadPaletteIconProperty = DependencyProperty.Register(
            nameof(LoadPaletteIcon), typeof(FrameworkElement), typeof(ColorPickerControl),
            new PropertyMetadata(default(FrameworkElement)));

        public static readonly DependencyProperty SavePaletteIconProperty = DependencyProperty.Register(
            nameof(SavePaletteIcon), typeof(FrameworkElement), typeof(ColorPickerControl),
            new PropertyMetadata(default(FrameworkElement)));

        public static readonly DependencyProperty ToolBarButtonStyleProperty = DependencyProperty.Register(
            nameof(ToolBarButtonStyle), typeof(Style), typeof(ColorPickerControl),
            new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty DropperImageProperty = DependencyProperty.Register(
            nameof(DropperImage), typeof(FrameworkElement), typeof(ColorPickerControl),
            new PropertyMetadata(default(FrameworkElement)));

        public static readonly DependencyProperty EyedropperCursorProperty = DependencyProperty.Register(
            nameof(EyedropperCursor), typeof(Cursor), typeof(ColorPickerControl),
            new PropertyMetadata(default(Cursor)));

        public static readonly DependencyProperty TransparentBrushProperty = DependencyProperty.Register(
            nameof(TransparentBrush), typeof(Brush), typeof(ColorPickerControl),
            new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty NumericUpDownStyleProperty = DependencyProperty.Register(
            nameof(NumericUpDownStyle), typeof(Style), typeof(ColorPickerControl),
            new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty GetColorForPaletteActionProperty = DependencyProperty.Register(
            nameof(GetColorForPaletteAction), typeof(GetColorHandler), typeof(ColorPickerControl),
            new PropertyMetadata(new GetColorHandler(GetColorForPalette)));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(ColorPickerControl),
            new PropertyMetadata(default(Color), ColorChanged));

        private static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPickerControl akimColorPicker)
                akimColorPicker.ColorChanged();
        }

        public static readonly DependencyProperty ShowAlphaChannelProperty = DependencyProperty.Register(
            nameof(ShowAlphaChannel), typeof(bool), typeof(ColorPickerControl), new PropertyMetadata(true));

        #endregion

        #region Public Properties

        public FrameworkElement LoadPaletteIcon
        {
            get => (FrameworkElement) GetValue(LoadPaletteIconProperty);
            set => SetValue(LoadPaletteIconProperty, value);
        }

        public FrameworkElement SavePaletteIcon
        {
            get => (FrameworkElement) GetValue(SavePaletteIconProperty);
            set => SetValue(SavePaletteIconProperty, value);
        }

        public Style ToolBarButtonStyle
        {
            get => (Style) GetValue(ToolBarButtonStyleProperty);
            set => SetValue(ToolBarButtonStyleProperty, value);
        }

        public FrameworkElement DropperImage
        {
            get => (FrameworkElement) GetValue(DropperImageProperty);
            set => SetValue(DropperImageProperty, value);
        }

        public Cursor EyedropperCursor
        {
            get => (Cursor) GetValue(EyedropperCursorProperty);
            set => SetValue(EyedropperCursorProperty, value);
        }

        public Brush TransparentBrush
        {
            get => (Brush) GetValue(TransparentBrushProperty);
            set => SetValue(TransparentBrushProperty, value);
        }

        public Style NumericUpDownStyle
        {
            get => (Style) GetValue(NumericUpDownStyleProperty);
            set => SetValue(NumericUpDownStyleProperty, value);
        }

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public GetColorHandler GetColorForPaletteAction
        {
            get => (GetColorHandler) GetValue(GetColorForPaletteActionProperty);
            set => SetValue(GetColorForPaletteActionProperty, value);
        }

        public bool ShowAlphaChannel
        {
            get => (bool) GetValue(ShowAlphaChannelProperty);
            set => SetValue(ShowAlphaChannelProperty, value);
        }

        #endregion

        #region Static Constructor

        static ColorPickerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerControl),
                new FrameworkPropertyMetadata(typeof(ColorPickerControl)));
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _colorEditorManager = new ColorEditorManager();

            if (GetTemplateChild(PART_ColorGrid) is ColorGrid colorGrid)
            {
                colorGrid.EditingColor += ColorGrid_EditingColor;
                _colorEditorManager.ColorGrid = colorGrid;
            }

            if (GetTemplateChild(PART_ColorEditor) is ColorEditor colorEditor)
            {
                _colorEditorManager.ColorEditor = colorEditor;
            }

            if (GetTemplateChild(PART_ScreenColorPicker) is ScreenColorPicker colorPicker)
            {
                _colorEditorManager.ScreenColorPicker = colorPicker;
            }

            if (GetTemplateChild(PART_ColorWheel) is ColorWheel colorWheel)
            {
                _colorEditorManager.ColorWheel = colorWheel;
            }

            if (GetTemplateChild(PART_PreviewPanel) is Border panel)
            {
                _previewPanel = panel;
            }

            if (GetTemplateChild(PART_LoadPaletteButton) is Button loadButton)
            {
                loadButton.Click += LoadBtnClick;
            }

            if (GetTemplateChild(PART_SavePaletteButton) is Button saveButton)
            {
                saveButton.Click += SavePalleteBtnClick;
            }

            _colorEditorManager.ColorChanged += ColorEditorManager_ColorChanged;
        }

        #endregion

        #region Private Fields

        private void ColorChanged()
        {
            _colorEditorManager.ColorChanged -= ColorEditorManager_ColorChanged;

            _colorEditorManager.Color = Color.ToColor();
            UpdatePreviewBorder();

            _colorEditorManager.ColorChanged += ColorEditorManager_ColorChanged;
        }

        private void ColorEditorManager_ColorChanged(object sender, EventArgs e)
        {
            Color = _colorEditorManager.Color.ToColor();

            UpdatePreviewBorder();
        }

        private void UpdatePreviewBorder()
        {
            _previewPanel.Background = new SolidColorBrush(Color);
        }

        private void LoadBtnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = PaletteSerializer.DefaultOpenFilter,
                DefaultExt = "pal",
                Title = "Open Palette File"
            };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var serializer = PaletteSerializer.GetSerializer(dialog.FileName);
                    if (serializer != null)
                    {
                        if (!serializer.CanRead)
                        {
                            throw new InvalidOperationException("Serializer does not support reading palettes.");
                        }

                        ColorCollection palette;

                        using (var file = File.OpenRead(dialog.FileName))
                        {
                            palette = serializer.DeserializeNew(file);
                        }

                        if (palette != null)
                        {
                            // we can only display 96 colors in the color grid due to it's size, so if there's more, bin them
                            while (palette.Count > 96)
                            {
                                palette.RemoveAt(palette.Count - 1);
                            }

                            // or if we have less, fill in the blanks
                            while (palette.Count < 96)
                            {
                                palette.Add(Colors.White);
                            }

                            _colorEditorManager.ColorGrid.Colors = palette;
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Sorry, unable to open palette, the file format is not supported or is not recognized.",
                            "Load Palette", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Sorry, unable to open palette. {ex.GetBaseException().Message}",
                        "Load Palette", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SavePalleteBtnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = PaletteSerializer.DefaultSaveFilter,
                DefaultExt = "pal",
                Title = "Save Palette File As"
            };

            if (dialog.ShowDialog() == true)
            {
                var serializer = PaletteSerializer.AllSerializers.Where(s => s.CanWrite)
                    .ElementAt(dialog.FilterIndex - 1);
                if (serializer != null)
                {
                    if (!serializer.CanWrite)
                    {
                        throw new InvalidOperationException("Serializer does not support writing palettes.");
                    }

                    try
                    {
                        using var file = File.OpenWrite(dialog.FileName);
                        serializer.Serialize(file, _colorEditorManager.ColorGrid.Colors);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Sorry, unable to save palette. {ex.GetBaseException().Message}",
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

        private void ColorGrid_EditingColor(object sender, EditColorCancelEventArgsNew e)
        {
            e.Cancel = true;

            var color = e.Color;

            var res = GetColorForPaletteAction?.Invoke(ref color);

            if (res.HasValue && res.Value)
                _colorEditorManager.ColorGrid.Colors[e.ColorIndex] = color;
        }

        private static bool GetColorForPalette(ref Color color)
        {
            using var dialog = new ColorDialog
            {
                FullOpen = true,
                Color = color.ToColor()
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                color = dialog.Color.ToColor();
                return true;
            }

            return false;
        }

        #endregion
    }
}