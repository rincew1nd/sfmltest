using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFMLTest.Calculations;
using SFMLTest.Entities;

namespace SFMLTest.Scenes
{
    class LineCollisionScene : IScene
    {
        private List<DragableLine> _lines;
        private Text _debugText;
        private RenderWindow _window;

        public LineCollisionScene(RenderWindow window)
        {
            Font font = new Font("resources/arcade.ttf");
            _debugText = new Text("Position: ", font)
            {
                Position = new Vector2f(440, 0),
                Scale = new Vector2f(0.7f, 0.7f),
                Color = Color.Red
            };

            _lines = new List<DragableLine>()
            {
                new DragableLine(new Vector2f(100, 100), new Vector2f(100, 200), Color.Blue),
                new DragableLine(new Vector2f(200, 100), new Vector2f(200, 200), Color.Red)
            };
            _window = window;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var line in _lines)
                target.Draw(line);
            target.Draw(_debugText);
        }

        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var mouse = Mouse.GetPosition(_window);
            if (e.Button == Mouse.Button.Left)
                foreach (var line in _lines)
                    line.CheckDrag(mouse);
        }

        public void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
                foreach (var line in _lines)
                    line.StopDrag();
        }

        public void Update()
        {
            var mouse = Mouse.GetPosition(_window);
            foreach (var line in _lines)
                line.Drag(mouse);

            _debugText.DisplayedString = Collision.CheckCollision(_lines[0], _lines[1]).ToString();
        }
    }
}
