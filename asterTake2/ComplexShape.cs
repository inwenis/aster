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
        public Vector Position = new Vector(0, 0);
        public Vector RotationCenter = new Vector(0, 0);
        public double Angle;
        public int Radius;
        private static readonly Random Random = new Random();

        public ComplexShape()
        {
        }

        public ComplexShape(Vector a, Vector b)
        {
            Shapes.Add(new Shape(a,b));
        }

        public void Rotate(double angle)
        {
            Angle += angle;
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
                    Vector a;
                    Vector b;
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
                    var center = (a + b)/2;
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
    }
}
