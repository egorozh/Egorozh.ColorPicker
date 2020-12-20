using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    [TemplatePart(Name = PART_AddMenuItem, Type = typeof(MenuItem))]
    [TemplatePart(Name = PART_RemoveMenuItem, Type = typeof(MenuItem))]
    [TemplatePart(Name = PART_LoadPaletteMenuItem, Type = typeof(MenuItem))]
    [TemplatePart(Name = PART_SavePaletteMenuItem, Type = typeof(MenuItem))]
    public class ColorPalette : ListBox, IColorClient
    {
        #region Private Fields

        private const string PART_AddMenuItem = "PART_AddMenuItem";
        private const string PART_RemoveMenuItem = "PART_RemoveMenuItem";
        private const string PART_LoadPaletteMenuItem = "PART_LoadPaletteMenuItem";
        private const string PART_SavePaletteMenuItem = "PART_SavePaletteMenuItem";

        private IColorManager? _colorManager;

        private MenuItem _addContextMenuItem;
        private MenuItem _removeItem;

        private MenuItem _loadPaletteContextMenuItem;
        private MenuItem _savePaletteContextMenuItem;

        private readonly ListBoxItem _addItem;
        private Color _prevColorForDoubleTappedHandler;

        #endregion

        #region Static Constructor

        static ColorPalette()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPalette),
                new FrameworkPropertyMetadata(typeof(ColorPalette)));
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty LoadPaletteHandlerProperty = DependencyProperty.Register(
            nameof(LoadPaletteHandler), typeof(LoadPaletteHandlerAsync), typeof(ColorPalette),
            new PropertyMetadata(default(LoadPaletteHandlerAsync)));

        public static readonly DependencyProperty SavePaletteHandlerProperty = DependencyProperty.Register(
            nameof(SavePaletteHandler), typeof(SavePaletteHandler), typeof(ColorPalette),
            new PropertyMetadata(default(SavePaletteHandler)));

        public static readonly DependencyProperty GetColorHandlerProperty = DependencyProperty.Register(
            nameof(GetColorHandler), typeof(GetColorHandler), typeof(ColorPalette),
            new PropertyMetadata(default(GetColorHandler)));

        public static readonly DependencyProperty ColorsProperty = DependencyProperty.Register(
            nameof(Colors), typeof(IEnumerable<System.Windows.Media.Color>), typeof(ColorPalette),
            new PropertyMetadata(default(IEnumerable<System.Windows.Media.Color>)));

        public static readonly DependencyProperty RemoveColorContextMenuTextProperty = DependencyProperty.Register(
            nameof(RemoveColorContextMenuText), typeof(string), typeof(ColorPalette),
            new PropertyMetadata("Remove color"));

        public static readonly DependencyProperty RemoveColorsContextMenuTextProperty = DependencyProperty.Register(
            nameof(RemoveColorsContextMenuText), typeof(string), typeof(ColorPalette),
            new PropertyMetadata("Remove colors"));

        public static readonly DependencyProperty AddColorContextMenuTextProperty = DependencyProperty.Register(
            nameof(AddColorContextMenuText), typeof(string), typeof(ColorPalette),
            new PropertyMetadata("Add color"));

        public static readonly DependencyProperty LoadPaletteContextMenuTextProperty = DependencyProperty.Register(
            nameof(LoadPaletteContextMenuText), typeof(string), typeof(ColorPalette),
            new PropertyMetadata("Load Palette"));

        public static readonly DependencyProperty SavePaletteContextMenuTextProperty = DependencyProperty.Register(
            nameof(SavePaletteContextMenuText), typeof(string), typeof(ColorPalette),
            new PropertyMetadata("Save Palette"));

        public static readonly DependencyProperty LoadPaletteIconTemplateProperty = DependencyProperty.Register(
            nameof(LoadPaletteIconTemplate), typeof(DataTemplate), typeof(ColorPalette),
            new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty SavePaletteIconTemplateProperty = DependencyProperty.Register(
            nameof(SavePaletteIconTemplate), typeof(DataTemplate), typeof(ColorPalette),
            new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty AddItemContainerStyleProperty = DependencyProperty.Register(
            "AddItemContainerStyle", typeof(Style), typeof(ColorPalette), new PropertyMetadata(default(Style),
                AddItemContainerStyleChanged));

        private static void AddItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPalette colorPalette)
                colorPalette._addItem.Style = colorPalette.AddItemContainerStyle;
        }

        #endregion

        #region Public Properties

        public LoadPaletteHandlerAsync? LoadPaletteHandler
        {
            get => (LoadPaletteHandlerAsync?) GetValue(LoadPaletteHandlerProperty);
            set => SetValue(LoadPaletteHandlerProperty, value);
        }

        public SavePaletteHandler? SavePaletteHandler
        {
            get => (SavePaletteHandler?) GetValue(SavePaletteHandlerProperty);
            set => SetValue(SavePaletteHandlerProperty, value);
        }

        public GetColorHandler? GetColorHandler
        {
            get => (GetColorHandler?) GetValue(GetColorHandlerProperty);
            set => SetValue(GetColorHandlerProperty, value);
        }

        public IEnumerable<System.Windows.Media.Color>? Colors
        {
            get => (IEnumerable<System.Windows.Media.Color>?) GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        public string RemoveColorContextMenuText
        {
            get => (string) GetValue(RemoveColorContextMenuTextProperty);
            set => SetValue(RemoveColorContextMenuTextProperty, value);
        }

        public string RemoveColorsContextMenuText
        {
            get => (string) GetValue(RemoveColorsContextMenuTextProperty);
            set => SetValue(RemoveColorsContextMenuTextProperty, value);
        }

        public string AddColorContextMenuText
        {
            get => (string) GetValue(AddColorContextMenuTextProperty);
            set => SetValue(AddColorContextMenuTextProperty, value);
        }

        public string LoadPaletteContextMenuText
        {
            get => (string) GetValue(LoadPaletteContextMenuTextProperty);
            set => SetValue(LoadPaletteContextMenuTextProperty, value);
        }

        public string SavePaletteContextMenuText
        {
            get => (string) GetValue(SavePaletteContextMenuTextProperty);
            set => SetValue(SavePaletteContextMenuTextProperty, value);
        }

        public DataTemplate LoadPaletteIconTemplate
        {
            get => (DataTemplate) GetValue(LoadPaletteIconTemplateProperty);
            set => SetValue(LoadPaletteIconTemplateProperty, value);
        }

        public DataTemplate SavePaletteIconTemplate
        {
            get => (DataTemplate) GetValue(SavePaletteIconTemplateProperty);
            set => SetValue(SavePaletteIconTemplateProperty, value);
        }

        public Style AddItemContainerStyle
        {
            get => (Style) GetValue(AddItemContainerStyleProperty);
            set => SetValue(AddItemContainerStyleProperty, value);
        }

        #endregion

        #region Constructor

        public ColorPalette()
        {
            _addItem = new ListBoxItem()
            {
                Style = AddItemContainerStyle,
            };

            SetBinding(_addItem, WidthProperty, "ActualWidth", new DivideDoubleToDoubleConverter(), 10);

            _addItem.PreviewMouseLeftButtonDown += AddItemOnMouseLeftButtonDown;

            SelectionChanged += OnSelectionChanged;
        }

        #endregion

        #region Public Methods

        public void ColorUpdated(Color color, IColorClient client)
        {
        }

        public void Init(IColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _addContextMenuItem = GetTemplateChild(PART_AddMenuItem) as MenuItem;
            _removeItem = GetTemplateChild(PART_RemoveMenuItem) as MenuItem;

            _loadPaletteContextMenuItem = GetTemplateChild(PART_LoadPaletteMenuItem) as MenuItem;
            _savePaletteContextMenuItem = GetTemplateChild(PART_SavePaletteMenuItem) as MenuItem;

            SetBinding(_addContextMenuItem, HeaderedItemsControl.HeaderProperty, nameof(AddColorContextMenuText));
            _removeItem.Header = RemoveColorContextMenuText;

            SetBinding(_loadPaletteContextMenuItem, HeaderedItemsControl.HeaderProperty,
                nameof(LoadPaletteContextMenuText));
            SetBinding(_savePaletteContextMenuItem, HeaderedItemsControl.HeaderProperty,
                nameof(SavePaletteContextMenuText));

            SetBinding(_loadPaletteContextMenuItem.Icon as ContentPresenter, ContentPresenter.ContentTemplateProperty,
                nameof(LoadPaletteIconTemplate));
            SetBinding(_savePaletteContextMenuItem.Icon as ContentPresenter, ContentPresenter.ContentTemplateProperty,
                nameof(SavePaletteIconTemplate));
            
            _removeItem.Click += RemoveItemOnClick;
            _addContextMenuItem.Click += AddItemOnClick;
            _loadPaletteContextMenuItem.Click += LoadPaletteContextMenuItemOnClick;
            _savePaletteContextMenuItem.Click += SavePaletteContextMenuItemOnClick;

            ColorsPropertyChanged();
        }

        #endregion

        #region Private Methods

        private void RemoveItemOnClick(object? sender, RoutedEventArgs e)
        {
            List<ListBoxItem> removedItems = new();

            foreach (var selectedItem in SelectedItems)
            {
                if (selectedItem is ListBoxItem removedItem && selectedItem != _addItem)
                    removedItems.Add(removedItem);
            }

            foreach (var removedItem in removedItems)
                RemoveItem(removedItem);

            UpdateColors();
        }

        private async void AddItemOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (GetColorHandler == null)
                return;

            var (success, newColor) = await GetColorHandler.Invoke(System.Windows.Media.Colors.Red);

            if (!success)
                return;

            Items.Insert(Items.Count - 1, CreateColorItem(newColor));

            UpdateColors();
        }

        private void ColorsPropertyChanged()
        {
            if (Colors == null)
                return;

            Items.Clear();

            foreach (var color in Colors)
                Items.Add(CreateColorItem(color));

            Items.Add(_addItem);
        }

        private async void AddItemOnClick(object? sender, RoutedEventArgs e)
        {
            if (GetColorHandler == null)
                return;

            var (success, newColor) = await GetColorHandler.Invoke(System.Windows.Media.Colors.Red);

            if (!success)
                return;

            var newColorItem = CreateColorItem(newColor);

            if (SelectedItems.Count > 0)
            {
                var selectedItem = SelectedItems[SelectedItems.Count-1] as ListBoxItem;

                if (selectedItem != _addItem)
                {
                    var index = Items.IndexOf(selectedItem);

                    Items.Insert(index, newColorItem);
                }
                else
                {
                    Items.Insert(Items.Count - 1, newColorItem);
                }
            }
            else
            {
                Items.Insert(Items.Count - 1, newColorItem);
            }

            UpdateColors();
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _removeItem.IsEnabled = SelectedItem != _addItem;

            _removeItem.Header = SelectedItems.Count > 1 ? RemoveColorsContextMenuText : RemoveColorContextMenuText;
        }

        private ListBoxItem CreateColorItem(System.Windows.Media.Color color)
        {
            ListBoxItem item = new()
            {
                Background = new SolidColorBrush(color)
            };

            item.PreviewMouseLeftButtonDown += ItemOnMouseLeftButtonDown;

            return item;
        }

        private void RemoveItem(ListBoxItem removedItem)
        {
            removedItem.PreviewMouseLeftButtonDown -= ItemOnMouseLeftButtonDown;

            Items.Remove(removedItem);
        }

        private async void ItemOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not ListBoxItem colorItem || _colorManager == null)
                return;

            if (e.ClickCount == 2)
            {
                if (GetColorHandler == null)
                    return;

                _colorManager.CurrentColor = _prevColorForDoubleTappedHandler;

                var color = GetColorFromItem(colorItem);

                var (success, newColor) = await GetColorHandler.Invoke(color);

                if (success)
                {
                    colorItem.Background = new SolidColorBrush(newColor);

                    UpdateColors();
                }
            }
            else
            {
                var color = GetColorFromItem(colorItem);

                _prevColorForDoubleTappedHandler = _colorManager.CurrentColor;

                _colorManager.CurrentColor = color.ToColor();
            }
        }

        private void SavePaletteContextMenuItemOnClick(object? sender, RoutedEventArgs e)
        {
            SavePaletteHandler?.Invoke(Colors);
        }

        private async void LoadPaletteContextMenuItemOnClick(object? sender, RoutedEventArgs e)
        {
            if (LoadPaletteHandler != null)
            {
                var (success, colors) = await LoadPaletteHandler.Invoke();

                if (success)
                    Colors = colors;
            }
        }

        private void UpdateColors()
        {
            Colors = Items.OfType<ListBoxItem>()
                .Where(item => item != _addItem)
                .Select(GetColorFromItem)
                .ToList();
        }

        private static System.Windows.Media.Color GetColorFromItem(ListBoxItem item)
        {
            var brush = item.Background as SolidColorBrush;

            return brush.Color;
        }

        private void SetBinding(DependencyObject element, DependencyProperty property, string path,
            IValueConverter? converter = null, object? converterParameter = null)
        {
            Binding widthBinding = new()
            {
                Source = this,
                Path = new PropertyPath(path),
                Converter = converter,
                ConverterParameter = converterParameter
            };

            BindingOperations.SetBinding(element, property, widthBinding);
        }

        #endregion
    }
}