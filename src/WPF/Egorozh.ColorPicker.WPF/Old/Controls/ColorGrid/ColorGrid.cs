using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public class ColorGrid : WrapPanel
    {
        /*
        #region Dependency Properties

        public static readonly DependencyProperty ColorButtonStyleProperty = DependencyProperty.Register(
            nameof(ColorButtonStyle), typeof(Style), typeof(ColorGrid),
            new PropertyMetadata(default(Style), ColorButtonStyleChanged));

        private static void ColorButtonStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorGrid colorGrid)
                colorGrid.ColorButtonStyleChanged();
        }

        public static readonly DependencyProperty PaletteProperty = DependencyProperty.Register(
            nameof(Palette), typeof(ColorPalette), typeof(ColorGrid),
            new PropertyMetadata(default(ColorPalette), PaletteChanged));

        private static void PaletteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorGrid colorGrid)
                colorGrid.Colors = ColorPalettes.GetPalette(colorGrid.Palette);
        }
        

        private static void ColorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorGrid colorGrid)
                colorGrid.CreateColors();
        }

        #endregion

        #region Public Properties

        public Color Color { get; set; }

        public ColorPalette Palette
        {
            get => (ColorPalette) GetValue(PaletteProperty);
            set => SetValue(PaletteProperty, value);
        }

        public ColorCollection Colors
        {
            get => (ColorCollection) GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        public Style ColorButtonStyle
        {
            get => (Style) GetValue(ColorButtonStyleProperty);
            set => SetValue(ColorButtonStyleProperty, value);
        }

        #endregion

        #region Events

        public event EventHandler ColorChanged;
        public event EventHandler<EditColorCancelEventArgs> EditingColor;

        #endregion

        #region Private Methods

        private void CreateColors()
        {
            foreach (var uiElement in Children)
            {
                if (uiElement is Button button)
                {
                    button.Click -= Button_Click;
                    button.MouseDoubleClick -= Button_MouseDoubleClick;
                }
            }

            Children.Clear();

            for (var i = 0; i < Colors.Count; i++)
            {
                var color = Colors[i];

                var button = new Button
                {
                    Background = new SolidColorBrush(color),
                    Tag = i,
                    Style = ColorButtonStyle,
                    ToolTip = color.ToString()
                };

                button.Click += Button_Click;
                button.MouseDoubleClick += Button_MouseDoubleClick;

                Children.Add(button);
            }
        }

        private void ColorButtonStyleChanged()
        {
            foreach (var uiElement in Children)
            {
                if (uiElement is Button button)
                    button.Style = ColorButtonStyle;
            }
        }

        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                var brush = button.Background as SolidColorBrush;

                var index = (int) button.Tag;

                //EditingColor?.Invoke(this, new EditColorCancelEventArgs(brush.Color, index));

                var updatedColor = Colors[index];

                button.Background = new SolidColorBrush(updatedColor);
                button.ToolTip = updatedColor.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var brush = button.Background as SolidColorBrush;

                Color = brush.Color.ToColor();

                ColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
        */
    }
}