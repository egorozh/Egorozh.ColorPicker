using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public partial class ColorGrid : IColorEditor
    {
        #region Dependency Properties

        public static readonly DependencyProperty ColorButtonStyleProperty = DependencyProperty.Register(
            nameof(ColorButtonStyle), typeof(Style), typeof(ColorGrid), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty PaletteProperty = DependencyProperty.Register(
            nameof(Palette), typeof(ColorPalette), typeof(ColorGrid),
            new PropertyMetadata(default(ColorPalette), PaletteChanged));

        private static void PaletteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorGrid colorGrid)
                colorGrid.Colors = ColorPalettesNew.GetPalette(colorGrid.Palette);
        }

        public static readonly DependencyProperty ColorsProperty = DependencyProperty.Register(
            nameof(Colors), typeof(ColorCollectionNew), typeof(ColorGrid),
            new PropertyMetadata(default(ColorCollectionNew), ColorsChanged));

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

        public ColorCollectionNew Colors
        {
            get => (ColorCollectionNew) GetValue(ColorsProperty);
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
        public event EventHandler<EditColorCancelEventArgsNew> EditingColor;

        #endregion

        #region Static Constructor

        static ColorGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorGrid),
                new FrameworkPropertyMetadata(typeof(ColorGrid)));
        }

        #endregion

        #region Constructor

        public ColorGrid()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void CreateColors()
        {
            foreach (var uiElement in WrapPanel.Children)
            {
                if (uiElement is Button button)
                {
                    button.Click -= Button_Click;
                    button.MouseDoubleClick -= Button_MouseDoubleClick;
                }
            }

            WrapPanel.Children.Clear();

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

                WrapPanel.Children.Add(button);
            }
        }

        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                var brush = button.Background as SolidColorBrush;

                var index = (int) button.Tag;

                EditingColor?.Invoke(this, new EditColorCancelEventArgsNew(brush.Color, index));

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
    }
}