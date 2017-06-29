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
        public int Radius;

        public void Rotate(double angle)
        {
            Angle += angle;
        }

        public void OffsetBy(PointF offset)
        {
            Position = Position.Offset(offset);
        }

        public virtual void Draw(Graphics graphics)
        {
            foreach (var shape in Shapes)
            {
                shape
                    .Rotate(Angle, RotationCenter)
                    .Offset(Position)
                    .Draw(graphics);
            }
        }
    }
}
