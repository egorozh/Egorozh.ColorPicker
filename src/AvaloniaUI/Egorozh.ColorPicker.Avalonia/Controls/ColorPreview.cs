using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public class ColorPreview : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Fields

        private IColorManager _colorManager;
        private Border _previewBorder1;
        private Border _previewBorder2;
        private Border _previewBorder3;
        private Border _previewBorder4;

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<global::Avalonia.Media.Color> ColorProperty =
            AvaloniaProperty.Register<ColorPreview, global::Avalonia.Media.Color>(nameof(Color));

        #endregion

        #region Public Properties

        public global::Avalonia.Media.Color Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPreview);

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

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _previewBorder1 = e.NameScope.Find<Border>("PART_PreviewBorder1");
            _previewBorder2 = e.NameScope.Find<Border>("PART_PreviewBorder2");
            _previewBorder3 = e.NameScope.Find<Border>("PART_PreviewBorder3");
            _previewBorder4 = e.NameScope.Find<Border>("PART_PreviewBorder4");

            _previewBorder1.PointerPressed += PreviewBorder_PointerPressed;
            _previewBorder2.PointerPressed += PreviewBorder_PointerPressed;
            _previewBorder3.PointerPressed += PreviewBorder_PointerPressed;
            _previewBorder4.PointerPressed += PreviewBorder_PointerPressed;

            Color = _colorManager.CurrentColor.ToColor();
        }

        #endregion

        #region Private Methods

        private void PreviewBorder_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (sender is Border previewBorder && previewBorder.Background is SolidColorBrush brush)
                _colorManager.CurrentColor = brush.Color.ToColor();
        }

        #endregion
    }
}