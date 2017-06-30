using System.Drawing;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    internal class Asteroid : ComplexShape
    {
        public bool Alive;
        public int Generation;
        public Vector Velocity;

        public Asteroid()
        {
            Alive = true;
            Velocity = new Vector(0, -1);
        }

        public void MarkDead()
        {
            Alive = false;
        }

        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);
            graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
            graphics.DrawLine(Pens.BlanchedAlmond, Position, Position + new Vector(0, -60).Rotate(AngleRadians));
        }

        public static void Scale(Asteroid asteroid, float scale)
        {
            asteroid.Shapes = asteroid.Shapes.Select(s => s.Scale(scale)).ToList();
            asteroid.Radius = (int) (asteroid.Radius * scale);
        }
    }
}