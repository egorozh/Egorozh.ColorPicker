using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Egorozh.ColorPicker.Dialog
{
    [ContentProperty(nameof(Content))]
    public class ColorPickerButton : Control
    {
        #region Dependency Properties

        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register(
            "Owner", typeof(Window), typeof(ColorPickerButton), new PropertyMetadata(default(Window)));


        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ColorPickerButton), new PropertyMetadata(default(Color)));


        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(ColorPickerButton), new PropertyMetadata(default(object)));

        #endregion

        #region Public Properties

        public object Content
        {
            get => (object) GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public Window Owner
        {
            get => (Window) GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }

        #endregion

        #region Static Constructor

        static ColorPickerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerButton),
                new FrameworkPropertyMetadata(typeof(ColorPickerButton)));
        }

        #endregion

        #region Constructor

        public ColorPickerButton()
        {
            Owner = Application.Current?.MainWindow;    
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MouseLeftButtonDown += ColorPickerButton_MouseLeftButtonDown;
        }

        #endregion

        #region Private Methods

        private void ColorPickerButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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