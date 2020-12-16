using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Egorozh.ColorPicker.Dialog
{
    public partial class ColorPickerDialog
    {
        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(ColorPickerDialog), new PropertyMetadata(default(Color)));

        public static readonly DependencyProperty GetColorHandlerProperty = DependencyProperty.Register(
            nameof(GetColorHandler), typeof(GetColorHandler), typeof(ColorPickerDialog),
            new PropertyMetadata(default(GetColorHandler)));

        public static readonly DependencyProperty ColorsProperty = DependencyProperty.Register(
            nameof(Colors), typeof(IEnumerable<Color>), typeof(ColorPickerDialog),
            new PropertyMetadata(ColorPalettes.PaintPalette.Select(c => c.ToColor())));

        #endregion

        #region Public Properties

        public Color Color
        {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public GetColorHandler GetColorHandler
        {
            get => (GetColorHandler) GetValue(GetColorHandlerProperty);
            set => SetValue(GetColorHandlerProperty, value);
        }

        public IEnumerable<Color> Colors
        {
            get => (IEnumerable<Color>) GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        #endregion

        #region Constructor

        public ColorPickerDialog()
        {
            InitializeComponent();

            GetColorHandler = GetColor;
        }

        #endregion

        #region Private Methods

        private void btOk_Click(object sender, RoutedEventArgs e) => this.DialogResult = true;

        private void btCancel_Click(object sender, RoutedEventArgs e) => this.DialogResult = false;

        private Task<(bool, Color)> GetColor(Color color)
        {
            ColorPickerDialog colorPicker = new()
            {
                Color = color,
                Owner = this,
                Colors = Colors
            };

            var res = colorPicker.ShowDialog();

            return Task.FromResult(res != true ? (false, color) : (true, colorPicker.Color));
        }
        
        #endregion
    }
}