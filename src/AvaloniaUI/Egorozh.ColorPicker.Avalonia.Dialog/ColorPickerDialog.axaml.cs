using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Egorozh.ColorPicker.Avalonia.Dialog
{
    public class ColorPickerDialog : Window
    {
        #region Dependency Properties

        public static readonly AvaloniaProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerDialog, Color>(nameof(Color));

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
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            Color = Colors.Red;

            Close(true);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e) => Close(false);

        //private static bool GetColorForPalette(ref Color color)
        //{
        //var colorPicker = new ColorPickerDialog
        //{
        //    Color = color,
        //};

        //var res = colorPicker.ShowDialog();

        //if (res != true)
        //    return false;

        //color = colorPicker.Color;

        //return true;
        //}

        #endregion
    }
}