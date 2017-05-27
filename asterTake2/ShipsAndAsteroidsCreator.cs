using System;
using System.Collections.Generic;
using System.Drawing;

namespace asterTake2
{
    internal class ShipsAndAsteroidsCreator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        private static readonly Shape AsteroidShapeGeneration2 = new Shape
        {
            Points = new[]
                {
                    new PointF(0, -10),
                    new PointF(10, -20),
                    new PointF(20, -10),
                    new PointF(15, 0),
                    new PointF(25, 5),
                    new PointF(12, 20),
                    new PointF(15, 25),
                    new PointF(-10, 20),
                    new PointF(-15, 25),
                    new PointF(-20, 10),
                    new PointF(-30, 0),
                    new PointF(-15, -25)
                }
        };

        public static Ship CreateShip()
        {
            var triangle = new Shape
            {
                Points = new PointF[]
                {
                    new PointF(-25, -25),
                    new PointF(25, -25),
                    new PointF(0, -40)
                }
            };
            var square = new Shape
            {
                Points = new PointF[]
                {
                    new PointF(-25, -25),
                    new PointF(25, -25),
                    new PointF(25, 25),
                    new PointF(-25, 25)
                }
            };
            var ship = new Ship
            {
                RotationCenter = new PointF(0, 0),
                Angle = Math.PI * 3.0/4.0,
                Position = new PointF(500, 300)
            };
            ship.Shapes.Add(triangle);
            ship.Shapes.Add(square);
            ship.Radius = 33;
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

            asteroid.Shapes.Add(AsteroidShapeGeneration2);
            asteroid.Angle = (Math.PI / 180) * Random.Next(360);
            asteroid.RotationCenter = new PointF(0, 0);
            asteroid.Generation = 2;
            asteroid.Radius = 30;
            return asteroid;
        }

        public static List<Asteroid> CreateAsteroids(int count = 50)
        {
            var asteroids = new List<Asteroid>();
            for (var i = 0; i < count; i++)
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
            return new Bullet(position, angle)
            {
                Radius = 10
            };
        }

        public static Asteroid[] CreateAsteroids(Asteroid destroyedAsteroid)
        {
            var asteroid1 = CreateAsteroid();
            asteroid1.Position = destroyedAsteroid.Position;
            asteroid1.Angle = destroyedAsteroid.Angle + Math.PI/2;
            asteroid1.Generation = destroyedAsteroid.Generation - 1;
            if (asteroid1.Generation == 1) { Asteroid.Scale(asteroid1, (float) 0.7); }
            if (asteroid1.Generation == 0) { Asteroid.Scale(asteroid1, (float) 0.35); }
            var asteroid2 = CreateAsteroid();
            asteroid2.Position = destroyedAsteroid.Position;
            asteroid2.Angle = destroyedAsteroid.Angle - Math.PI/2;
            asteroid2.Generation = destroyedAsteroid.Generation - 1;
            if (asteroid2.Generation == 1) { Asteroid.Scale(asteroid2, (float)0.7); }
            if (asteroid2.Generation == 0) { Asteroid.Scale(asteroid2, (float)0.35); }
            return new []{ asteroid1, asteroid2 };
        }
    }
}