using System;
using System.Collections.Generic;

namespace asterTake2
{
    internal class Collider
    {
        public Result FindAsteroidCollidingWithShipIfAny(List<Asteroid> asteroids, Ship ship, long currentMilisecond)
        {
            foreach (var asteroid in asteroids)
            {
                var distanceX = asteroid.Position.X - ship.Position.X;
                var distanceY = asteroid.Position.Y - ship.Position.Y;
                var dist = distanceX*distanceX + distanceY*distanceY;
                dist = (float) Math.Sqrt(dist);
                if (dist <= asteroid.Radius + ship.Radius)
                {
                    return new Result
                    {
                        Collision = true,
                        Asteroid = asteroid
                    };
                }
            }
            return new Result
            {
                Collision = false
            };
        }

        public static List<Asteroid> HandleAsteroidBulletCollisions(List<Asteroid> asteroids, List<Bullet> bullets)
        {
            var destroyedAsteroids = new List<Asteroid>();
            foreach (var asteroid in asteroids)
            {
                foreach (var bullet in bullets)
                {
                    var distanceX = asteroid.Position.X - bullet.Position.X;
                    var distanceY = asteroid.Position.Y - bullet.Position.Y;

                    var dist = distanceX*distanceX + distanceY*distanceY;
                    dist = (float) Math.Sqrt(dist);
                    if (dist <= asteroid.Radius + bullet.Radius)
                    {
                        asteroid.MarkDead();
                        destroyedAsteroids.Add(asteroid);
                        bullet.MarkDead();
                        break;
                    }
                }
            }
            return destroyedAsteroids;
        }
    }

    internal class Result
    {
        public bool Collision;
        public Asteroid Asteroid;
    }
}