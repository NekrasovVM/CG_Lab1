using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CG_Lab1V2
{
    internal class Mover
    {
        float _dx;
        float _dy;
        double _tg;

        Rectangle _rect;

        public int _boxWidth;
        public int _boxHeight;
        public Mover(Rectangle rectangle, float dx, float angle, int boxWidth, int boxHeight)
        {
            _dx = dx;
            _tg = Math.Tan(angle * Math.PI / 180);
            _dy = (float)(_tg * Math.Abs(_dx));

            _rect = rectangle;

            _boxWidth = boxWidth;
            _boxHeight = boxHeight;
        }

        public void move()
        {
            PointF mainPoint = _rect.getMainPoint();
            float dx, dy;
            float width = _rect.getFullWidth();
            float height = _rect.getFullHeight();

            if (mainPoint.X + _dx + width > _boxWidth)
            {
                dx = _boxWidth - (mainPoint.X + _dx + width);

                _dx = -_dx;
            }
            else if (mainPoint.X + _dx < 0)
            {
                _dx = -_dx;

                dx = _dx - mainPoint.X;
            }
            else
            {
                dx = _dx;
            }

            if (mainPoint.Y + _dy + height > _boxHeight)
            {
                dy = _boxHeight - (mainPoint.Y + _dy + height);

                _dy = -_dy;
            }
            else if (mainPoint.Y + _dy < 0)
            {
                _dy = -_dy;

                dy = _dy - mainPoint.Y;
            }
            else
            {
                dy = _dy;
            }

            _rect.move(dx, dy);
        }

        public void setStep(int step)
        {
            _dx = (_dx > 0) ? step : -step;
            _dy = (float)(((_dy > 0) ? 1 : -1) * (_tg * step));
        }
    }
}
