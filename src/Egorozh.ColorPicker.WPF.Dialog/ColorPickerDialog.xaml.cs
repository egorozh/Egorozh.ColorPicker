using System.Windows;
using System.Windows.Media;

namespace Egorozh.ColorPicker.Dialog
{
    public partial class ColorPickerDialog
    {
        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(ColorPickerDialog), new PropertyMetadata(default(Color)));

        public static readonly DependencyProperty GetColorForPaletteActionProperty = DependencyProperty.Register(
            nameof(GetColorForPaletteAction), typeof(GetColorHandler), typeof(ColorPickerDialog),
            new PropertyMetadata(new GetColorHandler(GetColorForPalette)));

        #endregion

        #region Public Properties

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

        #endregion

        #region Constructor

        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void btOk_Click(object sender, RoutedEventArgs e) => this.DialogResult = true;

        private void btCancel_Click(object sender, RoutedEventArgs e) => this.DialogResult = false;

        private static bool GetColorForPalette(ref Color color)
        {
            var colorPicker = new ColorPickerDialog
            {
                Color = color,
            };

            var res = colorPicker.ShowDialog();

            if (res != true)
                return false;

            color = colorPicker.Color;

            return true;
        }

        #endregion
    }
}