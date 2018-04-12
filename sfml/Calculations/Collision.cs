using SFML.Window;
using SFMLTest.Entities;
using SFMLTest.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace SFMLTest.Calculations
{
    public static class Collision
    {
        public static Boolean CheckCollision(this ICollidable obj1, ICollidable obj2)
        {
            if (obj1 is ISATCollidable && obj2 is ISATCollidable)
                return CheckSATCollision(obj1 as ISATCollidable, obj2 as ISATCollidable);
            else if (obj1 is ICollidable && obj2 is ICollidable)
                return CheckDefaultCollision(obj1 as ICollidable, obj2 as ICollidable);
            else
                return false;
        }

        public static bool CheckSATCollision(ISATCollidable obj1, ISATCollidable obj2)
        {
            var axises = new List<Vector2f>();
            axises.AddRange(obj1.GetAxis());
            axises.AddRange(obj2.GetAxis());

            foreach (var axis in axises)
            {
                GetMinMaxDotProduct(obj1.GetGlobalPoints(), axis, out float p1Min, out float p1Max);
                GetMinMaxDotProduct(obj2.GetGlobalPoints(), axis, out float p2Min, out float p2Max);
                if (p2Min > p1Max || p1Min > p2Max) return false;
            }

            return true;
        }

        public static bool CheckDefaultCollision(ICollidable obj1, ICollidable obj2)
        {
            if (obj1.GetPointCount() > 1 && obj2.GetPointCount() > 1)
            {
                var obj1Points = obj1.GetGlobalPoints();
                var obj2Points = obj2.GetGlobalPoints();

                for (int i = 0; i < obj1.GetPointCount(); i++)
                {
                    var p1 = obj1Points[i];
                    var p2 = i + 1 == obj1.GetPointCount() ?
                        obj1Points[0] : obj1Points[i + 1];
                    for (uint j = 0; j < obj2.GetPointCount(); j++)
                    {
                        var p3 = obj1Points[i];
                        var p4 = i + 1 == obj1.GetPointCount() ?
                            obj1Points[0] : obj1Points[i + 1];
                        if (CheckLineCollision(p1, p2, p3, p4))
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool CheckLineCollision(Vector2f s1, Vector2f e1, Vector2f s2, Vector2f e2)
        {
            var r = e1 - s1;
            var s = e2 - s2;

            var d = r.X * s.Y - r.Y * s.X;

            if (d == 0) return false;

            var u = ((s2.X - s1.X) * r.Y - (s2.Y - s1.Y) * r.X) / d;
            var t = ((s2.X - s1.X) * s.Y - (s2.Y - s1.Y) * s.X) / d;

            return (0 <= u && u <= 1 && 0 <= t && t <= 1);
        }

        private static void GetMinMaxDotProduct(List<Vector2f> v1, Vector2f v2, out float min, out float max)
        {
            min = max = 0;
            var firstIteration = true;
            foreach (var point in v1)
            {
                var prod = point.DotProduct(v2);
                if (firstIteration)
                {
                    min = max = prod;
                    firstIteration = false;
                }
                else
                {
                    if (min > prod) min = prod;
                    if (max < prod) max = prod;
                }
            }
        }
    }
}
