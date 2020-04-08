using Egorozh.ColorPicker.Dialog;
using System.Windows;
using System.Windows.Media;

namespace Egorozh.ColorPicker.WPF.Client
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorPickerDialog
            {
                Owner = this
            };

            var res = dialog.ShowDialog();

            if (res == true)
                Button.Background = new SolidColorBrush(dialog.Color);
        }
    }
}