using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    [TemplatePart(Name = PART_PreviewBorder1, Type = typeof(Border))]
    [TemplatePart(Name = PART_PreviewBorder2, Type = typeof(Border))]
    [TemplatePart(Name = PART_PreviewBorder3, Type = typeof(Border))]
    [TemplatePart(Name = PART_PreviewBorder4, Type = typeof(Border))]
    public class ColorPreview : Control, IColorClient
    {
        #region Private Fields

        private const string PART_PreviewBorder1 = "PART_PreviewBorder1";
        private const string PART_PreviewBorder2 = "PART_PreviewBorder2";
        private const string PART_PreviewBorder3 = "PART_PreviewBorder3";
        private const string PART_PreviewBorder4 = "PART_PreviewBorder4";

        private IColorManager? _colorManager;
        private Border? _previewBorder1;
        private Border? _previewBorder2;
        private Border? _previewBorder3;
        private Border? _previewBorder4;

        #endregion

        #region Static Constructor

        static ColorPreview()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPreview),
                new FrameworkPropertyMetadata(typeof(ColorPreview)));
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(System.Windows.Media.Color), typeof(ColorPreview),
            new PropertyMetadata(default(System.Windows.Media.Color)));

        #endregion

        #region Public Properties

        public System.Windows.Media.Color Color
        {
            get => (System.Windows.Media.Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
            Color = color.ToColor();
        }

        public void Init(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _previewBorder1 = GetTemplateChild(PART_PreviewBorder1) as Border;
            _previewBorder2 = GetTemplateChild(PART_PreviewBorder2) as Border;
            _previewBorder3 = GetTemplateChild(PART_PreviewBorder3) as Border;
            _previewBorder4 = GetTemplateChild(PART_PreviewBorder4) as Border;

            _previewBorder1.MouseLeftButtonDown += PreviewBorderOnMouseLeftButtonDown;
            _previewBorder2.MouseLeftButtonDown += PreviewBorderOnMouseLeftButtonDown;
            _previewBorder3.MouseLeftButtonDown += PreviewBorderOnMouseLeftButtonDown;
            _previewBorder4.MouseLeftButtonDown += PreviewBorderOnMouseLeftButtonDown;

            Color = _colorManager.CurrentColor.ToColor();
        }

        #endregion

        #region Private Methods

        private void PreviewBorderOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border previewBorder && previewBorder.Background is SolidColorBrush brush)
                _colorManager.CurrentColor = brush.Color.ToColor();
        }

        #endregion
    }
}