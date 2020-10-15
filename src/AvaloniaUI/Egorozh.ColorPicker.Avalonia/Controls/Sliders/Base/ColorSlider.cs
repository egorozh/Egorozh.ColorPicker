using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Layout;
using Avalonia.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker.Avalonia
{
    public abstract class ColorSlider : Slider, IStyleable, IColorClient
    {
        #region Protected Properties

        protected IColorManager ColorManager { get; private set; }

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorSlider);

        #endregion

        #region Constructor

        protected ColorSlider()
        {
            TickFrequency = 1;
        }

        #endregion

        #region Public Methods

        public virtual void ColorUpdated(Color color, IColorClient client)
        {
            PropertyChanged -= ColorSlider_PropertyChanged;

            UpdateColor(color);

            Background = CreateBackgroundBrush(color);

            PropertyChanged += ColorSlider_PropertyChanged;
        }

        public void Init(IColorManager colorManager)
        {
            ColorManager = colorManager;
        }

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            UpdateColor(ColorManager.CurrentColor);
            Background = CreateBackgroundBrush(ColorManager.CurrentColor);

            PropertyChanged += ColorSlider_PropertyChanged;
        }

        protected virtual void ValueChanged()
        {
        }

        protected virtual void UpdateColor(in Color color)
        {
        }

        protected virtual List<Color> CreateBackgroundColors(in Color color)
        {
            return new List<Color>()
            {
                Color.Transparent
            };
        }

        #endregion

        #region Private Methods

        private IBrush CreateBackgroundBrush(in Color color)
        {
            var colors = CreateBackgroundColors(color);
            var count = colors?.Count ?? 0;

            IBrush brush;

            var start = Orientation == Orientation.Vertical
                ? new RelativePoint(0, 1, RelativeUnit.Relative)
                : new RelativePoint(0, 0, RelativeUnit.Relative);

            var end = Orientation == Orientation.Vertical
                ? new RelativePoint(0, 0, RelativeUnit.Relative)
                : new RelativePoint(1, 0, RelativeUnit.Relative);

            if (colors != null && count > 0)
            {
                var gradStops = new GradientStops();

                for (var i = 0; i < colors.Count; i++)
                {
                    var offset = i == 0 ? 0 : i == count - 1 ? 1 : 1.0D / count * i;
                    gradStops.Add(new GradientStop(colors[i].ToColor(), offset));
                }

                brush = new LinearGradientBrush()
                {
                    GradientStops = gradStops,
                    StartPoint = start,
                    EndPoint = end
                };
            }
            else
                brush = Brushes.Transparent;

            return brush;
        }

        private void ColorSlider_PropertyChanged(object sender, global::Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ValueProperty)
                ValueChanged();
        }

        #endregion
    }
}