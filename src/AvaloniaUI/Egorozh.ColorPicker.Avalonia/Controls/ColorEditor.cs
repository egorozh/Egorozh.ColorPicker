using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class ColorEditor : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Methods

        private IColorManager _manager;
     
        private ListBox _modeComboBox;

        private RgbaColorSlider _rSlider;
        private RgbaColorNumericUpDown _rNumUpDown;
        private RgbaColorSlider _gSlider;
        private RgbaColorNumericUpDown _gNumUpDown;
        private RgbaColorSlider _bSlider;
        private RgbaColorNumericUpDown _bNumUpDown;

        private HueColorSlider _hSlider;
        private HueColorNumUpDown _hNumUpDown;
        private SaturationColorSlider _sSlider;
        private ValueColorSlider _vSlider;
        private ValueColorNumUpDown _vNumUpDown;
        private SaturationColorNumUpDown _sNumUpDown;

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorEditor);

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
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

            var  hex = e.NameScope.Find<ColorHexComboBox>("PART_HexComboBox");
            _modeComboBox = e.NameScope.Find<ListBox>("PART_ModeComboBox");

            var alphaSlider = e.NameScope.Find<RgbaColorSlider>("PART_AlphaSlider");
            var alphaNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_AlphaNumUpDown");

            _rSlider = e.NameScope.Find<RgbaColorSlider>("PART_RSlider");
            _rNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_RNumUpDown");

            _gSlider = e.NameScope.Find<RgbaColorSlider>("PART_GSlider");
            _gNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_GNumUpDown");

            _bSlider = e.NameScope.Find<RgbaColorSlider>("PART_BSlider");
            _bNumUpDown = e.NameScope.Find<RgbaColorNumericUpDown>("PART_BNumUpDown");

            _hSlider = e.NameScope.Find<HueColorSlider>("PART_HSlider");
            _hNumUpDown = e.NameScope.Find<HueColorNumUpDown>("PART_HNumUpDown");

            _sSlider = e.NameScope.Find<SaturationColorSlider>("PART_SSlider");
            _sNumUpDown = e.NameScope.Find<SaturationColorNumUpDown>("PART_SNumUpDown");

            _vSlider = e.NameScope.Find<ValueColorSlider>("PART_VSlider");
            _vNumUpDown = e.NameScope.Find<ValueColorNumUpDown>("PART_VNumUpDown");
            
            _manager.AddClient(alphaSlider, alphaNumUpDown, hex,
                _rSlider, _rNumUpDown,
                _gSlider, _gNumUpDown,
                _bSlider, _bNumUpDown,
                _hSlider, _hNumUpDown,
                _sSlider, _sNumUpDown,
                _vSlider, _vNumUpDown);


            _modeComboBox.SelectionChanged += ModeChanged;
            _modeComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Private Methods
        
        private void ModeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
                return;

            if (e.AddedItems[0] is ListBoxItem item)
            {
                CollapseClients();

                var mode = item.Content as string;

                switch (mode)
                {
                    case "RGB":

                        SetRgbVisible(true);

                        break;
                    case "HSV":

                        SetHsvVisible(true);

                        break;
                }
            }
        }

        private void CollapseClients()
        {
            SetRgbVisible(false);
            SetHsvVisible(false);
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

        private void SetHsvVisible(bool isVisible)
        {
            _hSlider.IsVisible = isVisible;
            _hNumUpDown.IsVisible = isVisible;

            _sSlider.IsVisible = isVisible;
            _sNumUpDown.IsVisible = isVisible;

            _vSlider.IsVisible = isVisible;
            _vNumUpDown.IsVisible = isVisible;
        }

        #endregion
    }
}