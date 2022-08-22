using System.Drawing;

namespace Egorozh.ColorPicker;

public class ColorManager : IColorManager
{
    #region Private Fields

    private readonly List<IColorClient> _colorClients = new ();
    private Color _currentColor;

    #endregion

    #region Events

    public event Action<Color>? ColorChanged;

    #endregion

    #region Public Properties

    public Color CurrentColor
    {
        get => _currentColor;
        set => UpdateClients(value);
    }

    #endregion

    #region Public Methods

    public void AddClient(params IColorClient?[] clients)
    {
        foreach (var client in clients)
        {
            if (client == null)
                continue;

            _colorClients.Add(client);
            client.Init(this);
        }
    }   

    public void SetColorFromHsl(double hue, double saturation, double lightness)
    {
        var alpha = CurrentColor.A;

        var hsl = new HslColor(alpha, hue, saturation, lightness);
            
        CurrentColor = hsl.ToRgbColor();
    }

    #endregion

    #region Private Methods

    private void UpdateClients(Color color)
    {
        _currentColor = color;

        foreach (var colorClient in _colorClients)
            colorClient.ColorUpdated(color, colorClient);

        ColorChanged?.Invoke(color);
    }

    #endregion
}