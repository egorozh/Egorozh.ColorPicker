using System;
using System.Drawing;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;

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

            PropertyChanged += ColorSlider_PropertyChanged;
        }

        protected virtual void ValueChanged()
        {
        }

        protected virtual void UpdateColor(in Color color)
        {
        }

        #endregion

        #region Private Methods

        private void ColorSlider_PropertyChanged(object sender, global::Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ValueProperty)
                ValueChanged();
        }

        #endregion
    }
}