﻿using System.Drawing;

namespace asterTake2
{
    internal class Bullet
    {
        public PointF Position;
        public bool Alive;
        public int Radius;
        private readonly double _angle;

        public Bullet(PointF position, double angle, int radius)
        {
            Position = position;
            _angle = angle;
            Radius = radius;
            Alive = true;
        }

        public void Move()
        {
            var movement = new PointF(0, -6).Rotate(_angle);
            Position = Position.Offset(movement);
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

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, Position.X, Position.Y, 1, 1);
            graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }

        public void MarkDead()
        {
            Alive = false;
        }
    }
}