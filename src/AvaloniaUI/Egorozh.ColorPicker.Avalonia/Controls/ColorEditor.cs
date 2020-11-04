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
        private ComboBox _modeComboBox;
        private RgbaColorSlider _rSlider;
        private RgbaColorNumericUpDown _rNumUpDown;
        private RgbaColorSlider _gSlider;
        private RgbaColorNumericUpDown _gNumUpDown;
        private RgbaColorSlider _bSlider;
        private RgbaColorNumericUpDown _bNumUpDown;

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
            _modeComboBox = e.NameScope.Find<ComboBox>("PART_ModeComboBox");

            var alphaSlider = e.NameScope.Find<RgbaColorSlider>("PART_AlphaSlider");
            var alphaNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_AlphaNumUpDown");

            _rSlider = e.NameScope.Find<RgbaColorSlider>("PART_RSlider");
            _rNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_RNumUpDown");

            _gSlider = e.NameScope.Find<RgbaColorSlider>("PART_GSlider");
            _gNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_GNumUpDown");

            _bSlider = e.NameScope.Find<RgbaColorSlider>("PART_BSlider");
            _bNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_BNumUpDown");

            _manager.AddClient(alphaSlider, alphaNumUpDown,
                _rSlider, _rNumUpDown,
                _gSlider, _gNumUpDown,
                _bSlider, _bNumUpDown);
            
            _hex.Items = HexComboBoxHelpers.GetNamedColors();
            _hex.SelectionChanged += Hex_SelectionChanged;

            _modeComboBox.SelectionChanged += ModeChanged;
            _modeComboBox.SelectedIndex = 0;

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

        private void ModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
                return;

            if (e.AddedItems[0] is ComboBoxItem item)
            {
                CollapseClients();

                var mode = item.Content as string;

                switch (mode)
                {
                    case "RGB":

                        SetRgbVisible(true);

                        break;
                }
            }
        }

        private void CollapseClients()
        {
            SetRgbVisible(false);
        }

        private void SetRgbVisible(bool isVisible)
        {
            _rSlider.IsVisible = isVisible;
            _rNumUpDown.IsVisible = isVisible;

            _gSlider.IsVisible = isVisible;
            _gNumUpDown.IsVisible = isVisible;

            _bSlider.IsVisible = isVisible;
            _bNumUpDown.IsVisible = isVisible;
        }

        #endregion
    }
}