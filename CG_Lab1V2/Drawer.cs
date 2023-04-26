using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CG_Lab1V2
{
    internal class Drawer
    {
        private Stack<Rectangle> _filling;
        private Stack<Rectangle> _stroke;

        private Pen _pen;
        private SolidBrush _brush;

        public Graphics _graphics;

        private Color _background { get; set; }

        public Bitmap _bitmap
        {
            set
            {
                _buffer?.Dispose();
                _buffer = value;
                _graphics?.Dispose();

                _graphics = Graphics.FromImage(_buffer);
                SetDefault();
            }
            get => new Bitmap(_buffer);
        }

        private Bitmap _buffer;

        public Drawer(int w, int h, Color background)
        {
            _bitmap = new Bitmap(w, h);

            _filling = new Stack<Rectangle>();
            _stroke = new Stack<Rectangle>();

            _pen = new Pen(_background);
            _brush = new SolidBrush(_background);

            _background = background;
        }

        public void clear()
        {
            while (_filling.Count > 0)
            {
                ClearFilling(_filling.Pop());
            }
            while (_stroke.Count > 0)
            {
                ClearStroke(_stroke.Pop());
            }
        }

        private void ClearFilling(Rectangle rectangle)
        {
            _brush.Color = _background;

            _graphics.FillPolygon(_brush, rectangle.GetPoints());
        }

        private void ClearStroke(Rectangle rectangle)
        {
            _pen.Color = _background;
            _pen.Width = rectangle.getStroke();

            _graphics.DrawPolygon(_pen, rectangle.GetPoints());
        }

        public void fillRectangle(Rectangle rectangle)
        {
            _brush.Color = rectangle._colorFill;

            _filling.Push(rectangle);

            _graphics.FillPolygon(_brush, rectangle.GetPoints());
        }

        public void drawStroke(Rectangle rectangle)
        {
            _pen.Color = rectangle._colorStroke;
            _pen.Width = rectangle.getStroke();

            _stroke.Push(rectangle);

            _graphics.DrawPolygon(_pen, rectangle.GetPoints());
        }
        public void SetDefault()
        {
            _filling?.Clear();
            _stroke?.Clear();
            _graphics.Clear(_background);
        }
    }
}
