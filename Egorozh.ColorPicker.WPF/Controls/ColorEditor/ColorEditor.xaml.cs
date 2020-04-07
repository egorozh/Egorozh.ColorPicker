using System;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Color = System.Drawing.Color;
using SystemColors = System.Drawing.SystemColors;

namespace Egorozh.ColorPicker
{
    public partial class ColorEditor : IColorEditor
    {
        #region Private Fields

        private Color _color;

        private HslColor _hslColor;

        private bool _showAlphaChannel;

        private bool _showColorSpaceLabels;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty NumericUpDownStyleProperty = DependencyProperty.Register(
            nameof(NumericUpDownStyle), typeof(Style), typeof(ColorEditor), new PropertyMetadata(default(Style)));

        public Style NumericUpDownStyle
        {
            get => (Style) GetValue(NumericUpDownStyleProperty);
            set => SetValue(NumericUpDownStyleProperty, value);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(ColorEditor), new PropertyMetadata(Color.Black, ColorChangedS));

        private static void ColorChangedS(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorEditor colorEditor)
                colorEditor.ColorPropertyChanged((Color) e.OldValue, (Color) e.NewValue);
        }

        #endregion

        #region Public Properties

        public virtual HslColor HslColor
        {
            get => _hslColor;
            set
            {
                if (HslColor != value)
                {
                    _hslColor = value;

                    if (!LockUpdates)
                    {
                        LockUpdates = true;
                        Color = value.ToRgbColor();
                        LockUpdates = false;
                        UpdateFields(false);
                    }
                    else
                    {
                        OnColorChanged(EventArgs.Empty);
                    }
                }
            }
        }

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets or sets a value indicating whether input changes should be processed.
        /// </summary>
        /// <value><c>true</c> if input changes should be processed; otherwise, <c>false</c>.</value>
        protected bool LockUpdates { get; set; }

        #endregion

        #region Events

        public event EventHandler ColorChanged;

        #endregion

        #region Constructor

