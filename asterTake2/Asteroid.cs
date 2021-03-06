﻿using System.Drawing;
using System.Linq;

namespace asterTake2
{
    public class Asteroid : ComplexShape
    {
        public bool Alive;
        public int Generation;

        public Asteroid()
        {
            Alive = true;
        }

        public void MarkDead()
        {
            Alive = false;
        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            graphics.DrawLine(Pens.BlanchedAlmond, Position, Position + Velocity * 60);
        }

        public static void Scale(Asteroid asteroid, float scale)
        {
            asteroid.Shapes = asteroid.Shapes.Select(s => s.Scale(scale)).ToList();
            asteroid.Radius = (int) (asteroid.Radius * scale);
        }
    }
}