using System;
using System.Collections.Generic;

namespace asterTake2
{
    internal class ShipMoverAndShooter
    {
        public static void Move(Ship ship, UserInput input)
        {
            if (input.IsUpKeyPressed)
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