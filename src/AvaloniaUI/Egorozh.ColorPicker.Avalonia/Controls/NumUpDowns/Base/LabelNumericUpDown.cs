using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public class LabelNumericUpDown : NumericUpDown, IStyleable, IColorClient
    {
        #region Protected Properties

        protected IColorManager? ColorManager { get; private set; }

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(LabelNumericUpDown);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<string> LabelProperty =
            AvaloniaProperty.Register<LabelNumericUpDown, string>(nameof(Label));

        #endregion

        #region Public Properties

        public string Label
        {
            get => GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        #endregion

        #region Constructor

        public LabelNumericUpDown()
        {
            PropertyChanged += ColorSlider_PropertyChanged;
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

            ColorUpdated(ColorManager.CurrentColor, this);
        }

        protected virtual void OnValueChanged()
        {
        }   

        protected virtual void UpdateColor(in Color color)
        {
        }

        #endregion

        #region Private Methods

        private void ColorSlider_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == ValueProperty)
                OnValueChanged();
        }

        #endregion
    }
}