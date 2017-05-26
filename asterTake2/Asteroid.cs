using System.Drawing;

namespace asterTake2
{
    internal class Asteroid : ComplexShape
    {
        public bool Alive;

        public Asteroid() : base()
        {
            Alive = true;
        }

        public void MarkDead()
        {
            Alive = false;
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