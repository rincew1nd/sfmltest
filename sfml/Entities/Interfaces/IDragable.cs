using SFML.Window;

namespace SFMLTest.Entities.Interfaces
{
    public interface IDragable
    {
        bool CheckDrag(Vector2i mouse);
        void Drag(Vector2i mouse);
        void StopDrag();
    }
}
