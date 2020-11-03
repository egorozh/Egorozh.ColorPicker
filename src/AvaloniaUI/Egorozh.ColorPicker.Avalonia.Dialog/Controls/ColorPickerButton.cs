using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;
using Avalonia.VisualTree;

namespace Egorozh.ColorPicker.Dialog
{
    public class ColorPickerButton : ContentControl, IStyleable
    {
        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPickerButton);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<global::Avalonia.Media.Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerButton, global::Avalonia.Media.Color>(nameof(Color));

        public static readonly StyledProperty<Window> OwnerProperty =
            AvaloniaProperty.Register<ColorPickerButton, Window>(nameof(Owner));

        #endregion

        #region Public Properties

        public global::Avalonia.Media.Color Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public Window Owner
        {
            get => GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            Owner = this.GetVisualRoot() as Window;

            PointerPressed += ColorPickerButton_PointerPressed;
        }

        #endregion

        #region Private Methods

        private async void ColorPickerButton_PointerPressed(object sender,
            global::Avalonia.Input.PointerPressedEventArgs e)
        {
            var dialog = new ColorPickerDialog()
            {
                Color = Color
            };

            var res = await dialog.ShowDialog<bool>(Owner);

            if (res)
                Color = dialog.Color;
        }

        #endregion
    }
}