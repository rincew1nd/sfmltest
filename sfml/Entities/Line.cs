using SFML.Graphics;
using SFML.Window;
using System;

namespace SFMLTest.Entities
{
    class Line
    {
        private Vector2f _start;
        private Vector2f _end;
        private Vector2f _size;
        public Color Color { get; private set; }

        public float Angle;
        public Vertex[] Vertex => new[]{ new Vertex(_start, Color), new Vertex(_end, Color) };

        public Line(Vector2f start, Vector2f size, Color color)
        {
            _start = start;
            _end = new Vector2f(start.X + size.X, start.Y + size.Y);
            _size = size;
            Color = color;
            Angle = 0;
        }

        public void Rotate(float angle)
        {
            var dx = _size.X * Math.Cos(Math.PI * angle / 180) - _size.Y * Math.Sin(Math.PI * angle / 180);
            var dy = _size.Y * Math.Cos(Math.PI * angle / 180) + _size.X * Math.Sin(Math.PI * angle / 180);
            _end = new Vector2f((float)dx + _start.X, (float)dy + _start.Y);
        }

        public void SetColor(Color color)
        {
            Color = color;
        }
    }
}
