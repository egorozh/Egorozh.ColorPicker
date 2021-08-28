using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Egorozh.ColorPicker
{
    public class ColorPickerButtonBase : ContentControl, IStyleable
    {
        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPickerButtonBase);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<IEnumerable<Color>> ColorsProperty =
            AvaloniaProperty.Register<ColorPickerButtonBase, IEnumerable<Color>>(
                nameof(Colors), ColorPalettes.PaintPalette.Select(c => c.ToColor()));

        public static readonly StyledProperty<Color> ColorProperty =
            AvaloniaProperty.Register<ColorPickerButtonBase, Color>(nameof(Color));

        public static readonly StyledProperty<Window> OwnerProperty =
            AvaloniaProperty.Register<ColorPickerButtonBase, Window>(nameof(Owner));

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

        protected virtual async Task ChangeColor()
        {
        }

        #endregion

        #region Private Methods

        private async void ColorPickerButton_PointerPressed(object? sender,
            PointerPressedEventArgs e)
        {
            await ChangeColor();
        }

        #endregion
    }
}