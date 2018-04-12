using SFML.Window;
using System.Collections.Generic;

namespace SFMLTest.Entities.Interfaces
{
    public interface ICollidable
    {
        List<Vector2f> GetGlobalPoints();
        Vector2f GetPoint(uint index);
        uint GetPointCount();
    }
}