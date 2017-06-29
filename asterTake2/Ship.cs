using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace asterTake2
{
    internal class Ship : ComplexShape
    {
        public int Lives;
        public bool IsAlive = true;
        public bool IsRespawning = false;
        public bool IsWaitingToBeRespawned = false;
        public bool IsVisible = true;
        public bool HasAutoAimBullets;
        public int Radius;
        public Vector Acceleration = new Vector(0, -0.1);
        public Vector Velocity;
        public Vector MaxVelocity = new Vector(3, 0);
        protected long LastShoot;
        protected readonly long ShootingInterval = 250;

        public override void Draw(Graphics graphics)
        {
            float radius = 33;
            if (!IsWaitingToBeRespawned && IsVisible)
            {
                graphics.DrawEllipse(Pens.Red, Position.X - radius, Position.Y - radius, 2 * radius, 2 * radius);
                base.Draw(graphics);
            }
            else if (IsWaitingToBeRespawned)
            {
                graphics.DrawEllipse(Pens.Aqua, Position.X - radius, Position.Y - radius, 2 * radius, 2 * radius);
            }
        }

        public void HandleShooting(List<Bullet> bullets, long currentMilisecond, List<Asteroid> asteroids)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && CanShoot(currentMilisecond) && !HasAutoAimBullets)
            {
                var bullet = ShipsAndAsteroidsCreator.CreateBullet(this);
                bullets.Add(bullet);
                Shoot(currentMilisecond);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && CanShoot(currentMilisecond) && HasAutoAimBullets)
            {
                var bullet = ShipsAndAsteroidsCreator.CreateAutoAimBullet(this, asteroids);
                bullets.Add(bullet);
                Shoot(currentMilisecond);
            }
        }

        public bool CanShoot(long elapsedMilliseconds)
        {
            return elapsedMilliseconds - LastShoot >= ShootingInterval;
        }

        public void Shoot(long elapsedMilliseconds)
        {
            LastShoot = elapsedMilliseconds;
        }
    }
}