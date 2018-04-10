using SFML.Graphics;
using SFML.Window;
using SFMLTest.Entities;
using System;
using System.Collections.Generic;

namespace SFMLTest.Scenes
{
    class TestScene : IScene
    {
        private static Dictionary<string, Drawable> _drawables;
        private static Dictionary<string, Line> _lines;
        private static List<CircleShape> _points;

        public TestScene()
        {
            _drawables = new Dictionary<string, Drawable>()
            {
                { "testSquare" , new RectangleShape()
                    {
                        Size = new Vector2f(100, 100),
                        Position = new Vector2f(100, 100),
                        FillColor = Color.Red,
                        Origin = new Vector2f(50, 50)
                    }
                }
            };
            _lines = new Dictionary<string, Line>()
            {
                { "testLine", new Line
                    (
                        new Vector2f(400, 300),
                        new Vector2f(100, 0),
                        Color.Red
                    )
                }
            };
            for (var i = 0; i < 7; i++)
            {
                _lines.Add(i.ToString(), new Line
                    (
                        new Vector2f(0, 130 + i * 100),
                        new Vector2f(800, 0),
                        Color.Red
                    )
                );
            }
            _points = new List<CircleShape>();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var drawable in _drawables)
                target.Draw(drawable.Value);
            foreach (var line in _lines)
                target.Draw(line.Value.Vertex, PrimitiveType.Lines);
            foreach (var drawable in _points)
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
            var testSquare = (RectangleShape)_drawables["testSquare"];
            var originalRotation = testSquare.Rotation <= 0 ? 360 : testSquare.Rotation - 1;

            testSquare.Rotation = originalRotation;
            _lines["testLine"].Rotate(originalRotation + 1);

            _points.Add(new CircleShape()
            {
                FillColor = Color.Red,
                Position = _lines["testLine"].Vertex[1].Position,
                Radius = 2
            });

            _points.Add(new CircleShape()
            {
                FillColor = Color.Black,
                Position = new Vector2f(800, 530 + (float)Math.Sin(Math.PI * originalRotation / 180) * 50),
                Radius = 2
            });

            _points.Add(new CircleShape()
            {
                FillColor = Color.Black,
                Position = new Vector2f(800, 430 + (float)Math.Cos(Math.PI * originalRotation / 180) * 50),
                Radius = 2
            });

            _points.Add(new CircleShape()
            {
                FillColor = Color.Black,
                Position = new Vector2f(800, 330 + (float)Math.Tan(Math.PI * originalRotation / 180) * 50),
                Radius = 2
            });

            _points.Add(new CircleShape()
            {
                FillColor = Color.Black,
                Position = new Vector2f(800, 230 + (float)Math.Cosh(Math.PI * originalRotation / 180) * 50),
                Radius = 2
            });

            for (int i = _points.Count - 1; i > -1; i--)
            {
                _points[i].Position = new Vector2f(_points[i].Position.X - 0.5f, _points[i].Position.Y);
                if (_points[i].Position.X < 0)
                    _points.RemoveAt(i);
            }
        }
    }
}
