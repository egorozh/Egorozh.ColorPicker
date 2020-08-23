using System.Collections.Generic;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class ColorManager
    {
        #region Private Fields

        private readonly List<IColorClient> _colorClients = new List<IColorClient>();
        private Color _currentColor;

        #endregion

        #region Public Properties

        public Color CurrentColor
        {
            get => _currentColor;
            set
            {
                _currentColor = value;
                Client_ColorChanged(_currentColor);
            }
        }

        #endregion

        #region Public Methods

        public void AddClient(IColorClient client)
        {
            client.ColorChanged += Client_ColorChanged;

            _colorClients.Add(client);
        }

        #endregion

        #region Private Methods

        private void Client_ColorChanged(Color color)
        {
            _currentColor = color;

            UpdateClients(color);
        }

        private void UpdateClients(Color color)
        {
            foreach (var colorClient in _colorClients)
                colorClient.ColorUpdated(color, colorClient);
        }

        #endregion
    }
}