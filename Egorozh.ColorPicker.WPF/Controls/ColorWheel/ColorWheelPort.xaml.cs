using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace Egorozh.ColorPicker
{
    public partial class ColorWheelPort : IColorEditor
    {
        #region Private Fields

        private Color _color;
        private HslColor _hslColor;
        private bool _lockUpdates;

        #endregion

        #region Public Properties

        public HslColor HslColor
        {
            get => _hslColor;
            set => SetHslColor(value);
        }

        public Color Color
        {
            get => _color;
            set => SetColor(value);
        }

        #endregion

        #region Events

        public event EventHandler ColorChanged;

        #endregion

        #region Constructor

        public ColorWheelPort()
        {
            InitializeComponent();

            _color = Color.Black;
            _hslColor = new HslColor(_color);
        }

        #endregion
        
        /// <summary>
        /// Creates the gradient brush used to paint the wheel.
        /// </summary>
        protected virtual System.Windows.Media.Brush CreateGradientBrush()
        {
        //    var points = CalculateColorPoints();
            
            var gradientStopCollection = new GradientStopCollection
            {
                
            };

            System.Windows.Media.Brush result = new RadialGradientBrush(gradientStopCollection);

            //if (_points.Length != 0 && _points.Length == _colors.Length)
            //{
            //    result = new PathGradientBrush(_points, WrapMode.Clamp)
            //    {
            //        CenterPoint = _centerPoint,
            //        CenterColor = Color.White,
            //        SurroundColors = _colors
            //    };
            //}
            //else
            //{
                result = null;
            //}

            return result;
        }
        
        protected virtual void CalculateWheel()
        {
            List<PointF> points;
            List<Color> colors;
            Size size;

            points = new List<PointF>();
            colors = new List<Color>();
            //size = this.ClientSize;

            var width = ActualWidth;
            var height = ActualHeight;

            //// Only define the points if the control is above a minimum size, otherwise if it's too small, you get an "out of memory" exceptions (of all things) when creating the brush
            //if (width > 16 && height > 16)
            //{
            //    int w;
            //    int h;

            //    w = (int) width;
            //    h = (int) height;

            //    _centerPoint = new PointF(w / 2.0F, h / 2.0F);
            //    _radius = this.GetRadius(_centerPoint);

            //    for (double angle = 0; angle < 360; angle += _colorStep)
            //    {
            //        double angleR;
            //        PointF location;

            //        angleR = angle * (Math.PI / 180);
            //        location = this.GetColorLocation(angleR, _radius);

            //        points.Add(location);
            //        colors.Add(new HslColor(angle, 1, 0.5).ToRgbColor());
            //    }
            //}

            //_points = points.ToArray();
            //_colors = colors.ToArray();
        }


        //private void RefreshWheel()
        //{
        //    CalculateWheel();
            
        //    _brush = this.CreateGradientBrush();

        //    //if (_selectionGlyph == null)
        //    //{
        //    //    _selectionGlyph = this.CreateSelectionGlyph();
        //    //}

        //    //this.Invalidate();
        //}

        private void OnColorChanged(EventArgs e)
        {
            if (!_lockUpdates)
                this.HslColor = new HslColor(_color);

            //Refresh();

            ColorChanged?.Invoke(this, e);
        }

        private void SetHslColor(HslColor value)
        {
            if (_hslColor != value)
            {
                _hslColor = value;

                if (!_lockUpdates)
                {
                    Color = _hslColor.ToRgbColor();
                }

                //this.Invalidate();

                //handler = (EventHandler)this.Events[_eventHslColorChanged];

                //handler?.Invoke(this, e);
            }
        }

        private void SetColor(Color value)
        {
            if (_color != value)
            {
                _color = value;

                OnColorChanged(EventArgs.Empty);
            }
        }
    }
}