using System.Drawing;
using TextCopy;

namespace Egorozh.ColorPicker;

public class ColorHexComboBox : ComboBox, IStyleable, IColorClient
{
    #region Private Fields

    private IColorManager _manager;

    #endregion

    #region IStyleable

    Type IStyleable.StyleKey => typeof(ColorHexComboBox);

    #endregion

    #region Public Methods

    public void ColorUpdated(Color color, IColorClient client)
    {
        SetSelectedItemInHex(color);
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

        ItemsSource = HexComboBoxHelpers.GetNamedColors();
        SelectionChanged += Hex_SelectionChanged;

        var copyButton = e.NameScope.Find<Button>("PART_CopyButton");
        copyButton.Click += CopyButtonOnClick;


        SetSelectedItemInHex(_manager.CurrentColor);
    }

    #endregion

    #region Private Methods

    private void Hex_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            if (e.AddedItems[0] is NamedColor namedColor)
                _manager.CurrentColor = namedColor.Color;
        }
    }

    private void CopyButtonOnClick(object? sender, RoutedEventArgs e)
    {
        var currentColor = _manager.CurrentColor;

        var hexColor = $"{currentColor.A:X2}{currentColor.R:X2}{currentColor.G:X2}{currentColor.B:X2}";

        ClipboardService.SetText(hexColor);
    }

    private void SetSelectedItemInHex(Color color)
    {
        var colors = (List<NamedColor>)ItemsSource;

        var namedColor = colors.FirstOrDefault(c => c.Color.A == color.A &&
                                                    c.Color.R == color.R &&
                                                    c.Color.G == color.G &&
                                                    c.Color.B == color.B);

        if (namedColor != null)
        {
            SelectedItem = namedColor;
        }
        else
        {
            SelectedItem = null;
            PlaceholderText = $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }

    #endregion
}