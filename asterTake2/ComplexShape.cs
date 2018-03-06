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
        //Ship derives from ComplexShape and does not use RotationSpeed at all
        public double RotationSpeed;
        public Vector Velocity;
        public int Radius;

        public virtual void Draw(Graphics graphics)
        {
            foreach (var shape in Shapes)
            {
                shape
                    .Rotate(AngleRadians, RotationCenter)
                    .Offset(Position)
                    .Draw(graphics, Pens.BlueViolet);
            }
        }
    }
}
