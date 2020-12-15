using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public class ColorPalette : ListBox, IStyleable, IColorClient
    {
        #region Private Fields

        private IColorManager? _colorManager;

        private MenuItem _addContextMenuItem;
        private MenuItem _removeItem;

        private MenuItem _loadPaletteContextMenuItem;
        private MenuItem _savePaletteContextMenuItem;

        private readonly ListBoxItem _addItem;

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPalette);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<IEnumerable<Avalonia.Media.Color>> ColorsProperty =
            AvaloniaProperty.Register<ColorPreview, IEnumerable<Avalonia.Media.Color>>(
                nameof(RemoveColorContextMenuText),
                ColorPalettes.PaintPalette.Select(c => c.ToColor()),
                notifying: ColorsPropertyChanged);

        private static void ColorsPropertyChanged(IAvaloniaObject arg1, bool arg2)
        {
            if (arg1 is ColorPalette colorPalette)
                colorPalette.ColorsPropertyChanged();
        }

        public static readonly StyledProperty<string> RemoveColorContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(RemoveColorContextMenuText), "Remove color");

        public static readonly StyledProperty<string> RemoveColorsContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(RemoveColorsContextMenuText), "Remove colors");

        public static readonly StyledProperty<string> AddColorContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(AddColorContextMenuText), "Add color");

        public static readonly StyledProperty<DataTemplate> LoadPaletteIconTemplateProperty =
            AvaloniaProperty.Register<ColorPreview, DataTemplate>(nameof(LoadPaletteIconTemplate));

        public static readonly StyledProperty<DataTemplate> SavePaletteIconTemplateProperty =
            AvaloniaProperty.Register<ColorPreview, DataTemplate>(nameof(SavePaletteIconTemplate));

        public static readonly StyledProperty<string> LoadPaletteContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(LoadPaletteContextMenuText), "Load Palette");

        public static readonly StyledProperty<string> SavePaletteContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(SavePaletteContextMenuText), "Save Palette");

        #endregion

        #region Public Properties

        public IEnumerable<Avalonia.Media.Color> Colors
        {
            get => GetValue(ColorsProperty);
            private set => SetValue(ColorsProperty, value);
        }

        public string RemoveColorContextMenuText
        {
            get => GetValue(RemoveColorContextMenuTextProperty);
            private set => SetValue(RemoveColorContextMenuTextProperty, value);
        }

        public string RemoveColorsContextMenuText
        {
            get => GetValue(RemoveColorsContextMenuTextProperty);
            private set => SetValue(RemoveColorsContextMenuTextProperty, value);
        }

        public string AddColorContextMenuText
        {
            get => GetValue(AddColorContextMenuTextProperty);
            private set => SetValue(AddColorContextMenuTextProperty, value);
        }

        public DataTemplate LoadPaletteIconTemplate
        {
            get => GetValue(LoadPaletteIconTemplateProperty);
            private set => SetValue(LoadPaletteIconTemplateProperty, value);
        }

        public DataTemplate SavePaletteIconTemplate
        {
            get => GetValue(SavePaletteIconTemplateProperty);
            private set => SetValue(SavePaletteIconTemplateProperty, value);
        }

        public string LoadPaletteContextMenuText
        {
            get => GetValue(LoadPaletteContextMenuTextProperty);
            private set => SetValue(LoadPaletteContextMenuTextProperty, value);
        }

        public string SavePaletteContextMenuText
        {
            get => GetValue(SavePaletteContextMenuTextProperty);
            private set => SetValue(SavePaletteContextMenuTextProperty, value);
        }

        #endregion

        #region Constructor

        public ColorPalette()
        {
            SelectionMode = SelectionMode.Multiple;

            _addItem = new ListBoxItem()
            {
                Classes = new Classes("Add")
            };

            _addItem.Tapped += AddItemOnTapped;

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

        #endregion

        #region Protected Methods

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _addContextMenuItem = e.NameScope.Find<MenuItem>("PART_AddMenuItem");
            _removeItem = e.NameScope.Find<MenuItem>("PART_RemoveMenuItem");

            _loadPaletteContextMenuItem = e.NameScope.Find<MenuItem>("PART_LoadPaletteMenuItem");
            _savePaletteContextMenuItem = e.NameScope.Find<MenuItem>("PART_SavePaletteMenuItem");

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
        }

        private void AddItemOnTapped(object? sender, RoutedEventArgs e)
        {
            if (Items is not IList<object> items)
                return;

            items.Insert(items.Count - 1, CreateColorItem(Avalonia.Media.Colors.Transparent));
        }

        private void ColorsPropertyChanged()
        {
            if (Items is not IList<object> items)
                return;

            items.Clear();

            foreach (var color in Colors)
                items.Add(CreateColorItem(color));

            items.Add(_addItem);
        }

        private void AddItemOnClick(object? sender, RoutedEventArgs e)
        {
            if (Items is not IList<object> items)
                return;

            var newColorItem = CreateColorItem(Avalonia.Media.Colors.Transparent);

            if (SelectedItems.Count > 0)
            {
                var selectedItem = SelectedItems[^1];

                if (selectedItem != _addItem)
                {
                    var index = items.IndexOf(selectedItem);

                    items.Insert(index, newColorItem);
                }
                else
                {
                    items.Insert(items.Count - 1, newColorItem);
                }
            }
            else
            {
                items.Insert(items.Count - 1, newColorItem);
            }
        }

        private void ItemOnDoubleTapped(object? sender, RoutedEventArgs e)
        {
        }

        private void ItemOnTapped(object? sender, RoutedEventArgs e)
        {
            if (sender is not ListBoxItem colorItem || _colorManager == null)
                return;

            var brush = colorItem.Background as SolidColorBrush;

            var color = brush.Color;

            _colorManager.CurrentColor = color.ToColor();
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            _removeItem.IsEnabled = SelectedItem != _addItem;

            _removeItem.Header = SelectedItems.Count > 1 ? RemoveColorsContextMenuText : RemoveColorContextMenuText;
        }

        private ListBoxItem CreateColorItem(Avalonia.Media.Color color)
        {
            ListBoxItem item = new()
            {
                Background = new SolidColorBrush(color)
            };

            item.Tapped += ItemOnTapped;
            item.DoubleTapped += ItemOnDoubleTapped;

            return item;
        }

        private void RemoveItem(ListBoxItem removedItem)
        {
            if (Items is not IList<object> items)
                return;

            removedItem.Tapped -= ItemOnTapped;
            removedItem.DoubleTapped -= ItemOnDoubleTapped;

            items.Remove(removedItem);
        }

        private void SavePaletteContextMenuItemOnClick(object? sender, RoutedEventArgs e)
        {
        }

        private void LoadPaletteContextMenuItemOnClick(object? sender, RoutedEventArgs e)
        {
        }

        #endregion
    }
}