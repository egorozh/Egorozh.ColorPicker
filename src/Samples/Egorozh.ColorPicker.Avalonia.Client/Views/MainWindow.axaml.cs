using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Egorozh.ColorPicker.Avalonia.Dialog;

namespace Egorozh.ColorPicker.Avalonia.Client
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ChangeColor();
        }

        private async Task ChangeColor()
        {
            var button = this.FindControl<Button>("Button");

            var oldColor = button.Background switch
            {
                ImmutableSolidColorBrush immutableBrush => immutableBrush.Color,
                SolidColorBrush brush => brush.Color,
                _ => Colors.Transparent
            };
            
            var dialog = new ColorPickerDialog()
            {
                Color = oldColor
            };

            var res = await dialog.ShowDialog<bool>(this);

            if (res)
                button.Background = new SolidColorBrush(dialog.Color);
        }
    }
}