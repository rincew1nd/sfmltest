using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;
using SFMLTest.Entities;
using SFMLTest.Calculations;

namespace SFMLTest.Scenes
{
    class CollisionDetectionScene : IScene
    {
        private RenderWindow _window;
        private List<Drawable> _drawables;

        public CollisionDetectionScene(RenderWindow window)
        {
            _window = window;

            Font font = new Font("resources/arcade.ttf");
            var debugtext = new Text("Position: ", font)
            {
                Position = new Vector2f(440, 0),
                Scale = new Vector2f(0.7f, 0.7f),
                Color = Color.Red
            };

            _drawables = new List<Drawable>()
            {
                debugtext,
                new Player(new Vector2f(100, 100), new Vector2f(100, 100)),
                new Player(new Vector2f(200, 200), new Vector2f(100, 100)),
                new DragableLine(new Vector2f(400, 400), new Vector2f(400, 500), Color.Green)
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var drawable in _drawables)
                target.Draw(drawable);
        }

        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            var player1 = (Player)_drawables[1];
            var line = (DragableLine)_drawables[3];
            var debugtext = (Text)_drawables[0];
            if (Collision.CheckCollision(player1, line))
            {
                player1.FillColor = Color.Blue;
                debugtext.DisplayedString += "\nCollision line: True";
            }
            else
            {
                player1.FillColor = Color.Red;
                debugtext.DisplayedString += "\nCollision line: False";
            }
        }

        public void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
        }

        public void Update()
        {
            var mousePos = Mouse.GetPosition(_window);
            var debugtext = (Text)_drawables[0];
            var player1 = (Player)_drawables[1];
            var player2 = (Player)_drawables[2];
            var line = (DragableLine)_drawables[3];

            player1.Position = new Vector2f(mousePos.X, mousePos.Y);
            player1.Rotation = player1.Rotation + 1 > 360 ? 0 : player1.Rotation + 1;
            player2.Rotation = player2.Rotation - 1 < 0 ? 360 : player2.Rotation - 1;

            if (Collision.CheckCollision(player1, player2))
            {
                player2.FillColor = Color.Blue;
                debugtext.DisplayedString = "Collision STA: True";
            }
            else
            {
                player2.FillColor = Color.Red;
                debugtext.DisplayedString = "Collision STA: False";
            }
        }
    }
}
