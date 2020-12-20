using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Egorozh.ColorPicker
{
    public class ColorHexComboBox : ComboBox, IColorClient
    {
        #region Private Fields

        private IColorManager? _manager;
        private TextChangedEventHandler _textChangedEventHandler;

        #endregion

        #region Static Constructor

        static ColorHexComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorHexComboBox),
                new FrameworkPropertyMetadata(typeof(ColorHexComboBox)));
        }

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
            SetSelectedItemInHex(color);
        }

        public void Init(IColorManager colorManager)
        {
            _manager = colorManager;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ItemsSource = HexComboBoxHelpers.GetNamedColors();
            SelectionChanged += Hex_SelectionChanged;

            var copyButton = GetTemplateChild("PART_CopyButton") as Button;
            copyButton.Click += CopyButtonOnClick;


            _textChangedEventHandler = HexTextBox_OnTextChanged;
            AddHandler(TextBoxBase.TextChangedEvent, _textChangedEventHandler);

            SetSelectedItemInHex(_manager.CurrentColor);
        }

        #endregion

        #region Private Methods

        private void Hex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is NamedColor namedColor)
                    _manager.CurrentColor = namedColor.Color;
            }
        }

        private void HexTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text.Length == 6 || Text.Length == 8)
            {
                try
                {
                    var color = ColorTranslator.FromHtml("#" + Text);
                    _manager.CurrentColor = color;
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch
                {
                }
            }
        }

        private void CopyButtonOnClick(object? sender, RoutedEventArgs e)
        {
            var currentColor = _manager.CurrentColor;

            var hexColor = $"{currentColor.A:X2}{currentColor.R:X2}{currentColor.G:X2}{currentColor.B:X2}";

            Clipboard.SetText(hexColor);
        }

        private void SetSelectedItemInHex(Color color)
        {
            var colors = (List<NamedColor>) ItemsSource;

            var namedColor = colors.FirstOrDefault(c => c.Color.A == color.A &&
                                                        c.Color.R == color.R &&
                                                        c.Color.G == color.G &&
                                                        c.Color.B == color.B);

            RemoveHandler(TextBoxBase.TextChangedEvent, _textChangedEventHandler);
            SelectionChanged -= Hex_SelectionChanged;
            
            if (namedColor != null)
            {
                SelectedItem = namedColor;
            }
            else
            {
                SelectedItem = null;


                Text = color.A == byte.MaxValue
                    ? $"{color.R:X2}{color.G:X2}{color.B:X2}"
                    : $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            
            SelectionChanged += Hex_SelectionChanged;
            AddHandler(TextBoxBase.TextChangedEvent, _textChangedEventHandler);
        }

        #endregion
    }
}