using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;

namespace Egorozh.ColorPicker.Dialog
{
    public class FluentColorPickerTheme : IStyle, IResourceProvider
    {
        #region Private Fields

        private readonly Uri _baseUri;
        private IStyle[]? _loaded;
        private bool _isLoading;

        #endregion

        #region Public Properties

        public IReadOnlyList<IStyle> Children => _loaded ?? Array.Empty<IStyle>();

        bool IResourceNode.HasResources => (Loaded as IResourceProvider)?.HasResources ?? false;

        public IResourceHost? Owner => (Loaded as IResourceProvider)?.Owner;

        /// <summary>
        /// Gets or sets the mode of the fluent theme (light, dark).
        /// </summary>
        public FluentThemeMode Mode { get; set; }

        /// <summary>
        /// Gets the loaded style.
        /// </summary>
        public IStyle Loaded
        {
            get
            {
                if (_loaded == null)
                {
                    _isLoading = true;
                    var loaded = (IStyle)AvaloniaXamlLoader.Load(GetUri(), _baseUri);
                    _loaded = new[] { loaded };
                    _isLoading = false;
                }

                return _loaded?[0]!;
            }
        }

        #endregion

        #region Events

        public event EventHandler OwnerChanged
        {
            add
            {
                if (Loaded is IResourceProvider rp) 
                    rp.OwnerChanged += value;
            }
            remove
            {
                if (Loaded is IResourceProvider rp)
                    rp.OwnerChanged -= value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentColorPickerTheme"/> class.
        /// </summary>
        /// <param name="baseUri">The base URL for the XAML context.</param>
        public FluentColorPickerTheme(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentColorPickerTheme"/> class.
        /// </summary>
        /// <param name="serviceProvider">The XAML service provider.</param>
        public FluentColorPickerTheme(IServiceProvider serviceProvider)
        {
            _baseUri = ((IUriContext)serviceProvider.GetService(typeof(IUriContext))).BaseUri;
        }

        #endregion

        #region Public Methods

        public SelectorMatchResult TryAttach(IStyleable target, IStyleHost? host) => Loaded.TryAttach(target, host);

        public bool TryGetResource(object key, out object? value)
        {
            if (!_isLoading && Loaded is IResourceProvider p)
                return p.TryGetResource(key, out value);

            value = null;
            return false;
        }

        public void AddOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.AddOwner(owner);
        public void RemoveOwner(IResourceHost owner) => (Loaded as IResourceProvider)?.RemoveOwner(owner);

        #endregion

        #region Protected Methods
        
        protected virtual Uri GetUri() => Mode switch
        {
            FluentThemeMode.Dark => new Uri("avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/FluentDark.axaml", UriKind.Absolute),
            _ => new Uri("avares://Egorozh.ColorPicker.Avalonia.Dialog/Themes/FluentLight.axaml", UriKind.Absolute),
        };

        #endregion
        
    }
}