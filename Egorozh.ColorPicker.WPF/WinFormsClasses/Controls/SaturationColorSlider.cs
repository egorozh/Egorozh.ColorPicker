/*
﻿The MIT License (MIT)

Copyright © 2013-2017 Cyotek Ltd.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.ComponentModel;
using System.Drawing;
using Egorozh.ColorPicker;

namespace AKIM.ColorPicker.Forms
{
    /// <summary>
    /// Represents a control for selecting the saturation of a color
    /// </summary>
    public class SaturationColorSlider : ColorSlider
    {
        #region Constants

        private static readonly object _eventColorChanged = new object();

        #endregion

        #region Fields

        private Color _color;

        #endregion

        #region Constructors

        public SaturationColorSlider()
        {
            this.BarStyle = ColorBarStyle.TwoColor;
            this.Color = Color.Black;
        }

        #endregion

        #region Events

        [Category("Property Changed")]
        public event EventHandler ColorChanged
        {
            add { this.Events.AddHandler(_eventColorChanged, value); }
            remove { this.Events.RemoveHandler(_eventColorChanged, value); }
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ColorBarStyle BarStyle
        {
            get { return base.BarStyle; }
            set { base.BarStyle = value; }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Black")]
        public virtual Color Color
        {
            get { return _color; }
            set
            {
                if (this.Color != value)
                {
                    _color = value;

                    this.OnColorChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color Color1
        {
            get { return base.Color1; }
            set { base.Color1 = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color Color2
        {
            get { return base.Color2; }
            set { base.Color2 = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color Color3
        {
            get { return base.Color3; }
            set { base.Color3 = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float Maximum
        {
            get { return base.Maximum; }
            set { base.Maximum = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float Minimum
        {
            get { return base.Minimum; }
            set { base.Minimum = value; }
        }

        public override float Value
        {
            get { return base.Value; }
            set { base.Value = (int) value; }
        }

        #endregion

        #region Methods

        protected virtual void CreateScale()
        {
            HslColor color;

            color = new HslColor(this.Color);

            color.S = 0;
            this.Color1 = color.ToRgbColor();

            color.S = 1;
            this.Color2 = color.ToRgbColor();
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            EventHandler handler;

            this.CreateScale();
            this.Invalidate();

            handler = (EventHandler) this.Events[_eventColorChanged];

            handler?.Invoke(this, e);
        }

        #endregion
    }
}