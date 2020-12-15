# Egorozh.ColorPicker

[![NuGet version (Egorozh.ColorPicker.WPF)](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob/v2.0/shield.svg)](https://www.nuget.org/packages/Egorozh.ColorPicker.Avalonia.Dialog/)

This is an attempt to port [Cyotek.Windows.Forms.ColorPicker](https://github.com/cyotek/Cyotek.Windows.Forms.ColorPicker "Cyotek.Windows.Forms.ColorPicker") from Windows Forms to WPF and AvaloniaUI.

## AvaloniaUI ColorPicker:
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-avalonia-1.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-avalonia-2.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-avalonia-3.png "Пример диалогого окна")

### Getting Started

Install the library as a NuGet package:

```powershell
Install-Package Egorozh.ColorPicker.Avalonia.Dialog
# Or 'dotnet add package Egorozh.ColorPicker.Avalonia.Dialog'
```

Then, reference the preffered theme from your `App.xaml` file:

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="YourNamespace.App">
  <Application.Styles>  
      <StyleInclude Source="avares://Avalonia.Themes.Default/DefaultTheme.xaml"/>
      <StyleInclude Source="avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"/>
    
      <StyleInclude Source="avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/Default.axaml" />
    
     <!-- To use other themes:-->
        <!--
      <StyleInclude Source="avares://Avalonia.Themes.Fluent/Accents/FluentDark.xaml"/>
      <StyleInclude Source="avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/FluentDark.axaml" />
    -->
      <!--
      <StyleInclude Source="avares://Avalonia.Themes.Fluent/Accents/FluentLight.xaml"/>
      <StyleInclude Source="avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/FluentLight.axaml" />
    -->
  </Application.Styles>
</Application>
```
Done! Use ColorPickerButton 
```xml
<dialog:ColorPickerButton Color="#99029344"
                          Cursor="Hand"/>
```
or ColorPickerDialog:
```c#
ColorPickerDialog dialog = new ()
{
  Color = _color
};

var res = await dialog.ShowDialog<bool>(Owner);

if (res)
  _color = dialog.Color;
```

## WPF ColorPicker (Not ready yet. Will be rewritten):
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-1.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-2.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-3.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-4.png "Пример диалогого окна")
