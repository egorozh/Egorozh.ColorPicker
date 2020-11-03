using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Egorozh.ColorPicker.Dialog
{
    public class ColorPickerDialog : Window
    {
        #region Dependency Properties

        public static readonly AvaloniaProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerDialog, Color>(nameof(Color));

        #endregion

        #region Public Properties

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
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
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            Close(true);
        }

        private void btCancel_Click(object sender, RoutedEventArgs e) => Close(false);
        
        #endregion
    }
}