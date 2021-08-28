using System.Threading.Tasks;

namespace Egorozh.ColorPicker.Dialog
{
    public class ColorPickerButton : ColorPickerButtonBase
    {
        protected override async Task ChangeColor()
        {
            ColorPickerDialog dialog = new()
            {
                Color = Color,
                Colors = Colors
            };

            var res = await dialog.ShowDialog<bool>(Owner);

            if (res)
                Color = dialog.Color;
        }
    }
}