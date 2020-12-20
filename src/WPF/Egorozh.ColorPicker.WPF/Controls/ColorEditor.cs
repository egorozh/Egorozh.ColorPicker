using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Egorozh.ColorPicker
{
    [TemplatePart(Name = PART_HexComboBox, Type = typeof(ColorHexComboBox))]
    [TemplatePart(Name = PART_ModeComboBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_AlphaSlider, Type = typeof(RgbaColorSlider))]
    [TemplatePart(Name = PART_AlphaNumUpDown, Type = typeof(RgbaColorNumericUpDown))]
    [TemplatePart(Name = PART_RSlider, Type = typeof(RgbaColorSlider))]
    [TemplatePart(Name = PART_RNumUpDown, Type = typeof(RgbaColorNumericUpDown))]
    [TemplatePart(Name = PART_GSlider, Type = typeof(RgbaColorSlider))]
    [TemplatePart(Name = PART_GNumUpDown, Type = typeof(RgbaColorNumericUpDown))]
    [TemplatePart(Name = PART_BSlider, Type = typeof(RgbaColorSlider))]
    [TemplatePart(Name = PART_BNumUpDown, Type = typeof(RgbaColorNumericUpDown))]
    [TemplatePart(Name = PART_HSlider, Type = typeof(HueColorSlider))]
    [TemplatePart(Name = PART_HNumUpDown, Type = typeof(HueColorNumUpDown))]
    [TemplatePart(Name = PART_SSlider, Type = typeof(SaturationColorSlider))]
    [TemplatePart(Name = PART_SNumUpDown, Type = typeof(SaturationColorNumUpDown))]
    [TemplatePart(Name = PART_VSlider, Type = typeof(ValueColorSlider))]
    [TemplatePart(Name = PART_VNumUpDown, Type = typeof(ValueColorNumUpDown))]
    public class ColorEditor : Control, IColorClient
    {
        #region Private Fields

        private const string PART_HexComboBox = "PART_HexComboBox";
        private const string PART_ModeComboBox = "PART_ModeComboBox";

        private const string PART_AlphaSlider = "PART_AlphaSlider";
        private const string PART_AlphaNumUpDown = "PART_AlphaNumUpDown";


        private const string PART_RSlider = "PART_RSlider";
        private const string PART_RNumUpDown = "PART_RNumUpDown";

        private const string PART_GSlider = "PART_GSlider";
        private const string PART_GNumUpDown = "PART_GNumUpDown";

        private const string PART_BSlider = "PART_BSlider";
        private const string PART_BNumUpDown = "PART_BNumUpDown";

        private const string PART_HSlider = "PART_HSlider";
        private const string PART_HNumUpDown = "PART_HNumUpDown";

        private const string PART_SSlider = "PART_SSlider";
        private const string PART_SNumUpDown = "PART_SNumUpDown";

        private const string PART_VSlider = "PART_VSlider";
        private const string PART_VNumUpDown = "PART_VNumUpDown";

        private IColorManager? _manager;

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

        #region Static Constructor

        static ColorEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorEditor),
                new FrameworkPropertyMetadata(typeof(ColorEditor)));
        }

        #endregion

        #region Dependency Properties

        #region Styling

        public static readonly DependencyProperty LabelNumericUpDownStyleProperty = DependencyProperty.Register(
            nameof(LabelNumericUpDownStyle), typeof(Style), typeof(ColorEditor),
            new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty ModeListBoxStyleProperty = DependencyProperty.Register(
            nameof(ModeListBoxStyle), typeof(Style), typeof(ColorEditor), new PropertyMetadata(default(Style)));

        public static readonly DependencyProperty ColorSliderStyleProperty = DependencyProperty.Register(
            nameof(ColorSliderStyle), typeof(Style), typeof(ColorEditor), new PropertyMetadata(default(Style)));

        #endregion

        #endregion

        #region Public Properties

        #region Styling

        public Style ModeListBoxStyle
        {
            get => (Style) GetValue(ModeListBoxStyleProperty);
            set => SetValue(ModeListBoxStyleProperty, value);
        }

        public Style LabelNumericUpDownStyle
        {
            get => (Style) GetValue(LabelNumericUpDownStyleProperty);
            set => SetValue(LabelNumericUpDownStyleProperty, value);
        }

        public Style ColorSliderStyle
        {
            get => (Style) GetValue(ColorSliderStyleProperty);
            set => SetValue(ColorSliderStyleProperty, value);
        }

        #endregion

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
            _manager = colorManager;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var hex = GetTemplateChild(PART_HexComboBox) as ColorHexComboBox;
            _modeComboBox = GetTemplateChild(PART_ModeComboBox) as ListBox;

            var alphaSlider = GetTemplateChild(PART_AlphaSlider) as RgbaColorSlider;
            var alphaNumUpDown = GetTemplateChild(PART_AlphaNumUpDown) as RgbaColorNumericUpDown;

            _rSlider = GetTemplateChild(PART_RSlider) as RgbaColorSlider;
            _rNumUpDown = GetTemplateChild(PART_RNumUpDown) as RgbaColorNumericUpDown;

            _gSlider = GetTemplateChild(PART_GSlider) as RgbaColorSlider;
            _gNumUpDown = GetTemplateChild(PART_GNumUpDown) as RgbaColorNumericUpDown;

            _bSlider = GetTemplateChild(PART_BSlider) as RgbaColorSlider;
            _bNumUpDown = GetTemplateChild(PART_BNumUpDown) as RgbaColorNumericUpDown;

            _hSlider = GetTemplateChild(PART_HSlider) as HueColorSlider;
            _hNumUpDown = GetTemplateChild(PART_HNumUpDown) as HueColorNumUpDown;

            _sSlider = GetTemplateChild(PART_SSlider) as SaturationColorSlider;
            _sNumUpDown = GetTemplateChild(PART_SNumUpDown) as SaturationColorNumUpDown;

            _vSlider = GetTemplateChild(PART_VSlider) as ValueColorSlider;
            _vNumUpDown = GetTemplateChild(PART_VNumUpDown) as ValueColorNumUpDown;

            _manager?.AddClient(alphaSlider, alphaNumUpDown, hex,
                _rSlider,
                _rNumUpDown,
                _gSlider,
                _gNumUpDown,
                _bSlider,
                _bNumUpDown,
                _hSlider,
                _hNumUpDown,
                _sSlider,
                _sNumUpDown,
                _vSlider,
                _vNumUpDown);


            _modeComboBox.SelectionChanged += ModeChanged;
            _modeComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Private Methods

        private void ModeChanged(object? sender, SelectionChangedEventArgs e)
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
            var visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;

            _rSlider.Visibility = visibility;
            _rNumUpDown.Visibility = visibility;

            _gSlider.Visibility = visibility;
            _gNumUpDown.Visibility = visibility;

            _bSlider.Visibility = visibility;
            _bNumUpDown.Visibility = visibility;
        }

        private void SetHsvVisible(bool isVisible)
        {
            var visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;

            _hSlider.Visibility = visibility;
            _hNumUpDown.Visibility = visibility;

            _sSlider.Visibility = visibility;
            _sNumUpDown.Visibility = visibility;

            _vSlider.Visibility = visibility;
            _vNumUpDown.Visibility = visibility;
        }

        #endregion
    }
}