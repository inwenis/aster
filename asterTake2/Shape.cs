using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace asterTake2
{
    public class Shape
    {
        public PointF[] Points;

        public Shape Rotate(double angle, PointF rotationCenter)
        {
            var rotatedPoints = new List<PointF>();
            foreach (var point in Points)
            {
                rotatedPoints.Add(point.Rotate(angle, rotationCenter));
            }
            return new Shape()
            {
                Points = rotatedPoints.ToArray()
            };
        }

        public Shape Offset(PointF position)
        {
            var offsetedPoints = Points.Select(point => point.Offset(position));
            return new Shape
            {
                Points = offsetedPoints.ToArray()
            };
        }

        public Shape Scale(float scale)
        {
            var scaledPoints = Points.Select(point => new PointF(point.X * scale, point.Y * scale));
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