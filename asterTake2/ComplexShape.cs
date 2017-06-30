using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace asterTake2
{
    public class ComplexShape
    {
        public List<Shape> Shapes = new List<Shape>();
        public Vector Position = new Vector(0, 0);
        public Vector RotationCenter = new Vector(0, 0);
        public double AngleRadians;
        public int Radius;

        public void Rotate(double angle)
        {
            AngleRadians += angle;
        }

        public void OffsetBy(Vector offset)
        {
            Position = Position + offset;
        }

        public virtual void Draw(Graphics graphics)
        {
            foreach (var shape in Shapes)
            {
                shape
                    .Rotate(AngleRadians, RotationCenter)
                    .Offset(Position)
                    .Draw(graphics);
            }
        }
    }
}
