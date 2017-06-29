using System;
using System.Drawing;
using System.Windows;

namespace asterTake2
{
    internal class Bullet
    {
        public Vector Position;
        public bool Alive;
        public int Radius;
        public readonly bool IsAutoAim;
        public readonly Asteroid Target;
        public double Angle;
        public Vector Speed;

        public Bullet(Vector position, double angle, int radius, bool isAutoAim = false, Asteroid target = null)
        {
            Position = position;
            Angle = angle;
            Radius = radius;
            IsAutoAim = isAutoAim;
            Target = target;
            Alive = true;
            Speed = new Vector(0, -6);
        }

        public void MarkDead()
        {
            Alive = false;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.White, Position.X, Position.Y, 1, 1);
            if (IsAutoAim)
            {
                graphics.DrawLine(Pens.Coral, Position, Target.Position);
                graphics.DrawEllipse(Pens.Coral, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            }
            else
            {
                graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            }
        }
    }
}