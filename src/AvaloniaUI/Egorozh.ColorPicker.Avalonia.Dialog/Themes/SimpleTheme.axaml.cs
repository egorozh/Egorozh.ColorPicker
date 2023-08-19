using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace Egorozh.ColorPicker.Dialog;

public class SimpleTheme : Styles, IResourceNode
{
    public SimpleTheme(IServiceProvider? sp = null)
    {
        AvaloniaXamlLoader.Load(sp, this);
    }
}