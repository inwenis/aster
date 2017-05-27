﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace asterTake2
{
    internal class Collider
    {
        public void HandleShipAsteroidCollisions(List<Asteroid> asteroids, Ship ship, long currentMilisecond)
        {
            foreach (var asteroid in asteroids)
            {
                var distanceX = asteroid.Position.X - ship.Position.X;
                var distanceY = asteroid.Position.Y - ship.Position.Y;

                var dist = distanceX*distanceX + distanceY*distanceY;
                dist = (float) Math.Sqrt(dist);
                if (dist <= asteroid.Radius + ship.Radius)
                {
                    ship.Lives -= 1;
                    ship.IsRespawning = true;
                    ship.RespawnStartTime = currentMilisecond;
                    Console.WriteLine("you got hit! Lives left " + ship.Lives);
                    if (ship.Lives == -1)
                    {
                        Console.WriteLine("you dead!");
                        ship.IsAlive = false;
                    }
                }
            }
        }

        public static void HandleAsteroidBulletCollisions(List<Asteroid> asteroids, List<Bullet> bullets)
        {
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
                        bullet.MarkDead();
                        break;
                    }
                }
            }
            foreach (var destroyedAsteroids in asteroids.Where(a => !a.Alive && a.Generation != 0).ToList())
            {
                var newAsteroids = ShipsAndAsteroidsCreator.CreateAsteroids(destroyedAsteroids);
                asteroids.AddRange(newAsteroids);
            }
        }
    }
}