using System;
using System.Drawing;
using AKIM.ColorPicker.Forms;

namespace Egorozh.ColorPicker
{
    /// <summary>
    /// Represents a control that binds multiple editors together as a single composite unit.
    /// </summary>
    public class ColorEditorManager : IColorEditor
    {
        #region Fields

        private Color _color;

        private ColorEditor _colorEditor;

        private ColorGrid _grid;

        private HslColor _hslColor;

        private LightnessColorSlider _lightnessColorSlider;

        private ScreenColorPicker _screenColorPicker;

        private ColorWheel _colorWheel;

        #endregion

        #region Events

        public event EventHandler ColorEditorChanged;

        public event EventHandler ColorGridChanged;
        
        public event EventHandler LightnessColorSliderChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the linked <see cref="ColorEditor"/>.
        /// </summary>
        public virtual ColorEditor ColorEditor
        {
            get => _colorEditor;
            set
            {
                if (ColorEditor != value)
                {
                    _colorEditor = value;

                    OnColorEditorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the linked <see cref="ColorGrid"/>.
        /// </summary>
        public virtual ColorGrid ColorGrid
        {
            get => _grid;
            set
            {
                if (ColorGrid != value)
                {
                    _grid = value;

                    OnColorGridChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the component color as a HSL structure.
        /// </summary>
        /// <value>The component color.</value>
        public virtual HslColor HslColor
        {
            get => _hslColor;
            set
            {
                if (HslColor != value)
                {
                    _hslColor = value;
                    _color = value.ToRgbColor();

                    OnColorChanged(EventArgs.Empty);
                }
            }
        }
        
        public ScreenColorPicker ScreenColorPicker
        {
            get => _screenColorPicker;
            set => SetScreenColorPicker(value);
        }

        public ColorWheel ColorWheel
        {
            get => _colorWheel;
            set => SetColorWheelPort(value);
        }

        private void SetColorWheelPort(ColorWheel value)
        {
            if (_colorWheel != value)
            {
                _colorWheel = value;

                if (_colorWheel != null)
                    BindEvents(_colorWheel);
            }
        }

        private void SetScreenColorPicker(ScreenColorPicker value)
        {
            if (_screenColorPicker != value)
            {
                _screenColorPicker = value;

                if (_screenColorPicker != null)
                    BindEvents(_screenColorPicker);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether updating of linked components is disabled.
        /// </summary>
        /// <value><c>true</c> if updated of linked components is disabled; otherwise, <c>false</c>.</value>
        protected bool LockUpdates { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Binds events for the specified editor.
        /// </summary>
        /// <param name="control">The <see cref="IColorEditor"/> to bind to.</param>
        protected virtual void BindEvents(IColorEditor control)
        {
            control.ColorChanged += ColorChangedHandler;
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            Synchronize(this);

            ColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ColorEditorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorEditorChanged(EventArgs e)
        {
            if (ColorEditor != null)
            {
                BindEvents(ColorEditor);
            }

            ColorEditorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ColorGridChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorGridChanged(EventArgs e)
        {
            if (ColorGrid != null)
            {
                BindEvents(ColorGrid);
            }

            ColorGridChanged?.Invoke(this, e);
        }
        
        /// <summary>
        /// Sets the color of the given editor.
        /// </summary>
        /// <param name="control">The <see cref="IColorEditor"/> to update.</param>
        /// <param name="sender">The <see cref="IColorEditor"/> triggering the update.</param>
        protected virtual void SetColor(IColorEditor control, IColorEditor sender)
        {
            if (control != null && control != sender)
                control.Color = sender.Color;
        }

        /// <summary>
        /// Synchronizes linked components with the specified <see cref="IColorEditor"/>.
        /// </summary>
        /// <param name="sender">The <see cref="IColorEditor"/> triggering the update.</param>
        protected virtual void Synchronize(IColorEditor sender)
        {
            if (!LockUpdates)
            {
                try
                {
                    LockUpdates = true;
                    SetColor(ColorGrid, sender);
                    SetColor(ColorWheel, sender);
                    SetColor(ScreenColorPicker, sender);
                    SetColor(ColorEditor, sender);
                }
                finally
                {
                    LockUpdates = false;
                }
            }
        }

        /// <summary>
        /// Handler for linked controls.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ColorChangedHandler(object sender, EventArgs e)
        {
            if (!LockUpdates)
            {
                var source = (IColorEditor) sender;

                LockUpdates = true;
                Color = source.Color;
                LockUpdates = false;
                Synchronize(source);
            }
        }

        #endregion

        #region IColorEditor Interface

        public event EventHandler ColorChanged;

        /// <summary>
        /// Gets or sets the component color.
        /// </summary>
        /// <value>The component color.</value>
        public virtual Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    _hslColor = new HslColor(value);

                    OnColorChanged(EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}