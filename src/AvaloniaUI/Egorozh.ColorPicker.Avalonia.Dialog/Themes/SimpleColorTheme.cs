using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace Egorozh.ColorPicker.Dialog;

public class SimpleColorTheme : AvaloniaObject, IStyle, IResourceProvider
{
    public static readonly StyledProperty<SimpleColorThemeMode> ModeProperty =
        AvaloniaProperty.Register<SimpleColorTheme, SimpleColorThemeMode>(nameof(Mode));

    private readonly Uri _baseUri;
    private bool _isLoading;
    private IStyle? _loaded;
    private Styles _sharedStyles = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleTheme"/> class.
    /// </summary>
    /// <param name="baseUri">The base URL for the XAML context.</param>
    public SimpleColorTheme(Uri? baseUri = null)
    {
        _baseUri = baseUri ?? new Uri("avares://Avalonia.Themes.Simple/");
        InitStyles(_baseUri);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleTheme"/> class.
    /// </summary>
    /// <param name="serviceProvider">The XAML service provider.</param>
    public SimpleColorTheme(IServiceProvider serviceProvider)
    {
        var service = serviceProvider.GetService(typeof(IUriContext));
        if (service == null)
        {
            throw new Exception("There is no service object of type IUriContext!");
        }

        _baseUri = ((IUriContext) service).BaseUri;
        InitStyles(_baseUri);
    }

    public event EventHandler? OwnerChanged
    {
        add
        {
            if (Loaded is IResourceProvider rp)
            {
                rp.OwnerChanged += value;
            }
        }
        remove
        {
            if (Loaded is IResourceProvider rp)
            {
                rp.OwnerChanged -= value;
            }
        }
    }

    IReadOnlyList<IStyle> IStyle.Children => _loaded?.Children ?? Array.Empty<IStyle>();

    bool IResourceNode.HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;

    public IStyle Loaded
    {
        get
        {
            if (_loaded == null)
            {
                _isLoading = true;

                if (Mode == SimpleColorThemeMode.Light)
                {
                    _loaded = new Styles {_sharedStyles};
                }
                else if (Mode == SimpleColorThemeMode.Dark)
                {
                    _loaded = new Styles {_sharedStyles};
                }

                _isLoading = false;
            }

            return _loaded!;
        }
    }

    /// <summary>
    /// Gets or sets the mode of the fluent theme (light, dark).
    /// </summary>
    public SimpleColorThemeMode Mode
    {
        get => GetValue(ModeProperty);
        set => SetValue(ModeProperty, value);
    }

    public IResourceHost? Owner => (Loaded as IResourceProvider)?.Owner;

    void IResourceProvider.AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);

    void IResourceProvider.RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);

    public SelectorMatchResult TryAttach(IStyleable target, object? host) => Loaded.TryAttach(target, host);

    public bool TryGetResource(object key, out object? value)
    {
        if (!_isLoading && Loaded is IResourceProvider p)
        {
            return p.TryGetResource(key, out value);
        }

        value = null;
        return false;
    }
    
    private void InitStyles(Uri baseUri)
    {
        _sharedStyles = new Styles
        {
            new StyleInclude(baseUri)
            {
                Source = new Uri("avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/Default.axaml")
            }
        };
    }
    
    public enum SimpleColorThemeMode
    {
        Dark,
        Light
    }
}