using System.Drawing;
using System.Windows;

namespace Egorozh.ColorPicker
{
    public class LabelNumericUpDown : NumericUpDown, IColorClient
    {
        #region Protected Properties

        protected IColorManager? ColorManager { get; private set; }

        #endregion

        #region Static Constructor

        static LabelNumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelNumericUpDown),
                new FrameworkPropertyMetadata(typeof(LabelNumericUpDown)));
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label), typeof(string), typeof(LabelNumericUpDown),
            new PropertyMetadata(default(string)));

        #endregion

        #region Public Properties

        public string Label
        {
            get => (string) GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        #endregion

        #region Public Methods

        public virtual void ColorUpdated(Color color, IColorClient client)
        {
            ValueChanged -= ColorSlider_PropertyChanged;

            UpdateColor(color);

            ValueChanged += ColorSlider_PropertyChanged;
        }

        public void Init(IColorManager colorManager)
        {
            ColorManager = colorManager;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateColor(ColorManager.CurrentColor);

            ValueChanged += ColorSlider_PropertyChanged;
        }

        #endregion

        #region Protected Methods

        protected virtual void OnValueChanged()
        {
        }

        protected virtual void UpdateColor(in Color color)
        {
        }

        #endregion

        #region Private Methods

        private void ColorSlider_PropertyChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            OnValueChanged();
        }

        #endregion
    }
}