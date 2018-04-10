using SFML.Graphics;
using SFML.Window;
using System;

namespace SFMLTest.Entities
{
    class DragableLine : Drawable
    {
        public Vector2f Start;
        public Vector2f End;

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _points[0].FillColor = _color;
                _points[1].FillColor = _color;
            }
        }

        public bool IsDragging;

        private DragableCircle[] _points;

        private Vertex[] Line => new[]
        {
            new Vertex(Start, _color), new Vertex(End, _color)
        };

        private Vertex[] NormalVector()
        {
            var vector = End - Start;
            var startPos = new Vector2f(Start.X + vector.X / 2, Start.Y + vector.Y / 2);
            vector = new Vector2f(vector.Y * -1, vector.X);
            return new []
            {
                new Vertex(startPos), new Vertex(new Vector2f(startPos.X + vector.X, startPos.Y + vector.Y))
            };
        }

        public DragableLine(Vector2f start, Vector2f end, Color color)
        {
            Start = start;
            End = end;
            _color = color;

            _points = new DragableCircle[]
            {
                new DragableCircle(Start, 4, _color),
                new DragableCircle(End, 4, _color)
            };
            _points[0].DragEvent += (pos) => Start = pos;
            _points[1].DragEvent += (pos) => End   = pos;

            IsDragging = false;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Line, PrimitiveType.Lines);
            target.Draw(NormalVector(), PrimitiveType.Lines);
            target.Draw(_points[0]);
            target.Draw(_points[1]);
        }

        public void CheckDrag(Vector2i mouse)
        {
            IsDragging = _points[0].CheckDrag(mouse);
            IsDragging = _points[1].CheckDrag(mouse) || IsDragging;
        }

        public void Drag(Vector2i mouse)
        {
            if (_points[0].IsDragging) _points[0].Drag(mouse);
            if (_points[1].IsDragging) _points[1].Drag(mouse);
        }

        public void StopDrag()
        {
            IsDragging = false;
            if (_points[0].IsDragging) _points[0].StopDrag();
            if (_points[1].IsDragging) _points[1].StopDrag();
        }
    }
}
