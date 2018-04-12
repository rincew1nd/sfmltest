using SFML.Window;

namespace SFMLTest.Entities.Interfaces
{
    public interface ISATCollidable : ICollidable
    {
        Vector2f[] GetAxis();
    }
}