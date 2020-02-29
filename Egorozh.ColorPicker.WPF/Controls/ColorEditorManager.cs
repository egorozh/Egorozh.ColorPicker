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

        private ColorEditorPort _colorEditor;

        private ColorGrid _grid;

        private HslColor _hslColor;

        private LightnessColorSlider _lightnessColorSlider;
        
        private ScreenColorPickerPort _screenColorPickerPort;
        private ColorWheel _wheel;

        #endregion

        #region Events

        public event EventHandler ColorEditorChanged;

        public event EventHandler ColorGridChanged;

        public event EventHandler ColorWheelChanged;

        public event EventHandler LightnessColorSliderChanged;

        public event EventHandler ScreenColorPickerChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the linked <see cref="ColorEditor"/>.
        /// </summary>
        public virtual ColorEditorPort ColorEditor
        {
            get => _colorEditor;
            set
            {
                if (this.ColorEditor != value)
                {
                    _colorEditor = value;

                    this.OnColorEditorChanged(EventArgs.Empty);
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
                if (this.ColorGrid != value)
                {
                    _grid = value;

                    this.OnColorGridChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the linked <see cref="ColorWheel"/>.
        /// </summary>

        public virtual ColorWheel ColorWheel
        {
            get => _wheel;
            set
            {
                if (this.ColorWheel != value)
                {
                    _wheel = value;

                    this.OnColorWheelChanged(EventArgs.Empty);
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
                if (this.HslColor != value)
                {
                    _hslColor = value;
                    _color = value.ToRgbColor();

                    this.OnColorChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the linked <see cref="LightnessColorSlider"/>.
        /// </summary>
        public virtual LightnessColorSlider LightnessColorSlider
        {
            get => _lightnessColorSlider;
            set
            {
                if (this.LightnessColorSlider != value)
                {
                    _lightnessColorSlider = value;

                    this.OnLightnessColorSliderChanged(EventArgs.Empty);
                }
            }
        }
        
        public ScreenColorPickerPort ScreenColorPickerPort
        {
            get => _screenColorPickerPort;
            set => SetScreenColorPickerPort(value);
        }

        private void SetScreenColorPickerPort(ScreenColorPickerPort value)
        {
            if (_screenColorPickerPort != value)
            {
                _screenColorPickerPort = value;

                if (_screenColorPickerPort != null)
                    BindEvents(_screenColorPickerPort);
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
            control.ColorChanged += this.ColorChangedHandler;
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            this.Synchronize(this);

            ColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ColorEditorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorEditorChanged(EventArgs e)
        {
            if (this.ColorEditor != null)
            {
                this.BindEvents(this.ColorEditor);
            }

            ColorEditorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ColorGridChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorGridChanged(EventArgs e)
        {
            if (this.ColorGrid != null)
            {
                this.BindEvents(this.ColorGrid);
            }

            ColorGridChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ColorWheelChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorWheelChanged(EventArgs e)
        {
            if (this.ColorWheel != null)
            {
                this.BindEvents(this.ColorWheel);
            }

            ColorWheelChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="LightnessColorSliderChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnLightnessColorSliderChanged(EventArgs e)
        {
            if (this.LightnessColorSlider != null)
            {
                this.BindEvents(this.LightnessColorSlider);
            }

            LightnessColorSliderChanged?.Invoke(this, e);
        }
        
        /// <summary>
        /// Sets the color of the given editor.
        /// </summary>
        /// <param name="control">The <see cref="IColorEditor"/> to update.</param>
        /// <param name="sender">The <see cref="IColorEditor"/> triggering the update.</param>
        protected virtual void SetColor(IColorEditor control, IColorEditor sender)
        {
            if (control != null && control != sender)
            {
                control.Color = sender.Color;
            }
        }

        /// <summary>
        /// Synchronizes linked components with the specified <see cref="IColorEditor"/>.
        /// </summary>
        /// <param name="sender">The <see cref="IColorEditor"/> triggering the update.</param>
        protected virtual void Synchronize(IColorEditor sender)
        {
            if (!this.LockUpdates)
            {
                try
                {
                    this.LockUpdates = true;
                    this.SetColor(this.ColorGrid, sender);
                    this.SetColor(this.ColorWheel, sender);
                    this.SetColor(this.ScreenColorPickerPort, sender);
                    this.SetColor(this.ColorEditor, sender);
                    this.SetColor(this.LightnessColorSlider, sender);
                }
                finally
                {
                    this.LockUpdates = false;
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
            if (!this.LockUpdates)
            {
                var source = (IColorEditor) sender;

                this.LockUpdates = true;
                this.Color = source.Color;
                this.LockUpdates = false;
                this.Synchronize(source);
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

                    this.OnColorChanged(EventArgs.Empty);
                }
            }
        }

        #endregion
    }
}