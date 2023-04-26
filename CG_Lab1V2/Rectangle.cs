using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CG_Lab1V2
{
    internal class Rectangle
    {
        private PointF _mainPoint;
        private float _h;
        private float _w;
        private float _stroke;

        private PointF[] _refPoints;

        public Color _colorFill { get; set; }
        public Color _colorStroke { get; set; }

        public float getStroke()
        {
            return _stroke;
        }

        public PointF getMainPoint()
        {
            return _mainPoint;
        }

        public PointF[] GetPoints()
        {
            return _refPoints;
        }

        public float getFullWidth()
        {
            return (_w + _stroke);
        }

        public float getFullHeight()
        {
            return (_h + _stroke);
        }

        public Rectangle(float X, float Y, float h, float w, float s, Color colorFill,
            Color colorStroke)
        {
            _mainPoint = new PointF(X, Y);
            _h = h;
            _w = w;

            _colorFill = colorFill;
            _colorStroke = colorStroke;

            _stroke = s;

            _refPoints = new PointF[4];

            calcRefPoints();
        }

        private void calcRefPoints()
        {
            float halfStroke = _stroke / 2;

            _refPoints[0].X = _mainPoint.X + halfStroke;
            _refPoints[0].Y = _mainPoint.Y + halfStroke;

            _refPoints[1].X = _refPoints[0].X + _w;
            _refPoints[1].Y = _refPoints[0].Y;

            _refPoints[2].X = _refPoints[1].X;
            _refPoints[2].Y = _refPoints[1].Y + _h;

            _refPoints[3].X = _refPoints[0].X;
            _refPoints[3].Y = _refPoints[2].Y;
        }

        public void move(float dx, float dy)
        {
            _mainPoint.X += dx;
            _mainPoint.Y += dy;

            for (int i = 0; i < 4; i++)
            {
                _refPoints[i].X += dx;
                _refPoints[i].Y += dy;
            }
        }
    }
}
