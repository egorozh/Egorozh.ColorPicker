using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Egorozh.ColorPicker
{
    public class ColorPalette : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Fields

        private IColorManager _colorManager;

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPalette);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<ObservableCollection<WrapItemViewModel>> ItemsProperty =
            AvaloniaProperty.Register<ColorPreview, ObservableCollection<WrapItemViewModel>>(nameof(Items),
                new ObservableCollection<WrapItemViewModel>());

        #endregion

        #region Public Properties

        public ObservableCollection<WrapItemViewModel> Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        #endregion

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            InitPalette(ColorPalettes.PaintPalette.Select(c => new ColorItemViewModel(c.ToColor())));
        }

        private void InitPalette(IEnumerable<ColorItemViewModel> items)
        {   
            Items.Clear();

            foreach (var vm in items)
                Items.Add(vm);

            Items.Add(new AddItemViewModel());
        }
    }
}