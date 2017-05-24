using System.Drawing;

namespace asterTake2
{
    internal class Bullet
    {
        public PointF Position;
        private readonly double _angle;

        public Bullet(PointF position, double angle)
        {
            Position = position;
            _angle = angle;
        }

        public void Move()
        {
            var movement = new PointF(0, -4).Rotate(_angle, new PointF(0, 0));
            Position = Position.Offset(movement);
        }

        public void Draw(Graphics graphics)
        {
            var drawFont = new Font("Arial", 16);
            var drawBrush = new SolidBrush(Color.Aquamarine);
            graphics.DrawString("o", drawFont, drawBrush, Position);
        }
    }
}