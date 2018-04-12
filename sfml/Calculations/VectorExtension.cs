using SFML.Window;
using System;

namespace SFMLTest.Calculations
{
    public static class VectorExtension
    {
        public static Vector2f Rotate(this Vector2f vector, float angle)
        {
            float rad = (float)Math.PI * angle / 180;

            float x1 = (float)(vector.X * Math.Cos(rad));
            float x2 = (float)(vector.Y * Math.Sin(rad));
            float x3 = (float)(vector.Y * Math.Cos(rad));
            float x4 = (float)(vector.X * Math.Sin(rad));

            return new Vector2f(x1 - x2, x3 + x4);
        }

        public static float Length(this Vector2f vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y + vector.Y);
        }

        public static Vector2f Normal(this Vector2f vector)
        {
            return new Vector2f(vector.Y * -1, vector.X);
        }

        public static Vector2f Normalize(this Vector2f vector)
        {
            var length = vector.Length();

            if (length != 0)
                return new Vector2f(vector.X / length, vector.Y / length);
            else
                return new Vector2f(0, 0);
        }

        public static float DotProduct(this Vector2f vector1, Vector2f vector2)
        {
            return (vector1.X * vector2.X + vector1.Y * vector2.Y);
        }
    }
}
