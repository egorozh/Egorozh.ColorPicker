using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public abstract class ColorSlider : Slider, IStyleable, IColorClient
    {
        #region Protected Properties

        protected bool UpdateBackgroundWhenColorUpdated = true;

        protected IColorManager? ColorManager { get; private set; }

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

            if (UpdateBackgroundWhenColorUpdated)
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

            if (ColorManager != null)
            {
                UpdateColor(ColorManager.CurrentColor);
                Background = CreateBackgroundBrush(ColorManager.CurrentColor);
            }

            PropertyChanged += ColorSlider_PropertyChanged;
        }

        protected virtual void ValueChanged()
        {
        }

        protected virtual void UpdateColor(in Color color)
        {
        }

        protected virtual List<Color> CreateBackgroundColors(in Color color) =>
            new()
            {
                Color.Transparent
            };

        protected virtual IBrush CreateBackgroundBrush(in Color color)
        {
            var colors = CreateBackgroundColors(color);

            IBrush brush;

            var count = colors.Count;

            if (count > 1)
            {
                GradientStops gradStops = new()
                {
                    new(colors[0].ToColor(), 0)
                };

                for (var i = 1; i < count - 1; i++)
                {
                    var offset = 1.0D / count * i;

                    gradStops.Add(new(colors[i].ToColor(), offset));
                }

                gradStops.Add(new(colors[^1].ToColor(), 1));

                var start = Orientation == Orientation.Vertical
                    ? new RelativePoint(0, 1, RelativeUnit.Relative)
                    : new RelativePoint(0, 0, RelativeUnit.Relative);

                var end = Orientation == Orientation.Vertical
                    ? new RelativePoint(0, 0, RelativeUnit.Relative)
                    : new RelativePoint(1, 0, RelativeUnit.Relative);

                brush = new LinearGradientBrush
                {
                    GradientStops = gradStops,
                    StartPoint = start,
                    EndPoint = end
                };
            }
            else if (count == 1)
                brush = new SolidColorBrush(colors[0].ToColor());
            else
                brush = Brushes.Transparent;

            return brush;
        }

        #endregion

        #region Private Methods

        private void ColorSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ValueProperty)
                ValueChanged();
        }

        #endregion
    }
}