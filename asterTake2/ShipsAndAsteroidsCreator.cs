using System;
using System.Collections.Generic;
using System.Drawing;

namespace asterTake2
{
    internal class ShipsAndAsteroidsCreator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static Ship CreateShip()
        {
            var triangle = new Shape
            {
                Points = new List<PointF>
                {
                    new PointF(0, 0),
                    new PointF(25, -25),
                    new PointF(50, 0)
                }
            };
            var square = new Shape
            {
                Points = new List<PointF>
                {
                    new PointF(0, 0),
                    new PointF(50, 0),
                    new PointF(50, 50),
                    new PointF(0, 50)
                }
            };
            var ship = new Ship
            {
                RotationCenter = new PointF(25, 25),
                Angle = Math.PI * 3.0/4.0,
                Position = new PointF(500, 300)
            };
            ship.Shapes.Add(triangle);
            ship.Shapes.Add(square);
            return ship;
        }

        public static Asteroid CreateAsteroid()
        {
            var x = Random.Next(1000);
            if (x > 300)
            {
                x += 400;
            }
            var asteroid = new Asteroid()
            {
                Position = new PointF(x, Random.Next(600))
            };
            var asteroidShape = new Shape
            {
                Points = new List<PointF>
                {
                    new PointF(0, 10),
                    new PointF(10, 20),
                    new PointF(0, 30),
                    new PointF(10, 30),
                    new PointF(30, 40),
                    new PointF(40, 20),
                    new PointF(30, 10),
                    new PointF(20, 10),
                    new PointF(10, 0),
                    new PointF(10, 10),
                    new PointF(0, 10),
                }
            };
            asteroid.Shapes.Add(asteroidShape);
            asteroid.Angle = (Math.PI / 180) * Random.Next(360);
            asteroid.RotationCenter = new PointF(20, 20);
            return asteroid;
        }

        public static List<Asteroid> CreateAsteroids()
        {
            var asteroids = new List<Asteroid>();
            for (var i = 0; i < 50; i++)
            {
                var asteroid = CreateAsteroid();
                asteroids.Add(asteroid);
            }
            return asteroids;
        }

        public static Bullet CreateBullet(ComplexShape ship)
        {
            var position = ship.Position;
            var angle = ship.Angle;
            return new Bullet(position, angle);
        }
    }
}