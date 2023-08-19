using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;


namespace Egorozh.ColorPicker.Dialog;


public class FluentTheme : Styles, IResourceNode
{
    public FluentTheme(IServiceProvider? sp = null)
    {
        AvaloniaXamlLoader.Load(sp, this);
    }
}