        public ColorEditor()
        {
            InitializeComponent();

            rColorBar.Channel = RgbaChannel.Red;
            gColorBar.Channel = RgbaChannel.Green;
            bColorBar.Channel = RgbaChannel.Blue;
            aColorBar.Channel = RgbaChannel.Alpha;

            Color = Color.FromArgb(0, 0, 0);

            FillNamedColors();
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Updates the editing field values.
        /// </summary>
        /// <param name="userAction">if set to <c>true</c> the update is due to user interaction.</param>
        private void UpdateFields(bool userAction)
        {
            if (!LockUpdates)
            {
                try
                {
                    LockUpdates = true;

                    // RGB
                    if (!(userAction && rNumericUpDown.IsFocused))
                    {
                        rNumericUpDown.Value = Color.R;
                    }

                    if (!(userAction && gNumericUpDown.IsFocused))
                    {
                        gNumericUpDown.Value = Color.G;
                    }

                    if (!(userAction && bNumericUpDown.IsFocused))
                    {
                        bNumericUpDown.Value = Color.B;
                    }

                    rColorBar.Value = Color.R;
                    rColorBar.Color = Color;
                    gColorBar.Value = Color.G;
                    gColorBar.Color = Color;
                    bColorBar.Value = Color.B;
                    bColorBar.Color = Color;

                    // HTML
                    if (!(userAction && hexTextBox.IsFocused))
                    {
                        hexTextBox.Text = Color.IsNamedColor
                            ? Color.Name
                            : string.Format("{0:X2}{1:X2}{2:X2}", Color.R, Color.G, Color.B);
                    }

                    // HSL
                    if (!(userAction && hNumericUpDown.IsFocused))
                    {
                        hNumericUpDown.Value = (int) HslColor.H;
                    }

                    if (!(userAction && sNumericUpDown.IsFocused))
                    {
                        sNumericUpDown.Value = (int) (HslColor.S * 100);
                    }

                    if (!(userAction && lNumericUpDown.IsFocused))
                    {
                        lNumericUpDown.Value = (int) (HslColor.L * 100);
                    }

                    hColorBar.Value = (int) HslColor.H;
                    sColorBar.Color = Color;
                    sColorBar.Value = (int) (HslColor.S * 100);
                    lColorBar.Color = Color;
                    lColorBar.Value = (int) (HslColor.L * 100);

                    // Alpha
                    if (!(userAction && aNumericUpDown.IsFocused))
                    {
                        aNumericUpDown.Value = Color.A;
                    }

                    aColorBar.Color = Color;
                    aColorBar.Value = Color.A;
                }
                finally
                {
                    LockUpdates = false;
                }
            }
        }

        private void OnColorChanged(EventArgs e)
        {
            UpdateFields(false);

            ColorChanged?.Invoke(this, e);
        }

        private void ColorPropertyChanged(Color oldColor, Color newColor)
        {
            /*
             * If the color isn't solid, and ShowAlphaChannel is false
             * remove the alpha channel. Not sure if this is the best
             * place to do it, but it is a blanket fix for now
             */
            if (newColor.A != 255 && !_showAlphaChannel)
            {
                newColor = Color.FromArgb(255, newColor);
            }

            if (oldColor != newColor)
            {
                if (!LockUpdates)
                {
                    LockUpdates = true;
                    HslColor = new HslColor(newColor);
                    LockUpdates = false;
                    UpdateFields(false);
                }
                else
                {
                    OnColorChanged(EventArgs.Empty);
                }
            }
        }

        private void AddColorProperties<T>()
        {
            var type = typeof(T);
            var colorType = typeof(Color);

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (property.PropertyType == colorType)
                {
                    Color color;

                    color = (Color) property.GetValue(type, null);
                    if (!color.IsEmpty)
                    {
                        hexTextBox.Items.Add(color.Name);
                    }
                }
            }
        }

        private void FillNamedColors()
        {
            var type = typeof(SystemColors);
            var colorType = typeof(Color);

            // ReSharper disable once LoopCanBePartlyConvertedToQuery
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (property.PropertyType == colorType)
                {
                    var color = (Color) property.GetValue(type, null);
                    if (!color.IsEmpty)
                    {
                        hexTextBox.Items.Add(color.Name);
                    }
                }
            }

