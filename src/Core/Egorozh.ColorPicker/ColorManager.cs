using System.Collections.Generic;
using System.Drawing;

namespace Egorozh.ColorPicker
{
    public class ColorManager
    {
        #region Private Fields

        private readonly List<IColorClient> _colorClients = new List<IColorClient>();

        #endregion

        #region Public Properties

        public Color CurrentColor { get; set; }

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
            CurrentColor = color;

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