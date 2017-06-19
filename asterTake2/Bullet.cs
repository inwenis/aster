using System;
using System.Drawing;
using System.Windows;

namespace asterTake2
{
    internal class Bullet
    {
        public PointF Position;
        public bool Alive;
        public int Radius;
        private readonly bool _isAutoAim;
        private readonly Asteroid _target;
        private double _angle;
        private Vector _speed;
        private Vector _debugVector;

        public Bullet(PointF position, double angle, int radius, bool isAutoAim = false, Asteroid target = null)
        {
            Position = position;
            _angle = angle;
            Radius = radius;
            _isAutoAim = isAutoAim;
            _target = target;
            Alive = true;
            _speed = new Vector(0, -6);
        }

        public void Move()
        {
            if (_isAutoAim && _target != null)
            {
                AutoAimMove();
            }
            else
            {
                NormalMove();
            }

            HandleBorders();
        }

        private void AutoAimMove()
        {
            var bulletVector = new Vector(Position.X, Position.Y);
            var targetAsteroidVector = new Vector(_target.Position.X, _target.Position.Y);
            var vectorFromBulletToAsteroid = targetAsteroidVector - bulletVector;
            vectorFromBulletToAsteroid.Normalize();
            vectorFromBulletToAsteroid /= 2;
            var velocity = _speed.Rotate(_angle);
            velocity = velocity + vectorFromBulletToAsteroid;
            velocity.Normalize();
            velocity *= 6;
            var angleBetweenDegrees = Vector.AngleBetween(new Vector(0, -1), velocity);
            var angleBetweenRadians = angleBetweenDegrees * Math.PI / 180;
            _angle = angleBetweenRadians;
            _debugVector = velocity;
            Position = Position.Offset(velocity);
        }

        private void NormalMove()
        {
            var movement = _speed.Rotate(_angle);
            Position = Position.Offset(movement);
        }

        private void HandleBorders()
        {
            if (Position.X > 1050)
            {
                Alive = false;
            }
            if (Position.Y > 650)
            {
                Alive = false;
            }
            if (Position.X < -50)
            {
                Alive = false;
            }
            if (Position.Y < -50)
            {
                Alive = false;
            }
        }

        public void MarkDead()
        {
            Alive = false;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, Position.X, Position.Y, 1, 1);
            if (_isAutoAim)
            {
                graphics.DrawLine(Pens.Coral, Position, Position.Offset(_debugVector * 10));
                graphics.DrawEllipse(Pens.Coral, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            }
            else
            {
                graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            }
        }
    }
}