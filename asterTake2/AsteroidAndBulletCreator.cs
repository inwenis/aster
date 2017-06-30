using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    internal class AsteroidAndBulletCreator
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);
        private static readonly int AsteroidGeneration2Radius = 30;
        private static readonly Shape AsteroidGeneration2Shape = new Shape
        {
            Points = new[]
                {
                    new Vector(0, -10),
                    new Vector(10, -20),
                    new Vector(20, -10),
                    new Vector(15, 0),
                    new Vector(25, 5),
                    new Vector(12, 20),
                    new Vector(15, 25),
                    new Vector(-10, 20),
                    new Vector(-15, 25),
                    new Vector(-20, 10),
                    new Vector(-30, 0),
                    new Vector(-15, -25)
                }
        };

        public static Asteroid CreateAsteroid()
        {
            var x = Random.Next(1000);
            if (x > 300)
            {
                x += 400;
            }
            var asteroid = new Asteroid()
            {
                Position = new Vector(x, Random.Next(600))
            };

            asteroid.Shapes.Add(AsteroidGeneration2Shape);
            asteroid.AngleRadians = (Math.PI / 180) * Random.Next(360);
            asteroid.RotationCenter = new Vector(0, 0);
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
            var angle = ship.AngleRadians;
            return new Bullet(position, angle, 10);
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
                AngleRadians = destroyedAsteroid.AngleRadians + angleChange,
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

        public static Bullet CreateAutoAimBullet(Ship ship, List<Asteroid> asteroids)
        {
            var angles = asteroids.Select(asteroid =>
            {
                var a = asteroid.Position;
                var b = new Vector(0, -6).Rotate(ship.AngleRadians);
                var vectorFromBulletToAsteroid = a - ship.Position;
                var angleBetween = Helpers.AngleBetweenRadians(b, vectorFromBulletToAsteroid);
                var abs = Math.Abs(angleBetween);
                return abs;
            });
            var position = ship.Position;
            var angle = ship.AngleRadians;

            var asteroidBulletVector = asteroids.Zip(angles, (aster, bulletVelocityAster) => new {aster, bulletVelocityAster });
            var asteroidsInRange = asteroidBulletVector.Where(x => x.bulletVelocityAster < Math.PI/12).ToArray();
            if (asteroidsInRange.Any())
            {
                var targetAster = asteroidsInRange.OrderBy(x => x.bulletVelocityAster).First().aster;
                
                return new Bullet(position, angle, 10, true, targetAster);
            }
            else
            {
                return new Bullet(position, angle, 10);
            }
        }
    }
}