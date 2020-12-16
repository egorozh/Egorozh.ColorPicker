using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Egorozh.ColorPicker.Dialog
{
    public class ColorPickerButton : ContentControl, IStyleable
    {
        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPickerButton);

        #endregion

        #region Dependency Properties
        
        public static readonly StyledProperty<IEnumerable<Color>> ColorsProperty =
            AvaloniaProperty.Register<ColorPickerButton, IEnumerable<Color>>(
                nameof(Colors), ColorPalettes.PaintPalette.Select(c => c.ToColor()));

        public static readonly StyledProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerButton, Color>(nameof(Color));

        public static readonly StyledProperty<Window> OwnerProperty =
            AvaloniaProperty.Register<ColorPickerButton, Window>(nameof(Owner));

        #endregion

        #region Public Properties

        public IEnumerable<Color> Colors
        {
            get => GetValue(ColorsProperty);
            private set => SetValue(ColorsProperty, value);
        }

        public Color Color
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

        private async void ColorPickerButton_PointerPressed(object? sender,
            PointerPressedEventArgs e)
        {
            ColorPickerDialog dialog = new ()
            {
                Color = Color,
                Colors = Colors
            };

            var res = await dialog.ShowDialog<bool>(Owner);

            if (res)
                Color = dialog.Color;
        }

        #endregion
    }
}