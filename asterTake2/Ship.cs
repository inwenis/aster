using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;

namespace asterTake2
{
    internal class Ship : ComplexShape
    {
        public int Lives;
        public bool IsAlive;
        public bool IsRespawning;
        public bool IsWaitingToBeRespawned;
        public bool IsVisible;
        public bool HasAutoAimBullets;
        public Vector Acceleration;
        public Vector MaxVelocity;
        protected long LastShoot;
        public long ShootingInterval;

        public Ship(Vector position)
        {
            var triangle = new Shape
            {
                Points = new[]
                {
                    new Vector(-25, -25),
                    new Vector(25, -25),
                    new Vector(0, -40)
                }
            };
            var square = new Shape
            {
                Points = new[]
                {
                    new Vector(-25, -25),
                    new Vector(25, -25),
                    new Vector(25, 25),
                    new Vector(-25, 25)
                }
            };
            Shapes.Add(triangle);
            Shapes.Add(square);
            RotationCenter = new Vector(0, 0);
            Position = position;
            AngleRadians = Math.PI * 3.0 / 4.0;
            ShootingInterval = 250;
            HasAutoAimBullets = false;
            MaxVelocity = new Vector(3, 0);
            Acceleration = new Vector(0, -0.1);
            IsVisible = true;
            IsWaitingToBeRespawned = false;
            IsRespawning = false;
            IsAlive = true;
            Radius = 33;
            Lives = 3;
        }

        public override void Draw(Graphics graphics)
        {
            if (!IsWaitingToBeRespawned && IsVisible)
            {
                if (Configuraiton.ShowDebugShapes)
                {
                    DrawDebug(graphics);
                }
                base.Draw(graphics);
            }
            else if (IsWaitingToBeRespawned)
            {
                graphics.DrawEllipse(Pens.Aqua, Position.X - Radius, Position.Y - Radius, 2 * Radius, 2 * Radius);
            }
        }

        private void DrawDebug(Graphics graphics)
        {
            graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, 2 * Radius, 2 * Radius);
        }

        public void HandleShooting(List<Bullet> bullets, long currentMilisecond, List<Asteroid> asteroids)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && CanShoot(currentMilisecond) && !HasAutoAimBullets)
            {
                var bullet = AsteroidAndBulletCreator.CreateBullet(this);
                bullets.Add(bullet);
                Shoot(currentMilisecond);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && CanShoot(currentMilisecond) && HasAutoAimBullets)
            {
                var bullet = AsteroidAndBulletCreator.CreateAutoAimBullet(this, asteroids);
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