using System.Drawing;


namespace Egorozh.ColorPicker;


public class RgbaColorNumericUpDown : LabelNumericUpDown
{
    #region Dependency Properties

    public static readonly StyledProperty<RgbaChannel> ChannelProperty =
        AvaloniaProperty.Register<RgbaColorNumericUpDown, RgbaChannel>(nameof(RgbaChannel));
    
    #endregion

    #region Public Properties

    public RgbaChannel Channel
    {
        get => GetValue(ChannelProperty);
        set => SetValue(ChannelProperty, value);
    }

    #endregion

    #region Constructor

    public RgbaColorNumericUpDown()
    {
        Minimum = 0;
        Maximum = 255;
    }

    #endregion

    #region Protected Methods

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == ChannelProperty)
        {
            ChannelChanged();
        }
    }
    
    protected override void UpdateColor(in Color color)
    {
        base.UpdateColor(in color);

        Value = Channel switch
        {
            RgbaChannel.Alpha => color.A,
            RgbaChannel.Red => color.R,
            RgbaChannel.Green => color.G,
            RgbaChannel.Blue => color.B,
            _ => 0
        };
    }

    protected override void OnValueChanged()
    {
        base.OnValueChanged();

        var color = ColorManager.CurrentColor;

        byte r = color.R;
        byte g = color.G;
        byte b = color.B;
        byte a = color.A;

        switch (Channel)
        {
            case RgbaChannel.Red:
                r = (byte)Value;
                break;
            case RgbaChannel.Green:
                g = (byte)Value;
                break;
            case RgbaChannel.Blue:
                b = (byte)Value;
                break;
            case RgbaChannel.Alpha:
                a = (byte)Value;
                break;
        }

        ColorManager.CurrentColor = Color.FromArgb(a, r, g, b);
    }

    #endregion

    #region Private Methods

    private void ChannelChanged()
    {
        if (ColorManager != null)
            UpdateColor(ColorManager.CurrentColor);
    }

    #endregion
}