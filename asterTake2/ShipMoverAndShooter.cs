using System;
using System.Collections.Generic;
using System.Drawing;

namespace asterTake2
{
    internal class ShipMoverAndShooter
    {
        public static void Move(Ship ship, UserInput input)
        {
            if (input.IsUpKeyPressed)
            {
                var accelerationInThisFrame = new PointF(0, (float) -0.1).Rotate(ship.Angle);
                var newVelocity = new PointF(ship.Velocity.X + accelerationInThisFrame.X, ship.Velocity.Y + accelerationInThisFrame.Y);
                var c2 = newVelocity.X * newVelocity.X + newVelocity.Y * newVelocity.Y;
                var newValocityLength = Math.Sqrt(c2);
                if (newValocityLength < 3)
                {
                    ship.Velocity = newVelocity;
                }
            }
            else
            {
                ship.Velocity = new PointF((float)(ship.Velocity.X * 0.99), (float) (ship.Velocity.Y * 0.99));
            }
            if (input.IsRightKeyPressed)
            {
                ship.Rotate(Math.PI / 90);
            }
            if (input.IsLeftKeyPressed)
            {
                ship.Rotate(-Math.PI / 90);
            }

            ship.Position = ship.Position.Offset(ship.Velocity);
        }

        public static void HandleShooting(Ship ship, UserInput input, List<Bullet> bullets, long currentMilisecond)
        {
            if (input.IsShooting && ship.CanShoot(currentMilisecond))
            {
                var bullet = ShipsAndAsteroidsCreator.CreateBullet(ship);
                bullets.Add(bullet);
            }
        }
    }
}