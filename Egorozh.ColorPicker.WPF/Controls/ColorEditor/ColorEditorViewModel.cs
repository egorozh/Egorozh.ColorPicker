using System;
using System.ComponentModel;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class ColorEditorViewModel : BaseViewModel, IColorEditor
    {
        #region Private Fields

        private Color _color;
        private HslColor _hslColor;

        #endregion

        #region Public Properties

        public int A { get; set; }

        public int R { get; set; }

        public int G { get; set; }

        public int B { get; set; }

        public int H { get; set; }

        public int S { get; set; }

        public int L { get; set; }

        public Color Color
        {
            get => _color;
            set => SetColor(value);
        }

        public HslColor HslColor
        {
            get => _hslColor;
            set => SetHslColor(value);
        }

        #endregion

        #region Events

        public event EventHandler ColorChanged;

        #endregion

        #region Constructor

        public ColorEditorViewModel()
        {
            _color = Color.FromArgb(0, 0, 0);

            PropertyChanged += ColorEditorViewModel_PropertyChanged;
        }

        private void ColorEditorViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(A):
                case nameof(R):
                case nameof(G):
                case nameof(B):

                    var color = Color.FromArgb(A, R, G, B);

                    Color = color;
                    _hslColor = new HslColor(color);

                    break;
                case nameof(H):
                case nameof(S):
                case nameof(L):

                    var hsvColor = new HslColor(A, H, (double) S / 100, (double) L / 100);

                    _hslColor = hsvColor;
                    Color = hsvColor.ToRgbColor();

                    break;
            }

            ColorChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void SetColor(Color value)
        {
            if (_color != value)
            {
                _hslColor = new HslColor(value);
                _color = HslColor.ToRgbColor();

                Update();
            }
        }

        private void SetHslColor(HslColor value)
        {
            if (_hslColor != value)
                _hslColor = value;
        }

        private void Update()
        {
            PropertyChanged -= ColorEditorViewModel_PropertyChanged;

            R = Color.R;
            G = Color.G;
            B = Color.B;
            A = Color.A;

            H = (int) HslColor.H;
            S = (int) (HslColor.S * 100);
            L = (int) (HslColor.L * 100);

            PropertyChanged += ColorEditorViewModel_PropertyChanged;

            /*

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
               hNumericUpDown.Value = (int)this.HslColor.H;
           }

           if (!(userAction && sNumericUpDown.IsFocused))
           {
               sNumericUpDown.Value = (int)(this.HslColor.S * 100);
           }

           if (!(userAction && lNumericUpDown.IsFocused))
           {
               lNumericUpDown.Value = (int)(this.HslColor.L * 100);
           }

           hColorBar.Value = (int)this.HslColor.H;
           sColorBar.Color = this.Color;
           sColorBar.Value = (int)(this.HslColor.S * 100);
           lColorBar.Color = this.Color;
           lColorBar.Value = (int)(this.HslColor.L * 100);

           // Alpha
           if (!(userAction && aNumericUpDown.IsFocused))
           {
               aNumericUpDown.Value = this.Color.A;
           }

           aColorBar.Color = this.Color;
           aColorBar.Value = this.Color.A;

           */
        }

        #endregion
    }
}