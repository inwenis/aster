using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace asterTake2
{
    internal class ShipMoverAndShooter
    {
        public static void Move(Ship ship)
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

        public static void HandleShooting(Ship ship, List<Bullet> bullets, long currentMilisecond, List<Asteroid> asteroids)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && ship.CanShoot(currentMilisecond) && ShipMoverAndShooter.AutoAimBullets == false)
            {
                var bullet = ShipsAndAsteroidsCreator.CreateBullet(ship);
                bullets.Add(bullet);
                ship.Shoot(currentMilisecond);
            }
            else if(Keyboard.IsKeyDown(Key.LeftCtrl) && ship.CanShoot(currentMilisecond) && ShipMoverAndShooter.AutoAimBullets == true)
            {
                var bullet = ShipsAndAsteroidsCreator.CreateAutoAimBullet(ship, asteroids);
                bullets.Add(bullet);
                ship.Shoot(currentMilisecond);
            }
        }

        public static bool AutoAimBullets { get; set; }
    }
}