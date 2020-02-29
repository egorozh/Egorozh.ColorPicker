using Egorozh.ColorPicker.Dialog;
using System.Windows;

namespace Egorozh.ColorPicker.WPF.Client
{
    public partial class MainWindow : Window
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
        }
    }
}