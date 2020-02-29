using System;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    internal sealed class ColorEditorViewModel : BaseViewModel, IColorEditor
    {
        #region Private Fields

        private Color _color;

        #endregion

        #region Public Properties

        public Color Color
        {
            get => _color;
            set => SetColor(value);
        }

        #endregion

        #region Events

        public event EventHandler ColorChanged;

        #endregion

        #region Private Methods

        private void SetColor(Color value)
        {
            _color = value;
        }

        #endregion
    }
}