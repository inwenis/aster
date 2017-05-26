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
                var movement = new PointF(0, -2).Rotate(ship.Angle, new PointF(0, 0));
                ship.Position = ship.Position.Offset(movement);
            }
            if (input.IsRightKeyPressed)
            {
                ship.Rotate(Math.PI / 90);
            }
            if (input.IsLeftKeyPressed)
            {
                ship.Rotate(-Math.PI / 90);
            }
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