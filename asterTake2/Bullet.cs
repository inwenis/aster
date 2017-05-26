using System.Drawing;

namespace asterTake2
{
    internal class Bullet
    {
        public PointF Position;
        private readonly double _angle;
        public bool Alive;

        public Bullet(PointF position, double angle)
        {
            Position = position;
            _angle = angle;
            Alive = true;
        }

        public void Move()
        {
            var movement = new PointF(0, -6).Rotate(_angle, new PointF(0, 0));
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
            graphics.FillRectangle(Brushes.White, Position.X, Position.Y, 2, 2);
        }

        public void MarkDead()
        {
            Alive = false;
        }
    }
}