namespace Egorozh.ColorPicker
{
    public class ColorItemViewModel : WrapItemViewModel
    {
        public Avalonia.Media.Color Color { get; }

        public ColorItemViewModel(Avalonia.Media.Color color)
        {
            Color = color;
        }
    }
}