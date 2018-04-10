using SFML.Window;
using System;

namespace SFMLTest
{
    public static class Utils
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
    }
}
