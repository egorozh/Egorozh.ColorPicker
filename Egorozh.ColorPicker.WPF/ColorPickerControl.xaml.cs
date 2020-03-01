using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Navigation;
using Color = System.Windows.Media.Color;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Egorozh.ColorPicker
{
    public partial class ColorPickerControl
    {
        #region Private Fields

        private ColorEditorManager _colorEditorManager;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty NumericUpDownStyleProperty = DependencyProperty.Register(
            nameof(NumericUpDownStyle), typeof(Style), typeof(ColorPickerControl),
            new PropertyMetadata(default(Style)));

        public Style NumericUpDownStyle
        {
            get => (Style) GetValue(NumericUpDownStyleProperty);
            set => SetValue(NumericUpDownStyleProperty, value);
        }

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

        public bool ShowAlphaChannel
        {
            get => (bool) GetValue(ShowAlphaChannelProperty);
            set => SetValue(ShowAlphaChannelProperty, value);
        }

        #endregion

        #region Public Properties

        public static System.Windows.Input.Cursor EyedropperCursor;

        public static DrawingGroup TransparentTile;

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static Uri BaseUri { get; private set; }

        public GetColorHandler GetColorForPaletteAction
        {
            get => (GetColorHandler) GetValue(GetColorForPaletteActionProperty);
            set => SetValue(GetColorForPaletteActionProperty, value);
        }

        #endregion

        #region Constructor

        public ColorPickerControl()
        {
            InitializeComponent();

            NumericUpDownStyle = (Style) this.FindResource("NumericUpDownStyle");

            BaseUri = BaseUriHelper.GetBaseUri(this);

            TransparentTile = (DrawingGroup) this.FindResource("TransparencyTile");
            EyedropperCursor = ((System.Windows.Input.Cursor) this.FindResource("EyedropperCursor"));

            InitWinFormsComponents();
        }

        #endregion

        #region Private Fields

        private void InitWinFormsComponents()
        {
            ScreenColorPicker.Color = System.Drawing.Color.Black;

            ScreenColorPicker.Zoom = 6;

            ColorEditor.Color = System.Drawing.Color.FromArgb(0, 0, 0);

            ColorGrid.AutoAddColors = false;
            ColorGrid.Size = new System.Drawing.Size(192, 92);
            ColorGrid.CellBorderStyle = ColorCellBorderStyle.None;
            ColorGrid.EditMode = ColorEditingMode.Both;
            ColorGrid.Palette = ColorPalette.Paint;
            ColorGrid.SelectedCellStyle = ColorGridSelectedCellStyle.Standard;
            ColorGrid.ShowCustomColors = false;

            ColorGrid.EditingColor += ColorGrid_EditingColor;

            _colorEditorManager = new ColorEditorManager
            {
                ColorEditor = ColorEditor,
                ColorGrid = ColorGrid,
                ColorWheel = ColorWheel,

                ScreenColorPicker = ScreenColorPicker
            };

            _colorEditorManager.ColorChanged += ColorEditorManager_ColorChanged;
        }

        private void ColorChanged()
        {
            _colorEditorManager.ColorChanged -= ColorEditorManager_ColorChanged;

            _colorEditorManager.Color = Color.ToColor();
            UpdatePreviewBorder();

            _colorEditorManager.ColorChanged += ColorEditorManager_ColorChanged;
        }

        private void ColorEditorManager_ColorChanged(object sender, System.EventArgs e)
        {
            Color = _colorEditorManager.Color.ToColor();

            UpdatePreviewBorder();
        }

        private void UpdatePreviewBorder()
        {
            PreviewPanel.Background = new SolidColorBrush(Color);
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
                        ColorCollection palette;

                        if (!serializer.CanRead)
                        {
                            throw new InvalidOperationException("Serializer does not support reading palettes.");
                        }

                        using (FileStream file = File.OpenRead(dialog.FileName))
                        {
                            palette = serializer.Deserialize(file);
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
                                palette.Add(System.Drawing.Color.White);
                            }

                            //ColorGrid.Colors = palette;
                        }
                    }
                    else
                    {
                        //System.Windows.Forms.MessageBox.Show(
                        //    "Sorry, unable to open palette, the file format is not supported or is not recognized.",
                        //    "Load Palette", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show(
                    //    string.Format("Sorry, unable to open palette. {0}", ex.GetBaseException().Message),
                    //    "Load Palette", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                IPaletteSerializer serializer;

                serializer = PaletteSerializer.AllSerializers.Where(s => s.CanWrite)
                    .ElementAt(dialog.FilterIndex - 1);
                if (serializer != null)
                {
                    if (!serializer.CanWrite)
                    {
                        throw new InvalidOperationException("Serializer does not support writing palettes.");
                    }

                    try
                    {
                        using (FileStream file = File.OpenWrite(dialog.FileName))
                        {
                            //serializer.Serialize(file, ColorGrid.Colors);
                        }
                    }
                    catch (Exception ex)
                    {
                        //System.Windows.Forms.MessageBox.Show(
                        //    string.Format("Sorry, unable to save palette. {0}", ex.GetBaseException().Message),
                        //    "Save Palette", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(
                    //    "Sorry, unable to save palette, the file format is not supported or is not recognized.",
                    //    "Save Palette", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ColorGrid_EditingColor(object sender, EditColorCancelEventArgs e)
        {
            e.Cancel = true;

            var color = e.Color.ToColor();

            var res = GetColorForPaletteAction?.Invoke(ref color);

            //if (res.HasValue && res.Value)
            //    ColorGrid.Colors[e.ColorIndex] = color.ToColor();
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

    public delegate bool GetColorHandler(ref Color color);
}