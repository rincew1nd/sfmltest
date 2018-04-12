using SFML.Graphics;
using SFML.Window;
using SFMLTest.Scenes;

namespace SFMLTest
{
    class Window
    {
        private RenderWindow _window;
        private IScene _scene;

        public Window()
        {
            _window = new RenderWindow(new VideoMode(800, 600), "test");

            _window.SetFramerateLimit(60);
            _window.KeyPressed += KeyboardPressed;

            //_scene = new TestScene();
            _scene = new CollisionDetectionScene(_window);
            //_scene = new DragableLineScene(_window);
            //_scene = new LineCollisionScene(_window);
            _window.MouseButtonPressed += _scene.MouseButtonPressed;
            _window.MouseButtonReleased += _scene.MouseButtonReleased;

            while (_window.IsOpen())
            {
                _window.DispatchEvents();

                _window.Clear(Color.Cyan);

                _scene.Update();

                _window.Draw(_scene);

                _window.Display();
            }
        }

        private void KeyboardPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                _window.Close();
        }
    }
}
