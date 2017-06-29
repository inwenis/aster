using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    public class Shape
    {
        public Vector[] Points;

        public Shape()
        {
        }

        public Shape(Vector a, Vector b)
        {
            Points = new[] {a, b};
        }

        public Shape Rotate(double angle, Vector rotationCenter)
        {
            var rotatedPoints = new List<Vector>();
            foreach (var point in Points)
            {
                rotatedPoints.Add(point.Rotate(angle, rotationCenter));
            }
            return new Shape()
            {
                Points = rotatedPoints.ToArray()
            };
        }

        public Shape Offset(Vector position)
        {
            var offsetedPoints = Points.Select(point => point + position);
            return new Shape
            {
                Points = offsetedPoints.ToArray()
            };
        }

        public Shape Scale(float scale)
        {
            var scaledPoints = Points.Select(point => point * scale);
            return new Shape
            {
                Points = scaledPoints.ToArray()
            };
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(Pens.BlueViolet, Points);
        }
    }
}