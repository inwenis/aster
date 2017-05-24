using System;
using System.Collections.Generic;
using System.Drawing;

namespace asterTake2
{
    internal class ShipsAndAsteroidsCreator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static ComplexShape CreateShip()
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
            var ship = new ComplexShape();
            ship.Shapes.Add(triangle);
            ship.Shapes.Add(square);
            ship.RotationCenter = new PointF(25, 25);
            return ship;
        }

        public static ComplexShape CreateAsteroid()
        {
            var asteroid = new ComplexShape
            {
                Position = new PointF(Random.Next(500), Random.Next(500))
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

        public static List<ComplexShape> CreateAsteroids()
        {
            var asteroids = new List<ComplexShape>();
            for (var i = 0; i < 10; i++)
            {
                var asteroid = CreateAsteroid();
                asteroids.Add(asteroid);
            }
            return asteroids;
        }
    }
}