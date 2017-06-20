using System.Collections.Generic;
using System.Drawing;

namespace asterTake2
{
    public class ComplexShape
    {
        public List<Shape> Shapes = new List<Shape>();
        public PointF Position = new PointF(0, 0);
        public PointF RotationCenter = new PointF(0, 0);
        public double Angle;
        private long _lastShoot;
        private readonly long _shootingInterval = 250;

        public void Rotate(double angle)
        {
            Angle += angle;
        }

        public void OffsetBy(PointF offset)
        {
            Position.X += offset.X;
            Position.Y += offset.Y;
        }

        public bool CanShoot(long elapsedMilliseconds)
        {
            return elapsedMilliseconds - _lastShoot >= _shootingInterval;
        }

        public void Shoot(long elapsedMilliseconds)
        {
            _lastShoot = elapsedMilliseconds;
        }
    }
}
