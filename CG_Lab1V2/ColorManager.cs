using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CG_Lab1V2
{
    internal class ColorManager
    {
        private Rectangle _rect;
        private Color[] _colors;
        private int _i;
        public ColorManager(Rectangle rectangle, Color[] colors)
        {
            _rect = rectangle;
            _colors = colors;
            _i = 0;
        }

        public void changeColor()
        {
            _rect._colorStroke = _colors[_i];

            _i++;
            if (_i == _colors.Length) { _i = 0; }
        }
    }
}
