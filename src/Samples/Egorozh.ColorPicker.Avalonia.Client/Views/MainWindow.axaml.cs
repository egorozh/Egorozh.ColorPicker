using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Egorozh.ColorPicker.Dialog;

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

    internal class CustomColorPickerButton : ColorPickerButtonBase
    {
        protected override async Task ChangeColor()
        {
            ColorPickerDialog dialog = new()
            {
                Color = Color,
                Colors = Colors,
                Title = "Custom Title"
            };

            var res = await dialog.ShowDialog<bool>(Owner);

            if (res)
                Color = dialog.Color;
        }
    }
}