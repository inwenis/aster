using System;
using System.Collections.Generic;
using System.Drawing;

namespace asterTake2
{
    internal class ShipsAndAsteroidsCreator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        private static readonly int AsteroidGeneration2Radius = 30;
        private static readonly Shape AsteroidGeneration2Shape = new Shape
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

            asteroid.Shapes.Add(AsteroidGeneration2Shape);
            asteroid.Angle = (Math.PI / 180) * Random.Next(360);
            asteroid.RotationCenter = new PointF(0, 0);
            asteroid.Generation = 2;
            asteroid.Radius = AsteroidGeneration2Radius;
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

        public static Asteroid[] CreateSmallerAsteroids(Asteroid destroyedAsteroid)
        {
            var asteroid1 = CreateSmallerAsteroid(destroyedAsteroid, Math.PI/6);
            var asteroid2 = CreateSmallerAsteroid(destroyedAsteroid, -Math.PI/6);
            return new []{ asteroid1, asteroid2 };
        }

        private static Asteroid CreateSmallerAsteroid(Asteroid destroyedAsteroid, double angleChange)
        {
            var asteroid = new Asteroid
            {
                Position = destroyedAsteroid.Position,
                Angle = destroyedAsteroid.Angle + angleChange,
                Generation = destroyedAsteroid.Generation - 1,
                Shapes = new List<Shape> {AsteroidGeneration2Shape},
                Radius = AsteroidGeneration2Radius
            };
            if (asteroid.Generation == 1)
            {
                Asteroid.Scale(asteroid, (float) 0.7);
            }
            if (asteroid.Generation == 0)
            {
                Asteroid.Scale(asteroid, (float) 0.35);
            }
            return asteroid;
        }
    }
}