using SFML.Graphics;
using SFML.Window;

namespace SFMLTest.Scenes
{
    interface IScene : Drawable
    {
        void Update();
        void MouseButtonPressed(object sender, MouseButtonEventArgs e);
        void MouseButtonReleased(object sender, MouseButtonEventArgs e);
    }
}