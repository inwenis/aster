using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace asterTake2
{
    internal class Mover
    {
        private readonly int _mapWidth;
        private readonly int _mapHeight;

        public Mover(int mapWidth, int mapHeight)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }

        public void Move(Asteroid asteroid)
        {
            var velocity = new PointF(1, 0);
            var offset = velocity.Rotate(asteroid.Angle);
            var asteroidMaxX = _mapWidth + asteroid.Radius;
            var asteroidMinX = 0 - asteroid.Radius;
            var asteroidMaxY = _mapHeight + asteroid.Radius;
            var asteroidMinY = 0 - asteroid.Radius;
            if      (asteroid.Position.X > asteroidMaxX) { asteroid.Position.X = asteroidMinX; }
            else if (asteroid.Position.X < asteroidMinX) { asteroid.Position.X = asteroidMaxX; }
            if      (asteroid.Position.Y > asteroidMaxY) { asteroid.Position.Y = asteroidMinY; }
            else if (asteroid.Position.Y < asteroidMinY) { asteroid.Position.Y = asteroidMaxY; }
            asteroid.Position = asteroid.Position.Offset(offset);
        }

        public void Move(Ship ship)
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                var velocityAddition = ship.Acceleration.Rotate(ship.Angle);
                var newVelocity = ship.Velocity + velocityAddition;
                if (newVelocity.LengthSquared < ship.MaxVelocity.LengthSquared)
                {
                    ship.Velocity = newVelocity;
                }
            }
            else
            {
                ship.Velocity = ship.Velocity * 0.99;
            }
            if (Keyboard.IsKeyDown(Key.Right))
            {
                ship.Rotate(Math.PI / 90);
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                ship.Rotate(-Math.PI / 90);
            }

            ship.Position = ship.Position.Offset(ship.Velocity);
        }

        public void Move(Bullet bullet)
        {
            if (bullet.IsAutoAim && bullet.Target != null && bullet.Target.Alive)
            {
                AutoAimMove(bullet);
                AutoAimHandleBorders(bullet);
            }
            else
            {
                NormalMove(bullet);
                HandleBorders(bullet);
            }
        }

        private static void AutoAimMove(Bullet bullet)
        {
            var bulletVector = new Vector(bullet.Position.X, bullet.Position.Y);
            var targetAsteroidVector = new Vector(bullet.Target.Position.X, bullet.Target.Position.Y);
            var vectorFromBulletToAsteroid = targetAsteroidVector - bulletVector;
            vectorFromBulletToAsteroid.Normalize();
            vectorFromBulletToAsteroid /= 4;
            var velocity = bullet.Speed.Rotate(bullet.Angle);
            velocity = velocity + vectorFromBulletToAsteroid;
            velocity.Normalize();
            velocity *= 6;
            var angleBetweenDegrees = Vector.AngleBetween(new Vector(0, -1), velocity);
            var angleBetweenRadians = angleBetweenDegrees * Math.PI / 180;
            bullet.Angle = angleBetweenRadians;
            bullet.Position = bullet.Position.Offset(velocity);
        }

        private static void AutoAimHandleBorders(Bullet bullet)
        {
            if (bullet.Position.X > 1500)
            {
                bullet.Alive = false;
            }
            if (bullet.Position.Y > 1050)
            {
                bullet.Alive = false;
            }
            if (bullet.Position.X < -550)
            {
                bullet.Alive = false;
            }
            if (bullet.Position.Y < -550)
            {
                bullet.Alive = false;
            }
        }

        private static void NormalMove(Bullet bullet)
        {
            var movement = bullet.Speed.Rotate(bullet.Angle);
            bullet.Position = bullet.Position.Offset(movement);
        }

        private static void HandleBorders(Bullet bullet)
        {
            if (bullet.Position.X > 1050)
            {
                bullet.Alive = false;
            }
            if (bullet.Position.Y > 650)
            {
                bullet.Alive = false;
            }
            if (bullet.Position.X < -50)
            {
                bullet.Alive = false;
            }
            if (bullet.Position.Y < -50)
            {
                bullet.Alive = false;
            }
        }
    }
}