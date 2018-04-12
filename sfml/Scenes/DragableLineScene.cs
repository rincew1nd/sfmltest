using SFML.Graphics;
using SFML.Window;
using SFMLTest.Entities;
using System.Collections.Generic;

namespace SFMLTest.Scenes
{
    class DragableLineScene : IScene
    {
        private RenderWindow _window;
        private List<DragableLine> _lines;
        private DragableCircle _circle;

        public DragableLineScene(RenderWindow window)
        {
            _window = window;
            _circle = new DragableCircle(new Vector2f(200, 200), 8);
            _lines = new List<DragableLine>()
            {
                new DragableLine(new Vector2f(100, 100), new Vector2f(150, 100), Color.Red),
                new DragableLine(new Vector2f(300, 200), new Vector2f(350, 200), Color.Blue)
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var line in _lines)
                target.Draw(line);
            target.Draw(_circle);
        }

        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var mousePos = Mouse.GetPosition(_window);

            if (e.Button == Mouse.Button.Left)
            {
                _circle.CheckDrag(mousePos);
                foreach (var line in _lines)
                    line.CheckDrag(mousePos);
            }
        }

        public void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (_circle.IsDragging) _circle.StopDrag();
                foreach (var line in _lines)
                    if (line.IsDragging)
                        line.StopDrag();
            }
        }

        public void Update()
        {
            if (_circle.IsDragging)
                _circle.Drag(Mouse.GetPosition(_window));

            foreach (var line in _lines)
                if (line.IsDragging)
                    line.Drag(Mouse.GetPosition(_window));
        }
    }
}
