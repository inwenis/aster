using System;
using System.Drawing;
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
    }
}