using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
    }
}