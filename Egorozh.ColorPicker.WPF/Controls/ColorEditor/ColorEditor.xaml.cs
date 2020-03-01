using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
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
                if (this.HslColor != value)
                {
                    _hslColor = value;

                    if (!this.LockUpdates)
                    {
                        this.LockUpdates = true;
                        this.Color = value.ToRgbColor();
                        this.LockUpdates = false;
                        this.UpdateFields(false);
                    }
                    else
                    {
                        this.OnColorChanged(EventArgs.Empty);
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
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Updates the editing field values.
        /// </summary>
        /// <param name="userAction">if set to <c>true</c> the update is due to user interaction.</param>
        private void UpdateFields(bool userAction)
        {
            if (!this.LockUpdates)
            {
                try
                {
                    this.LockUpdates = true;

                    // RGB
                    if (!(userAction && rNumericUpDown.IsFocused))
                    {
                        rNumericUpDown.Value = this.Color.R;
                    }

                    if (!(userAction && gNumericUpDown.IsFocused))
                    {
                        gNumericUpDown.Value = this.Color.G;
                    }

                    if (!(userAction && bNumericUpDown.IsFocused))
                    {
                        bNumericUpDown.Value = this.Color.B;
                    }

                    rColorBar.Value = this.Color.R;
                    rColorBar.Color = this.Color;
                    gColorBar.Value = this.Color.G;
                    gColorBar.Color = this.Color;
                    bColorBar.Value = this.Color.B;
                    bColorBar.Color = this.Color;

                    // HTML
                    if (!(userAction && hexTextBox.Focused))
                    {
                        hexTextBox.Text = this.Color.IsNamedColor
                            ? this.Color.Name
                            : string.Format("{0:X2}{1:X2}{2:X2}", this.Color.R, this.Color.G, this.Color.B);
                    }

                    // HSL
                    if (!(userAction && hNumericUpDown.IsFocused))
                    {
                        hNumericUpDown.Value = (int) this.HslColor.H;
                    }

                    if (!(userAction && sNumericUpDown.IsFocused))
                    {
                        sNumericUpDown.Value = (int) (this.HslColor.S * 100);
                    }

                    if (!(userAction && lNumericUpDown.IsFocused))
                    {
                        lNumericUpDown.Value = (int) (this.HslColor.L * 100);
                    }

                    hColorBar.Value = (int) this.HslColor.H;
                    sColorBar.Color = this.Color;
                    sColorBar.Value = (int) (this.HslColor.S * 100);
                    lColorBar.Color = this.Color;
                    lColorBar.Value = (int) (this.HslColor.L * 100);

                    // Alpha
                    if (!(userAction && aNumericUpDown.IsFocused))
                    {
                        aNumericUpDown.Value = this.Color.A;
                    }

                    aColorBar.Color = this.Color;
                    aColorBar.Value = this.Color.A;
                }
                finally
                {
                    this.LockUpdates = false;
                }
            }
        }

        private void OnColorChanged(EventArgs e)
        {
            this.UpdateFields(false);

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
                if (!this.LockUpdates)
                {
                    this.LockUpdates = true;
                    this.HslColor = new HslColor(newColor);
                    this.LockUpdates = false;
                    this.UpdateFields(false);
                }
                else
                {
                    this.OnColorChanged(EventArgs.Empty);
                }
            }
        }

        private void AddColorProperties<T>()
        {
            Type type;
            Type colorType;

            type = typeof(T);
            colorType = typeof(Color);

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

        private void SetDropDownWidth()
        {
            if (hexTextBox.Items.Count != 0)
            {
                hexTextBox.DropDownWidth = hexTextBox.ItemHeight * 2 + hexTextBox.Items.Cast<string>()
                                               .Max(s => TextRenderer.MeasureText(s, hexTextBox.Font).Width);
            }
        }

        private void FillNamedColors()
        {
            //this.AddColorProperties<SystemColors>();

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

            this.AddColorProperties<Color>();
            this.SetDropDownWidth();
        }

        private void NumericUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (!this.LockUpdates)
            {
                bool useHsl;
                bool useRgb;
                bool useNamed;

                useHsl = false;
                useRgb = false;
                useNamed = false;

                this.LockUpdates = true;

                if (sender == aNumericUpDown || sender == rNumericUpDown || sender == gNumericUpDown ||
                    sender == bNumericUpDown)
                {
                    useRgb = true;
                }
                else if (sender == hNumericUpDown || sender == sNumericUpDown || sender == lNumericUpDown)
                {
                    useHsl = true;
                }

                if (useRgb || useNamed)
                {
                    Color color;

                    color = useNamed
                        ? Color.FromName(hexTextBox.Text)
                        : Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                            (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);

                    this.Color = color;
                    this.HslColor = new HslColor(color);
                }
                else if (useHsl)
                {
                    HslColor color;

                    color = new HslColor((int) aNumericUpDown.Value, (double) hNumericUpDown.Value,
                        (double) sNumericUpDown.Value / 100, (double) lNumericUpDown.Value / 100);
                    this.HslColor = color;
                    this.Color = color.ToRgbColor();
                }

                this.LockUpdates = false;
                this.UpdateFields(true);
            }
        }

        private string AddSpaces(string text)
        {
            string result;

            //http://stackoverflow.com/a/272929/148962

            if (!string.IsNullOrEmpty(text))
            {
                StringBuilder newText;

                newText = new StringBuilder(text.Length * 2);
                newText.Append(text[0]);
                for (int i = 1; i < text.Length; i++)
                {
                    if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    {
                        newText.Append(' ');
                    }

                    newText.Append(text[i]);
                }

                result = newText.ToString();
            }
            else
            {
                result = null;
            }

            return result;
        }

        private void HexTextBox_OnDrawItem(object sender, DrawItemEventArgs e)
        {
            // TODO: Really, this should be another control - ColorComboBox or ColorListBox etc.

            if (e.Index != -1)
            {
                Rectangle colorBox;
                string name;
                Color color;

                e.DrawBackground();

                name = (string) hexTextBox.Items[e.Index];
                color = Color.FromName(name);
                colorBox = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Height - 3, e.Bounds.Height - 3);

                using (Brush brush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(brush, colorBox);
                }

                e.Graphics.DrawRectangle(SystemPens.ControlText, colorBox);

                TextRenderer.DrawText(e.Graphics, this.AddSpaces(name), hexTextBox.Font,
                    new Point(colorBox.Right + 3, colorBox.Top), e.ForeColor);

                //if (color == Color.Transparent && (e.State & DrawItemState.Selected) != DrawItemState.Selected)
                //  e.Graphics.DrawLine(SystemPens.ControlText, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);

                e.DrawFocusRectangle();
            }
        }

        private void HexTextBox_OnDropDown(object? sender, EventArgs e)
        {
            if (hexTextBox.Items.Count == 0)
            {
                this.FillNamedColors();
            }
        }

        private void HexTextBox_OnSelectedIndexChanged(object? sender, EventArgs e)
        {
            if (hexTextBox.SelectedIndex != -1)
            {
                this.LockUpdates = true;
                this.Color = Color.FromName((string) hexTextBox.SelectedItem);
                this.LockUpdates = false;
            }
        }

        private void HexTextBox_OnTextChanged(object sender, EventArgs e)
        {
            if (!this.LockUpdates)
            {
                bool useHsl;
                bool useRgb;
                bool useNamed;

                useHsl = false;
                useRgb = false;
                useNamed = false;

                this.LockUpdates = true;

                if (sender == hexTextBox)
                {
                    string text;
                    int namedIndex;

                    text = hexTextBox.Text;
                    if (text.StartsWith("#"))
                    {
                        text = text.Substring(1);
                    }

                    if (hexTextBox.Items.Count == 0)
                    {
                        this.FillNamedColors();
                    }

                    namedIndex = hexTextBox.FindStringExact(text);

                    if (namedIndex != -1 || text.Length == 6 || text.Length == 8)
                    {
                        try
                        {
                            Color color;

                            color = namedIndex != -1 ? Color.FromName(text) : ColorTranslator.FromHtml("#" + text);
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
                    Color color;

                    color = useNamed
                        ? Color.FromName(hexTextBox.Text)
                        : Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                            (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);

                    this.Color = color;
                    this.HslColor = new HslColor(color);
                }

                this.LockUpdates = false;
                this.UpdateFields(true);
            }
        }

        private void HexTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.PageUp:
                case Keys.PageDown:
                    if (hexTextBox.Items.Count == 0)
                    {
                        this.FillNamedColors();
                    }

                    break;
            }
        }

        private void ColorBar_OnValueChanged(object? sender, EventArgs e)
        {
            if (!this.LockUpdates)
            {
                bool useHsl;
                bool useRgb;
                bool useNamed;

                useHsl = false;
                useRgb = false;
                useNamed = false;

                this.LockUpdates = true;

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


                if (useRgb || useNamed)
                {
                    Color color;

                    color = useNamed
                        ? Color.FromName(hexTextBox.Text)
                        : Color.FromArgb((int) aNumericUpDown.Value, (int) rNumericUpDown.Value,
                            (int) gNumericUpDown.Value, (int) bNumericUpDown.Value);

                    this.Color = color;
                    this.HslColor = new HslColor(color);
                }
                else if (useHsl)
                {
                    HslColor color;

                    color = new HslColor((int) aNumericUpDown.Value, (double) hNumericUpDown.Value,
                        (double) sNumericUpDown.Value / 100, (double) lNumericUpDown.Value / 100);
                    this.HslColor = color;
                    this.Color = color.ToRgbColor();
                }

                this.LockUpdates = false;
                this.UpdateFields(true);
            }
        }

        #endregion
    }
}