[![Nuget (with prereleases)](https://img.shields.io/nuget/v/Egorozh.ColorPicker.Avalonia.Dialog?label=avalonia-nuget&style=plastic)](https://www.nuget.org/packages/Egorozh.ColorPicker.Avalonia.Dialog/) [![Nuget (with prereleases)](https://img.shields.io/nuget/v/Egorozh.ColorPicker.WPF.Dialog?label=wpf-nuget&style=plastic)](https://www.nuget.org/packages/Egorozh.ColorPicker.WPF.Dialog/) [![Nuget (with prereleases)](https://img.shields.io/nuget/v/Egorozh.ColorPicker.WPF.Dialog.MahApps?label=wpf-mahapps-nuget&style=plastic)](https://www.nuget.org/packages/Egorozh.ColorPicker.WPF.Dialog.MahApps/)

# Egorozh.ColorPicker

## AvaloniaUI ColorPicker:
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-avalonia-1.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-avalonia-2.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-avalonia-3.png "Пример диалогого окна")

### AvaloniaUI  Getting Started

Install the library as a NuGet package:

```powershell
Install-Package Egorozh.ColorPicker.Avalonia.Dialog
# Or 'dotnet add package Egorozh.ColorPicker.Avalonia.Dialog'
```

Then, reference the preffered theme from your `App.xaml` file:

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:dialog="clr-namespace:Egorozh.ColorPicker.Dialog;assembly=Egorozh.ColorPicker.Avalonia.Dialog"
             x:Class="YourNamespace.App">
  <Application.Styles>  
      <StyleInclude Source="avares://Avalonia.Themes.Default/DefaultTheme.xaml"/>
      <StyleInclude Source="avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"/>
    
      <StyleInclude Source="avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/Default.axaml" />
    
      <!-- To use other themes:-->
      <!--
      <FluentTheme Mode="Light" />
      <dialog:FluentColorPickerTheme Mode="Light" />
      -->

      <!--
      <FluentTheme Mode="Dark" />
      <dialog:FluentColorPickerTheme Mode="Dark" />
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

## WPF ColorPicker:
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-1.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-2.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-3.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-4.png "Пример диалогого окна")
![example](https://github.com/egorozh/Egorozh.ColorPicker.WPF/blob//v2.0/images/example-wpf-mahapps-1.png "MahApps")

### WPF Getting Started

Install the library as a NuGet package:

```powershell
Install-Package Egorozh.ColorPicker.WPF.Dialog
# Or 'dotnet add package Egorozh.ColorPicker.WPF.Dialog'
```
Done! Use ColorPickerButton 
```xml
<dialog:ColorPickerButton Color="#99029344"
                          Cursor="Hand"/>
```
or ColorPickerDialog:
```c#
var dialog = new ColorPickerDialog
{
  Owner = Owner,
  Color = Color
};

var res = dialog.ShowDialog();

if (res == true)
  Color = dialog.Color;
```
### To run MahApps Version:
Install the library as a NuGet package:

```powershell
Install-Package Egorozh.ColorPicker.WPF.Dialog.MahApps
# Or 'dotnet add Egorozh.ColorPicker.WPF.Dialog.MahApps'
```
Then, reference the preffered theme from your `App.xaml` file:

```xml
<Application x:Class="Egorozh.ColorPicker.Client.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            
             StartupUri="MainWindow.xaml">

    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml" />
                <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml" />
                <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.Crimson.xaml" />


                <ResourceDictionary Source="pack://application:,,,/Egorozh.ColorPicker.WPF.Dialog.MahApps;component/Themes/Generic.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```
