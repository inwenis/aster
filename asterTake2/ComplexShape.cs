using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;

namespace asterTake2
{
    public class ComplexShape
    {
        public List<Shape> Shapes = new List<Shape>();
        public PointF Position = new PointF(0, 0);
        public PointF RotationCenter = new PointF(0, 0);
        public double Angle;
        public int Radius;
        private static readonly Random Random = new Random();

        public ComplexShape()
        {
        }

        public ComplexShape(PointF a, PointF b)
        {
            Shapes.Add(new Shape(a,b));
        }

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

        public List<ComplexShape> GetLinesOfShape()
        {
            var lines = new List<ComplexShape>();
            foreach (var shape in Shapes)
            {
                var points = shape.Points;
                for (var i = 0; i < points.Length; i++)
                {
                    PointF a;
                    PointF b;
                    if (i == points.Length - 1)
                    {
                        a = points.Last();
                        b = points.First();
                    }
                    else
                    {
                        a = points[i];
                        b = points[i + 1];
                    }
                    a = a.Rotate(Angle, RotationCenter);
                    b = b.Rotate(Angle, RotationCenter);
                    var half = (b.ToVector() + a.ToVector())/2;
                    var center = ToPoint(half);
                    var line = new ComplexShape(a, b)
                    {
                        Position = Position,
                        RotationCenter = center,
                        VelocitySpecial = new Vector(Random.Next(10) - 5, Random.Next(10) - 5),
                        RotationSpecialRadians = (Random.NextDouble() - 0.5) * Math.PI/12
                    };
                    lines.Add(line);
                }
            }
            return lines;
        }

        public double RotationSpecialRadians { get; set; }
        public Vector VelocitySpecial { get; set; }

        private static PointF ToPoint(Vector half)
        {
            return new PointF((float) half.X, (float) half.Y);
        }
    }
}
