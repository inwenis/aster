using System.Drawing;

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

        public void Move()
        {
            var movementA = new PointF(0, -1).Rotate(Angle, new PointF(0, 0));
            if (Position.X > 1050)
            {
                Position.X = -50;
            }
            if (Position.Y > 650)
            {
                Position.Y = -50;
            }
            if (Position.X < -50)
            {
                Position.X = 1050;
            }
            if (Position.Y < -50)
            {
                Position.Y = 650;
            }
            Position = Position.Offset(movementA);
        }
    }
}