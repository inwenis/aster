using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace asterTake2
{
    public class Shape
    {
        public List<PointF> Points = new List<PointF>();

        public Shape Rotate(double angle, PointF rotationCenter)
        {
            var rotatedPoints = new List<PointF>();
            foreach (var point in Points)
            {
                rotatedPoints.Add(point.Rotate(angle, rotationCenter));
            }
            return new Shape()
            {
                Points = rotatedPoints
            };
        }

        public Shape Offset(PointF position)
        {
            var offsetedPoints = Points.Select(point => point.Offset(position)).ToList();
            return new Shape()
            {
                Points = offsetedPoints
            };
        }

        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = i + 1; j < Points.Count; j++)
                {
                    graphics.DrawLine(Pens.BlueViolet, Points[i], Points[j]);
                }
            }

        }
    }
}