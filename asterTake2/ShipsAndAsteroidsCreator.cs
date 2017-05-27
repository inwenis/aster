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
//            var asteroidShape = new Shape
//            {
//                Points = new List<PointF>
//                {
//                    new PointF(0, 10),
//                    new PointF(10, 20),
//                    new PointF(0, 30),
//                    new PointF(10, 30),
//                    new PointF(30, 40),
//                    new PointF(40, 20),
//                    new PointF(30, 10),
//                    new PointF(20, 10),
//                    new PointF(10, 0),
//                    new PointF(10, 10),
//                    new PointF(0, 10),
//                }
//            };

            var asteroidShape = new Shape
            {
                Points = new PointF[]
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
            asteroid.Shapes.Add(asteroidShape);
            asteroid.Angle = (Math.PI / 180) * Random.Next(360);
            asteroid.RotationCenter = new PointF(0, 0);
            asteroid.Generation = 1;
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

        public static List<Asteroid> CreateSmallerAsteroids(Asteroid destroyedAsteroid)
        {
            var asteroidShape = new Shape
            {
                Points = new PointF[]
                {
                    new PointF(0, 5),
                    new PointF(5, 10),
                    new PointF(0, 15),
                    new PointF(5, 15),
                    new PointF(15, 20),
                    new PointF(20, 10),
                    new PointF(15, 5),
                    new PointF(10, 5),
                    new PointF(5, 0),
                    new PointF(5, 5),
                    new PointF(0, 5),
                }
            };

            var asteroid1 = new Asteroid()
            {
                Position = destroyedAsteroid.Position
            };
            var asteroid2 = new Asteroid()
            {
                Position = destroyedAsteroid.Position
            };
            asteroid1.Shapes.Add(asteroidShape);
            asteroid1.Angle = destroyedAsteroid.Angle + Math.PI / 2;
            asteroid1.RotationCenter = new PointF(20, 20);
            asteroid1.Generation = 0;
            asteroid2.Shapes.Add(asteroidShape);
            asteroid2.Angle = destroyedAsteroid.Angle - Math.PI / 2;
            asteroid2.RotationCenter = new PointF(20, 20);
            asteroid1.Generation = 0;

            return new List<Asteroid>{ asteroid1, asteroid2 };
        }
    }
}