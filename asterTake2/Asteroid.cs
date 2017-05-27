using System.Drawing;
using System.Linq;
using System.Windows.Media;

namespace asterTake2
{
    internal class Asteroid : ComplexShape
    {
        public bool Alive;
        public int Radius;

        public Asteroid() : base()
        {
            Alive = true;
        }

        public int Generation;

        public void MarkDead()
        {
            Alive = false;
        }

        public void Draw(Graphics graphics)
        {
            foreach (var shape in Shapes)
            {
                shape
                    .Offset(Position)
                    .Draw(graphics);
            }
            graphics.DrawEllipse(Pens.Red, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);
        }

        public static void Scale(Asteroid asteroid, float scale)
        {
            asteroid.Shapes = asteroid.Shapes.Select(s => s.Scale(scale)).ToList();
            asteroid.Radius = (int) (asteroid.Radius * scale);
        }
    }
}