            AddColorProperties<Color>();
        }

        private void NumericUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!LockUpdates)
            {
                var useHsl = false;
                var useRgb = false;

                LockUpdates = true;

                if (sender == aNumericUpDown || sender == rNumericUpDown || sender == gNumericUpDown ||
                    sender == bNumericUpDown)
                {
                    useRgb = true;
                }
                else if (sender == hNumericUpDown || sender == sNumericUpDown || sender == lNumericUpDown)
                {
                    useHsl = true;
                }

                if (useRgb)
                {
                    Color color;

                    color = Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                        (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);

                    Color = color;
                    HslColor = new HslColor(color);
                }
                else if (useHsl)
                {
                    HslColor color;

                    color = new HslColor((int) aNumericUpDown.Value, (double) hNumericUpDown.Value,
                        (double) sNumericUpDown.Value / 100, (double) lNumericUpDown.Value / 100);
                    HslColor = color;
                    Color = color.ToRgbColor();
                }

                LockUpdates = false;
                UpdateFields(true);
            }
        }

        private void HexTextBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hexTextBox.SelectedIndex != -1)
            {
                LockUpdates = true;
                Color = Color.FromName((string) hexTextBox.SelectedItem);
                LockUpdates = false;
            }
        }

        private void HexTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!LockUpdates)
            {
                var useRgb = false;
                var useNamed = false;

                LockUpdates = true;

                if (true)
                {
                    var text = hexTextBox.Text;

                    if (text.StartsWith("#"))
                        text = text.Substring(1);

                    if (text.Length == 6 || text.Length == 8)
                    {
                        try
                        {
                            var color = ColorTranslator.FromHtml("#" + text);
                            aNumericUpDown.Value = color.A;
                            rNumericUpDown.Value = color.R;
                            bNumericUpDown.Value = color.B;
                            gNumericUpDown.Value = color.G;

                            useRgb = true;
                        }
                        // ReSharper disable EmptyGeneralCatchClause
                        catch
                        {
                        }
                        // ReSharper restore EmptyGeneralCatchClause
                    }
                    else
                    {
                        useNamed = true;
                    }
                }

                if (useRgb || useNamed)
                {
                    var color = useNamed
                        ? Color.FromName(hexTextBox.Text)
                        : Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                            (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);

                    Color = color;
                    HslColor = new HslColor(color);
                }

                LockUpdates = false;
                UpdateFields(true);
            }
        }

        private void ColorBar_OnValueChanged(object? sender, EventArgs e)
        {
            if (!LockUpdates)
            {
                var useHsl = false;
                var useRgb = false;

                LockUpdates = true;

                if (sender == aColorBar || sender == rColorBar || sender == gColorBar || sender == bColorBar)
                {
                    aNumericUpDown.Value = (int) aColorBar.Value;
                    rNumericUpDown.Value = (int) rColorBar.Value;
                    gNumericUpDown.Value = (int) gColorBar.Value;
                    bNumericUpDown.Value = (int) bColorBar.Value;

                    useRgb = true;
                }
                else if (sender == hColorBar || sender == lColorBar || sender == sColorBar)
                {
                    hNumericUpDown.Value = (int) hColorBar.Value;
                    sNumericUpDown.Value = (int) sColorBar.Value;
                    lNumericUpDown.Value = (int) lColorBar.Value;

                    useHsl = true;
                }


                if (useRgb)
                {
                    var color = Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                        (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);

                    Color = color;
                    HslColor = new HslColor(color);
                }
                else if (useHsl)
                {
                    var color = new HslColor((int) aNumericUpDown.Value, (double) hNumericUpDown.Value,
                        (double) sNumericUpDown.Value / 100, (double) lNumericUpDown.Value / 100);
                    HslColor = color;
                    Color = color.ToRgbColor();
                }

                LockUpdates = false;
                UpdateFields(true);
            }
        }

        private void HColorBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!LockUpdates)
            {
                var useHsl = false;
                var useRgb = false;

                LockUpdates = true;

                if (sender == aColorBar || sender == rColorBar || sender == gColorBar || sender == bColorBar)
                {
                    aNumericUpDown.Value = (int)aColorBar.Value;
                    rNumericUpDown.Value = (int)rColorBar.Value;
                    gNumericUpDown.Value = (int)gColorBar.Value;
                    bNumericUpDown.Value = (int)bColorBar.Value;

                    useRgb = true;
                }
                else if (sender == hColorBar || sender == lColorBar || sender == sColorBar)
                {
                    hNumericUpDown.Value = (int)hColorBar.Value;
                    sNumericUpDown.Value = (int)sColorBar.Value;
                    lNumericUpDown.Value = (int)lColorBar.Value;

                    useHsl = true;
                }


                if (useRgb)
                {
                    var color = Color.FromArgb((int)aNumericUpDown.Value, (int)rNumericUpDown.Value,
                        (int)gNumericUpDown.Value, (int)bNumericUpDown.Value);

                    Color = color;
                    HslColor = new HslColor(color);
                }
                else if (useHsl)
                {
                    var color = new HslColor((int)aNumericUpDown.Value, (double)hNumericUpDown.Value,
                        (double)sNumericUpDown.Value / 100, (double)lNumericUpDown.Value / 100);
                    HslColor = color;
                    Color = color.ToRgbColor();
                }

                LockUpdates = false;
                UpdateFields(true);
            }
        }

        #endregion
    }
}