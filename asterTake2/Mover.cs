using System;
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

        public void Move(Asteroid x)
        {
            x.Position += x.Velocity;
            x.AngleRadians += x.RotationSpeed;
            HandleBorders(x);
        }

        public void Move(Line x)
        {
            x.Position += x.Velocity;
            x.AngleRadians += x.RotationSpeed;
        }

        public void Move(Ship ship)
        {
            if (Keyboard.IsKeyDown(Key.Up))
            {
                var velocityAddition = ship.Acceleration.Rotate(ship.AngleRadians);
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
                ship.AngleRadians += Math.PI / 90;
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                ship.AngleRadians += -Math.PI / 90;
            }

            ship.Position = ship.Position + ship.Velocity;
            HandleBorders(ship);
        }

        private void HandleBorders(ComplexShape shape)
        {
            var maxX = _mapWidth + shape.Radius;
            var minX = 0 - shape.Radius;
            var maxY = _mapHeight + shape.Radius;
            var minY = 0 - shape.Radius;
            if (shape.Position.X > maxX)
            {
                shape.Position.X = minX;
            }
            else if (shape.Position.X < minX)
            {
                shape.Position.X = maxX;
            }
            if (shape.Position.Y > maxY)
            {
                shape.Position.Y = minY;
            }
            else if (shape.Position.Y < minY)
            {
                shape.Position.Y = maxY;
            }
        }

        public void Move(Bullet bullet)
        {
            if (bullet.IsAutoAim && bullet.Target != null && bullet.Target.Alive)
            {
                AutoAimMove(bullet);
                HandleBorders(bullet);
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
            var angleBetweenRadians = Helpers.AngleBetweenRadians(new Vector(0, -1), velocity);
            bullet.Angle = angleBetweenRadians;
            bullet.Position = bullet.Position + velocity;
        }

        private void HandleBorders(Bullet bullet)
        {
            if (bullet.Position.X > _mapWidth + 50)  { bullet.Alive = false; }
            if (bullet.Position.Y > _mapHeight + 50) { bullet.Alive = false; }
            if (bullet.Position.X < 0 - 50)          { bullet.Alive = false; }
            if (bullet.Position.Y < 0 - 50)          { bullet.Alive = false; }
        }

        private static void NormalMove(Bullet bullet)
        {
            var movement = bullet.Speed.Rotate(bullet.Angle);
            bullet.Position = bullet.Position + movement;
        }
    }
}