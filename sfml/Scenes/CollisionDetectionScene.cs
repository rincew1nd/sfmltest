using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;
using SFMLTest.Entities;
using System.Text;

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
                Position = new Vector2f(550, 0),
                Scale = new Vector2f(0.7f, 0.7f),
                Color = Color.Red
            };

            _drawables = new List<Drawable>()
            {
                new Player(new Vector2f(100, 100), new Vector2f(100, 100)),
                new CircleShape(40) { Origin = new Vector2f(40,40), FillColor = Color.Cyan },
                new CircleShape(40) { Origin = new Vector2f(40,40), FillColor = Color.Cyan },
                new CircleShape(40) { Origin = new Vector2f(40,40), FillColor = Color.Cyan },
                new CircleShape(40) { Origin = new Vector2f(40,40), FillColor = Color.Cyan },
                debugtext,
                new CircleShape(10) { Origin = new Vector2f(10,10), FillColor = Color.Red }
            };
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var drawable in _drawables)
                target.Draw(drawable);
        }

        public void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
        }

        public void MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
        }

        public void Update()
        {
            var player = (Player)_drawables[0];
            player.Rotation = player.Rotation + 1 > 360 ? 0 : player.Rotation + 1;

            ((CircleShape)_drawables[6]).Position =
                new Vector2f(
                    Mouse.GetPosition(_window).X,
                    Mouse.GetPosition(_window).Y
                );
            player.Position =
                new Vector2f(
                    Mouse.GetPosition(_window).X,
                    Mouse.GetPosition(_window).Y
                );

            var text = (Text)_drawables[5];
            var textBuilder = new StringBuilder();
            textBuilder.Append("Position:\n");
            foreach (var point in player.GetGlobalPoints())
                textBuilder.Append($"X: {(int)point.X} Y: {(int)point.Y}\n");
            text.DisplayedString = textBuilder.ToString();

            ((CircleShape)_drawables[1]).Position = player.GetGlobalPoints()[0];
            ((CircleShape)_drawables[2]).Position = player.GetGlobalPoints()[1];
            ((CircleShape)_drawables[3]).Position = player.GetGlobalPoints()[2];
            ((CircleShape)_drawables[4]).Position = player.GetGlobalPoints()[3];
        }
    }
}
