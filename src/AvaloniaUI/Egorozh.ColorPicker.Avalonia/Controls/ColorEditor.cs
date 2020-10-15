using System;
using System.Drawing;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;

namespace Egorozh.ColorPicker.Avalonia
{
    public class ColorEditor : TemplatedControl, IStyleable, IColorClient
    {
        #region Private Methods

        private IColorManager _manager;

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
            
            var alphaSlider = e.NameScope.Find<RgbaColorSlider>("PART_AlphaSlider");
            
            _manager.AddClient(alphaSlider);
        }

        #endregion
    }
}