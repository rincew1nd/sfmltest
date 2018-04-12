using SFML.Graphics;
using SFML.Window;
using SFMLTest.Entities.Interfaces;
using System;

namespace SFMLTest.Entities
{
    public class DragableCircle : CircleShape, IDragable
    {
        public bool IsDragging;
        public Vector2f DragStartPos;

        public delegate void DragEventHandler(Vector2f position);
        public event DragEventHandler DragEvent;

        public DragableCircle(Vector2f position, float radius) : base(radius)
        {
            Position = position;
            Origin = new Vector2f(radius, radius);
        }

        public DragableCircle(Vector2f position, float radius, Color color) : this(position, radius)
        {
            FillColor = color;
        }

        public void Drag(Vector2i mouse)
        {
            Position = new Vector2f(mouse.X - DragStartPos.X, mouse.Y - DragStartPos.Y);
            if (DragEvent != null)
                DragEvent(Position);
        }

        private bool IsPointInCircle(Vector2f mouse) =>
            (Math.Pow((mouse.X - Position.X), 2) + Math.Pow((mouse.Y - Position.Y), 2)) <= Math.Pow(Radius,2) * 2;

        public void StopDrag() => IsDragging = false;

        public bool CheckDrag(Vector2i mouse)
        {
            if (IsPointInCircle(new Vector2f(mouse.X, mouse.Y)))
            {
                IsDragging = true;
                DragStartPos = new Vector2f(mouse.X - Position.X, mouse.Y - Position.Y);
                return true;
            }
            return false;
        }
    }
}
