using System.Windows;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    public class ColorPickerButtonBase : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty OwnerProperty = DependencyProperty.Register(
            "Owner", typeof(Window), typeof(ColorPickerButtonBase), new PropertyMetadata(default(Window)));


        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(System.Windows.Media.Color), typeof(ColorPickerButtonBase),
            new PropertyMetadata(default(System.Windows.Media.Color)));

        #endregion

        #region Public Properties

        public System.Windows.Media.Color Color
        {
            get => (System.Windows.Media.Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public Window Owner
        {
            get => (Window) GetValue(OwnerProperty);
            set => SetValue(OwnerProperty, value);
        }

        #endregion

        #region Static Constructor

        static ColorPickerButtonBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPickerButtonBase),
                new FrameworkPropertyMetadata(typeof(ColorPickerButtonBase)));
        }

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Owner = Window.GetWindow(this) ?? Application.Current?.MainWindow;

            this.MouseLeftButtonDown += ColorPickerButton_MouseLeftButtonDown;
        }

        #endregion

        #region Protected Methods

        private void ColorPickerButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangeColor();
        }

        protected virtual void ChangeColor()
        {
        }

        #endregion
    }
}