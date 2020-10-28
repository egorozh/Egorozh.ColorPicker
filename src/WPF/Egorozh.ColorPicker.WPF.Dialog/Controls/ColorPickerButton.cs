using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Egorozh.ColorPicker.Dialog
{
    [ContentProperty(nameof(Content))]
    public class ColorPickerButton : Control
    {
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ColorPickerButton), new PropertyMetadata(default(Color)));

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(ColorPickerButton), new PropertyMetadata(default(object)));

        public object Content
        {
            get => (object) GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        static ColorPickerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerButton),
                new FrameworkPropertyMetadata(typeof(ColorPickerButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeftButtonDown += ColorPickerButton_MouseLeftButtonDown;
        }

        private void ColorPickerButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dialog = new ColorPickerDialog
            {
                //Owner = this,
                Color = Color
            };

            var res = dialog.ShowDialog();

            if (res == true)
                Color = dialog.Color;
        }
    }
}