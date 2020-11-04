using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;

namespace Egorozh.ColorPicker
{
    public class ColorEditor : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Methods

        private IColorManager _manager;
        private ComboBox _hex;

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorEditor);

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

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _hex = e.NameScope.Find<ComboBox>("PART_HexComboBox");

            var alphaSlider = e.NameScope.Find<RgbaColorSlider>("PART_AlphaSlider");
            var alphaNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_AlphaNumUpDown");

            _manager.AddClient(alphaSlider, alphaNumUpDown);

            _hex.Items = HexComboBoxHelpers.GetNamedColors();
            _hex.SelectionChanged += Hex_SelectionChanged;

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

        private void SetSelectedItemInHex(Color color)
        {
            if (_hex != null)
            {
                var colors = (List<NamedColor>) _hex.Items;

                var namedColor = colors.FirstOrDefault(c => c.Color.A == color.A &&
                                                            c.Color.R == color.R &&
                                                            c.Color.G == color.G &&
                                                            c.Color.B == color.B);

                if (namedColor != null)
                {
                    _hex.SelectedItem = namedColor;
                }
                else
                {
                    _hex.SelectedItem = null;
                    _hex.PlaceholderText = $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
                }
            }
        }

        #endregion
    }
}