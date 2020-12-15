using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public class ColorPalette : ListBox, IStyleable, IColorClient
    {
        #region Private Fields

        private IColorManager? _colorManager;

        private readonly MenuItem _addContextMenuItem;
        private readonly MenuItem _removeItem;

        private readonly ListBoxItem _addItem;

        #endregion

        #region IStyleable

        Type IStyleable.StyleKey => typeof(ColorPalette);

        #endregion

        #region Dependency Properties

        public static readonly StyledProperty<string> RemoveColorContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(RemoveColorContextMenuText), "Remove color");

        public static readonly StyledProperty<string> RemoveColorsContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(RemoveColorsContextMenuText), "Remove colors");

        public static readonly StyledProperty<string> AddColorContextMenuTextProperty =
            AvaloniaProperty.Register<ColorPreview, string>(nameof(AddColorContextMenuText), "Add color");

        #endregion

        #region Public Properties

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

        #endregion

        #region Constructor

        public ColorPalette()
        {
            SelectionMode = SelectionMode.Multiple;

            _removeItem = new()
            {
                Header = RemoveColorContextMenuText
            };

            _addContextMenuItem = new()
            {
                Header = AddColorContextMenuText
            };

            _removeItem.Click += RemoveItemOnClick;
            _addContextMenuItem.Click += AddItemOnClick;

            _addItem = new ListBoxItem()
            {
                Classes = new Classes("Add")
            };


            ContextMenu = new ContextMenu()
            {
                Items = new List<MenuItem>
                {
                    _addContextMenuItem,
                    _removeItem,
                }
            };

            InitPalette(ColorPalettes.PaintPalette);

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

            items.Insert(items.Count - 1, CreateColorItem(Colors.Transparent));
        }

        private void InitPalette(IEnumerable<Color> colors)
        {
            if (Items is not IList<object> items)
                return;

            items.Clear();

            foreach (var color in colors)
                items.Add(CreateColorItem(color.ToColor()));

            items.Add(_addItem);
        }

        private void AddItemOnClick(object? sender, RoutedEventArgs e)
        {
            if (Items is not IList<object> items)
                return;


            if (SelectedItems.Count > 0)
            {
                var selectedItem = SelectedItems[^1];

                if (selectedItem != _addItem)
                {
                    var index = items.IndexOf(selectedItem);

                    items.Insert(index, CreateColorItem(Colors.Transparent));
                }
                else
                {
                    items.Insert(items.Count - 1, CreateColorItem(Colors.Transparent));
                }
            }
            else
            {
                items.Insert(items.Count - 1, CreateColorItem(Colors.Transparent));
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

        #endregion
    }
}