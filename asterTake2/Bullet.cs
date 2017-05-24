using System.Drawing;

namespace asterTake2
{
    internal class Bullet
    {
        private PointF _position;
        private readonly double _angle;

        public Bullet(PointF position, double angle)
        {
            _position = position;
            _angle = angle;
        }

        public void Move()
        {
            var movement = new PointF(0, -1).Rotate(_angle, new PointF(0, 0));
            _position = _position.Offset(movement);
        }

        public void Draw(Graphics graphics)
        {
            var drawFont = new Font("Arial", 16);
            var drawBrush = new SolidBrush(Color.Aquamarine);
            graphics.DrawString("o", drawFont, drawBrush, _position);
        }
    }
}