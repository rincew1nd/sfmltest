using SFMLTest;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;

namespace SFMLTest.Entities
{
    class Player : Shape
    {
        public Vector2f Size;

        public Player(Vector2f pos, Vector2f size)
        {
            Position = pos;
            Size = size;
            Origin = new Vector2f(Size.X / 2, Size.Y / 2);
            FillColor = Color.Red;
            Update();
        }

        public override uint GetPointCount()
        {
            return 4;
        }

        public override Vector2f GetPoint(uint index)
        {
            switch (index)
            {
                default:
                case 0: return new Vector2f(0, 0);
                case 1: return new Vector2f(Size.X, 0);
                case 2: return new Vector2f(Size.X, Size.Y);
                case 3: return new Vector2f(0, Size.Y);
            }
        }

        public List<Vector2f> GetGlobalPoints()
        {
            return new List<Vector2f>()
            {
                Transform.TransformPoint(GetPoint(1)),
                Transform.TransformPoint(GetPoint(2)),
                Transform.TransformPoint(GetPoint(3)),
                Transform.TransformPoint(GetPoint(4))
            };
        }

        public Vector2f[] GetAxis()
        {
            var points = GetGlobalPoints();
            return new []
            {
                points[1] - points[0],
                points[2] - points[1]
            };
        }
    }
}
