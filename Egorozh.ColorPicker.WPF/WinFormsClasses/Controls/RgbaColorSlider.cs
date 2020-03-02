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
using System.Windows.Forms;
using Egorozh.ColorPicker;

namespace AKIM.ColorPicker.Forms
{
    public class RgbaColorSlider : ColorSlider
    {
        #region Constants

        private static readonly object _eventChannelChanged = new object();

        private static readonly object _eventColorChanged = new object();

        #endregion

        #region Fields

        private Brush _cellBackgroundBrush;

        private RgbaChannel _channel;

        private Color _color;

        #endregion

        #region Constructors

        public RgbaColorSlider()
        {
            base.BarStyle = ColorBarStyle.Custom;
            base.Maximum = 255;
            this.Color = Color.Black;
            this.CreateScale();
        }

        #endregion

        #region Events

        [Category("Property Changed")]
        public event EventHandler ChannelChanged
        {
            add { this.Events.AddHandler(_eventChannelChanged, value); }
            remove { this.Events.RemoveHandler(_eventChannelChanged, value); }
        }

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
        [DefaultValue(typeof(RgbaChannel), "Red")]
        public virtual RgbaChannel Channel
        {
            get { return _channel; }
            set
            {
                if (this.Channel != value)
                {
                    _channel = value;

                    this.OnChannelChanged(EventArgs.Empty);
                }
            }
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
            ColorCollection custom;
            Color color;
            RgbaChannel channel;

            custom = new ColorCollection();
            color = this.Color;
            channel = this.Channel;

            for (int i = 0; i < 254; i++)
            {
                int a;
                int r;
                int g;
                int b;

                a = color.A;
                r = color.R;
                g = color.G;
                b = color.B;

                switch (channel)
                {
                    case RgbaChannel.Red:
                        r = i;
                        break;
                    case RgbaChannel.Green:
                        g = i;
                        break;
                    case RgbaChannel.Blue:
                        b = i;
                        break;
                    case RgbaChannel.Alpha:
                        a = i;
                        break;
                }

                custom.Add(Color.FromArgb(a, r, g, b));
            }

            this.CustomColors = custom;
        }

        protected virtual Brush CreateTransparencyBrush() => PortExtensions.CreateTransparencyBrush();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cellBackgroundBrush != null)
                {
                    _cellBackgroundBrush.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="ChannelChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnChannelChanged(EventArgs e)
        {
            EventHandler handler;

            this.CreateScale();
            this.Invalidate();

            handler = (EventHandler) this.Events[_eventChannelChanged];

            handler?.Invoke(this, e);
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

        protected override void PaintBar(PaintEventArgs e)
        {
            if (this.Color.A != 255)
            {
                if (_cellBackgroundBrush == null)
                {
                    _cellBackgroundBrush = this.CreateTransparencyBrush();
                }

                e.Graphics.FillRectangle(_cellBackgroundBrush, this.BarBounds);
            }

            base.PaintBar(e);
        }

        #endregion
    }
}