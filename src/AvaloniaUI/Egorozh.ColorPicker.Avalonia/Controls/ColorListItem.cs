namespace Egorozh.ColorPicker;

public class ColorListItem : ListBoxItem
{
    public ColorListItem(Avalonia.Media.Color color = default)
    {
        Background = new Avalonia.Media.SolidColorBrush(color);
    }
}