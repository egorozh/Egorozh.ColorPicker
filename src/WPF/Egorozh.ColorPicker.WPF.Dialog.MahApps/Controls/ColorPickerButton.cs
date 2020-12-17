namespace Egorozh.ColorPicker.Dialog
{
    public class ColorPickerButton : ColorPickerButtonBase
    {
        #region Private Methods

        protected override void ChangeColor()
        {
            var dialog = new ColorPickerDialog
            {
                Owner = Owner,
                Color = Color
            };

            var res = dialog.ShowDialog();

            if (res == true)
                Color = dialog.Color;
        }

        #endregion
    }
}